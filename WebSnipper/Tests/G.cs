using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Domain;
using Domain.Business;
using Infrastructure;

namespace Tests
{
    class G
    {
        public static void Main(string[] ar)
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.RegisterModule<InfrastructureModule>();
            cb.RegisterModule<DomainModule>();

            var container = cb.Build();

            container.Resolve<IGetSiteQuery>()
                .Execute()
                .Subscribe(x => { });

            Console.Read();
        }
    }
}
