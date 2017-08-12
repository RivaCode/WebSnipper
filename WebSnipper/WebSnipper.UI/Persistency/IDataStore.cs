using System;
using System.Threading.Tasks;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public interface IDataStore
    {
        IObservable<Site> GetAll();
        Task Save(Site newSite);
    }
}