using System;
using System.Net;
using System.Threading.Tasks;
using WebSnipper.UI.Business.Interfaces;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Infrastructure
{
    public class UrlValidator : IUrlValidator
    {
        public Task Validate(string url)
            => url.IfNot(
                    theUrl => theUrl.StartsWith("http://"),
                    theUrl => $"http://{theUrl}")
                .Map(ToHttp())
                .Tee(http => http.Timeout = 5000)
                .GetResponseAsync();

        private Func<string, HttpWebRequest> ToHttp() => WebRequest.CreateHttp;
    }
}