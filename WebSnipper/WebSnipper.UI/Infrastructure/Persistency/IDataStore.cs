using System;
using System.Threading.Tasks;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public interface IDataStore
    {
        IObservable<Site> GetAllSites();
        Task SaveSiteAsync(Site newSite);
        Task UpdateSiteIsChanged(SiteUpdate updateInfo);

        IObservable<RefreshRate> GetRefershRate();
    }
}