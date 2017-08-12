using System;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Business.Interfaces.Persistency
{
    public interface ISiteRepository : IRepository<Site>
    {
        IObservable<Site> SiteAdded { get; }
    }
}