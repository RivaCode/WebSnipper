using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces.Persistency;
using WebSnipper.UI.Core;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Business.Queries
{
    public class SiteInfoChangedQuery : ISiteInfoChangedQuery
    {
        private readonly ISiteRepository _siteRepository;
        private readonly IMetadataRepository _metadataRepository;

        public SiteInfoChangedQuery(
            ISiteRepository siteRepository,
            IMetadataRepository metadataRepository)
        {
            _siteRepository = siteRepository;
            _metadataRepository = metadataRepository;
        }

        public IObservable<SiteUpdateModel> Execute()
        {
            return _metadataRepository
                .ObserveAll()
                .SwitchSelect(ma => Observable.Interval(ma.RefreshRate.Refresh))
                .SwitchSelect(_ => _siteRepository.ObserveAll().Select(ToDeferRequest()))
                .Merge(4);
        }

        private Func<Site, IObservable<SiteUpdateModel>> ToDeferRequest()
            => site
                => Observable
                    .Defer(() =>
                        WebRequest.CreateHttp($"http://{site.Url}")
                            .Tee(wr => wr.IfModifiedSince = new DateTime(2017,8,19,16,24,09))
                            .GetResponseAsync()
                            .ToObservable()
                    )
                    .Select(wr =>
                    {
                        
                        return new SiteUpdateModel();
                    })
            .Catch((Exception e)=>
                    {
                        return Observable.Empty<SiteUpdateModel>();
                    });
    }
}