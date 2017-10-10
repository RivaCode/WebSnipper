using Autofac;
using Domain.Business;
using Domain.Core;
using Domain.Util;

namespace Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetSiteQuery>().As<IGetSiteQuery>();
            builder.RegisterType<CreateSiteCommand>().As<ICreateSiteCommand>();
        }
    }
}
