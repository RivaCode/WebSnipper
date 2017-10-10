using System;

namespace Domain.Util
{
    public static class FunctionalExtensions
    {
        public static TOut Map<TIn, TOut>(this TIn src, Func<TIn, TOut> mapFn) => mapFn(src);

        public static T IfNot<T>(this T src, Func<T, bool> ifFn, Func<T,T> then)
        {
            if (!ifFn(src))
            {
                src = then(src);
            }
            return src;
        }

        public static T Tee<T>(this T src, Action<T> callback)
        {
            callback(src);
            return src;
        }
    }
}