using WebSnipper.UI.Business.Queries;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class EditableSiteDisplayStrategy : ISiteDisplayStrategy
    {
        private readonly SiteModel _model;

        public string Description
        {
            get => _model.Description;
            set => _model.Description = value;
        }

        public bool IsReadOnly => false;

        public string Url
        {
            get => _model.Url;
            set => _model.Url = value;
        }


        public EditableSiteDisplayStrategy(SiteModel model) => _model = model;
    }
}