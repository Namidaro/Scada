using System.Windows.Input;
using Prism.Commands;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Вью-моделька для окна установки периода обновлений
    /// </summary>
    public class UpdateTimeoutInteractionViewModel : InteractionDialogViewModel, IUpdateTimeoutInteractionViewModel
    {
        #region [Private fields]

        private ICommand _increaseSecondCommand;
        private ICommand _reduceSecondCommand;
        private int _period;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Конструктор
        /// </summary>
        public UpdateTimeoutInteractionViewModel()
        {
            this.MinValue = 5;
            this.MaxValue = 60;
        } 

        #endregion

        #region [Properties]

        /// <summary>
        ///     Результат завершения вызова вьюшки
        /// </summary>
        public MessageBoxResult Result { get; set; }

        /// <summary>
        ///     Период введенный полльзователем для задержки
        /// </summary>
        public int Timeout
        {
            get { return this._period; }
            set
            {
                if (this._period == value) return;
                this._period = value;
                OnPropertyChanged("Timeout");
            }
        }

        #endregion [Properties]

        #region [ISynchronizationTimeInteractionViewModel members]

        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreaseSecondCommand
        {
            get
            {
                return this._increaseSecondCommand ?? (this._increaseSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Timeout == MaxValue)
                        {
                            this.Timeout = MinValue;
                        }
                        else
                        {
                            this.Timeout++;
                        }
                    }));
            }
        }

        

        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReduceSecondCommand
        {
            get
            {
                return this._reduceSecondCommand ?? (this._reduceSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Timeout == MinValue)
                        {
                            this.Timeout = MaxValue;
                        }
                        else
                        {
                            this.Timeout--;
                        }
                    }));
            }
        }

        /// <summary>
        ///     Минимальное значение для обновления
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления
        /// </summary>
        public int MaxValue { get; set; }

        #endregion [ISynchronizationTimeInteractionViewModel members]

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
