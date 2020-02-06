using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FluentValidation;
using FluentValidation.Results;
using Prism.Commands;
using Prism.Mvvm;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;
using ULA.AddinsHost.Business.Driver;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Enums;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Localization;
using ULA.Presentation.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.Factories;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.ViewModels.Interactions.Entities;
using ULA.Presentation.ViewModels.Interactions.Validators;

namespace ULA.Presentation.ViewModels.Interactions
{
    /// <summary>
    ///     Represents default system create new deviceViewModel view model
    ///     Представляет вью-модель создания/редактирования устройства
    /// </summary>
    public class ModifyDeviceInteractionViewModel : ValidatableInteractionDialogViewModel<IModifyDeviceViewModel>,IModifyDeviceViewModel
    {
        #region [Private fields]

        private string _deviceDescription = LocalizationResources.Instance.DeviceDescription;
        private string _deviceName = LocalizationResources.Instance.DeviceNameExample;
        private string _ipAddress = LocalizationResources.Instance.IpAddressExample;
        private string _ipPort = LocalizationResources.Instance.PortExample;
        private string _deviceType = "UNKNOWN";
        private bool _isEditing;
        private string _viewTitle;
        private string _okButtonTitle;
        private string _transformKoef;
        private readonly IInteractionService _interactionService;
        private readonly Func<ICustomFider> _customFiderCreator;
        private readonly IFiderViewModelFactory _fiderViewModelFactory;
        private readonly Func<ICustomSignal> _customSignalCreator;
        private readonly Func<ICustomIndicator> _customIndicatorCreator;
        private readonly ISignalViewModelFactory _signalViewModelFactory;
        private readonly IIndicatorViewModelFactory _indicatorViewModelFactory;
        private readonly Func<ICascadeIndicator> _cascadeIndicatorCreator;
        private readonly ICascadeViewModelFactory _cascadeViewModelFactory;
        private readonly IConfigurationModeDriversService _driversService;
        private readonly IConfigurationModeDevicesService _devicesService;

        private ObservableCollection<StarterDescriptionViewModel> _starterDescriptionsViewModels =
            new ObservableCollection<StarterDescriptionViewModel>();

        private ObservableCollection<string> _analogMetersCollection = new ObservableCollection<string>();
        private string _selectedAnalogMeter;
        private string _transformKoefA;
        private string _transformKoefB;
        private string _transformKoefC;
        private bool _isFiderOrganization;
        private bool _isSignalsEnabled;
        private bool _isIndicatorsEnabled;
        private bool _isCascadeEnabled;
        private List<string> _deviceNamesExisting;
        private List<string> _existingIps;
        private string _channelNumber1;
        private string _channelNumber2;
        private string _channelNumber3;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        /// заголовок
        /// </summary>
        public string ViewTitle
        {
            get { return _viewTitle; }
            set
            {
                _viewTitle = value;
                OnPropertyChanged(nameof(ViewTitle));
            }
        }
        /// <summary>
        /// заголовок
        /// </summary>
        public string OkButtonTitle
        {
            get { return _okButtonTitle; }
            set
            {
                _okButtonTitle = value;
                OnPropertyChanged(nameof(OkButtonTitle));
            }
        }

        /// <summary>
        ///     Gets or sets the name of currently modified deviceViewModel
        ///     Имя устройства
        /// </summary>
        public string DeviceName
        {
            get { return this._deviceName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(this._deviceName) && this._deviceName.Equals(value)) return;
                OnPropertyChanging("DeviceName");
                this._deviceName = value;
                OnPropertyChanged("DeviceName");
            }
        }

        /// <summary>
        ///     Gets or sets the description of currently modified deviceViewModel
        ///     Описание устройства
        /// </summary>
        public string Description
        {
            get { return this._deviceDescription; }
            set
            {
                if (!string.IsNullOrWhiteSpace(this._deviceDescription) && this._deviceDescription.Equals(value))
                    return;
                OnPropertyChanging("Description");
                this._deviceDescription = value;
                OnPropertyChanged("Description");
            }
        }

