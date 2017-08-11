using System;
using System.Threading.Tasks;

namespace WebSnipper.UI.Business.Interfaces.Persistency
{
    public interface IRepository<T>
    {
        IObservable<T> ObserveAll();
        
        Task AddAsync(T entity);

        Task RemoveAsync(T entity);
    }
}