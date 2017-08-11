using System.Threading.Tasks;

namespace WebSnipper.UI.Core
{
    public interface ICoreCommand<in T>
    {
        Task Execute(T entity);
    }
}