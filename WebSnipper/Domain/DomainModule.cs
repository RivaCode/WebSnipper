using Autofac;
using Domain.Business;

namespace Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetSiteQuery>().As<IGetSiteQuery>();
            builder.RegisterType<GetSiteInfoQuery>().As<IGetSiteInfoQuery>();
            builder.RegisterType<CreateSiteCommand>().As<ICreateSiteCommand>();
        }
    }
}
