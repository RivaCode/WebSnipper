using System;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Domain
{
    public sealed class Site : ValueObject<Site>
    {
        public static Site New(string url) => new Site(url);

        public string Url { get; }
        public string Description { get; private set; }
        public DateTime LastWatchTime { get; private set; }

        private Site(string url) => Url = url;
        
        public Site With(string descr) => New(Url).Tee(watch => watch.Description = descr);
        public Site With(DateTime lastTime) => New(Url).Tee(watch => watch.LastWatchTime = lastTime);

        protected override bool EqualsCore(Site other) => Url.Equals(other.Url);

        protected override int GetHashCodeCore() => Url.GetHashCode();
    }
}