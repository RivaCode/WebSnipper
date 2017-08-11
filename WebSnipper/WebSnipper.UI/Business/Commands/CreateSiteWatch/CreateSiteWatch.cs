namespace WebSnipper.UI.Business.Commands
{
    public class CreateSiteWatch
    {
        public string Url { get; }
        public string Description { get;}

        public CreateSiteWatch(string url, string description)
        {
            Url = url;
            Description = description;
        }
    }
}