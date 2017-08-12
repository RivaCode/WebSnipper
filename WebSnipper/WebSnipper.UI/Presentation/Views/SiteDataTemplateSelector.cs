using System.Windows;
using System.Windows.Controls;
using WebSnipper.UI.Presentation.ViewModels;

namespace WebSnipper.UI.Presentation.Views
{
    public class SiteDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AddCard { private get; set; }
        public DataTemplate SiteCard { private get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) 
            => item is UrlAddViewModel ? AddCard : SiteCard;
    }
}
