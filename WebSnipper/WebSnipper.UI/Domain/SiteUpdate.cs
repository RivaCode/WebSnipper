namespace WebSnipper.UI.Domain
{
    public class SiteUpdate : ValueObject<SiteUpdate>
    {
        public string Url { get;}
        public bool IsChanged { get; }

        public SiteUpdate(string url, bool isChanged)
        {
            Url = url;
            IsChanged = isChanged;
        }

        protected override bool EqualsCore(SiteUpdate other) => Url.Equals(other.Url);

        protected override int GetHashCodeCore() => Url.GetHashCode();
    }
}
