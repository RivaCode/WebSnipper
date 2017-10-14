using System.Collections.Generic;
using System.Threading.Tasks;
using Optional;

namespace Domain.Business.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<Option<T>> TryFindById(string id);

        Task AddAsync(T entity);

        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}