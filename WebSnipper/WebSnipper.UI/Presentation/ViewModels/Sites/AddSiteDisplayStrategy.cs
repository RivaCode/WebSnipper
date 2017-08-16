namespace WebSnipper.UI.Presentation.ViewModels
{
    public class AddSiteDisplayStrategy : ISiteDisplayStrategy
    {

        public string Url {
            get { return null; }
            set { }
        }

        public string Description
        {
            get { return "Click to add new item"; }
            set {  }
        }

        public bool IsReadOnly => true;
    }
}