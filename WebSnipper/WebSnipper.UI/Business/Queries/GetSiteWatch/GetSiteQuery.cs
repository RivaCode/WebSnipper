using System;
using System.Reactive.Linq;
using WebSnipper.UI.Business.Interfaces.Persistency;

namespace WebSnipper.UI.Business.Queries
{
    public class GetSiteQuery : IGetSiteQuery
    {
        private readonly ISiteRepository _siteRepository;

        public GetSiteQuery(ISiteRepository siteRepository)
            => _siteRepository = siteRepository;

        public IObservable<SiteModel> Execute()
            => _siteRepository
                .ObserveAll()
                .SelectMany( //Make it look like lazy loading
                    (site, index) => Observable.Start(() => site).Delay(TimeSpan.FromSeconds(index + 1)))
                .Merge(_siteRepository.SiteAdded)
                .Select(site => new SiteModel
                {
                    Url = site.Url,
                    Description = site.Description
                });
    }
}