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
        public SiteInfoViewModel SiteInfoVm { get; }
        public NewSiteInfoViewModel NewSiteInfoVm { get; }

        public MainViewModel()
        {
            var repo = new SiteRepository(new JsonDataStore());
            SiteInfoVm = new SiteInfoViewModel(
                new GetSiteQuery(repo));

            NewSiteInfoVm = new NewSiteInfoViewModel(
                new CreateSiteCommand(repo, new UrlValidator()));
        }
    }
}
