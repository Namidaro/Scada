using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a warning message box interaction view model
    ///     Представляет вью-модель сообщения-предупреждения
    /// </summary>
    public class WarningMessageBoxInteractionViewModel : InteractionDialogViewModel,
        IWarningMessageBoxInteractionViewModel
    {
        #region [Fields]

        private MessageBoxButtonType _buttonType = MessageBoxButtonType.OK;
        private string _message = string.Empty;
        private string _title = string.Empty;

        #endregion [Fields]

        #region [IWarningMessageBoxInteractionViewModel mmebers]

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

        /// <summary>
        ///     Gets a value that indicates result of interaction with user
        ///     Результирующий запрос интерактивного диалога
        /// </summary>
        public MessageBoxResult Result { get; private set; }

        /// <summary>
        ///     Gets or sets a type byttons for interaction with user
        ///     Свойство с типом кновок диалога предупреждения
        /// </summary>
        public MessageBoxButtonType ButtonType
        {
            get { return this._buttonType; }
            set
            {
                if (this._buttonType.Equals(value))
                    OnPropertyChanging("ButtonType");
                this._buttonType = value;
                OnPropertyChanged("ButtonType");
            }
        }

        #endregion [IWarningMessageBoxInteractionViewModel mmebers]

        #region [Override members]

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Запрос на отмену
        /// </summary>
        protected override void OnCanceling()
        {
            switch (this.ButtonType)
            {
                case MessageBoxButtonType.OK_CANCEL:
                    this.Result = MessageBoxResult.CANCEL;
                    break;
                case MessageBoxButtonType.YES_NO:
                    this.Result = MessageBoxResult.NO;
                    break;
            }
            base.OnCanceling();
        }

        /// <summary>
        ///     Submitted current interaction dialog
        ///     Запрос на подтверждение
        /// </summary>
        protected override void OnSubmitting()
        {
            switch (this.ButtonType)
            {
                case MessageBoxButtonType.OK_CANCEL:
                case MessageBoxButtonType.OK:
                    this.Result = MessageBoxResult.OK;
                    break;
                case MessageBoxButtonType.YES_NO:
                    this.Result = MessageBoxResult.YES;
                    break;
            }
            base.OnSubmitting();
        }

        #endregion [Override members]
    }
}