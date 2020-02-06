using System;
using System.Windows.Input;
using Prism.Commands;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a syncronization time interaction view model
    ///     Представляет вью-модель окна синхронизации времени
    /// </summary>
    public class SynchronizationTimeInteractionViewModel : InteractionDialogViewModel, ISynchronizationTimeInteractionViewModel
    {
        #region [Private fields]

        private DateTime _time;
        private int _hour;
        private int _second;
        private int _minute;
        private ICommand _systemTimeCommand;
        private ICommand _increaseSecondCommand;
        private ICommand _increaseMinuteCommand;
        private ICommand _increaseHourCommand;
        private ICommand _reduceSecondCommand;
        private ICommand _reduceMinuteCommand;
        private ICommand _reduceHourCommand;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Create a instance of <see cref="SynchronizationTimeInteractionViewModel"/>
        /// </summary>
        public SynchronizationTimeInteractionViewModel()
        {
            this.DateTime = System.DateTime.Now;
            this.Hour = this.DateTime.Hour;
            this.Minute = this.DateTime.Minute;
            this.Second = this.DateTime.Second;
        }

        #endregion [Ctor's]

        #region [ISynchronizationTimeInteractionViewModel members]

        /// <summary>
        ///     Gets or sets the time of current interaction request
        ///     Свойство с дата-временем
        /// </summary>
        public DateTime DateTime
        {
            get { return this._time; }
            set
            {
                if (this._time.Equals(value)) return;
                OnPropertyChanging("DateTime");
                this._time = value;
                OnPropertyChanged("DateTime");
            }
        }

        /// <summary>
        ///     Gets or sets the Hour of current interaction request
        ///     Час
        /// </summary>
        public int Hour
        {
            get { return this._hour; }
            set
            {
                if (this._hour.Equals(value)) return;
                OnPropertyChanging("Hour");
                this._hour = value;
                OnPropertyChanged("Hour");
            }
        }

        /// <summary>
        ///     Gets or sets the Minute of current interaction request
        ///     Минута
        /// </summary>
        public int Minute
        {
            get { return this._minute; }
            set
            {
                if (this._minute.Equals(value)) return;
                OnPropertyChanging("Minute");
                this._minute = value;
                OnPropertyChanged("Minute");
            }
        }

        /// <summary>
        ///     Gets or sets the Second of current interaction request
        ///     Секунда
        /// </summary>
        public int Second
        {
            get { return this._second; }
            set
            {
                if (this._second.Equals(value)) return;
                OnPropertyChanging("Second");
                this._second = value;
                OnPropertyChanged("Second");
            }
        }

        /// <summary>
        ///     Gets the system time command of current interaction request
        ///     Команда представляющая установку системного времени
        /// </summary>
        public ICommand SystemTimeCommand {
            get { return this._systemTimeCommand ?? (this._systemTimeCommand = new DelegateCommand(OnSetSystemTime)); } 
        }

        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увелечения счетчика секунд
        /// </summary>
        public ICommand IncreaseSecondCommand 
        {
            get
            {
                return this._increaseSecondCommand ?? (this._increaseSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Second == 59)
                        {
                            this.Second = 0;
                        }
                        else
                        {
                            this.Second++;
                        }
                    }));
            } 
        }

        /// <summary>
        ///     Gets the increase minute command of current interaction request
        ///     Команда увелечения счетчика минут
        /// </summary>
        public ICommand IncreaseMinuteCommand 
        {
            get
            {
                return this._increaseMinuteCommand ?? (this._increaseMinuteCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Minute == 59)
                        {
                            this.Minute = 0;
                        }
                        else
                        {
                            this.Minute++;
                        }
                    }));
            } 
        }

        /// <summary>
        ///     Gets the increase hour command of current interaction request
        ///     Команда увелечения счетчика часов
        /// </summary>
        public ICommand IncreaseHourCommand
        {
            get
            {
                return this._increaseHourCommand ?? (this._increaseHourCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Hour == 23)
                        {
                            this.Hour = 0;
                        }
                        else
                        {
                            this.Hour++;
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
                        if (this.Second == 0)
                        {
                            this.Second = 59;
                        }
                        else
                        {
                            this.Second--;
                        }
                    }));
            }
        }

        /// <summary>
        ///     Gets the reduce minute command of current interaction request
        ///     Команда уменьшения счетчика минут
        /// </summary>
        public ICommand ReduceMinuteCommand
        {
            get
            {
                return this._reduceMinuteCommand ?? (this._reduceMinuteCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Minute == 0)
                        {
                            this.Minute = 59;
                        }
                        else
                        {
                            this.Minute--;
                        }
                    }));
            }
        }

        /// <summary>
        ///     Gets the reduce hour command of current interaction request
        ///     Команда уменьшения счетчика часов
        /// </summary>
        public ICommand ReduceHourCommand
        {
            get
            {
                return this._reduceHourCommand ?? (this._reduceHourCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.Hour == 0)
                        {
                            this.Hour = 23;
                        }
                        else
                        {
                            this.Hour--;
                        }
                    }));
            }
        }

        /// <summary>
        ///     Gets or sets the result of current interaction request
        ///     Представляет результатирующий запрос интерактивного диалога
        /// </summary>
        public MessageBoxResult Result { get; private set; }

        #endregion [ISynchronizationTimeInteractionViewModel members]

        #region [Override members]

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Запрос на отмену
        /// </summary>
        protected override void OnCanceling()
        {
            this.Result = MessageBoxResult.CANCEL;
            base.OnCanceling();
        }

        /// <summary>
        ///     Submitted current interaction dialog
        ///     Запрос на подтверждение
        /// </summary>
        protected override void OnSubmitting()
        {
            this.DateTime = new DateTime(this.DateTime.Year, this.DateTime.Month, this.DateTime.Day,
                this.Hour, this.Minute, this.Second);
            this.Result = MessageBoxResult.OK;
            base.OnSubmitting();
        }

        #endregion [Override members]

        #region [Help members]

        //Установка системного времени
        private void OnSetSystemTime()
        {
            this.DateTime = System.DateTime.Now;
            this.Hour = System.DateTime.Now.Hour;
            this.Minute = System.DateTime.Now.Minute;
            this.Second = System.DateTime.Now.Second;
            this.OnSubmit();
        }

        #endregion [Help members]
    }
}
