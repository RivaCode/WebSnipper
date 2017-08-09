namespace WebSnipper.UI.Domain
{
    public sealed class SiteWatch : ValueObject<SiteWatch>
    {
        public static SiteWatch New(string url) => new SiteWatch(url);

        public string Url { get; }
        public string Description { get; private set; }

        private SiteWatch(string url) => Url = url;

        public SiteWatch ChangeUrl(string url) => New(url);

        public SiteWatch ChangeDescription(string descr) => New(Url).With(descr);

        protected override bool EqualsCore(SiteWatch other) => Url.Equals(other.Url);

        protected override int GetHashCodeCore() => Url.GetHashCode();

        private SiteWatch With(string descr)
        {
            Description = descr;
            return this;
        }
    }
}