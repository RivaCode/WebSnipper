using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Domain.Business.Interfaces;
using Domain.Util;

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

        public IObservable<SlimSiteModel> Execute()
            => _siteRepository.GetAllAsync()
                .ToObservable()
                .DelayEachItemBy(_delayBy, _scheduler)
                .Select(website => new SlimSiteModel { Name = website.Properties.Name });
    }
}