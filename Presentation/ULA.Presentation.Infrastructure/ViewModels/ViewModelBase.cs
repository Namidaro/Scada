using System.ComponentModel;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Represents a basic view model functionality
    /// </summary>
    public class ViewModelBase : Disposable, INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region [INotifyPropertyChanged members]

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion [INotifyPropertyChanged members]

        #region [INotifyPropertyChanging members]

        /// <summary>
        ///     Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion [INotifyPropertyChanging members]

        #region [Virtual mmebers]

        /// <summary>
        ///     Notifies property changed
        /// </summary>
        /// <param name="propertyName">The name of the property that has been changed</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Notifies property changing
        /// </summary>
        /// <param name="propertyName">The name of the property that is being changed</param>
        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (handler != null) handler(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion [Virtual mmebers]
    }
}