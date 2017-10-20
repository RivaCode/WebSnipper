using Autofac;
using Domain;
using Domain.Business;
using Infrastructure;
using WebSnipper.UI.Core;
using WebSnipper.UI.Presentation.SiteCards;
using WebSnipper.UI.Presentation.ViewModels;

namespace WebSnipper.UI
{
    public class MainViewModel : NotifyObject
    {
        public SiteCardsViewModel SiteCardsVm { get; }
        public NewSiteInfoViewModel NewSiteInfoVm { get; }

        public MainViewModel()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.RegisterModule<InfrastructureModule>();
            cb.RegisterModule<DomainModule>();

            var container = cb.Build();

            SiteCardsVm = new SiteCardsViewModel(
                container.Resolve<IGetSiteQuery>());
            NewSiteInfoVm = null;
        }
    }
}
