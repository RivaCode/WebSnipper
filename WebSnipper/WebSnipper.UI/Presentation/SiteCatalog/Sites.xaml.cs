using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebSnipper.UI.Presentation.SiteCatalog
{
    /// <summary>
    /// Interaction logic for Sites.xaml
    /// </summary>
    public partial class Sites : UserControl
    {
        public static readonly DependencyProperty OpenSiteEditorCommandProperty = DependencyProperty.Register(
            nameof(OpenSiteEditorCommand), 
            typeof(ICommand), 
            typeof(Sites));

        public ICommand OpenSiteEditorCommand
        {
            get => (ICommand) GetValue(OpenSiteEditorCommandProperty);
            set => SetValue(OpenSiteEditorCommandProperty, value);
        }

        public Sites() => InitializeComponent();
    }
}
