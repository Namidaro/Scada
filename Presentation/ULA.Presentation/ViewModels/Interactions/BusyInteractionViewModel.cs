using System;
using System.Windows.Input;
using Prism.Commands;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a default system busy interaction view model
    ///     Представляет вью-модель окна ожидания
    /// </summary>
    public class BusyInteractionViewModel : ViewModelBase, IBusyInteractionViewModel
    {
        #region [Fields]

        private string _message = string.Empty;
        private string _title = string.Empty;

        #endregion [Fields]

        #region [IBusyInteractionViewModel mmebers]

        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заглавие
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title.Equals(value))
                    OnPropertyChanging("Title");
                this._title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///     Gets or sets the message of current interaction request
        ///     Сообщение
        /// </summary>
        public string Message
        {
            get { return this._message; }
            set
            {
                if (this._message.Equals(value)) return;
                OnPropertyChanging("Message");
                this._message = value;
                OnPropertyChanged("Message");
            }
        }

        /// <summary>
        ///     Gets an event that will be fired if user canceled interaction request
        ///     событие, которое бросается когда пользователь вывдает запрос на отмену
        /// </summary>
        public event EventHandler OnInteractionComplete;

        #endregion [IBusyInteractionViewModel mmebers]

        #region [Help members]

        /// <summary>
        ///     Fires the <see cref="OnInteractionComplete" /> event
        ///     Вызывает <see cref="OnInteractionComplete" /> событие
        /// </summary>
        protected virtual void OnOnInteractionComplete()
        {
            EventHandler handler = OnInteractionComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }
        /// <summary>
        ///   CancelBusyCommand
        /// </summary>
        public ICommand CancelBusyCommand
        {
            get
            {
                return this._submitCommand ?? (this._submitCommand = new DelegateCommand(this.OnOnInteractionComplete));
            }
        }
        #endregion [Help members]

        private bool _buttonVisible = false;
        private DelegateCommand _submitCommand;
        /// <summary>
        /// is  Button Visible
        /// </summary>
        public bool ButtonVisible
        {
            get { return _buttonVisible; }
            set
            {
                _buttonVisible = value;
               OnPropertyChanging("ButtonVisible");
            }
        }
    }
}