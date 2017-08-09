using System;
using System.Threading.Tasks;
using Optional;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class SiteWatchRepository : ISiteWatchRepository
    {
        private readonly DataProvider _provider;

        public SiteWatchRepository(DataProvider provider)
        {
            _provider = provider;
        }
        public IObservable<SiteWatch> ObserveAll()
        {
            return _provider.Get();
        }

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