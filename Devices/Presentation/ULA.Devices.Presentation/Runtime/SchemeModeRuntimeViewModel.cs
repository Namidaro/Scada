using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Devices.Presentation.Interfaces;
using ULA.Devices.Presentation.View;
using ULA.Interceptors;
using ULA.Localization;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.ViewModels;
using ULA.Presentation.ViewModels.UserControl;
using ULA.Presentation.Views;
using ULA.Presentation.Views.Interactions;
using ULA.Presentation.Views.UserControl;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;
using MessageBoxResult = ULA.Presentation.Infrastructure.ViewModels.MessageBoxResult;

namespace ULA.Devices.Presentation.Runtime
{
    /// <summary>
    ///      Represents a Scheme viewModel in Runtie mode
    ///     Представляет вью-модель схемы устройства в режиме реального времемни
    /// </summary>
    public class SchemeModeRuntimeViewModel : DisposableBindableBase, ISchemeModeRuntimeViewModel, INavigationAware
    {
        #region [PrivateField]

        private readonly IInteractionService _interactionService;
        private ObservableCollection<bool> _starter1ToResistorsLink;
        private ObservableCollection<bool> _starter2ToResistorsLink;
        private ObservableCollection<bool> _starter3ToResistorsLink;
        private ITagNamedObjectCollection<IDeviceDataTableRow> _deviceData;
        private ICommand _backToListWidgetCommand;
        private ICommand _navigateToConfigurationRuno;
        private ICommand _navigateToLightingShedule;
        private ICommand _showAnalogDataCommand;
        private ICommand _acknowledgeFailCommand;
        private ObservableCollection<bool>[] _linksCollections;
        private ObservableCollection<bool> _busLinkVisbleCollection;
        private IRuntimeDeviceViewModel _currentDeviceViewModel;
        private IPersistanceService _persistanceService;
        private IRuntimeModeDevicesServices _devicesServices;
        private IRuntimeModeDriversService _driversService;
        private readonly IDataLoadingService _dataLoadingService;
        private readonly IGlobalDefectAcknowledgingService _globalDefectAcknowledgingService;
        private readonly ILogInteractionViewModel _logInteractionViewModel;
        private readonly IOutgoingLinesViewModelFactory _outgoingLinesViewModelFactory;
        private readonly IAnalogMeterViewModelFactory _analogMeterViewModelFactory;
        private readonly IUnityContainer _container;
        private ICommand _getConnectionErrors;
        private ICommand _startStoregEnergyCommand;
        private ICommand _stopStoregEnergyCommand;
        private ICommand _navigateToInternalDeviceJournal;

        private string _meterType;
        private string _deviceVersionString;

        private List<string> _fiderDescriptions = new List<string>();
        private List<string> _starterDescriptions = new List<string>();

        private Dictionary<string, object> _navigationContext = new Dictionary<string, object>();

        #endregion [PrivateField]

        #region [Properties]

