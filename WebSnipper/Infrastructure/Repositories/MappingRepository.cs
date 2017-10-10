using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Business.Interfaces;
using Domain.Util;
using Infrastructure.Core;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Repositories
{
    internal abstract class MappingRepository<TModel, TPersistence> : IRepository<TModel>
        where TPersistence : PersistencyObject
    {
        private readonly IJsonStore _store;
        private readonly IModelConverter<TModel, TPersistence> _converter;

        private readonly Dictionary<TModel, TPersistence> _materializedObject;

        protected abstract StoreKey MappingKey { get; }

        protected MappingRepository(
            IJsonStore store,
            IModelConverter<TModel, TPersistence> converter)
        {
            _store = store;
            _converter = converter;

            _materializedObject = new Dictionary<TModel, TPersistence>();
        }

        #region IRepository<TModel> members

        public async Task<IEnumerable<TModel>> GetAllAsync()
            => await _store.ObserveSlicedChangesUsing(MappingKey)
                .SelectMany(rootStream => rootStream.Values<TPersistence>())
                .Aggregate(
                    new List<TModel>(),
                    (allModels, persisted)
                        => persisted.AsModel(_converter)
                            .Tee(model => _materializedObject[model] = persisted)
                            .Map(model => allModels.Tee(self => self.Add(model))));

        public async Task AddAsync(TModel entity)
        {
            if (_materializedObject.ContainsKey(entity))
            {
                throw new ArgumentException();
            }
            var persisted = entity.AsPersistence(_converter);
            persisted.Id = Guid.NewGuid().ToString();

            await _store.SaveAsync(
                MappingKey,
                mappingRoot => AddOverride(mappingRoot, persisted));

            _materializedObject[entity] = persisted;
        }

        public async Task UpdateAsync(TModel entity)
        {
            if (!_materializedObject.ContainsKey(entity))
            {
                throw new ArgumentException();
            }

            var persisted = _materializedObject[entity];
            _converter.SyncChanges(entity, persisted);

            await _store.SaveAsync(
                MappingKey,
                mappingRoot => UpdateOverride(mappingRoot, persisted));
        }

        public async Task RemoveAsync(TModel entity)
        {
            if (!_materializedObject.ContainsKey(entity))
            {
                throw new ArgumentException();
            }
            var persisted = _materializedObject[entity];
            await _store.SaveAsync(
                MappingKey,
                mappingRoot => RemoveOverride(mappingRoot, persisted));

            _materializedObject.Remove(entity);
        } 

        #endregion

        protected abstract void AddOverride(JToken head, TPersistence newNode);
        protected abstract void RemoveOverride(JToken head, TPersistence oldNode);
        protected abstract void UpdateOverride(JToken head, TPersistence node);
    }
}