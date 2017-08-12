using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebSnipper.UI.Presentation.Views
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
