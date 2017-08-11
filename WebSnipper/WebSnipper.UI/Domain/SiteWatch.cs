using System;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Domain
{
    public sealed class SiteWatch : ValueObject<SiteWatch>
    {
        public static SiteWatch New(string url) => new SiteWatch(url);

        public string Url { get; }
        public string Description { get; private set; }
        public DateTime LastWatchTime { get; private set; }

        private SiteWatch(string url) => Url = url;
        
        public SiteWatch With(string descr) => New(Url).Tee(watch => watch.Description = descr);
        public SiteWatch With(DateTime lastTime) => New(Url).Tee(watch => watch.LastWatchTime = lastTime);

        protected override bool EqualsCore(SiteWatch other) => Url.Equals(other.Url);

        protected override int GetHashCodeCore() => Url.GetHashCode();
    }
}