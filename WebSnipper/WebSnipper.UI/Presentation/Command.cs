using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace WebSnipper.UI.Presentation
{
    public interface IReactiveCommand : ICommand, IObservable<Unit>
    {

    }

    public static class Command
    {
        public static IReactiveCommand Create(Action apply)
            => new ObservableCommand(Observable.Start(() => true), apply);

        public static IReactiveCommand Create(IObservable<bool> canExecute, Action apply)
            => new ObservableCommand(canExecute, apply);

        private class ObservableCommand : ObservableBase<Unit> , IReactiveCommand
        {
            private readonly Action _apply;
            private bool _canExecute;
            private readonly IDisposable _canSubscription;

            public ObservableCommand(
                IObservable<bool> canExecute,
                Action apply)
            {
                _apply = apply;
                _canSubscription = canExecute
                    .Subscribe(can =>
                    {
                        _canExecute = can;
                        var handler = CanExecuteChanged;
                        handler?.Invoke(this, EventArgs.Empty);
                    });
            }

            public bool CanExecute(object parameter) => _canExecute;

            public void Execute(object parameter) => _apply();

            public event EventHandler CanExecuteChanged;

            protected override IDisposable SubscribeCore(IObserver<Unit> observer) => _canSubscription;
        }
    }
}
