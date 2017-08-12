using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Disposables;
using WebSnipper.UI.Core;

namespace WebSnipper.UI.Presentation
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
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