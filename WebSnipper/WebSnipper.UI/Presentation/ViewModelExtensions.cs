using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive.Linq;

namespace WebSnipper.UI.Presentation
{
    public static class ViewModelExtensions
    {
        public static IObservable<TProp> ObserveProperty<T, TProp>(
            this T @src,
            Expression<Func<T, TProp>> valueReader)
            where T : INotifyPropertyChanged
            => Observable
                .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                    h => @src.PropertyChanged += h,
                    h => @src.PropertyChanged -= h)
                .Where(pattern => pattern.EventArgs.PropertyName.Equals(valueReader.AsName()))
                .Select(patter => valueReader.Compile()((T) patter.Sender));

        public static IObservable<bool> If<T>(
            this IObservable<T> @src,
            Func<T, bool> ifCondition)
            => @src.Select(ifCondition).Where(ifTrue => ifTrue);

        private static string AsName<T, TProp>(this Expression<Func<T, TProp>> @src)
            => ((MemberExpression) @src.Body).Member.Name;
    }
}