        /// <summary>
        ///     Описание пускателей
        /// </summary>
        public List<string> StarterDescriptions
        {
            get { return this._starterDescriptions; }
            set
            {
                if (this._starterDescriptions.Equals(value)) return;

                this._starterDescriptions = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Описание фидеров
        /// </summary>
        public List<string> FiderDescriptions
        {
            get { return this._fiderDescriptions; }
            set
            {
                if (this._fiderDescriptions.Equals(value)) return;

                this._fiderDescriptions = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Показывает видимость шин от каждого стартера
        /// </summary>
        public ObservableCollection<bool> BusLinkVisibleCollection
        {
            get { return this._busLinkVisbleCollection; }
            set
            {
                if (this._busLinkVisbleCollection != null && this._busLinkVisbleCollection.Equals(value)) return;
                this._busLinkVisbleCollection = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        ///     Represents deviceViewModel data, which gets from DAL
        ///     Представляет таблицу данных устройства
        /// </summary>
        public ITagNamedObjectCollection<IDeviceDataTableRow> DeviceDate
        {
            get { return this._deviceData; }
            set
            {
                if (this._deviceData != null && this._deviceData.Equals(value)) return;
                this._deviceData = value;
                RaisePropertyChanged();
            }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Create a instance of <see cref="SchemeModeRuntimeViewModel"/>
        /// </summary>
        public SchemeModeRuntimeViewModel(IInteractionService interactionService,
            IPersistanceService persistanceService, IRuntimeModeDevicesServices devicesServices,
            IRuntimeModeDriversService driversService, IDeviceCommandQueueViewModel deviceCommandQueueViewModel,
            IDataLoadingService dataLoadingService, IGlobalDefectAcknowledgingService globalDefectAcknowledgingService,
            ILogInteractionViewModel logInteractionViewModel,
            IOutgoingLinesViewModelFactory outgoingLinesViewModelFactory, IAnalogMeterViewModelFactory analogMeterViewModelFactory,IUnityContainer container)
        {
            Guard.AgainstNullReference(interactionService, "interactionService");
            Guard.AgainstNullReference(persistanceService, "persistanceService");
            Guard.AgainstNullReference(devicesServices, "devicesServices");
            Guard.AgainstNullReference(driversService, "driversService");
            this._interactionService = interactionService;
            this._persistanceService = persistanceService;
            this._devicesServices = devicesServices;
            this._driversService = driversService;
            _dataLoadingService = dataLoadingService;
            _globalDefectAcknowledgingService = globalDefectAcknowledgingService;
            _logInteractionViewModel = logInteractionViewModel;
            _outgoingLinesViewModelFactory = outgoingLinesViewModelFactory;
            _analogMeterViewModelFactory = analogMeterViewModelFactory;
            _container = container;
            DeviceCommandQueueViewModel = deviceCommandQueueViewModel;

            this.BusLinkVisibleCollection = new ObservableCollection<bool>(new List<bool> { false, false, false });

            this.Starter1ToResistorsLink = new ObservableCollection<bool>(new List<bool>(8))
            {
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false,
                false
            };
            this.Starter2ToResistorsLink =
                new ObservableCollection<bool>(new List<bool>(8)
                {
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false
                });
            this.Starter3ToResistorsLink =
                new ObservableCollection<bool>(new List<bool>(8)
                {
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false
                });
            this._linksCollections = new ObservableCollection<bool>[3];
            this._linksCollections[0] = Starter1ToResistorsLink;
            this._linksCollections[1] = Starter2ToResistorsLink;
            this._linksCollections[2] = Starter3ToResistorsLink;

            OpenPicon2ModuleInformationCommand = new DelegateCommand(OnOpenPicon2ModuleInformationExecute);
        }

        private void OnOpenPicon2ModuleInformationExecute()
        {
            Window picon2ModuleInfoView = _container.Resolve<object>(ApplicationGlobalNames.PICON2_MODULE_INFO_VIEW_NAME) as Window;
            picon2ModuleInfoView.Show();
        }

        #endregion [Ctor's]

        #region [ISchemeModeConfigurationViewModel]

        /// <summary>
        ///    квитирование неисправности
        /// </summary>
        public ICommand AcknowledgeFailCommand => this._acknowledgeFailCommand ??
                                                  (_acknowledgeFailCommand =
                                                      new DelegateCommand(OnAcknowledgeFailCommand));


        /// <summary>
        ///     открывает окно с данными аналогов
        /// </summary>
        public ICommand ShowAnalogDataCommand => this._showAnalogDataCommand ??
                                                 (_showAnalogDataCommand =
                                                     new DelegateCommand(OnShowAnalogDataCommand));

        public bool IsPicon2
        {
            get { return _isPicon2; }
            set
            {
                _isPicon2 = value;
                RaisePropertyChanged();
            }
        }

        public ICommand OpenPicon2ModuleInformationCommand { get; }

        /// <summary>
        ///     Навигация на журнал устройства(считываемый из самого устройства)
        /// </summary>
        public ICommand NavigateToInternalDeviceJournal => this._navigateToInternalDeviceJournal ??
                                                           (this._navigateToInternalDeviceJournal =
                                                               new DelegateCommand(OnNavigateToInternalDeviceJournal));






        /// <summary>
        ///     Показывает существет ли связь между первым(левым на схеме) пускателем и предохранителями(0-7)
        /// </summary>
        public ObservableCollection<bool> Starter1ToResistorsLink
        {
            get { return this._starter1ToResistorsLink; }
            set
            {
                if (this._starter1ToResistorsLink != null && this._starter1ToResistorsLink.Equals(value)) return;
                this._starter1ToResistorsLink = value;
                OnPropertyChanged("Starter1ToResistorsLink");
            }
        }

        /// <summary>
        ///     Показывает существет ли связь между вторым(средним на схеме) пускателем и предохранителями(0-7)
        /// </summary>
        public ObservableCollection<bool> Starter2ToResistorsLink
        {
            get { return this._starter2ToResistorsLink; }
            set
            {
                if (this._starter2ToResistorsLink != null && this._starter2ToResistorsLink.Equals(value)) return;
                this._starter2ToResistorsLink = value;
                OnPropertyChanged("Starter2ToResistorsLink");
            }
        }

        /// <summary>
        ///     Показывает существет ли связь между третьим(правым на схеме) пускателем и предохранителями(0-7)
        /// </summary>
        public ObservableCollection<bool> Starter3ToResistorsLink
        {
            get { return this._starter3ToResistorsLink; }
            set
            {
                if (this._starter3ToResistorsLink != null && this._starter3ToResistorsLink.Equals(value)) return;
                this._starter3ToResistorsLink = value;
                OnPropertyChanged("Starter3ToResistorsLink");
            }
        }


        /// <summary>
        ///     Gets Device name
        ///     строка устройства, полученная из устройства
        /// </summary>
        public string DeviceVersionString
        {
            get { return _deviceVersionString; }
            set
            {
                if (this._deviceVersionString != null && this._deviceVersionString.Equals(value)) return;
                this._deviceVersionString = value;
                OnPropertyChanged(nameof(DeviceVersionString));
            }
        }


        /// <summary>
        ///     Command represents action of changing mode to ListWidgetModeMode
        ///     Команада возвращение к листу устройств
        /// </summary>
        public ICommand BackToListWidgetCommand => this._backToListWidgetCommand ?? (this._backToListWidgetCommand =
                                                       new DelegateCommand(OnBackToToListWidget));

        /// <summary>
        ///     Команды навигации на вьюху конфигуратора руно
        /// </summary>
        public ICommand NavigateToConfigurationCommand => this._navigateToConfigurationRuno ??
                                                          (this._navigateToConfigurationRuno =
                                                              new DelegateCommand(OnNavigateToConfiguration));

        /// <summary>
        ///     Команда навигации на вьюху графика освещения
        /// </summary>
        public ICommand NavigateToLightingShedulerCommand => this._navigateToLightingShedule ??
                                                             (this._navigateToLightingShedule =
                                                                 new DelegateCommand(OnNavigateToLightingShedule));



        /// <summary>
        ///    Открыть окно ошибок связи
        /// </summary>
        public ICommand GetConnectionErrors => this._getConnectionErrors ?? (this._getConnectionErrors =
                                                   new DelegateCommand(OnGetConnectionErrors));





        #endregion [ISchemeModeConfigurationViewModel]

        #region [Start/Stop light command]





        /// <summary>
        ///     Включить энергосбережение
        /// </summary>
        public ICommand RunStoregEnergyCommand => this._startStoregEnergyCommand ?? (this._startStoregEnergyCommand =
                                                      new DelegateCommand(OnRunStoregEnergyCommand));

        private void OnRunStoregEnergyCommand()
        {
            var result = LightingCommandResult.UNDEFINED;
            this._interactionService.Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                viewModel =>
                {
                    viewModel.Title = "Вы уверены?";
                    viewModel.Message = "Подтвердить включение энергосбережения?";
                },
                viewModel =>
                {
                    if (viewModel.Result == MessageBoxResult.YES)
                    {
                        //this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) +
                        //                   "  Диспетчер.   Включить энергосбережение.");
                        //    result = await this._currentDeviceViewModel.StartLightinByLightingMode(LightingModeEnum.ENERGY_SAVING);

                        if (result.Equals(LightingCommandResult.NO_EXIST))
                        {
                            this._interactionService.Interact(
                                ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                viewModelEx =>
                                {
                                    viewModelEx.Title = LocalizationResources.Instance.RepairDefandTitle;
                                    viewModelEx.Message =
                                        Localization.LocalizationResources.Instance.StarterNoExistMessage;
                                });
                            return;
                        }
                        if (result.Equals(LightingCommandResult.REPAIR))
                        {
                            this._interactionService.Interact(
                                ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                                viewModelRep =>
                                {
                                    viewModelRep.Title = LocalizationResources.Instance.RepairDefandTitle;
                                    viewModelRep.Message = Localization.LocalizationResources.Instance.RepairDefand;
                                });
                            return;
                        }
                    }
                });
        }



        /// <summary>
        /// 
        /// </summary>
        public string MeterType
        {
            get { return _meterType; }
            set
            {
                _meterType = value;
                OnPropertyChanged(nameof(MeterType));
            }
        }


        #endregion

        #region [INavigationAware]

        /// <summary>
        ///     Called to determine if this instance can handle the navigation request
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        /// <returns>true if this instance accepts the navigation request; otherwise, false </returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        ///     Called when the implementer is being navigated away from.
        ///     Вызывается при переходе с вьюхи
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

            this.DeviceDate = null;
            navigationContext.Parameters.Add(ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL, _currentDeviceViewModel);
            _navigationContext.ForEach((pair =>
            {
                navigationContext.Parameters.Add(pair.Key, pair.Value);
            }));
            (_currentDeviceViewModel.Model as IRuntimeDevice).DeviceInitialized -= OnDeviceInitialized;
            (_currentDeviceViewModel.Model as IRuntimeDevice).SetUpdatingMode(false);
            OutgoingLinesViewModel?.Dispose();
            OutgoingLinesViewModel = null;
            StarterDescriptions = new List<string>();
            FiderDescriptions = new List<string>();
            this._currentDeviceViewModel = null;
            _analogMeterViewModel?.Dispose();
            _analogMeterViewModel = null;
        }

        /// <summary>
        ///     Called when the implementer has been navigated to.
        ///     Вызывается при переходе на вьюхи
        /// </summary>
        /// <param name="navigationContext">The navigation context</param>
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            var busyToken = InteractWithBusy();
            DefectStateViewModel = null;
            StarterViewModels = null;
            AnalogDataViewModel = null;
            DeviceName = null;
            OutgoingLinesViewModel = null;
            CustomItemsViewModel = null;
            _navigationContext.Clear();
            if (navigationContext.Parameters[ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL] != null)
            {
                CurrentDeviceViewModel =
                (navigationContext.Parameters[ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL] as
                    IRuntimeDeviceViewModel);

                DeviceCommandQueueViewModel.SetContext(_currentDeviceViewModel);
                DefectStateViewModel = _currentDeviceViewModel.DefectStateViewModel;
                StarterViewModels = _currentDeviceViewModel.StarterViewModels;
                AnalogDataViewModel = _currentDeviceViewModel.AnalogDataViewModel;
                DeviceName = _currentDeviceViewModel.DeviceName;
                OutgoingLinesViewModel = _currentDeviceViewModel.OutgoingLinesViewModel;



                if ((_currentDeviceViewModel.Model as IRuntimeDevice).DeviceMomento.State.DeviceType.Contains("PICON2"))
                {
                    IsPicon2 = true;
                    (_currentDeviceViewModel.Model as IRuntimeDevice).UpdateSignal();
                }
                else
                {
                    IsPicon2 = false;
                    _dataLoadingService.UpdateSignalLevel(_currentDeviceViewModel.Model as IRuntimeDevice);
                }
                if ((_currentDeviceViewModel.Model as IRuntimeDevice).DeviceSignature == null)
                {

                    if ((_currentDeviceViewModel.Model as IRuntimeDevice).DeviceMomento.State.DeviceType.Contains("PICON2"))
                    {
                        (_currentDeviceViewModel.Model as IRuntimeDevice).UpdateSignature();
                    }
                    else
                    {
                        _dataLoadingService.UpdateDeviceSignature(_currentDeviceViewModel.Model as IRuntimeDevice);
                    }
                }
                if (_currentDeviceViewModel.CustomItemsViewModel != null)
                {
                    CustomItemsViewModel = _currentDeviceViewModel.CustomItemsViewModel;
                }
            }
            MeterType = (_currentDeviceViewModel.Model as IRuntimeDevice).AnalogMeter?.AnalogMeterType;
            (_currentDeviceViewModel.Model as IRuntimeDevice).SetUpdatingMode(true);
            (_currentDeviceViewModel.Model as IRuntimeDevice).DeviceInitialized += OnDeviceInitialized;
            OnDeviceInitialized();
            busyToken.Dispose();
        }

        #endregion [INavigationAware]

        #region [HelpMembers]

        private void OnDeviceInitialized()
        {
            UpdateBusLinkVisibility();
            OutgoingLinesViewModel = _currentDeviceViewModel.OutgoingLinesViewModel;
            if (_currentDeviceViewModel.CustomItemsViewModel != null)
            {
                CustomItemsViewModel = _currentDeviceViewModel.CustomItemsViewModel;
            }
        }





        private async void OnNavigateToInternalDeviceJournal()
        {
            //TODO" check picon2 internal device journal
            if (IsPicon2)
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModel2 =>
                    {
                        viewModel2.Title = "Функция не реализована";
                        viewModel2.Message = "Для устройства PICON2 функция просмотра журнал устройства не реализована";
                    }, viewModel2 => { });
                return;
            }

            //if (CheckRepair())
            //{
            //    this._interactionService.Interact(
            //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = LocalizationResources.Instance.RepairDefandTitle;
            //            viewModel.Message = Localization.LocalizationResources.Instance.RepairDefand;
            //        });
            //    return;
            //}

            if (_currentDeviceViewModel.StateWidget == WidgetState.NoConnection)
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModel2 =>
                    {
                        viewModel2.Title = "Нет связи";
                        viewModel2.Message = "Нет связи с устройством. Чтение журнала невозможно.";
                    }, viewModel2 => { });
                return;
            }


            _logInteractionViewModel.OpenDeviceLog(_currentDeviceViewModel);

        }