        /// <summary>
        ///     Gets or sets the Tcp-ip address of currently modified deviceViewModel
        ///     Tcp-Ip - адрес
        /// </summary>
        public string TcpIpAddress
        {
            get { return this._ipAddress; }
            set
            {
                if (!string.IsNullOrWhiteSpace(this._ipAddress) && this._ipAddress.Equals(value)) return;
                OnPropertyChanging("TcpIpAddress");
                this._ipAddress = value;
                OnPropertyChanged("TcpIpAddress");
            }
        }

        /// <summary>
        ///     Gets or sets the Tcp-ip port of currently modified deviceViewModel
        ///     Tcp/Ip - порт
        /// </summary>
        public string TcpIpPort
        {
            get { return this._ipPort; }
            set
            {
                if (!string.IsNullOrWhiteSpace(this._ipPort) && this._ipPort.Equals(value)) return;
                OnPropertyChanging("TcpIpPort");
                this._ipPort = value;
                OnPropertyChanged("TcpIpPort");
            }
        }


        /// <summary>
        ///     Описание стартеров для отображения
        /// </summary>
        public ObservableCollection<StarterDescriptionViewModel> StarterDescriptionsViewModels
        {
            get { return this._starterDescriptionsViewModels; }
            set
            {

                this._starterDescriptionsViewModels = value;
                OnPropertyChanged("StarterDescriptionsViewModels");
            }
        }

        /// <summary>
        ///     Описание фидеров
        /// </summary>
        public List<string> StarterDescriptions
        {
            get
            {
                List<string> toRet = new List<string>();
                foreach (StarterDescriptionViewModel sdvm in StarterDescriptionsViewModels)
                {
                    toRet.Add(sdvm.Description);
                }
                return toRet;
            }
            set
            {
                int index = 1;

                foreach (string desc in value)
                {

                    StarterDescriptionsViewModels.Add(new StarterDescriptionViewModel()
                    {
                        Description = desc,
                        IndexAndType = "Описание фидера" + index
                    });
                    index++;
                }
            }
        }




