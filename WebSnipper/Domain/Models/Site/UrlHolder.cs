using System;

namespace Domain.Models
{
    public class UrlHolder : ValueObject<UrlHolder>
    {
        public Uri Url { get; }

        public UrlHolder(string url)
        {
            if (!url?.StartsWith("http://") ?? false)
            {
                url = $"http://{url}";
            }
            if (!(Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp ||
                 uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                throw new ArgumentException($"{url} is not a valid URL");
            }
            Url = uriResult;
        }

        protected override bool EqualsCore(UrlHolder other) => Url == other.Url;

        protected override int GetHashCodeCore() => Url.GetHashCode();
    }
}
