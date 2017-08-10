using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using WebSnipper.UI.Business.SiteWatchList;
using WebSnipper.UI.Core;
using WebSnipper.UI.Persistency;
using WebSnipper.UI.Persistency.Json;
using WebSnipper.UI.ViewModels;

namespace WebSnipper.UI
{
    public class MainViewModel : NotifyObject
    {
        public SiteInfoViewModel SiteInfoVm { get; }

        public MainViewModel()
        {
            SiteInfoVm = new SiteInfoViewModel(
                new GetSiteWatchListQuery(
                    new SiteWatchRepository(
                        new JsonDataStore())));
        }
    }
}
