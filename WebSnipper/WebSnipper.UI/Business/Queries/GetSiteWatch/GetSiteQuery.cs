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
                .SelectMany( //Look like lazy loading
                    (siteWatch, index) => Observable.Start(() => siteWatch).Delay(TimeSpan.FromSeconds(index)))
                .Merge(_siteRepository.SiteAdded)
                .Select(site => new SiteModel
                {
                    Url = site.Url,
                    Description = site.Description
                });
    }
}