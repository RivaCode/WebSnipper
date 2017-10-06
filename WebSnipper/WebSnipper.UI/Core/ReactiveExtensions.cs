using System;
using System.Reactive.Linq;

namespace WebSnipper.UI.Core
{
    public static class ReactiveExtensions
    {
        public static IObservable<TOut> SwitchSelect<TIn, TOut>(
            this IObservable<TIn> src,
            Func<TIn, IObservable<TOut>> mapFn) 
            => src.Select(mapFn).Switch();
    }
}
