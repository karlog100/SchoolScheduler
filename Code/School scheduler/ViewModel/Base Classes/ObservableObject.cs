using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    /// <summary>
    /// Base class for object that provides property change notifications.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        protected ObservableObject()
        {
        }

        #region RaisePropertyChanged
        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
