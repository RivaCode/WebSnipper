using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Business.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        
        Task AddAsync(T entity);

        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}