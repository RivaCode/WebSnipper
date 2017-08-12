using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using WebSnipper.UI.Business.Queries;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class SiteInfoViewModel : NotifyObject
    {
        public ObservableCollection<IUrlViewModel> Urls { get; }

        public IUrlViewModel Selected { get; set; }
        public IReactiveCommand RemoveCmd { get; }

        public SiteInfoViewModel(
            IGetSiteQuery siteQuery)
        {
            Urls = new ObservableCollection<IUrlViewModel>
            {
              new UrlAddViewModel()  
            };

            siteQuery
                .Execute()
                .Select(model => new UrlViewModel(model))
                .ObserveOnDispatcher()
                .Subscribe(Urls.Add);

            RemoveCmd = this.ObserveProperty(self => self.Selected)
                .If(selectedVm => selectedVm != null)
                .Map(canDeleteStream =>
                    Command.Create(canDeleteStream,
                        () =>
                        {
                            Urls.Remove(Selected);
                        }));
        }
    }
}