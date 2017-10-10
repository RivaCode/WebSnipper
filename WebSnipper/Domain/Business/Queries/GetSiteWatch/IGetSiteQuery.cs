using System;
using Domain.Core;

namespace Domain.Business
{
    public interface IGetSiteQuery : ICoreQuery<IObservable<SiteModel>>
    {
        
    }
}