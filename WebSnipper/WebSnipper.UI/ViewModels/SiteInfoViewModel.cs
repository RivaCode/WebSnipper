using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using WebSnipper.UI.Business.SiteWatchList;

namespace WebSnipper.UI.ViewModels
{
    public class SiteInfoViewModel
    {
        public ObservableCollection<UrlViewModel> Urls { get; }
        public SiteInfoViewModel(
            ISiteWatchListQuery siteWatchListQuery)
        {
            Urls = new ObservableCollection<UrlViewModel>();

            siteWatchListQuery
                .Execute()
                .SelectMany((model, i) => Observable.Start(() => new UrlViewModel(model)).Delay(TimeSpan.FromSeconds(i+2)))
                .ObserveOnDispatcher()
                .Subscribe(Urls.Add);
        }
    }
}