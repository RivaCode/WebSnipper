using System;
using Domain.Core;

namespace Domain.Business
{
    public interface IGetSiteInfoQuery : ICoreQuery<string, IObservable<SiteInfoModel>>
    {
    }
}