        private void UpdateBusLinkVisibility()
        {
            if (StarterViewModels == null) return;
            for (int i = 0; i < BusLinkVisibleCollection.Count; i++)
            {
                BusLinkVisibleCollection[i] = false;
            }
            if (StarterViewModels.Count < 4)
            {
                for (int i = 0; i < StarterViewModels.Count; i++)
                {
                    if (StarterViewModels[i].Model != null)
                    {
                        this.BusLinkVisibleCollection[i] = true;
                    }
                }
            }
            else
            {
                throw new IndexOutOfRangeException("Count of starters must be between 0 to 3.");
            }
        }



        private void OnBackToToListWidget()
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



        private void OnNavigateToConfiguration()
        {
            //if (CheckRepair())
            //{
            //    this._interactionService.Interact(
            //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = LocalizationResources.Instance.RepairDefandTitle;
            //            viewModel.Message = Localization.LocalizationResources.Instance.RepairDefand;
            //        });
            //    return;
            //}
            InvokeActionAfterCheckingPassword((() =>
            {
                var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];

                var uri = new Uri(_currentDeviceViewModel.Model.DeviceMomento.State.DeviceType + ApplicationGlobalNames.CONFIGURATION_VIEW_NAME, UriKind.Relative);
                runtimeRegion.RequestNavigate(uri, result =>
                {
                    if (result.Result == false)
                    {
                        throw new Exception(result.Error.Message);
                    }
                });

            }));


        }

