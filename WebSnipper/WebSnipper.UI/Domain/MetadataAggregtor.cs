namespace WebSnipper.UI.Domain
{
    public sealed class MetadataAggregtor
    {
        public RefreshRate RefreshRate { get; }

        public MetadataAggregtor(RefreshRate refresh) => RefreshRate = refresh;
    }
}