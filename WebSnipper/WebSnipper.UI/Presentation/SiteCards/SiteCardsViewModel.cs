using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Domain.Business;
using Domain.Util;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation.SiteCards
{
    public class SiteCardsViewModel : NotifyObject
    {
        public ObservableCollection<CardViewModel> Cards { get; }
            = new ObservableCollection<CardViewModel>();

        public SiteCardsViewModel(
            IGetSiteQuery getSiteQuery,
            IScheduler scheduler = null)
        {
            getSiteQuery.Execute()
                .Select(m => new CardViewModel(m))
                .Select(cardVm => cardVm.Expand().Except(Cards, new CardComparer()))
                .ObserveOn(scheduler ?? DispatcherScheduler.Current)
                .Subscribe(cardVms => cardVms.ForEach(Cards.Add));
        }

        private class CardComparer : IEqualityComparer<CardViewModel>
        {
            public bool Equals(CardViewModel x, CardViewModel y) 
                => x?.Name.Equals(y?.Name) ?? false;

            public int GetHashCode(CardViewModel obj) => obj.Name.GetHashCode();
        }
    }
}