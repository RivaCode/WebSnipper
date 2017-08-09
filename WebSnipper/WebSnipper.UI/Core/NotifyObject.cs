using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebSnipper.UI.Core
{
    public class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Action<PropertyChangedEventArgs> NotifyChanged()
        {
            var handler = PropertyChanged;
            return args => handler?.Invoke(this, args);
        }
    }

    public static class NotifyPropertyChangeExtensions
    {
        public static void SetAndRaise<TValue>(
            this INotifyPropertyChanged @src,
            ref TValue oldFieldValue,
            TValue newFieldValue,
            Action<PropertyChangedEventArgs> raise,
            [CallerMemberName] string propertyName = null)
        {
            if(EqualityComparer<TValue>.Default.Equals(oldFieldValue, newFieldValue)) return;

            oldFieldValue = newFieldValue;
            raise?.Invoke(new PropertyChangedEventArgs(propertyName));
        }
    }
}
