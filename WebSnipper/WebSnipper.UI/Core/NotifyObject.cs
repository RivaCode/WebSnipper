using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebSnipper.UI.Core
{
    public class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected void SetAndNotifyPropertyChanged<T>(
            ref T oldValue,
            T newValue,
            [CallerMemberName] string propertyName = null)
        {
            if (Equals(oldValue, newValue)) return;

            oldValue = newValue;
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
