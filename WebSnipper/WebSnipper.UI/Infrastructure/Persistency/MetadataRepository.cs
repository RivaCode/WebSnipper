using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class MetadataRepository : IMetadataRepository
    {
        private readonly IDataStore _store;

        public MetadataRepository(IDataStore store) => _store = store;

        public IObservable<MetadataAggregtor> ObserveAll()
            => _store
                .GetRefershRate()
                .Select(rr => new MetadataAggregtor(rr));

        public Task AddAsync(MetadataAggregtor entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(MetadataAggregtor entity)
        {
            throw new NotImplementedException();
        }
    }
}