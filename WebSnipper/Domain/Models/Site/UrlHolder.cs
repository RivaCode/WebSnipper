using System;

namespace Domain.Models
{
    public class UrlHolder : ValueObject<UrlHolder>
    {
        public Uri Url { get; }


        protected override bool EqualsCore(UrlHolder other) => Url == other.Url;

        protected override int GetHashCodeCore() => Url.GetHashCode();
    }
}
