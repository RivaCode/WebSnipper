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

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            NotifyChanged()(new PropertyChangedEventArgs(propertyName));
        }
    }

    public static class NotifyPropertyChangeExtensions
    {
        public static void SetAndRaise<TValue>(
            this INotifyPropertyChanged _,
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
