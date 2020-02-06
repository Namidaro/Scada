using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a question message box interaction view model
    ///     Представляет вью-модель интерактивного окна вопроса
    /// </summary>
    public class QuestionMessageBoxInteractionViewModel : InteractionDialogViewModel,
        IQuestionMessageBoxInteractionViewModel
    {
        #region [Fields]

        private string _message = string.Empty;
        private string _title = string.Empty;

        #endregion [Fields]

        #region [IQuestionMessageBoxInteractionViewModel mmebers]

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
        ///     Результат взаимодействия с пользователем
        /// </summary>
        public MessageBoxResult Result { get; private set; }

        #endregion [IQuestionMessageBoxInteractionViewModel mmebers]

        #region [Override members]

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Запрос на отмену
        /// </summary>
        protected override void OnCanceling()
        {
            this.Result = MessageBoxResult.NO;
            base.OnCanceling();
        }

        /// <summary>
        ///     Submitted current interaction dialog
        ///     Запрос на подтверждение
        /// </summary>
        protected override void OnSubmitting()
        {
            this.Result = MessageBoxResult.YES;
            base.OnSubmitting();
        }

        #endregion [Override members]
    }
}