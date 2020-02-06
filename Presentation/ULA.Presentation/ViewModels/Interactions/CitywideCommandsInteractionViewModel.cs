using System.Windows.Input;
using Prism.Commands;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a CitywideCommands interaction view model
    ///     Представляет вью-модель окна "Общегородские команды"
    /// </summary>
    public class CitywideCommandsInteractionViewModel : InteractionDialogViewModel,
        ICitywideCommandsInteractionViewModel
    {
        #region [Fields]

        private string _message = string.Empty;
        private string _title = string.Empty;
        private ICommand _runNightlightCommand;
        private ICommand _stopNightlightCommand;
        private ICommand _runEveningCommand;
        private ICommand _stopEveningCommand;
        private ICommand _runFullCommand;
        private ICommand _stopFulltCommand;
        private ICommand _runBacklightCommand;
        private ICommand _stopBacklightCommand;
        private ICommand _runIlluminationCommand;
        private ICommand _stopIlluminationCommand;
        private ICommand _runStoregEnergyCommand;
        private ICommand _stopStoregEnergyCommand;
        private ICommand _autoAllCommand;
        private ICommand _manualModeAllCommand;

        #endregion [Fields]

        #region [ICitywideCommandsInteractionViewModel mmebers]

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
        ///     Get a result a citywide interaction
        ///     Результат вызова интерактивного окна
        /// </summary>
        public CitywideCommandResult Result { get; private set; }

        /// <summary>
        ///     Включить ночное освещение
        /// </summary>
        public ICommand RunNightLightCommand
        {
            get
            {
                return this._runNightlightCommand ??
                       (this._runNightlightCommand = new DelegateCommand(OnRunNighlightCommand));
            }
        }

        private void OnRunNighlightCommand()
        {
            this.Result = CitywideCommandResult.OnNighlight;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить ночное освещение
        /// </summary>
        public ICommand StopNightLightCommand
        {
            get
            {
                return this._stopNightlightCommand ??
                       (this._stopNightlightCommand = new DelegateCommand(OnStopNighlightCommand));
            }
        }

        private void OnStopNighlightCommand()
        {
            this.Result = CitywideCommandResult.OffNighlight;
            base.OnSubmit();
        }

        /// <summary>
        ///     Включить вечернее освещение
        /// </summary>
        public ICommand RunEveningCommand
        {
            get
            {
                return this._runEveningCommand ??
                       (this._runEveningCommand = new DelegateCommand(OnStartEveningCommand));
            }
        }

        private void OnStartEveningCommand()
        {
            this.Result = CitywideCommandResult.OnEvening;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить вечернее освещение
        /// </summary>
        public ICommand StopEveningCommand
        {
            get
            {
                return this._stopEveningCommand ??
                       (this._stopEveningCommand = new DelegateCommand(OnStopEveningCommand));
            }
        }

        private void OnStopEveningCommand()
        {
            this.Result = CitywideCommandResult.OffEvening;
            base.OnSubmit();
        }

        /// <summary>
        ///     Включить полное освещение
        /// </summary>
        public ICommand RunFullCommand
        {
            get
            {
                return this._runFullCommand ??
                       (this._runFullCommand = new DelegateCommand(OnRunFullCommand));
            }
        }

        private void OnRunFullCommand()
        {
            this.Result = CitywideCommandResult.OnFull;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить полное освещение
        /// </summary>
        public ICommand StopFullCommand
        {
            get
            {
                return this._stopFulltCommand ??
                       (this._stopFulltCommand = new DelegateCommand(OnStopFullCommand));
            }
        }

        private void OnStopFullCommand()
        {
            this.Result = CitywideCommandResult.OffFull;
            base.OnSubmit();
        }

        /// <summary>
        ///     Включить подсветку
        /// </summary>
        public ICommand RunBacklightCommand
        {
            get
            {
                return this._runBacklightCommand ??
                       (this._runBacklightCommand = new DelegateCommand(OnRunBacklightCommand));
            }
        }

        private void OnRunBacklightCommand()
        {
            this.Result = CitywideCommandResult.OnBacklight;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить подсветку
        /// </summary>
        public ICommand StopBacklightCommand
        {
            get
            {
                return this._stopBacklightCommand ??
                       (this._stopBacklightCommand = new DelegateCommand(OnStopBacklightCommand));
            }
        }

        private void OnStopBacklightCommand()
        {
            this.Result = CitywideCommandResult.OffBacklight;
            base.OnSubmit();
        }

        /// <summary>
        ///     Включить иллюминацию
        /// </summary>
        public ICommand RunIlluminationCommand
        {
            get
            {
                return this._runIlluminationCommand ??
                       (this._runIlluminationCommand = new DelegateCommand(OnStartIlluminationCommand));
            }
        }

        private void OnStartIlluminationCommand()
        {
            this.Result = CitywideCommandResult.OnIllumination;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить иллюминацию
        /// </summary>
        public ICommand StopIlluminationCommand
        {
            get
            {
                return this._stopIlluminationCommand ??
                       (this._stopIlluminationCommand = new DelegateCommand(OnStopIlluminationCommand));
            }
        }

        private void OnStopIlluminationCommand()
        {
            this.Result = CitywideCommandResult.OffIllumination;
            base.OnSubmit();
        }

        /// <summary>
        ///     Включить энергосбережение
        /// </summary>
        public ICommand RunStoregEnergyCommand
        {
            get
            {
                return this._runStoregEnergyCommand ??
                       (this._runStoregEnergyCommand = new DelegateCommand(OnRunStoregEnergyCommand));
            }
        }

        private void OnRunStoregEnergyCommand()
        {
            this.Result = CitywideCommandResult.OnStoregEnergy;
            base.OnSubmit();
        }

        /// <summary>
        ///     Отключить энергосбережение
        /// </summary>
        public ICommand StopStoregEnergyCommand
        {
            get
            {
                return this._stopStoregEnergyCommand ??
                       (this._stopStoregEnergyCommand = new DelegateCommand(OnStopStoregEnergyCommand));
            }
        }

        private void OnStopStoregEnergyCommand()
        {
            this.Result = CitywideCommandResult.OffStoregEnergy;
            base.OnSubmit();
        }

        /// <summary>
        ///     Represent run auto mode all devices action
        ///     Авторежим
        /// </summary>
        public ICommand AutoAllCommand
        {
            get
            {
                return this._autoAllCommand ??
                       (this._autoAllCommand = new DelegateCommand(OnAutoAllCommand));
            }
        }

        private void OnAutoAllCommand()
        {
            this.Result = CitywideCommandResult.AutoAll;
            base.OnSubmit();
        }

        /// <summary>
        /// Run manual mode on all devices (Ручной режим)
        /// </summary>
        public ICommand ManualModeAllCommand
        {
            get
            {
                return this._manualModeAllCommand ??
                       (this._manualModeAllCommand = new DelegateCommand(OnManualModeAllCommand));
            }
        }

        private void OnManualModeAllCommand()
        {
            this.Result = CitywideCommandResult.ManualModeAll;
            base.OnSubmit();
        }
        #endregion [ICitywideCommandsInteractionViewModel mmebers]

        #region [Override members]

        /// <summary>
        ///     Canceling current interaction dialog
        ///     Отмена текущего интерактивного диалога
        /// </summary>
        protected override void OnCanceling()
        {
            this.Result = CitywideCommandResult.Cancel;
            base.OnCanceling();
        }

        #endregion [Override members]
    }
}