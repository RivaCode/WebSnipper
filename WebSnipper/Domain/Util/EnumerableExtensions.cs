using System;
using System.Collections.Generic;

namespace Domain.Util
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(
            this IEnumerable<T> src,
            Action<T> doForItem)
        {
            foreach (var item in src)
            {
                doForItem(item);
            }
        }
    }
}
