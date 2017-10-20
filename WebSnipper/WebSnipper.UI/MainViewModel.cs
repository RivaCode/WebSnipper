using Autofac;
using Domain;
using Domain.Business;
using Infrastructure;
using WebSnipper.UI.Core;
using WebSnipper.UI.Presentation.SiteCatalog;
using WebSnipper.UI.Presentation.ViewModels;

namespace WebSnipper.UI
{
    public class MainViewModel : NotifyObject
    {
        public SitesCatalogViewModel SitesCatalogVm { get; }
        public NewSiteInfoViewModel NewSiteInfoVm { get; }

        public MainViewModel()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.RegisterModule<InfrastructureModule>();
            cb.RegisterModule<DomainModule>();

            var container = cb.Build();

            SitesCatalogVm = new SitesCatalogViewModel(
                container.Resolve<IGetSiteQuery>());
            NewSiteInfoVm = null;
        }
    }
}
