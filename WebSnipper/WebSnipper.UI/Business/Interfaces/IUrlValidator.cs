using System.Threading.Tasks;

namespace WebSnipper.UI.Business.Interfaces
{
    public interface IUrlValidator
    {
        Task Validate(string url);
    }
}