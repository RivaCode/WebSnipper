using System;
using System.Reactive.Linq;
using WebSnipper.UI.Business.Interfaces.Persistency;

namespace WebSnipper.UI.Business.SiteWatchList
{
    public class GetSiteWatchListQuery : ISiteWatchListQuery
    {
        private readonly ISiteWatchRepository _siteWatchRepository;

        public GetSiteWatchListQuery(
            ISiteWatchRepository siteWatchRepository)
        {
            _siteWatchRepository = siteWatchRepository;
        }

        public IObservable<SiteWatchModel> Execute()
            => _siteWatchRepository
                .ObserveAll()
                .Select(site => new SiteWatchModel
                {
                    Url = site.Url,
                    Description = site.Description
                });
    }
}