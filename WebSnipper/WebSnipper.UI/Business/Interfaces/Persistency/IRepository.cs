using System;

namespace WebSnipper.UI.Business.Interfaces.Persistency
{
    public interface IRepository<T>
    {
        IObservable<T> ObserveAll();
        
        void Add(T entity);

        void Remove(T entity);
    }
}