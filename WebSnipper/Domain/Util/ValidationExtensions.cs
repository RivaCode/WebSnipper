using System;

namespace Domain.Util
{
    internal static class ValidationExtensions
    {
        public static TRef ValidateArgument<TRef>(this TRef reference, string name)
            where TRef : class
            => reference ?? throw new ArgumentException(name);
    }
}
