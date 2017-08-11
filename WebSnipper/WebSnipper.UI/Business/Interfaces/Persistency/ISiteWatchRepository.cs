using System;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Business.Interfaces.Persistency
{
    public interface ISiteWatchRepository : IRepository<SiteWatch>
    {
        IObservable<SiteWatch> SiteWatchAdd { get; }
    }
}