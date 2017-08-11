using System;
using System.Reactive.Linq;
using WebSnipper.UI.Business.Interfaces.Persistency;

namespace WebSnipper.UI.Business.Queries
{
    public class GetSiteWatchQuery : IGetSiteWatchQuery
    {
        private readonly ISiteWatchRepository _siteWatchRepository;

        public GetSiteWatchQuery(ISiteWatchRepository siteWatchRepository) 
            => _siteWatchRepository = siteWatchRepository;

        public IObservable<SiteWatchModel> Execute()
            => _siteWatchRepository
                .ObserveAll()
                .SelectMany( //Look like lazy loading
                    (siteWatch, index) => Observable.Start(() => siteWatch).Delay(TimeSpan.FromSeconds(index)))
                .Merge(_siteWatchRepository.SiteWatchAdd)
                .Select(site => new SiteWatchModel
                {
                    Url = site.Url,
                    Description = site.Description
                });
    }
}