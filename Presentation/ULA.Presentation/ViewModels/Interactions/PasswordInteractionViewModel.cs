using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Представляет вью-модель окна валидации (пароля)
    /// </summary>
    public class PasswordInteractionViewModel : InteractionDialogViewModel, IPasswordInteractionViewModel
    {
        #region [Private fields]

        private string _password;
        private string _title;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Результат завершения вызова вьюшки
        /// </summary>
        public MessageBoxResult Result { get; set; }

        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title != null && this._title.Equals(value))
                    OnPropertyChanging("Title");
                this._title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///     Свойство представляющее пароль введенный пользователем
        /// </summary>
        public string Password
        {
            get { return this._password; }
            set
            {
                if(this._password != null && this._password.Equals(value)) return;
                this._password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion [Properties]

        #region [Override members]

        /// <summary>
        ///     Cancelling current interaction dialog
        /// </summary>
        protected override void OnCanceling()
        {
            this.Result = MessageBoxResult.CANCEL;
            base.OnCanceling();
        }

        /// <summary>
        ///     Submitted current interaction dialog
        /// </summary>
        protected override void OnSubmitting()
        {
            this.Result = MessageBoxResult.OK;
            base.OnSubmitting();
        }

        #endregion [Override members]
    }
}