        private void InvokeActionAfterCheckingPassword(Action actionToInvoke)
        {
            this._interactionService.Interact(ApplicationInteractionProviders.PasswordInteractionProvider,
                viewModel => { viewModel.Title = "Введите пароль"; },
                viewModel =>
                {
                    var validPassword = GetPasswordFromFile();
                    var inputPassword = new byte[] { 0 };
                    if (viewModel.Password != null)
                    {
                        inputPassword = MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(viewModel.Password));
                    }
                    if (viewModel.Result == MessageBoxResult.OK && viewModel.Password != null &&
                        !inputPassword.Where((t, i) => t != validPassword[i]).Any())
                    {
                        actionToInvoke?.Invoke();
                    }
                    else if (viewModel.Result == MessageBoxResult.OK)
                    {
                        this._interactionService.Interact(
                            ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                            viewModel2 =>
                            {
                                viewModel2.Title = "Нет доступа";
                                viewModel2.Message = "Пароль введен не верно";
                            }, viewModel2 => { });
                    }
                });

        }
        private bool CheckRepair()
        {
            return (_currentDeviceViewModel.Model as IRuntimeDevice).StartersOnDevice.Any((starter =>
                {
                    return starter.IsInRepairState != null && starter.IsInRepairState.Value;
                }));
        }

        private void OnNavigateToLightingShedule()
        {
            //if (CheckRepair())
            //{
            //    this._interactionService.Interact(
            //        ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
            //        viewModel =>
            //        {
            //            viewModel.Title = LocalizationResources.Instance.RepairDefandTitle;
            //            viewModel.Message = Localization.LocalizationResources.Instance.RepairDefand;
            //        });
            //    return;
            //}

            InvokeActionAfterCheckingPassword((() =>
            {
                if (_currentDeviceViewModel.Model.DeviceMomento.State.DeviceType.Contains("PICON2"))
                {

                    var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                    var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
                    var uri = new Uri(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME,
                        UriKind.Relative);

                    _navigationContext.Add("title", "График 1(освещение)");
                    runtimeRegion.RequestNavigate(uri, result =>
                    {
                        if (result.Result == false)
                        {
                            throw new Exception(result.Error.Message);
                        }
                    });


                }

                else
                {
                    var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
                    var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
                    var uri = new Uri(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);

                    //    _navigationContext.Add("address", _currentDeviceViewModel.SheduleBaseAddress);
                    _navigationContext.Add("title", "График 1(освещение)");
                    runtimeRegion.RequestNavigate(uri, result =>
                    {
                        if (result.Result == false)
                        {
                            throw new Exception(result.Error.Message);
                        }
                    });
                }

            }));

        }

        private byte[] GetPasswordFromFile()
        {
            var filePath = "password.txt";
            if (!File.Exists(filePath))
            {
                using (var newFile = File.Create(filePath))
                {
                    byte[] info = Encoding.Unicode.GetBytes("bemn");
                    var result = MD5.Create().ComputeHash(info);
                    newFile.Write(result, 0, result.Length);
                }
            }
            var file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return file.ReadAllBytes();
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


        private IInteractionToken InteractWithBusyWait()
        {
            _cancelFlag = false;
            return this._interactionService
                .Interact(ApplicationInteractionProviders.BusyInteraction, dialog =>
                {
                    dialog.Title = LocalizationResources.Instance.BusyDialogTitle;
                    dialog.Message = LocalizationResources.Instance.WaitingBusyDialogMessage;
                    dialog.ButtonVisible = true;
                    dialog.OnInteractionComplete += dialog_OnInteractionComplete;
                });
        }

        private bool _cancelFlag = false;
        private IDefectStateViewModel _defectStateViewModel;
        private ObservableCollection<IStarterViewModel> _starterViewModels;
        private IDeviceCommandQueueViewModel _deviceCommandQueueViewModel;
        private IAnalogDataViewModel _analogDataViewModel;
        private string _deviceName;
        private IOutgoingLinesViewModel _outgoingLinesViewModel;
        private ICustomItemsViewModel _customItemsViewModel;
        private IAnalogMeterViewModel _analogMeterViewModel;
        private bool _isPicon2;

        void dialog_OnInteractionComplete(object sender, EventArgs e)
        {
            _cancelFlag = true;
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


        private void OnGetConnectionErrors()
        {

            var connectionErrorsView = new ConnectionErrorsView
            {
                DataContext = (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.ConnectionExceptionList,
                Margin = new Thickness(5, 5, 5, 5)
            };
            var connectionErrorsWindow = new Window
            {
                Owner = Application.Current.MainWindow,
                Content = connectionErrorsView,
                Title = String.Format("Ошибки связи устройства {0}", this.DeviceName),
                ResizeMode = ResizeMode.CanResize,
                Height = 250,
                Width = 600
            };
            connectionErrorsWindow.Show();
        }




        private void SetDateTimeToPersistance(DateTime newDate)
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                //   _currentDeviceViewModel.SynchronizateTime(newDate);
            }
            catch (Exception ex)
            {
                busyToken.Dispose();
                this.InteractWithError(ex);
            }
            busyToken.Dispose();
        }





        private void OnAcknowledgeFailCommand()
        {

            _globalDefectAcknowledgingService.GetDefectAcknowledgingService(
                _currentDeviceViewModel.Model as IRuntimeDevice).AcknowledgeFail();
            try
            {
                (ServiceLocator.Current.GetInstance(typeof(ISoundNotificationsService)) as ISoundNotificationsService).ReleaseNotificationSource(_currentDeviceViewModel);
            }
            catch { }
        }


        private void OnShowAnalogDataCommand()
        {
            if ((_currentDeviceViewModel.Model as IRuntimeDevice).AnalogMeter != null)
            {
                if ((_analogMeterViewModel != null) && (!_analogMeterViewModel.IsViewShowing))
                {
                    _analogMeterViewModel.SetDevice(_currentDeviceViewModel);
                    _analogMeterViewModel.ShowView();
                    return;
                }
                if ((_analogMeterViewModel != null) && (_analogMeterViewModel.IsViewShowing))
                {
                    return;
                }
                _analogMeterViewModel =
                    _analogMeterViewModelFactory.CreateAnalogMeterViewModel(_currentDeviceViewModel,
                        (_currentDeviceViewModel.Model as IRuntimeDevice).AnalogMeter);
                _analogMeterViewModel.ShowView();
            }
        }

        #endregion [HelpMembers]


        public IDefectStateViewModel DefectStateViewModel
        {
            get { return _defectStateViewModel; }
            set
            {
                _defectStateViewModel = value;
                RaisePropertyChanged();
            }
        }


        public ObservableCollection<IStarterViewModel> StarterViewModels
        {
            get { return _starterViewModels; }
            set
            {
                _starterViewModels = value;
                UpdateBusLinkVisibility();
                RaisePropertyChanged();
            }
        }

        public IDeviceCommandQueueViewModel DeviceCommandQueueViewModel
        {
            get { return _deviceCommandQueueViewModel; }
            set
            {
                _deviceCommandQueueViewModel = value;
                RaisePropertyChanged();
            }
        }

        public IAnalogDataViewModel AnalogDataViewModel
        {
            get { return _analogDataViewModel; }
            set
            {
                _analogDataViewModel = value;
                RaisePropertyChanged();
            }
        }


        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                RaisePropertyChanged();
            }
        }

        public IRuntimeDeviceViewModel CurrentDeviceViewModel
        {
            get { return _currentDeviceViewModel; }
            set
            {
                _currentDeviceViewModel = value;
                RaisePropertyChanged();
            }
        }

        public IOutgoingLinesViewModel OutgoingLinesViewModel
        {
            get { return _outgoingLinesViewModel; }
            set
            {
                _outgoingLinesViewModel = value;
                RaisePropertyChanged();
            }
        }


        public ICustomItemsViewModel CustomItemsViewModel
        {
            get { return _customItemsViewModel; }
            set
            {
                _customItemsViewModel = value;
                RaisePropertyChanged();
            }
        }
    }
}