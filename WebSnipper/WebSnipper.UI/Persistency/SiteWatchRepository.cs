using System;
using System.Threading.Tasks;
using Optional;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class SiteWatchRepository : ISiteWatchRepository
    {
        private readonly IDataStore _store;

        public SiteWatchRepository(IDataStore store)
        {
            _store = store;
        }
        public IObservable<SiteWatch> ObserveAll() => _store.GetAll();

        public Option<SiteWatch> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Option<SiteWatch>> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(SiteWatch entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(SiteWatch entity)
        {
            throw new NotImplementedException();
        }
    }
}