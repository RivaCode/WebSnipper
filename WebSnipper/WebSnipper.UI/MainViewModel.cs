using WebSnipper.UI.Business.Commands;
using WebSnipper.UI.Business.Queries;
using WebSnipper.UI.Core;
using WebSnipper.UI.Infrastructure;
using WebSnipper.UI.Persistency;
using WebSnipper.UI.Persistency.Json;
using WebSnipper.UI.Presentation.ViewModels;

namespace WebSnipper.UI
{
    public class MainViewModel : NotifyObject
    {
        public SitesCatalogViewModel SitesCatalogVm { get; }
        public NewSiteInfoViewModel NewSiteInfoVm { get; }

        public MainViewModel()
        {
            var dataStore = new JsonDataStore();
            var siteRepo = new SiteRepository(dataStore);
            var metaRepo = new MetadataRepository(dataStore);

            SitesCatalogVm = new SitesCatalogViewModel(
                new GetSiteQuery(siteRepo),
                new SiteInfoChangedQuery(siteRepo, metaRepo));

            NewSiteInfoVm = new NewSiteInfoViewModel(
                new CreateSiteCommand(siteRepo, new UrlValidator()));
        }
    }
}
