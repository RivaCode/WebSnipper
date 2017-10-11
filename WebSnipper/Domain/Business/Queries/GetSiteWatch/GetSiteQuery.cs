using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Domain.Business.Interfaces;

namespace Domain.Business
{
    public class GetSiteQuery : IGetSiteQuery
    {
        private readonly IWebsiteRepository _siteRepository;
        private readonly double _delayBy;
        private readonly IScheduler _scheduler;

        public GetSiteQuery(
            IWebsiteRepository siteRepository,
            double delayBy = 0.5,
            IScheduler scheduler = null)
        {
            _siteRepository = siteRepository;
            _delayBy = delayBy;
            _scheduler = scheduler ?? new EventLoopScheduler();
        }

        public IObservable<SiteModel> Execute()
            => _siteRepository.GetAllAsync().ToObservable()
                .SelectMany(allWebsites => allWebsites.Select((website, index) => new {website, index}))
                .SelectMany( 
                    websiteArgs =>
                        Observable //Make it look like lazy loading
                            .Start(() => websiteArgs.website)
                            .Delay(TimeSpan.FromSeconds(websiteArgs.index + _delayBy), _scheduler))
                .Select(website => new SiteModel
                {
                    Url = website.UrlHolder.Url.ToString(),
                    Name = website.Properties.Name,
                    Description = website.Properties.Description.ValueOr((string)null)
                });
    }
}