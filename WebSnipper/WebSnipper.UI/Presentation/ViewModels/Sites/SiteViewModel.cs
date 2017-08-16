using WebSnipper.UI.Business.Queries;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class SiteViewModel : NotifyObject
    {
        private readonly ISiteDisplayStrategy _displayStrategy;
        private string _url;

        public string Description
        {
            get => _displayStrategy.Description;
            set
            {
                _displayStrategy.Description = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsSelectable => !_displayStrategy.IsReadOnly;

        public string Url
        {
            get => _displayStrategy.Url;
            set
            {
                _displayStrategy.Url = value;
                NotifyPropertyChanged();
            }
        }


        public SiteViewModel(ISiteDisplayStrategy displayStrategy) => _displayStrategy = displayStrategy;
    }
}