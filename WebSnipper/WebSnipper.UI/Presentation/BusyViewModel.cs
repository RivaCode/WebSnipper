using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class BusyViewModel : NotifyObject
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            private set => this.SetAndRaise(ref _isBusy, value, NotifyChanged());
        }

        public IDisposable StartBusy()
        {
            IsBusy = true;
            return Disposable.Create(() => IsBusy = false);
        }
    }
}