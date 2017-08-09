using System;
using System.Threading.Tasks;
using Optional;

namespace WebSnipper.UI.Business.Interfaces.Persistency
{
    public interface IRepository<T>
    {
        IObservable<T> ObserveAll();

        Option<T> Get(Guid id);
        Task<Option<T>> GetAsync(Guid id);

        void Add(T entity);

        void Remove(T entity);
    }
}