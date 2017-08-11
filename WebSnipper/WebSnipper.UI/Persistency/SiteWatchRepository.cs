using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public class SiteWatchRepository : ISiteWatchRepository
    {
        private readonly IDataStore _store;
        private readonly Subject<SiteWatch> _addSubject = new Subject<SiteWatch>();

        public SiteWatchRepository(IDataStore store) => _store = store;

        public IObservable<SiteWatch> SiteWatchAdd => _addSubject.AsObservable();
        public IObservable<SiteWatch> ObserveAll() => _store.GetAll();

        public async Task AddAsync(SiteWatch entity)
        {
            await _store.Save(entity);
            _addSubject.OnNext(entity);
        }

        public Task RemoveAsync(SiteWatch entity)
        {
            throw new NotImplementedException();
        }
    }
}