        /// <summary>
        ///     Тип устройства
        /// </summary>
        public string DeviceType
        {
            get { return _deviceType; }
            set
            {
                if (value == "PICON2")
                {
                    TcpIpPort = "502";
                }
                if (value == "PICONGS")
                {
                    TcpIpPort = "4444";
                }
                if (value == "RUNO")
                {
                    TcpIpPort = "4444";
                }
                _deviceType = value;
                OnPropertyChanged("DeviceType");
            }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ModifyDeviceInteractionViewModel" />
        /// </summary>
        public ModifyDeviceInteractionViewModel(IInteractionService interactionService,
            Func<ICustomFider> customFiderCreator, IFiderViewModelFactory fiderViewModelFactory,
            Func<ICustomSignal> customSignalCreator, Func<ICustomIndicator> customIndicatorCreatorCreator,
            ISignalViewModelFactory signalViewModelFactory, IIndicatorViewModelFactory indicatorViewModelFactory,
            Func<ICascadeIndicator> cascadeIndicatorCreator, ICascadeViewModelFactory cascadeViewModelFactory, IConfigurationModeDriversService driversService, IConfigurationModeDevicesService devicesService)
        {
            _interactionService = interactionService;
            _customFiderCreator = customFiderCreator;
            _fiderViewModelFactory = fiderViewModelFactory;
            _customSignalCreator = customSignalCreator;
            _customIndicatorCreator = customIndicatorCreatorCreator;
            _signalViewModelFactory = signalViewModelFactory;
            _indicatorViewModelFactory = indicatorViewModelFactory;
            _cascadeIndicatorCreator = cascadeIndicatorCreator;
            _cascadeViewModelFactory = cascadeViewModelFactory;
            _driversService = driversService;
            _devicesService = devicesService;
            this.ResultedDeviceModel = new DeviceModel();
            DeviceType = "UNKNOWN";
            TransformKoefA = "100";
            TransformKoefB = "100";
            TransformKoefC = "100";

            _isEditing = false;
            ViewTitle = "Создание виртуального устройства";
            OkButtonTitle = "Создать";
            AnalogMetersCollection = new ObservableCollection<string>(Enum.GetNames(typeof(AnalogMetersEnum)).ToList());
            SelectedAnalogMeter = AnalogMetersCollection.LastOrDefault();

            AddFiderCommand = new DelegateCommand(OnAddFiderExecute);
            FiderDefinitionsViewModels = new ObservableCollection<IFiderDefinitionsViewModel>();
            DeleteFiderCommand = new DelegateCommand<object>(OnDeleteFiderExecute);

            AddSignalCommand = new DelegateCommand(OnAddSignalExecute);
            SignalDefinitionsViewModels = new ObservableCollection<ISignalDefinitionsViewModel>();
            DeleteSignalCommand = new DelegateCommand<object>(OnDeleteSignalExecute);

            AddIndicatorCommand = new DelegateCommand(OnAddIndicatorExecute);
            IndicatorDefinitionsViewModels = new ObservableCollection<IIndicatorDefinitionsViewModel>();
            DeleteIndicatorCommand = new DelegateCommand<object>(OnDeleteIndicatorExecute);

            AddCascadeCommand = new DelegateCommand(OnAddCascadeExecute);
            CascadeDefinitionsViewModels = new ObservableCollection<ICascadeDefinitionsViewModel>();
            DeleteCascadeCommand = new DelegateCommand<object>(OnDeleteCascadeExecute);


            StarterDescriptionsViewModels = new ObservableCollection<StarterDescriptionViewModel>()
            {
                new StarterDescriptionViewModel() {IndexAndType = "Описание пускателя1",ChannelNumberOfStarter = 1,IsStartedEnabled = true},
                new StarterDescriptionViewModel() {IndexAndType = "Описание пускателя2",ChannelNumberOfStarter = 2,IsStartedEnabled = true},
                new StarterDescriptionViewModel() {IndexAndType = "Описание пускателя3",ChannelNumberOfStarter = 3,IsStartedEnabled = true}
            };

        }

        private void OnDeleteCascadeExecute(object obj)
        {
            CascadeDefinitionsViewModels.Remove(obj as ICascadeDefinitionsViewModel);

        }

        private void OnAddCascadeExecute()
        {
            CascadeDefinitionsViewModels.Add(_cascadeViewModelFactory.CreateCascadeDefinitionsViewModel(_cascadeIndicatorCreator.Invoke()));
        }

        private void OnDeleteIndicatorExecute(object obj)
        {
            IndicatorDefinitionsViewModels.Remove(obj as IIndicatorDefinitionsViewModel);
        }

        private void OnAddIndicatorExecute()
        {
            IndicatorDefinitionsViewModels.Add(_indicatorViewModelFactory.CreateIndicatorDefinitionsViewModel(_customIndicatorCreator.Invoke()));
        }

        private void OnDeleteSignalExecute(object obj)
        {
            SignalDefinitionsViewModels.Remove(obj as ISignalDefinitionsViewModel);
        }

        private void OnAddSignalExecute()
        {
            SignalDefinitionsViewModels.Add(_signalViewModelFactory.CreateSignalDefinitionsViewModel(_customSignalCreator.Invoke()));
        }

        private void OnDeleteFiderExecute(object fider)
        {
            FiderDefinitionsViewModels.Remove(fider as IFiderDefinitionsViewModel);
        }

        private void OnAddFiderExecute()
        {
            FiderDefinitionsViewModels.Add(_fiderViewModelFactory.CreateFiderDefinitionsViewModel(_customFiderCreator.Invoke()));
        }

        #endregion [Ctor's]

        #region [IModifyDeviceViewModel members]

        /// <summary>
        ///     Gets an instance of <see cref="DeviceModel" /> that represents a model for the deviceViewModel that should be created
        ///     результат - модель устройства
        /// </summary>
        public DeviceModel ResultedDeviceModel { get; private set; }

        /// <summary>
        /// выбранный счетчик
        /// </summary>
        public string SelectedAnalogMeter
        {
            get { return _selectedAnalogMeter; }
            set
            {
                _selectedAnalogMeter = value;
                OnPropertyChanged(nameof(SelectedAnalogMeter));

            }
        }

        /// <summary>
        /// варианты счетчиков для выбора
        /// </summary>
        public ObservableCollection<string> AnalogMetersCollection
        {
            get { return _analogMetersCollection; }
            set
            {
                _analogMetersCollection = value;
                OnPropertyChanged(nameof(AnalogMetersCollection));
            }
        }
        /// <summary>
        /// коэффициент для фазы А
        /// </summary>

        public string TransformKoefA
        {
            get { return _transformKoefA; }

            set
            {
                _transformKoefA = value;
                OnPropertyChanged(nameof(TransformKoefA));
            }
        }
        /// <summary>
        /// коэффициент для фазы B
        /// </summary>
        public string TransformKoefB
        {
            get { return _transformKoefB; }

            set
            {
                _transformKoefB = value;
                OnPropertyChanged(nameof(TransformKoefB));

            }
        }

        /// <summary>
        /// коэффициент для фазы С
        /// </summary>
        public string TransformKoefC
        {
            get { return _transformKoefC; }

            set
            {
                _transformKoefC = value;
                OnPropertyChanged(nameof(TransformKoefC));

            }
        }

        /// <summary>
        ///     Sets a deviceViewModel for editing
        ///     Установка устройства для редактирования
        /// </summary>
        /// <param name="deviceModel">Device for editing.</param>
        public async void SetEditingContext(DeviceModel deviceModel, IEnumerable<string> deviceNamesExisting)
        {

            _deviceNamesExisting = deviceNamesExisting.ToList();
            _deviceNamesExisting.Remove(deviceModel.Name);
            _existingIps = (await _driversService.GetAllDriversAsync()).Select((driver => driver.CreateMomento().State.GetTcpAddress())).ToList();
            _existingIps.Remove(deviceModel.TcpAddress);
            if (deviceModel == null)
            {
                throw new NullReferenceException("Editing deviceViewModel don't be a null");
            }
            DeviceType = deviceModel.DeviceFactoryTypeName;
            this.DeviceName = deviceModel.Name;
            this.Description = deviceModel.Description;
            this.TcpIpAddress = deviceModel.TcpAddress;
            this.TcpIpPort = deviceModel.TcpPort.ToString();
            this.TransformKoefA = deviceModel.TransformKoefA.ToString();
            this.TransformKoefB = deviceModel.TransformKoefB.ToString();
            this.TransformKoefC = deviceModel.TransformKoefC.ToString();


            if (!string.IsNullOrEmpty(deviceModel.AnalogMeterTypeName))
            {
                if (deviceModel.AnalogMeterTypeName.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.NO))
                {
                    SelectedAnalogMeter = AnalogMetersEnum.Нет.ToString();
                }
                if (deviceModel.AnalogMeterTypeName.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE))
                {
                    SelectedAnalogMeter = AnalogMetersEnum.Энергомера.ToString();
                }
                if (deviceModel.AnalogMeterTypeName.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE))
                {
                    SelectedAnalogMeter = AnalogMetersEnum.МСА961.ToString();
                }
            }


            if (deviceModel.CustomFidersOnDevice.Count != 0)
            {
                IsFiderOrganization = true;
                deviceModel.CustomFidersOnDevice.ForEach((fider =>
                {
                    FiderDefinitionsViewModels.Add(_fiderViewModelFactory.CreateFiderDefinitionsViewModel(fider));
                }));
            }

            if (deviceModel.CustomIndicatorsOnDevice?.Count != 0)
            {
                IsIndicatorsEnabled = true;
                deviceModel.CustomIndicatorsOnDevice.ForEach((ind =>
                {
                    IndicatorDefinitionsViewModels.Add(_indicatorViewModelFactory.CreateIndicatorDefinitionsViewModel(ind));
                }));
            }
            if (deviceModel.CustomSignalsOnDevice?.Count != 0)
            {
                IsSignalsEnabled = true;
                deviceModel.CustomSignalsOnDevice.ForEach((signal =>
                {
                    SignalDefinitionsViewModels.Add(_signalViewModelFactory.CreateSignalDefinitionsViewModel(signal));
                }));
            }

            if (deviceModel.CascadeIndicatorsOnDevice.Count != 0)
            {
                IsCascadeEnabled = true;
                deviceModel.CascadeIndicatorsOnDevice.ForEach((cascade =>
                {
                    CascadeDefinitionsViewModels.Add(_cascadeViewModelFactory.CreateCascadeDefinitionsViewModel(cascade));
                }));
            }




            StarterDescriptionsViewModels.Clear();
            int index = 1;

            for (int i = 1; i <= 3; i++)
            {
                if (deviceModel.StarterDescriptions.Count > i - 1)
                {
                    StarterDescriptionsViewModels.Add(new StarterDescriptionViewModel()
                    {
                        Description = deviceModel.StarterDescriptions[i - 1],
                        IndexAndType = "Описание пускателя" + index
                    });
                    index++;
                }
                else
                {
                    StarterDescriptionsViewModels.Add(new StarterDescriptionViewModel()
                    {
                        Description = String.Empty,
                        IndexAndType = "Описание пускателя" + index
                    });
                    index++;

                }


            }



            StarterDescriptionsViewModels[0].SetChannelNumberOfStarter(uint.Parse(deviceModel.ChannelNumberOfStarter1));
            StarterDescriptionsViewModels[1].SetChannelNumberOfStarter(uint.Parse(deviceModel.ChannelNumberOfStarter2));
            StarterDescriptionsViewModels[2].SetChannelNumberOfStarter(uint.Parse(deviceModel.ChannelNumberOfStarter3));




            _isEditing = true;
            OkButtonTitle = "Сохранить";
            ViewTitle = "Редактирование виртуального устройства";
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<IFiderDefinitionsViewModel> FiderDefinitionsViewModels { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AddFiderCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        public ICommand DeleteFiderCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFiderOrganization
        {
            get { return _isFiderOrganization; }
            set
            {
                _isFiderOrganization = value;
                //RaisePropertyChanged();
                OnPropertyChanged(nameof(IsFiderOrganization));
            }
        }

        public ICommand AddSignalCommand { get; }

        public ICommand DeleteSignalCommand { get; }

        public bool IsSignalsEnabled
        {
            get { return _isSignalsEnabled; }
            set
            {
                _isSignalsEnabled = value;
                OnPropertyChanged(nameof(IsSignalsEnabled));
            }
        }

        public ObservableCollection<ISignalDefinitionsViewModel> SignalDefinitionsViewModels { get; }
        public ICommand AddIndicatorCommand { get; }

        public ICommand DeleteIndicatorCommand { get; }

        public bool IsIndicatorsEnabled
        {
            get { return _isIndicatorsEnabled; }
            set
            {
                _isIndicatorsEnabled = value;
                OnPropertyChanged(nameof(IsIndicatorsEnabled));
            }
        }

        public ObservableCollection<IIndicatorDefinitionsViewModel> IndicatorDefinitionsViewModels { get; }
        public ICommand AddCascadeCommand { get; }
        public ICommand DeleteCascadeCommand { get; }

        public bool IsCascadeEnabled
        {
            get { return _isCascadeEnabled; }
            set
            {
                _isCascadeEnabled = value;
                OnPropertyChanged(nameof(IsCascadeEnabled));
            }
        }

        public ObservableCollection<ICascadeDefinitionsViewModel> CascadeDefinitionsViewModels { get; }

        #endregion [IModifyDeviceViewModel members]

        #region [Override members]

        /// <summary>
        ///     Gets current view model validator mechanism
        ///     Возвращает вью-модель механизма валидации
        /// </summary>
        /// <returns>
        ///     Resulted instance of <see cref="AbstractValidator{T}" />
        /// </returns>
        protected override AbstractValidator<IModifyDeviceViewModel> GetValidator()
        {
            DeviceDataModelValidator dataModelValidator = new DeviceDataModelValidator();

            return dataModelValidator;
        }

        /// <summary>
        ///     Gets an instance of validation model
        ///     Возвращает сущность модели валидации
        /// </summary>
        /// <returns>Resulted instance of a validation model</returns>
        protected override IModifyDeviceViewModel GetValidationModel()
        {
            return this;
        }

        /// <summary>
        ///     Validating current interaction dialog
        ///     Проводит валидацию текущего интерактивного диалога
        /// </summary>
        /// <param name="validator">
        ///     An instance of <see cref="AbstractValidator{T}" /> to use for validation
        /// </param>
        protected override ValidationResult OnValidate(AbstractValidator<IModifyDeviceViewModel> validator)
        {
            GetExisting();
            (validator as DeviceDataModelValidator).SetContext(_existingIps, _deviceNamesExisting,this);

            var result = base.OnValidate(validator);

            return result;
        }

        private async void GetExisting()
        {
            if (!_isEditing)
            {
                _deviceNamesExisting = (await _devicesService.GetAllDevicesAsync()).Select((device => device.CreateMomento().State.DeviceName)).ToList();
                _existingIps = (await _driversService.GetAllDriversAsync()).Select((driver => driver.CreateMomento().State.GetTcpAddress())).ToList();
            }

        }


        /// <summary>
        ///     Canceling current interaction dialog
        ///     Обработчик отмены
        /// </summary>
        protected override void OnCanceling()
        {
            this.ResultedDeviceModel = null;
            base.OnCanceling();
        }

        /// <summary>
        ///     Submitting current interaction dialog
        ///     Обработчик подтверждения
        /// </summary>
        protected override async void OnSubmitting()
        {


            this.ResultedDeviceModel = new DeviceModel
            {

                Description = this.Description,
                Name = this.DeviceName,
                TcpAddress = this.TcpIpAddress,
                TcpPort = int.Parse(this.TcpIpPort),
                StarterDescriptions = StarterDescriptions.ToList(),
                DeviceFactoryTypeName = DeviceType.ToString(),
                DriverFactoryTypeName = "TCP_MB",
                TransformKoefA = int.Parse(TransformKoefA),
                TransformKoefB = int.Parse(TransformKoefB),
                TransformKoefC = int.Parse(TransformKoefC),
                ChannelNumberOfStarter1 = StarterDescriptionsViewModels[0].ChannelNumberOfStarter.ToString(),
                ChannelNumberOfStarter2 = StarterDescriptionsViewModels[1].ChannelNumberOfStarter.ToString(),
                ChannelNumberOfStarter3 = StarterDescriptionsViewModels[2].ChannelNumberOfStarter.ToString(),
            };


            if (IsFiderOrganization)
            {
                ResultedDeviceModel.CustomFidersOnDevice.AddRange(FiderDefinitionsViewModels.Select((model => model.FiderDefinitionModel)));
            }
            if (IsSignalsEnabled)
            {
                ResultedDeviceModel.CustomSignalsOnDevice.AddRange(SignalDefinitionsViewModels.Select((model => model.Model)));
            }
            if (IsIndicatorsEnabled)
            {
                ResultedDeviceModel.CustomIndicatorsOnDevice.AddRange(IndicatorDefinitionsViewModels.Select((model => model.Model)));
            }
            if (IsCascadeEnabled)
            {
                ResultedDeviceModel.CascadeIndicatorsOnDevice.AddRange(CascadeDefinitionsViewModels.Select((model => model.Model)));
            }
            if (
               SelectedAnalogMeter.Equals(AnalogMetersEnum.Нет.ToString()))
            {
                ResultedDeviceModel.AnalogMeterTypeName = DeviceStringKeys.DeviceAnalogMetersTagKeys.NO;
            }
            if (
                SelectedAnalogMeter.Equals(AnalogMetersEnum.Энергомера.ToString()))
            {
                ResultedDeviceModel.AnalogMeterTypeName = DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE;
            }
            if (
                SelectedAnalogMeter.Equals(AnalogMetersEnum.МСА961.ToString()))
            {
                ResultedDeviceModel.AnalogMeterTypeName = DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE;
            }
            base.OnSubmitting();
        }

        #endregion [Override members]
    }
}