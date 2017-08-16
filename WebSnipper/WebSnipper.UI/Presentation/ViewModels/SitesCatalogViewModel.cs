using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using WebSnipper.UI.Business.Queries;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.ViewModels
{
    public class SitesCatalogViewModel : NotifyObject
    {
        public ObservableCollection<SiteViewModel> Urls { get; }

        public SiteViewModel Selected { get; set; }
        public IReactiveCommand RemoveCmd { get; }

        public SitesCatalogViewModel(
            IGetSiteQuery siteQuery,
            ISiteInfoChangedQuery siteInfoChangedQuery)
        {
            Urls = new ObservableCollection<SiteViewModel>
            {
              new SiteViewModel(new AddSiteDisplayStrategy())
            };

            siteInfoChangedQuery
                .Execute()
                .Select(updateModel =>
                    Urls
                        .SkipWhile(vm => !vm.IsSelectable)
                        .FirstOrDefault(urlVm => urlVm.Url == updateModel.Url))
                .Where(foundVm => foundVm != null)
                .ObserveOnDispatcher()
                .Subscribe();

            siteQuery
                .Execute()
                .Select(model => new SiteViewModel(new EditableSiteDisplayStrategy(model)))
                .ObserveOnDispatcher()
                .Subscribe(Urls.Add);

            RemoveCmd = this.ObserveProperty(self => self.Selected)
                .If(selectedVm => selectedVm != null)
                .Map(canDeleteStream =>
                    Command.Create(canDeleteStream,
                        () => Urls.Remove(Selected)));
        }
    }
}