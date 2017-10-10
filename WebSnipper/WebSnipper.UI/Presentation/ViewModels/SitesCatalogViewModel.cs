using Domain.Business;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class SitesCatalogViewModel : NotifyObject
    {
        private readonly IGetSiteQuery _getSiteQuery;

        public SitesCatalogViewModel(IGetSiteQuery getSiteQuery)
        {
            _getSiteQuery = getSiteQuery;
        }
    }
}