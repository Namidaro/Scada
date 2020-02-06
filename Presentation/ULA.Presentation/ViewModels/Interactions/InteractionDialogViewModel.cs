using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents basic interaction dialog view model with submit and cancel functionality
    ///     Представляет базовую вью-модель интерактивного окна с функциональностью подтверждения и отмены
    /// </summary>
    public abstract class InteractionDialogViewModel : ViewModelBase
    {
        #region [Private fields]

        private ICommand _cancelCommand;
        private ICommand _submitCommand;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets a value that indicates whether current dialog was canceled by a user
        ///     Получает значение показывающее или текущий диалог был отменён
        /// </summary>
        public bool IsCanceled { get; protected set; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents current dialog cancel command
        ///     Команда представляющая отмену
        /// </summary>
        public ICommand CancelCommand
        {
            get { return this._cancelCommand ?? (this._cancelCommand = new DelegateCommand(this.OnCancel)); }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents current dialog submit command
        ///     Команда представляющая подтверждение
        /// </summary>
        public ICommand SubmitCommand
        {
            get { return this._submitCommand ?? (this._submitCommand = new DelegateCommand(this.OnSubmit)); }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Initializes an instance of <see cref="InteractionDialogViewModel" />
        /// </summary>
        protected InteractionDialogViewModel()
        {
            this.IsCanceled = false;
        }

        #endregion [Ctor's] 

        #region [Templated members]

        /// <summary>
        ///     Submitting current interaction dialog
        ///     Подтверждение текущего интерактивного диалога(для переопределения в дочерних классах)
        /// </summary>
        protected virtual void OnSubmitting()
        {
            /*None*/
        }

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Отмена текущего интерактивного диалога(для переопределения в дочерних классах)
        /// </summary>
        protected virtual void OnCanceling()
        {
            /*None*/
        }

        /// <summary>
        ///     Completes current interaction session
        ///     Окончание текущей интерактивной сессии
        /// </summary>
        protected virtual void OnOnInteractionComplete()
        {
            var handler = OnInteractionComplete;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Submits current interaction request
        ///     Запрос на подтверждение
        /// </summary>
        protected virtual void OnSubmit()
        {

            this.OnSubmitting();
            this.IsCanceled = false;
            this.OnOnInteractionComplete();
            
        }

        #endregion [Templated members]

        #region [ICreateNewDeviceViewModel members]

        /// <summary>
        ///     Gets an ivent that will be fired if user canceled interaction request
        ///     Событие выбрасываемое при завершении интерактивного диалога
        /// </summary>
        public event EventHandler OnInteractionComplete;

        #endregion [ICreateNewDeviceViewModel members]

        #region [Help members]

        private void OnCancel()
        {
            this.OnCanceling();
            this.IsCanceled = true;
            this.OnOnInteractionComplete();
        }

        #endregion [Help members]
    }
}