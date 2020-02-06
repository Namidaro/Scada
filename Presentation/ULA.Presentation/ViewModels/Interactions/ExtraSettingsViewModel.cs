using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    /// 
    /// </summary>
    public class ExtraSettingsViewModel : InteractionDialogViewModel, IExtraSettingsViewModel
    {


        #region [Private fields]

        private ICommand _increasePartialSecondCommand;
        private ICommand _reducePartialSecondCommand;

        private ICommand _increaseFullSecondCommand;
        private ICommand _reduceFullSecondCommand;

        private ICommand _increaseQuerySecondCommand;
        private ICommand _reduceQuerySecondCommand;


        private ICommand _increaseMinuteRepeatIntervalCommand;
        private ICommand _reduceMinuteRepeatIntervalCommand;

        private ICommand _increaseNumberOfLightingCommandRepeatCommand;
        private ICommand _reduceNumberOfLightingCommandRepeatCommand;

        private bool _acknowledgeEnabled;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Конструктор
        /// </summary>
        public ExtraSettingsViewModel()
        {
            PartialMinValue = 5;
            PartialMaxValue = 60;


            FullMinValue = 3;
            FullMaxValue = 60;

            QueryMinValue = 10;
            QueryMaxValue = 60;
            MaxMinuteRepeatInterval = 10;
            MinMinuteRepeatInterval = 1;

            MinNumberOfLightingCommandRepeat = 0;
            MaxNumberOfLightingCommandRepeat = 5;
        }


        #endregion





        #region [Properties]


        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// повторения комманд освещения
        /// </summary>
       public int NumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        public int MinuteRepeatInterval { get; set; }
        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        public int MaxNumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        public int MaxMinuteRepeatInterval { get; set; }

        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        public int MinNumberOfLightingCommandRepeat { get; set; }


        /// <summary>
        ///  интервал повторения комманд освещения
        /// </summary>
        public int MinMinuteRepeatInterval { get; set; }



        /// <summary>
        ///     Результат завершения вызова вьюшки
        /// </summary>
        public MessageBoxResult Result { get; set; }




        /// <summary>
        ///     Описывает задержку в схеме в сукундах, которую будет выбирать пользователь
        /// </summary>
        public int FullTimeout { get; set; }



        /// <summary>
        ///     Описывает задержку в списке в сукундах, которую будет выбирать пользователь
        /// </summary>
        public int PartialTimeout { get; set; }


        /// <summary>
        ///     Описывает таймаут обменов, который будет выбирать пользователь
        /// </summary>
        public int QueryTimeout { get; set; }

        /// <summary>
        ///     Минимальное значение для обновления в схеме
        /// </summary>
        public int FullMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления в схеме
        /// </summary>
        public int FullMaxValue { get; set; }

        /// <summary>
        ///     Минимальное значение для обновления в списке
        /// </summary>
        public int PartialMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления в списке
        /// </summary>
        public int PartialMaxValue { get; set; }



        /// <summary>
        ///     Минимальное значение для таймаута обменов
        /// </summary>
        public int QueryMinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для таймаута обменов
        /// </summary>
        public int QueryMaxValue { get; set; }

        /// <summary>
        /// автоквитирование
        /// </summary>
        public bool AcknowledgeEnabled
        {
            get { return _acknowledgeEnabled; }
            set
            {
                _acknowledgeEnabled = value;
                OnPropertyChanged(nameof(AcknowledgeEnabled));
            }
        }








        #endregion [Properties]

        #region [ISynchronizationTimeInteractionViewModel members]

        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreasePartialSecondCommand
        {
            get
            {
                return this._increasePartialSecondCommand ?? (this._increasePartialSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.PartialTimeout == PartialMaxValue)
                        {
                            this.PartialTimeout = PartialMinValue;
                        }
                        else
                        {
                            this.PartialTimeout++;                            
                        }
                        OnPropertyChanged(nameof(PartialTimeout));
                    }));
            }
        }



        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReducePartialSecondCommand
        {
            get
            {
                return this._reducePartialSecondCommand ?? (this._reducePartialSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.PartialTimeout == PartialMinValue)
                        {
                            this.PartialTimeout = PartialMaxValue;
                        }
                        else
                        {
                            this.PartialTimeout--;
                        }
                        OnPropertyChanged(nameof(PartialTimeout));
                    }));
            }
        }





        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreaseFullSecondCommand
        {
            get
            {
                return this._increaseFullSecondCommand ?? (this._increaseFullSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.FullTimeout == FullMaxValue)
                        {
                            this.FullTimeout = FullMinValue;
                        }
                        else
                        {
                            this.FullTimeout++;                           
                        }
                        OnPropertyChanged(nameof(FullTimeout));
                    }));
            }
        }



        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReduceFullSecondCommand
        {
            get
            {
                return this._reduceFullSecondCommand ?? (this._reduceFullSecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.FullTimeout == FullMinValue)
                        {
                            this.FullTimeout = FullMaxValue;
                        }
                        else
                        {
                            this.FullTimeout--;
                        }
                        OnPropertyChanged(nameof(FullTimeout));
                    }));
            }
        }












        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreaseQuerySecondCommand
        {
            get
            {
                return this._increaseQuerySecondCommand ?? (this._increaseQuerySecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.QueryTimeout == QueryMaxValue)
                        {
                            this.QueryTimeout = QueryMinValue;
                        }
                        else
                        {
                            this.QueryTimeout++; 
                        }
                        OnPropertyChanged(nameof(QueryTimeout));
                    }));
            }
        }










        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReduceQuerySecondCommand
        {
            get
            {
                return this._reduceQuerySecondCommand ?? (this._reduceQuerySecondCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.QueryTimeout == QueryMinValue)
                        {
                            this.QueryTimeout = QueryMaxValue;
                        }
                        else
                        {
                            this.QueryTimeout--;
                        }
                        OnPropertyChanged(nameof(QueryTimeout));
                    }));
            }
        }




        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreaseMinuteRepeatIntervalCommand
        {
            get
            {
                return this._increaseMinuteRepeatIntervalCommand ?? (this._increaseMinuteRepeatIntervalCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.MinuteRepeatInterval == MaxMinuteRepeatInterval)
                        {
                            this.MinuteRepeatInterval = MinMinuteRepeatInterval;
                        }
                        else
                        {
                            this.MinuteRepeatInterval++;
                        }
                        OnPropertyChanged(nameof(MinuteRepeatInterval));
                    }));
            }
        }










        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReduceMinuteRepeatIntervalCommand
        {
            get
            {
                return this._reduceMinuteRepeatIntervalCommand ?? (this._reduceMinuteRepeatIntervalCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.MinuteRepeatInterval == MinMinuteRepeatInterval)
                        {
                            this.MinuteRepeatInterval = MaxMinuteRepeatInterval;
                        }
                        else
                        {
                            this.MinuteRepeatInterval--; 
                        }
                        OnPropertyChanged(nameof(MinuteRepeatInterval));
                    }));
            }
        }

        /// <summary>
        ///     Gets the increase second command of current interaction request
        ///     Команда увеличения счетчика секунд
        /// </summary>
        public ICommand IncreaseNumberOfLightingCommandRepeatCommand
        {
            get
            {
                return this._increaseNumberOfLightingCommandRepeatCommand ?? (this._increaseNumberOfLightingCommandRepeatCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.NumberOfLightingCommandRepeat == MaxNumberOfLightingCommandRepeat)
                        {
                            this.NumberOfLightingCommandRepeat = MinNumberOfLightingCommandRepeat;
                        }
                        else
                        {
                            this.NumberOfLightingCommandRepeat++;
                        }
                        OnPropertyChanged(nameof(NumberOfLightingCommandRepeat));
                    }));
            }
        }










        /// <summary>
        ///     Gets the reduce second command of current interaction request
        ///     Команда уменьшения счетчика секунд
        /// </summary>
        public ICommand ReduceNumberOfLightingCommandRepeatCommand
        {
            get
            {
                return this._reduceNumberOfLightingCommandRepeatCommand ?? (this._reduceNumberOfLightingCommandRepeatCommand =
                    new DelegateCommand(() =>
                    {
                        if (this.NumberOfLightingCommandRepeat == MinNumberOfLightingCommandRepeat)
                        {
                            this.NumberOfLightingCommandRepeat = MaxNumberOfLightingCommandRepeat;
                        }
                        else
                        {
                            this.NumberOfLightingCommandRepeat--;   
                        }
                        OnPropertyChanged(nameof(NumberOfLightingCommandRepeat));
                    }));
            }
        }


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