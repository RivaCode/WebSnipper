using System.Threading.Tasks;

namespace Domain.Core
{
    public interface ICoreCommand<in T>
    {
        Task Execute(T entity);
    }
}