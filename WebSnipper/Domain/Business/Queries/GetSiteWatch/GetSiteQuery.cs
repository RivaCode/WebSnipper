using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Domain.Business.Interfaces;

namespace Domain.Business
{
    public class GetSiteQuery : IGetSiteQuery
    {
        private readonly IWebsiteRepository _siteRepository;

        public GetSiteQuery(IWebsiteRepository siteRepository)
            => _siteRepository = siteRepository;

        public IObservable<SiteModel> Execute()
            => _siteRepository.GetAllAsync().ToObservable()
                .SelectMany(allWebsites => allWebsites.Select((website, index) => new {website, index}))
                .SelectMany( //Make it look like lazy loading
                    websiteArgs =>
                        Observable
                            .Start(() => websiteArgs.website)
                            .Delay(TimeSpan.FromSeconds(websiteArgs.index + 0.5)))
                .Select(website => new SiteModel
                {
                    Url = website.UrlHolder.Url.ToString(),
                    Name = website.Properties.Name,
                    Description = website.Properties.Description.ValueOr((string)null)
                });
    }
}