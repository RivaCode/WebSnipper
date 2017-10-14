using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Domain.Util
{
    public static class ReactiveExtensions
    {
        public static IObservable<T> DelayBy<T>(
            this T src,
            double delay,
            IScheduler scheduler)
            => Observable
                .Start(() => src)
                .Delay(TimeSpan.FromSeconds(delay), scheduler);

        public static IObservable<TOut> SwitchSelect<TIn, TOut>(
            this IObservable<TIn> src,
            Func<TIn, IObservable<TOut>> mapFn) 
            => src.Select(mapFn).Switch();

        public static IObservable<TOut> SwitchSelect<TIn, TOut>(
            this IObservable<TIn> src,
            Func<TIn, Task<TOut>> mapFn)
            => src.Select(mapFn).Switch();

        public static IObservable<TOut> SwitchSelect<TIn, TOut>(
            this IObservable<TIn> src,
            Func<IObservable<TOut>> mapFn)
            => src.Select(_ => mapFn()).Switch();

        public static IObservable<TOut> SwitchSelect<TIn, TOut>(
            this IObservable<TIn> src,
            Func<Task<TOut>> mapFn)
            => src.Select(_ => mapFn()).Switch();
    }
}
