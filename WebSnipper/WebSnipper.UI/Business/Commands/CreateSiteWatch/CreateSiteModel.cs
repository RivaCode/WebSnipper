namespace WebSnipper.UI.Business.Commands
{
    public class CreateSiteModel
    {
        public string Url { get; }
        public string Description { get;}

        public CreateSiteModel(string url, string description)
        {
            Url = url;
            Description = description;
        }
    }
}