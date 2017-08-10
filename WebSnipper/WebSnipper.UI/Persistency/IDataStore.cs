using System;
using WebSnipper.UI.Domain;

namespace WebSnipper.UI.Persistency
{
    public interface IDataStore
    {
        IObservable<SiteWatch> GetAll();
    }
}