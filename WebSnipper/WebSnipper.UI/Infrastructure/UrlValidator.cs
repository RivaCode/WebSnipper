using System.Net;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces;

namespace WebSnipper.UI.Infrastructure
{
    public class UrlValidator : IUrlValidator
    {
        public Task Validate(string url) => WebRequest.CreateHttp(url).GetResponseAsync();
    }
}