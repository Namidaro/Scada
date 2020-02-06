using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Practices.ServiceLocation;
using NLog;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Presentation;
using ULA.Business.Image;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Interceptors.Application;
using ULA.Localization;
using ULA.Log;
using ULA.Log.LoggerService;
using ULA.Presentation.DTOs;
using ULA.Presentation.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Log;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.Views.Interactions;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Validation;
using Application = System.Windows.Application;
using MessageBoxResult = ULA.Presentation.Infrastructure.ViewModels.MessageBoxResult;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Represents an application configuration mode view model functionality
    ///     Представляет вью-модель для режима конфигурации приложения
    /// </summary>
    public class ApplicationConfigurationModeViewModel : ViewModelBase, IApplicationConfigurationModeViewModel
    {
        #region [Private fields]

        private readonly IConfigurationModeDevicesService _configService;
        private readonly IInteractionService _interactionService;
        private readonly IApplicationSettingsService _settings;
        private readonly IConfigDeviceViewModelFactory _configDeviceViewModelFactory;
        private readonly IApplicationLogViewModel _applicationLogViewModel;
        private ICommand _createNewDeviceCommand;
        private IConfigDeviceViewModel _currentDevice;
        private ICommand _deleteCurrentDeviceCommand;
        private ObservableCollection<IConfigDeviceViewModel> _devices;
        private ICommand _editCurrentDeviceCommand;
        private ICommand _navigateToAboutCommand;
        private ICommand _navigateToJournalOfSystemCommand;
        private ICommand _navigateToExtraSettingsCommand;
        private ICommand _navigateToEditPasswordCommand;
        private ICommand _navigateToHelpCommand;
        private ICommand _addImageCommand;
        private Logger _logger;
        private IBackgroundPicture _backgroundPicture;
        private ICommand _deleteImageCommand;
        private ICommand _changeImageEditingModeToOff;
        private ICommand _changeImageEditingModeToOn;

        //это чтобы достать Port и Address. Переделать!
        private IPersistanceService _persistanceService;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets or sets an instance of <see cref="IConfigLogicalDevice" /> that represents current virtual deviceViewModel model
        ///     Текущее(в данный момент выбранное) устройство
        /// </summary>
        public IConfigDeviceViewModel CurrentDevice
        {
            get { return this._currentDevice; }
            set
            {
                if (this._currentDevice != null && this._currentDevice.Equals(value)) return;
                this._currentDevice = value;
                OnPropertyChanged("CurrentDevice");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        ///     Gets or sets a collection of devices
        ///     Коллекция устройств
        /// </summary>
        public ObservableCollection<IConfigDeviceViewModel> Devices
        {
            get { return this._devices; }
            set
            {
                this.OnPropertyChanging("Devices");
                this._devices = value;
                this.OnPropertyChanged("Devices");
            }
        }
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

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ApplicationConfigurationModeViewModel" />
        /// </summary>
        /// <param name="interactionService">
        ///     An instance of <see cref="IInteractionService" /> to use
        /// </param>
        /// <param name="configService">
        ///     An instance of <see cref="IConfigurationModeDevicesService" /> to use
        /// </param>
        /// <param name="persistanceService"></param>
        /// <param name="settings">Сервис с настройками приложения</param>
        /// <param name="configDeviceViewModelFactory"></param>
        public ApplicationConfigurationModeViewModel(IInteractionService interactionService,
            IConfigurationModeDevicesService configService,
            IPersistanceService persistanceService,
            IApplicationSettingsService settings, IConfigDeviceViewModelFactory configDeviceViewModelFactory,
            IApplicationLogViewModel applicationLogViewModel)
        {
            Guard.AgainstNullReference(interactionService, "interactionService");
            Guard.AgainstNullReference(configService, "configService");
            Guard.AgainstNullReference(persistanceService, "persistanceService");
            Guard.AgainstNullReference(settings, "settings");

            this._interactionService = interactionService;
            this._configService = configService;
            this._persistanceService = persistanceService;
            this._settings = settings;
            _configDeviceViewModelFactory = configDeviceViewModelFactory;
            _applicationLogViewModel = applicationLogViewModel;

            this._logger = LogManager.GetLogger("Конфигурация");
        }

        #endregion [Ctor's]

        #region [Public members]
        /// <summary>
        ///     Изменяет расположение устройства и решает коллизии перемещения устройств
        /// </summary>
        /// <param name="device">перемещаемое устройство</param>
        /// <param name="newPosition">новая позиция устройства</param>
        public void ChangeDevicePositionNumber(IConfigDeviceViewModel device, long newPosition)
        {
            var repeatPositionDevice = this.Devices.FirstOrDefault(dev => dev.DeviceNumber.Equals((int)newPosition));
            if (repeatPositionDevice != null)
            {
                repeatPositionDevice.DeviceNumber = device.DeviceNumber;
            }
            if(device.DeviceNumber==(int)newPosition)return;
            device.DeviceNumber = (int)newPosition;
            SaveNewWidgetPosition();
        }

        #endregion [Public members]

        #region [IApplicationConfigurationModeViewModel members]
        /// <summary>
        ///  Навигация на окно помощи
        /// </summary>
        public ICommand NavigateToHelpCommand => this._navigateToHelpCommand ??
                                                 (this._navigateToHelpCommand = new DelegateCommand(OnNavigateToHelpCommand));

     

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to journal of system view command
        ///     Команда навигации на журнал системы
        /// </summary>
        public ICommand NavigateToJournalOfSystemCommand => this._navigateToJournalOfSystemCommand ??
                                                            (this._navigateToJournalOfSystemCommand = new DelegateCommand(this.OnNavigateToJournalOfSystem));

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents create new virtual deviceViewModel command
        ///     Команда навигации на создание устройства
        /// </summary>
        public ICommand CreateNewVirtualDeviceCommand => this._createNewDeviceCommand ??
                                                         (this._createNewDeviceCommand = new DelegateCommand(this.OnCreateNewDeviceCommand));

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents edit current virtual deviceViewModel command
        ///     Команда навигации на редактирование устройства
        /// </summary>
        public ICommand EditCurrentDeviceCommand => this._editCurrentDeviceCommand ??
                                                    (this._editCurrentDeviceCommand =
                                                        new DelegateCommand<IConfigDeviceViewModel>(this.OnEditDeviceCommand));

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents delete virtual deviceViewModel command
        ///     Команда удаление текущего устройства
        /// </summary>
        public ICommand DeleteCurrentDeviceCommand => this._deleteCurrentDeviceCommand ??
                                                      (this._deleteCurrentDeviceCommand =
                                                          new DelegateCommand<IConfigDeviceViewModel>(this.OnDeleteCurrentDevice));

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to about view command
        ///     Команда навигации на овно "О программе"
        /// </summary>
        public ICommand NavigateToAboutCommand => this._navigateToAboutCommand ??
                                                  (this._navigateToAboutCommand = new DelegateCommand(this.OnNavigateToAbout));



        /// <summary>
        ///     Возвращает команду, которая описывает навигацию на вьюшку установки полного интервала обновлений
        ///     Навигация на окно установки периодаобновлений
        /// </summary>
        public ICommand NavigateToExtraSettingsCommand => _navigateToExtraSettingsCommand ??
                                                              (this._navigateToExtraSettingsCommand =
                                                                  new DelegateCommand(this.OnNavigateToExtraSettingsCommand));




        /// <summary>
        ///     Команда навигации на вьюшку изменения пароля привелигированного доступа
        /// </summary>
        public ICommand NaigateToEditPassword => this._navigateToEditPasswordCommand ??
                                                 (this._navigateToEditPasswordCommand = new DelegateCommand(OnNavigateToEditPassword));


        /// <summary>
        /// коммандан на добавление картинки
        /// </summary>
       public ICommand AddImageCommand => _addImageCommand ??
                                          (this._addImageCommand = new DelegateCommand(OnAddImage));



        /// <summary>
        /// коммандан на добавление картинки
        /// </summary>
        public ICommand DeleteImageCommand => _deleteImageCommand ??
                                           (this._deleteImageCommand = new DelegateCommand(OnDeleteImageCommand));





        /// <summary>
        /// коммандан на добавление картинки
        /// </summary>
        public ICommand ChangeImageEditingModeToOff => _changeImageEditingModeToOff ??
                                           (_changeImageEditingModeToOff = new DelegateCommand(OnChangeImageEditingModeToOff));





        /// <summary>
        /// коммандан на добавление картинки
        /// </summary>
        public ICommand ChangeImageEditingModeToOn=> _changeImageEditingModeToOn ??
                                           (_changeImageEditingModeToOn = new DelegateCommand(OnChangeImageEditingModeToOn));



     private void OnChangeImageEditingModeToOff()
     {
         if (BackgroundPicture?.BackgroundImageSource != null)
         {
             BackgroundPicture.IsEditMode = false;
             _configService.SetBackgroundPictureAsync(BackgroundPicture);
         }
     }


        private void OnChangeImageEditingModeToOn()
        {
            if (BackgroundPicture?.BackgroundImageSource != null)
            {
                BackgroundPicture.IsEditMode = true;
            }
        }

        private void OnDeleteImageCommand()
        {
           BackgroundPicture.Save(true);
            BackgroundPicture = null;
        }


        private void OnAddImage()
        {
            try
            {
                BackgroundPicture = new BackgroundPicture();
                BackgroundPicture.IsEditMode = true;
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Open Image";
                dlg.Filter = "Image File|*.bmp; *.gif; *.jpg; *.jpeg; *.png;";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(dlg.FileName))
                    {
                        BackgroundPicture.InitImageFromPath(dlg.FileName);
                    }
                }
            }
            catch
            {
            }
            OnPropertyChanged(nameof(BackgroundPicture));
        }


        /// <summary>
        ///     Initializes current application mode asynchronously
        ///     Асинхронная инициализация 
        /// </summary>
        /// <returns>
        ///     An instance of System.Threading.Tasks.Task that represents current asynchronous operation
        /// </returns>
        public async Task InitializeAsync()
        {
            var devices = await this._configService.GetAllDevicesAsync();
          
            this.Devices =
                new ObservableCollection<IConfigDeviceViewModel>(
                    devices.Select((m)=>_configDeviceViewModelFactory.CreateConfigDeviceViewModel(m)));
            if (Devices.Any((model =>model.DeviceNumber>103 )))
            {
                while (Devices.Any((model => model.DeviceNumber > 103)))
                {
                    Devices.Remove(Devices.First((model => model.DeviceNumber > 103)));
                    await this._configService.RemoveDeviceAsync(devices.First((device => device.DeviceNumber > 103)));
                }
            }
                this.CurrentDevice = this.Devices.FirstOrDefault();
            BackgroundPicture = _configService.GetBackgroundPicture();
            (new LoggerServiceBase()).LogMessage(" Переход в режим конфигурации", LogManager.GetLogger(LogMessageTypes.COMMEN_MESSAGE), LogMessageTypes.COMMEN_MESSAGE);
        }

        /// <summary>
        ///     Un-initializes current application mode asynchronously
        ///     Асинхронная деинициализация
        /// </summary>
        /// <returns>
        ///     An instance of System.Threading.Tasks.Task that represents current asynchronous operation
        /// </returns>
        public Task UninitializeAsync()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            regionManager.Regions.Remove("ModeRuntimeRegion");
            regionManager.Regions.Remove("ModeConfigurationRegion");
            BackgroundPicture?.Save(false);
            OnChangeImageEditingModeToOff();
            return Task<object>.Factory.FromResult(null);
        }

        /// <summary>
        ///     Invokes when initialization process is over
        ///     Вызывается, когда процесс инициализации закончен
        /// </summary>
        public void OnInitialized()
        {
            if (this.Devices.Count != 0) return;
            this._interactionService
                .Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                          onInitialized: dialog =>
                              {
                                  dialog.Title = LocalizationResources.Instance.NoDeviceDetected;
                                  dialog.Message = LocalizationResources.Instance.WantToCreateNewDevice;
                              },
                          onUninitialized: dialog =>
                              {
                                  if (dialog.Result == MessageBoxResult.YES)
                                      this.CreateNewDeviceWithInteraction();
                              });
        }

        #endregion [IApplicationConfigurationModeViewModel members]

        #region [Help members]

        private void OnNavigateToHelpCommand()
        {
            var path = Directory.GetCurrentDirectory();
            Help.ShowHelp(null, "file://" + path + "\\Files\\SUNO.chm");
        }

      
        private async void SaveNewWidgetPosition()
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                for (int i = 0; i < this.Devices.Count; i++)
                {
                    if (this.Devices[i].Model.DeviceNumber!=Devices[i].DeviceNumber)
                    {
                        await this._configService.UpdateDevicePositionAsync(this.Devices[i].Model as IConfigLogicalDevice, Devices[i].DeviceNumber);
                    }
                }
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private bool OnCanEditCurrentDevice(IConfigDeviceViewModel currentDevice)
        {
            return currentDevice != null;
        }

        private void OnEditDeviceCommand(IConfigDeviceViewModel currentDevice)
        {
            if (!OnCanEditCurrentDevice(this.CurrentDevice))
            {
                this._interactionService
                    .Interact(ApplicationInteractionProviders.WarningMessageBoxInteractionProvider,
                        viewModel =>
                        {
                            viewModel.ButtonType = MessageBoxButtonType.OK;
                            viewModel.Title = "Команда не может быть выполнена";
                            viewModel.Message = "Устройство для редактирования не выбрано";
                        },
                        viewModel => { });
                return;
            }
           this._interactionService
                 .Interact(ApplicationInteractionProviders.CreateNewDeviceProvider,
                   viewModel =>
                   {
                       try
                       {
                           var editDeviceModel = (this.CurrentDevice.Model as IConfigLogicalDevice).ToDeviceModel();
                           var state = this._persistanceService.GetDriverPersistableContextAsync(
                                   Guid.Parse((currentDevice.Model as IConfigLogicalDevice).CreateMomento().State.RelatedDriverId))
                               .Result.GetDriverMomentoAsync().Result.State;

                           editDeviceModel.TcpAddress = state.GetTcpAddress();
                           editDeviceModel.TcpPort = state.GetTcpPort();

                           viewModel.SetEditingContext(editDeviceModel, Devices.Select((model => model.DeviceName)));
                       }
                       catch (Exception e)
                       {
                           
                       }
                   
                    }
                    ,
                    viewModel =>
                    {
                        if (!viewModel.IsCanceled)
                        {
                            if (viewModel.ResultedDeviceModel.DeviceFactoryTypeName == null) return;
                            this.OnEditDevice(viewModel.ResultedDeviceModel);
                        }
                    }
                   );
        }

        private void OnCreateNewDeviceCommand()
        {
            this.CreateNewDeviceWithInteraction();
        }



        private void OnNavigateToExtraSettingsCommand()
        {
            _settings.Load();
            _interactionService.Interact(ApplicationInteractionProviders.ExtraSettingsInteractionProvider, viewModel =>
            {
                viewModel.Title = "Дополнительные настройки";
                 viewModel.AcknowledgeEnabled=_settings.AcknowledgeEnabled;
                viewModel.FullTimeout = _settings.FullTimeoutPeriod;
                viewModel.PartialTimeout = _settings.PartialTimeoutPeriod;
                viewModel.QueryTimeout = _settings.QueryTimeoutPeriod;
                viewModel.MinuteRepeatInterval = _settings.MillisecondRepeatInterval/(60*1000);
                viewModel.NumberOfLightingCommandRepeat = _settings.NumberOfLightingCommandRepeat;
            }, viewModel =>
            {
                if (viewModel.Result == MessageBoxResult.OK)
                {
                    _settings.AcknowledgeEnabled = viewModel.AcknowledgeEnabled;
                    _settings.FullTimeoutPeriod = viewModel.FullTimeout;
                    _settings.PartialTimeoutPeriod = viewModel.PartialTimeout;
                    _settings.QueryTimeoutPeriod = viewModel.QueryTimeout;
                    _settings.NumberOfLightingCommandRepeat = viewModel.NumberOfLightingCommandRepeat;
                    _settings.MillisecondRepeatInterval = viewModel.MinuteRepeatInterval*60*1000;
                    _settings.Save();
                }
            });
        }



        private void OnNavigateToEditPassword()
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
                       this._interactionService.Interact(ApplicationInteractionProviders.PasswordInteractionProvider,
                           newViewModel => { newViewModel.Title = "Введите новый пароль"; },
                           newViewModel =>
                           {
                               if (newViewModel.Result == MessageBoxResult.OK && newViewModel.Password != null)
                               {
                                   StorePassword(newViewModel.Password);
                               }
                           });
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

        private void StorePassword(string password)
        {

            var filePath = "password.txt";
            if (!File.Exists(filePath))
            {
                using (var newFile = File.Create(filePath))
                {
                    byte[] newInfo = Encoding.Unicode.GetBytes(password);
                    var newResult = MD5.Create().ComputeHash(newInfo);
                    newFile.Write(newResult, 0, newResult.Length);
                }
            }
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Write))
            {
                byte[] info = Encoding.Unicode.GetBytes(password);
                var result = MD5.Create().ComputeHash(info);
                file.Write(result, 0, result.Length);
            }
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

        private bool OnCanDeleteCurrentDevice(IConfigDeviceViewModel currentDevice)
        {
            return currentDevice != null;
        }

        private void OnDeleteCurrentDevice(IConfigDeviceViewModel currentDevice)
        {
            if (!OnCanDeleteCurrentDevice(this.CurrentDevice))
            {
                this._interactionService
                    .Interact(ApplicationInteractionProviders.WarningMessageBoxInteractionProvider,
                        viewModel =>
                        {
                            viewModel.ButtonType = MessageBoxButtonType.OK;
                            viewModel.Title = "Команда не может быть выполнена";
                            viewModel.Message = "Устройство для удаления не выбрано";
                        },
                        viewModel => { });
                return;
            }
            this._interactionService
                .Interact(ApplicationInteractionProviders.QuestionMessageBoxInteractionProvider,
                    viewModel =>
                    {
                        viewModel.Message = "Вы действительно хотите удалить устройство с именем \"" + currentDevice.DeviceName + "\"?";
                        viewModel.Title = "Вы уверены?";
                    },
                          viewModel =>
                          {
                              if (viewModel.Result == MessageBoxResult.YES)
                                  this.OnDeleteDevice(this.CurrentDevice);
                          });
        }

        private void OnNavigateToAbout()
        {
            this._interactionService
                .Interact(ApplicationInteractionProviders.AboutInteractionProvider);
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


        private void CreateNewDeviceWithInteraction()
        {
            if(Devices.Count==104)
                return;
           
            this._interactionService
                .Interact(ApplicationInteractionProviders.CreateNewDeviceProvider,
                    viewModel =>
                    {

                    },
                    viewModel =>
                    {
                        if (!viewModel.IsCanceled)
                            this.OnCreateNewDevice(viewModel.ResultedDeviceModel);
                    });
        }

        private async void OnCreateNewDevice(DeviceModel deviceModel)
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                IConfigLogicalDevice resultedDevice = await this._configService.CreateDeviceAsync(deviceModel.ToLogicalDeviceInfo());
                this.Devices.Add(_configDeviceViewModelFactory.CreateConfigDeviceViewModel(resultedDevice));
                this.SaveNewWidgetPosition();
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private async void OnEditDevice(DeviceModel deviceModel)
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                var editingDevice = await this._configService.UpdateDeviceAsync(deviceModel.ToLogicalDeviceInfo(), CurrentDevice.Model as IConfigLogicalDevice);

                this.Devices.Remove(this.CurrentDevice);
                var editingDeviceViewModel = _configDeviceViewModelFactory.CreateConfigDeviceViewModel(editingDevice);
                this.Devices.Add(editingDeviceViewModel);
                this.Devices =
                new ObservableCollection<IConfigDeviceViewModel>(
                    Devices.OrderBy(dev => dev.DeviceNumber));
                this.CurrentDevice = editingDeviceViewModel;
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                if(error is ObjectDisposedException)return;
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private async void OnDeleteDevice(IConfigDeviceViewModel deviceModel)
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                await this._configService.RemoveDeviceAsync(deviceModel.Model as IConfigLogicalDevice);
                this.Devices.Remove(deviceModel);
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
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

        #endregion [Help members]
    }
}