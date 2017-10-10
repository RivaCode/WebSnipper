namespace Domain.Business
{
    public class CreateSiteModel
    {
        public string Url { get; }
        public string Name { get; }
        public string Description { get;}

        public CreateSiteModel(string url, string name, string description = null)
        {
            Url = url;
            Name = name;
            Description = description;
        }
    }
}