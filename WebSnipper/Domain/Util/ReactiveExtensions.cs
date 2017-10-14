using Optional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Domain.Util
{
    public static class ReactiveExtensions
    {
        public static IObservable<TOut> ReduceTo<TIn, TOut>(
            this IObservable<Option<TIn>> src,
            Func<TIn, TOut> mapFn)
            => src.SelectMany(
                option => option.Map(mapFn).ToEnumerable());

        public static IObservable<Option<T>> ThrowIfEmpty<T, TException>(
         this IObservable<Option<T>> src,
         Func<TException> exceptionFactory)
            where TException : Exception
            => src.Select(
                option =>
                option.Tee(self => 
                    self.MatchNone(() => throw exceptionFactory())));

        public static IObservable<T> DelayEachItemBy<T>(
            this IObservable<IEnumerable<T>> src,
            double delay,
            IScheduler scheduler)
            => src
                .SelectMany(items => items.Select((item, index) => new { item, index }))
                .SelectMany(itemArgs => itemArgs.item.DelaySelf(itemArgs.index + delay, scheduler));

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

        private static IObservable<T> DelaySelf<T>(
            this T src,
            double delay,
            IScheduler scheduler)
            => Observable
                .Start(() => src)
                .Delay(TimeSpan.FromSeconds(delay), scheduler);
    }
}
