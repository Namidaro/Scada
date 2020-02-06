using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a information message box interaction view model
    ///     Представляет вью-модель информационного модального окна
    /// </summary>
    public class InformationMessageBoxInteractionViewModel : InteractionDialogViewModel,
        IInformationMessageBoxInteractionViewModel
    {
        #region [Fields]

        private string _message = string.Empty;
        private string _title = string.Empty;

        #endregion [Fields]

        #region [IInformationMessageBoxInteractionViewModel mmebers]

        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
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

        #endregion [IInformationMessageBoxInteractionViewModel mmebers]
    }
}