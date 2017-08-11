using System;
using System.Reactive.Disposables;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation
{
    public class BusyViewModel : NotifyObject
    {
        public bool IsBusy { get; private set; }

        protected IDisposable StartBusy()
        {
            IsBusy = true;
            return Disposable.Create(() => IsBusy = false);
        }
    }
}