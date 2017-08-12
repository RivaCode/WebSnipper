using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IDataStore _store;
        private readonly Subject<Site> _addSubject = new Subject<Site>();

        public SiteRepository(IDataStore store) => _store = store;

        public IObservable<Site> SiteAdded => _addSubject.AsObservable();
        public IObservable<Site> ObserveAll() => _store.GetAll();

        public async Task AddAsync(Site entity)
        {
            await _store.SaveAsync(entity);
            _addSubject.OnNext(entity);
        }

        public Task RemoveAsync(Site entity)
        {
            throw new NotImplementedException();
        }
    }
}