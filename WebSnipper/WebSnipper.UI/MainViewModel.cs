using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using WebSnipper.UI.Core;
using WebSnipper.UI.ViewModels;

namespace WebSnipper.UI
{
    public class MainViewModel : NotifyObject
    {
        public List<UrlViewModel> Urls { get; }

        public MainViewModel()
        {
            Urls = Enumerable.Range(1, 10)
                .Select(i => new
                {
                    descr = "This is a test run",
                    url = "www.google.com",
                    i
                })
                .Select(x => new UrlViewModel
                {
                    Description = x.i % 2 == 0 ? x.descr : $"{x.descr}-{x.descr}",
                    Url = x.url
                })
                .ToList();
        }
    }
}
