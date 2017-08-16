namespace WebSnipper.UI.Presentation.ViewModels
{
    public interface ISiteDisplayStrategy
    {
        string Url { get; set; }
        string Description { get; set; }
        bool IsReadOnly { get; }
    }
}