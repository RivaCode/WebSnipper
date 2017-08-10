using System;

namespace WebSnipper.UI.Core
{
    public static class FunctionalExtensions
    {
        public static TOut Map<TIn, TOut>(this TIn @src, Func<TIn, TOut> mapFn) => mapFn(@src);
    }
}