using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebSnipper.UI.Presentation.SiteCards
{
    /// <summary>
    /// Interaction logic for SiteCardsView.xaml
    /// </summary>
    public partial class SiteCardsView : UserControl
    {
        public static readonly DependencyProperty OpenSiteEditorCommandProperty = DependencyProperty.Register(
            nameof(OpenSiteEditorCommand), 
            typeof(ICommand), 
            typeof(SiteCardsView));

        public ICommand OpenSiteEditorCommand
        {
            get => (ICommand) GetValue(OpenSiteEditorCommandProperty);
            set => SetValue(OpenSiteEditorCommandProperty, value);
        }

        public SiteCardsView() => InitializeComponent();
    }
}
