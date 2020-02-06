using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using NLog;
using ULA.AddinsHost.Business.Device;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Interceptors;
using ULA.Interceptors.Application;
using ULA.Localization;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.Views.Interactions;
using YP.Toolkit.System.Validation;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Factories;
using ULA.Log;
using ULA.Log.LoggerService;
using ULA.Presentation.Infrastructure.ViewModels.Log;
using ULA.Presentation.Loggers;
using ULA.Presentation.ViewModels.Interactions;
using Application = System.Windows.Application;
using MessageBoxResult = ULA.Presentation.Infrastructure.ViewModels.MessageBoxResult;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Represents application runtime mode view model functionality
    ///     Представляет вью-модель для режима реального времени
    /// </summary>
    public class ApplicationRuntimeModeViewModel : ViewModelBase, IApplicationRuntimeModeViewModel, INavigationAware
    {
        #region [Private fields]

        private readonly IRuntimeModeDevicesServices _configService;
        private readonly IInteractionService _interactionService;
        private ICommand _navigateToAboutCommand;
        private ICommand _navigateToCommandsOnSelectedDevicesCommand;
        private ICommand _navigateToCitywideCommandsCommand;
        private ICommand _navigateToCommandOnTemplatesCommand;

        private ICommand _navigateToSynchronizationTimeCommand;
        private ICommand _navigateToSynchronizationTimeOnSelectedDevicesCommand;
        private ICommand _changeTimeModeCommand;


        private ICommand _changeModeToScheme;
        private ICommand _changeModeToListWidget;
        private ICommand _navigateToJournalOfSystemCommand;
        private ICommand _navigateToHelpCommand;
        private IRuntimeDeviceViewModel _currentDeviceViewModel;
        private List<IRuntimeDeviceViewModel> _selectedDeviceViewModels;

        private ObservableCollection<IRuntimeDeviceViewModel> _devices;
        private IBackgroundPicture _backgroundPicture;
        private IApplicationSettingsService _settings;
        private readonly CityWideCommandsLogger _cityWideCommandsLogger;
        private readonly IApplicationLogViewModel _applicationLogViewModel;
        private readonly IDeviceCommandFactory _deviceCommandFactory;
        private readonly IDeviceCommandService _deviceCommandService;
        private Logger _logger;


        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ApplicationRuntimeModeViewModel" />
        /// </summary>
        /// <param name="interactionService">
        ///     An instance of <see cref="IInteractionService" /> to use
        /// </param>
        /// <param name="configService">
        ///     An instance of <see cref="IRuntimeModeDevicesServices" /> to use
        /// </param>
        /// <param name="settings">Сервис с настойками программы</param>
        public ApplicationRuntimeModeViewModel(IInteractionService interactionService,
            IRuntimeModeDevicesServices configService, IApplicationSettingsService settings, CityWideCommandsLogger cityWideCommandsLogger, IApplicationLogViewModel applicationLogViewModel, IDeviceCommandFactory deviceCommandFactory, IDeviceCommandService deviceCommandService)
        {
            Guard.AgainstNullReference(interactionService, "interactionService");
            Guard.AgainstNullReference(configService, "configService");
            Guard.AgainstNullReference(settings, "settings");

            this._interactionService = interactionService;
            this._configService = configService;
            this._settings = settings;
            _cityWideCommandsLogger = cityWideCommandsLogger;
            _applicationLogViewModel = applicationLogViewModel;
            _deviceCommandFactory = deviceCommandFactory;
            _deviceCommandService = deviceCommandService;

            try
            {
                if (!File.Exists(String.Format("./nlog_{0}_{1}.txt", DateTime.Now.Month.ToString("D2"), DateTime.Now.Year.ToString("D4"))))
                {
                    using (File.Create(String.Format("./nlog_{0}_{1}.txt", DateTime.Now.Month.ToString("D2"), DateTime.Now.Year.ToString("D4")))) { }
                }
            }
            catch (Exception)
            { }
            this._logger = LogManager.GetLogger("Общие");

        }

        #endregion [Ctor's]

        #region [Properties]


        /// <summary>
        /// 
        /// </summary>
        public IBackgroundPicture BackgroundPicture
        {
            get { return _backgroundPicture; }
            set
            {
                _backgroundPicture = value;
                OnPropertyChanged(nameof(BackgroundPicture));
            }
        }



        /// <summary>
        ///     Gets or sets an instance of <see cref="IConfigLogicalDevice" /> that represents current virtual deviceViewModel model
        ///     Устройство выбранное в данный момент
        /// </summary>
        public IRuntimeDeviceViewModel CurrentDeviceViewModel
        {
            get { return this._currentDeviceViewModel; }
            set
            {
                if (this._currentDeviceViewModel != null && this._currentDeviceViewModel.Equals(value)) return;
                this._currentDeviceViewModel = value;
                OnPropertyChanged("CurrentDeviceViewModel");
            }
        }

        /// <summary>
        ///     Gets or sets an instance of <see cref="IConfigLogicalDevice" /> that represents current virtual deviceViewModel model
        ///     Устройство выбранное в данный момент
        /// </summary>
        public List<IRuntimeDeviceViewModel> SelectedDeviceViewModels
        {
            get { return this._selectedDeviceViewModels; }
            set
            {
                if (this._selectedDeviceViewModels != null && this._selectedDeviceViewModels.Equals(value)) return;
                this._selectedDeviceViewModels = value;
                OnPropertyChanged("SelectedDeviceViewModels");
            }
        }



        /// <summary>
        ///     Gets or sets a collection of devices
        ///     Все устройства
        /// </summary>
        public ObservableCollection<IRuntimeDeviceViewModel> Devices
        {
            get { return this._devices; }
            set
            {
                this.OnPropertyChanging("Devices");
                this._devices = value;
                this.OnPropertyChanged("Devices");
            }
        }

        #endregion [Properties]

        #region [IApplicationRuntimeModeViewModel members]
       /// <summary>
        ///     Команда навигации на файл помощи
        /// </summary>
        public ICommand ChangeTimeModeCommand
        {
            get
            {
                return this._changeTimeModeCommand ??
                       (this._changeTimeModeCommand = new DelegateCommand(OnChangeTimeModeCommand));
            }
        }
        /// <summary>
        ///     Команда навигации на файл помощи
        /// </summary>
        public ICommand NavigateToHelpCommand
        {
            get
            {
                return this._navigateToHelpCommand ??
                       (this._navigateToHelpCommand = new DelegateCommand(OnNavigateToHelpCommand));
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to journal of system view command
        ///     Навигация на окно журнала системы
        /// </summary>
        public ICommand NavigateToJournalOfSystemCommand
        {
            get
            {
                return this._navigateToJournalOfSystemCommand ??
                       (this._navigateToJournalOfSystemCommand = new DelegateCommand(this.OnNavigateToJournalOfSystem));
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to about view command
        ///     Навигация на окно "О программе"
        /// </summary>
        public ICommand NavigateToAboutCommand
        {
            get
            {
                return this._navigateToAboutCommand ??
                       (_navigateToAboutCommand = new DelegateCommand(OnNavigateToAbout));
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to citywide commands view command
        ///     Навигация на окно общегородских команд
        /// </summary>
        public ICommand NavigateToCitywideCommandsCommand
        {
            get
            {
                return this._navigateToCitywideCommandsCommand ??
                       (_navigateToCitywideCommandsCommand = new DelegateCommand(() =>
                       {
                           OnNavigateToCitywideCommands(false);
                       }));
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to command on templates view command
        ///     Навигация на окно команд по шаблону
        /// </summary>
        public ICommand NavigateToCommandOnTemplatesCommand
        {
            get
            {
                return this._navigateToCommandOnTemplatesCommand ??
                       (_navigateToCommandOnTemplatesCommand = new DelegateCommand(OnNavigateToCommandOnTemplatesCommand));
            }
        }



        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to citywide commands view command
        ///     Навигация на окно общегородских команд
        /// </summary>
        public ICommand NavigateToCommandsOnSelectedDevicesCommand
        {
            get
            {
                return this._navigateToCommandsOnSelectedDevicesCommand ??
                       (_navigateToCommandsOnSelectedDevicesCommand = new DelegateCommand(() =>
                       {
                           OnNavigateToCitywideCommands(true);
                       }));
            }
        }

        /// <summary>
        /// Gets an instance of <see cref="ICommand" /> that represents navigate to synchronization command view command
        ///     Навигация на окно синхронизации времени на выбранные устройства
        /// </summary>
        public ICommand NavigateToSynchronizationTimeOnSelectedDevicesCommand
        {
            get
            {
                return this._navigateToSynchronizationTimeOnSelectedDevicesCommand ??
                       (_navigateToSynchronizationTimeOnSelectedDevicesCommand = new DelegateCommand(() =>
                       {
                           OnNavigateToSynchronizationTime(true);
                       }));
            }
        }




        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to synchronization command view command
        ///     Навигация на окно синхронизации времени
        /// </summary>
        public ICommand NavigateToSynchronizationTimeCommand
        {
            get
            {
                return this._navigateToSynchronizationTimeCommand ?? (this._navigateToSynchronizationTimeCommand =
                    new DelegateCommand(() => { OnNavigateToSynchronizationTime(false); }));
            }
        }

        /// <summary>
        ///     Command represents action of changing mode to SchemeMode
        ///     Команда перехода на схему устройства
        /// </summary>
        public ICommand ChangeModeToScheme
        {
            get
            {
                return this._changeModeToScheme ?? (this._changeModeToScheme = new DelegateCommand(OnChangeModeToScheme));
            }
        }

        /// <summary>
        ///     Command represents action of changing mode to ListWidgetModeMode
        ///     Команда перехода на лист устройств
        /// </summary>
        public ICommand ChangeModeToListWidget
        {
            get
            {
                return this._changeModeToListWidget ?? (this._changeModeToListWidget =
                    new DelegateCommand(OnChangeModeToListWidget));
            }
        }

        #endregion [IApplicationRuntimeModeViewModel members]

        #region [IApplicationModeViewModel members]

        /// <summary>
        ///     Initializes current application mode asynchronously
        ///     Ачинхронная инициализация
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task InitializeAsync()
        {
            var devices = await this._configService.GetAllDevicesAsync();
            this.Devices = new ObservableCollection<IRuntimeDeviceViewModel>(devices.OrderBy(dev => dev.DeviceNumber));
            BackgroundPicture = _configService.GetBackgroundPicture();

            this.CurrentDeviceViewModel = this.Devices.FirstOrDefault();
        }

        /// <summary>
        ///     Un-initializes current application mode asynchronously
        ///     Ачинхронная Де-инициализация
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task UninitializeAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var runtimeLogicalDevice in this.Devices)
                {
                    runtimeLogicalDevice.Dispose();
                }
            });
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.Regions.Remove(ApplicationGlobalNames.RUNTIME_REGION_NAME);
            regionManager.Regions.Remove(ApplicationGlobalNames.CONFIGURATION_REGION_NAME);
            this.Devices = null;
        }

        /// <summary>
        ///     Invokes when initialization process is over
        ///     вызывается когда процесс инициализации закончен
        /// </summary>
        public void OnInitialized()
        {
            _settings.Load();

            (new LoggerServiceBase()).LogMessage(" Переход в режим реального времени", LogManager.GetLogger(LogMessageTypes.COMMEN_MESSAGE), LogMessageTypes.COMMEN_MESSAGE);
            if (this.Devices.Count != 0)
            {
                foreach (var runtimelogicaldevice in _devices)
                {
                    (runtimelogicaldevice.Model as IRuntimeDevice).SetUpdatingMode(false);
                }
                return;
            };
            this._interactionService
                .Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                          onInitialized: dialog =>
                          {
                              dialog.Title = "Перейти в режим конфигурации?";
                              dialog.Message = "Программа не сконфигурирована. Перейти в режим конфигурации?";
                          },
                          onUninitialized: dialog =>
                          {
                              if (dialog.Result == MessageBoxResult.YES)
                                  SendKeys.SendWait("^{F5}");
                          });
        }

        #endregion [IApplicationModeViewModel members]

        #region [Help members]

        private void OnNavigateToHelpCommand()
        {
            var path = Directory.GetCurrentDirectory();
            Help.ShowHelp(null, "file://" + path + "\\Files\\SUNO.chm");
        }


        private void OnChangeTimeModeCommand()
        {
            Devices.ForEach((model =>model.IsTimeMode=!model.IsTimeMode ));
        }



        private void OnNavigateToJournalOfSystem()
        {

            try
            {
                _applicationLogViewModel.ShowLog();
            }
            catch (Exception error)
            {
                this.InteractWithError(error);
            }
        }


        private void OnNavigateToAbout()
        {
            this._interactionService.Interact(ApplicationInteractionProviders.AboutInteractionProvider, viewModel => { },
                                         viewModel => { });
        }

        private void OnNavigateToSynchronizationTime(bool isCommandOnSelectedDevices)
        {
            IEnumerable<IRuntimeDeviceViewModel> devicesToOperate;
            if (isCommandOnSelectedDevices)
            {
                devicesToOperate = SelectedDeviceViewModels;
            }
            else
            {
                devicesToOperate = Devices;
            }
            if (!devicesToOperate.Any()) return;

            DateTime result = DateTime.MinValue;
            this._interactionService.Interact(ApplicationInteractionProviders.SynchronizationTimeInteractionProvider,
                viewModel => { }, viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.CANCEL)
                    {
                        return;
                    }
                    else if (viewModel.Result == MessageBoxResult.OK)
                    {

                        result = viewModel.DateTime;
                        if (result == DateTime.MinValue) return;
                        OnNavigateToAcceptSyncTime(result, devicesToOperate);
                    }
                    else
                    {
                        throw new ArgumentException("SynchronizationTimeInteraction result is incorrect");
                    }
                });

        }
        private void OnNavigateToCitywideCommands(bool isCommandOnSelectedDevices)
        {

            IEnumerable<IRuntimeDeviceViewModel> devicesToOperate;
            if (isCommandOnSelectedDevices)
            {
                devicesToOperate = SelectedDeviceViewModels;
            }
            else
            {
                devicesToOperate = Devices;
            }
            if (!devicesToOperate.Any()) return;
            _cityWideCommandsLogger.SetIsCommandOnSelectedDevices(isCommandOnSelectedDevices);
            CitywideCommandResult result = CitywideCommandResult.Cancel;
            this._interactionService.Interact(ApplicationInteractionProviders.CitywideCommandsInteractionProvider,
                viewModel => { },
                viewModel =>
                {
                    result = viewModel.Result;
                    if (result != CitywideCommandResult.Cancel) AcceptCitywideCommand(result, devicesToOperate);
                });

        }


        private void OnNavigateToCommandOnTemplatesCommand()
        {
            this._interactionService.Interact(ApplicationInteractionProviders.CommandOnTemplatesInteractionProvider,
                viewModel => { },
                viewModel => { });
        }


        private void AcceptCitywideCommand(CitywideCommandResult result, IEnumerable<IRuntimeDeviceViewModel> devicesToOperate)
        {


            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    if (result == CitywideCommandResult.OnNighlight)
                    {
                        viewModel.Message = "Включить ночное освещение?";
                    }
                    else if (result == CitywideCommandResult.OffNighlight)
                    {
                        viewModel.Message = "Выключить ночное освещение?";
                    }
                    else if (result == CitywideCommandResult.AutoAll)
                    {
                        viewModel.Message = "Включить авторежим?";
                    }
                    else if (result == CitywideCommandResult.ManualModeAll)
                    {
                        viewModel.Message = "Включить ручной режим?";
                    }
                    else if (result == CitywideCommandResult.OnEvening)
                    {
                        viewModel.Message = "Включить вечернее освещение?";
                    }
                    else if (result == CitywideCommandResult.OffEvening)
                    {
                        viewModel.Message = "Выключить вечернее освещение?";
                    }
                    else if (result == CitywideCommandResult.OnFull)
                    {
                        viewModel.Message = "Включить полное освещение?";
                    }
                    else if (result == CitywideCommandResult.OffFull)
                    {
                        viewModel.Message = "Выключить полное освещение?";
                    }
                    else if (result == CitywideCommandResult.OnBacklight)
                    {
                        viewModel.Message = "Включить подсветку?";
                    }
                    else if (result == CitywideCommandResult.OffBacklight)
                    {
                        viewModel.Message = "Выключить подсветку?";
                    }
                    else if (result == CitywideCommandResult.OnIllumination)
                    {
                        viewModel.Message = "Включить иллюминацию?";
                    }
                    else if (result == CitywideCommandResult.OffIllumination)
                    {
                        viewModel.Message = "Выключить иллюминацию?";
                    }
                    else if (result == CitywideCommandResult.OnStoregEnergy)
                    {
                        viewModel.Message = "Включить энергосбережение?";
                    }
                    else if (result == CitywideCommandResult.OffStoregEnergy)
                    {
                        viewModel.Message = "Выключить энергосбережение?";
                    }
                },
                viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.YES)
                    {
                        if (result == CitywideCommandResult.Cancel)
                        {
                            return;
                        }
                        else if (result == CitywideCommandResult.OnNighlight)
                        {
                            _cityWideCommandsLogger.NightLightingOn(_logger);
                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {
                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunNightlightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffNighlight)
                        {
                            _cityWideCommandsLogger.NightLightingOff(_logger);
                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {
                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopNightlightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.AutoAll)
                        {
                            _cityWideCommandsLogger.AutoModeAllStarters(_logger);
                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunAutoModeCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.ManualModeAll)
                        {
                            _cityWideCommandsLogger.ManualModeAllStarters(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopAutoModeCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OnEvening)
                        {
                            _cityWideCommandsLogger.EveninglightingLightingOn(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunEveninglightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffEvening)
                        {
                            _cityWideCommandsLogger.EveninglightingLightingOff(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopEveninglightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OnFull)
                        {
                            _cityWideCommandsLogger.FulllightingLightingOn(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunFullLightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffFull)
                        {
                            _cityWideCommandsLogger.FulllightingLightingOff(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {


                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopFullLightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OnBacklight)
                        {
                            _cityWideCommandsLogger.BackLightOn(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunBackLightLightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffBacklight)
                        {
                            _cityWideCommandsLogger.BackLightOff(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopBackLightLightingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OnIllumination)
                        {
                            _cityWideCommandsLogger.IlluminationOn(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateRunIlluminationCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffIllumination)
                        {
                            _cityWideCommandsLogger.IlluminationOff(_logger);

                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopIlluminationCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OnStoregEnergy)
                        {
                            _cityWideCommandsLogger.EnergySavingOn(_logger);
                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStartEnergySavingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);

                            }
                        }
                        else if (result == CitywideCommandResult.OffStoregEnergy)
                        {
                            _cityWideCommandsLogger.EnergySavingOff(_logger);
                            foreach (var runtimeLogicalDevice in devicesToOperate)
                            {

                                _deviceCommandService.AddDeviceCommandCreator(
                                    () => _deviceCommandFactory.CreateStopEnergySavingCommand(
                                        runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Citywide result exeption");
                        }
                    }
                });
        }



        private void OnNavigateToAcceptSyncTime(DateTime resDateTime, IEnumerable<IRuntimeDeviceViewModel> devicesToOperate)
        {

            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Синхронизировать время на всех устройствах?";
                },
                viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.YES)
                    {
                        _cityWideCommandsLogger.SuncTimeAllDevices(_logger);
                        foreach (var runtimeLogicalDevice in devicesToOperate)
                        {
                            _deviceCommandService.AddDeviceCommandCreator(
                                () => _deviceCommandFactory.CreateSyncTimeCommand(resDateTime,
                                    runtimeLogicalDevice.Model as IRuntimeDevice), runtimeLogicalDevice);
                        }
                    }
                });
        }


        private void InteractWithError(Exception error)
        {
            this._interactionService
                .Interact(ApplicationInteractionProviders.ErrorMessageBoxInteractionProvider,
                          dialog =>
                          {
                              dialog.ButtonType = MessageBoxButtonType.OK;
                              dialog.Title = LocalizationResources.Instance.ErrorHeader;
                              dialog.Message =
                                  string.Format(LocalizationResources.Instance.UnexpectedErrorMessagePattern,
                                                error.Message);
                          });
        }

        private IInteractionToken InteractWithBusy()
        {
            return this._interactionService
                       .Interact(ApplicationInteractionProviders.BusyInteraction, dialog =>
                       {
                           dialog.Title = LocalizationResources.Instance.BusyDialogTitle;
                           dialog.Message = LocalizationResources.Instance.WaitingBusyDialogMessage;
                       });
        }

        private void SetDateTimeToPersistance(IRuntimeDeviceViewModel deviceViewModel, DateTime newDate)
        {
            //deviceViewModel.SynchronizateTime(newDate);
        }

        private void OnChangeModeToScheme()
        {
            foreach (var runtimeLogicalDevice in Devices)
            {
                if (runtimeLogicalDevice.Equals(this.CurrentDeviceViewModel))
                {
                    continue;
                }
            }
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.SCHEME_RUNTIME_VIEW_NAME, UriKind.Relative);
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        private void OnChangeModeToListWidget()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIST_WIDGET_RUNTIME_VIEW_NAME, UriKind.Relative);
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }



        #endregion [Help members]

        #region Implementation of INavigationAware
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL, CurrentDeviceViewModel);
        }

        #endregion
    }
}