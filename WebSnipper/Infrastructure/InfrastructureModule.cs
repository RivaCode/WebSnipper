using Autofac;
using Domain.Business.Interfaces;
using Domain.Util;
using Infrastructure.Converters;
using Infrastructure.Core;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonAdapterStore>().As<IJsonStore>();

            builder.RegisterType<WebsiteRepository>().As<IWebsiteRepository>();
            builder.RegisterType<WebsiteConverter>().AsImplementedInterfaces();
        }
    }
}
