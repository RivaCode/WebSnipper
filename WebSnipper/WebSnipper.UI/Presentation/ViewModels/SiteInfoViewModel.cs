using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using WebSnipper.UI.Business.SiteWatchList;

namespace WebSnipper.UI.Presentation.ViewModels
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
                .Select(model => new UrlViewModel(model))
                .ObserveOnDispatcher()
                .Subscribe(Urls.Add);
        }
    }
}