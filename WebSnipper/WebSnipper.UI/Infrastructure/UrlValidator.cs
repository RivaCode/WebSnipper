using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces;

namespace WebSnipper.UI.Infrastructure
{
    public class UrlValidator : IUrlValidator
    {
        public async Task Validate(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                && result.Scheme == Uri.UriSchemeHttp)
            {
                Ping testUrl = new Ping();
                await testUrl.SendPingAsync(IPAddress.Parse(url));
            }
            throw new UriFormatException($"{url} is not a valid URL");
        }
    }
}