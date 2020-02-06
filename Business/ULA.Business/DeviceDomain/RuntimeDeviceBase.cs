using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.Business.Driver.Context;
using ULA.AddinsHost.Business.Strings;
using ULA.ApplicationConnectionService;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Business.LoggerServices;
using ULA.Common;
using ULA.Interceptors;
using ULA.Interceptors.Application;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RuntimeDeviceBase : Disposable, IRuntimeDevice
    {
        private readonly IApplicationSettingsService _applicationSettingsService;
        private readonly IRuntimeModeDriversService _runtimeModeDriversService;
        private readonly ApplicationConnectionService.ApplicationConnectionService _applicationConnectionService;
        private readonly IDeviceTimerInterrogationService _timerInterrigationService;
        private readonly IResistorFactory _resistorFactory;
        private readonly ICustomItemsFactory _customItemsFactory;
        private readonly ConnectionLogger _connectionLogger;
        private readonly IDataWritingService _dataWritingService;
        protected TcpDeviceConnection _tcpDeviceConnection;
        private int _queryTimeout;
        private string _deviceSignature;
        private Guid _deviceId;
        private string _deviceName;
        private string _deviceDescription;
        private bool _isOnline;
        private bool _isConnectionWasOnline;
        protected Dictionary<int, LightingModeEnum> _starterToLighttingModeDictionary;
        private Logger _logger;
        private int _partialPeriod;
        private int _fullPeriod;
        private IDeviceMomento _deviceMomento;
        private IDriverMomento _driverMomento;
        private IDeviceCustomItems _deviceCustomItems;
        private bool _isDeviceInitialized;
        private IAnalogMeter _analogMeter;

        #region Implementation of IRuntimeDevice

        /// <summary>
        /// 
        /// </summary>
        protected RuntimeDeviceBase(IApplicationSettingsService applicationSettingsService,
            IRuntimeModeDriversService runtimeModeDriversService,
            ApplicationConnectionService.ApplicationConnectionService applicationConnectionService,
            IDeviceTimerInterrogationService timerInterrigationService, IDefectState defectState,
            IDeviceDataCache deviceDataCache, IAnalogData analogData, IResistorFactory resistorFactory,
            ICustomItemsFactory customItemsFactory, ConnectionLogger connectionLogger)
        {
            _applicationSettingsService = applicationSettingsService;
            _runtimeModeDriversService = runtimeModeDriversService;
            _applicationConnectionService = applicationConnectionService;
            _timerInterrigationService = timerInterrigationService;
            _resistorFactory = resistorFactory;
            _customItemsFactory = customItemsFactory;
            _connectionLogger = connectionLogger;
            this._starterToLighttingModeDictionary = new Dictionary<int, LightingModeEnum>();
            this._starterToLighttingModeDictionary.Add(1, LightingModeEnum.UNDEFINED);
            this._starterToLighttingModeDictionary.Add(2, LightingModeEnum.UNDEFINED);
            this._starterToLighttingModeDictionary.Add(3, LightingModeEnum.UNDEFINED);
            _timerInterrigationService.SetDeviceForInterrogation(this);
            _timerInterrigationService.InterrogationCycleComplete += () =>
            {
                DeviceValuesUpdated?.Invoke();
            };
            DefectState = defectState;
            DeviceDataCache = deviceDataCache;
            AnalogData = analogData;
            ResistorsOnDevice = new List<IResistor>();
            StartersOnDevice = new List<IStarter>();
        }

        public abstract Task UpdateSignature();
        public abstract Task UpdateSignal();

        public string DeviceSignature { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDeviceDataCache DeviceDataCache { get; }

        /// <summary>
        /// 
        /// </summary>
        public IDefectState DefectState { get; }

        public IDeviceCustomItems DeviceCustomItems
        {
            get { return _deviceCustomItems; }
        }

        public IAnalogMeter AnalogMeter
        {
            get { return _analogMeter; }
            set { _analogMeter = value; }
        }

        public bool IsDeviceInitialized
        {
            get { return _isDeviceInitialized; }
        }

        public IAnalogData AnalogData { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task InitializeAsync()
        {
            ResistorsOnDevice.Clear();
            ResistorsOnDevice.AddRange(_resistorFactory.CreateResistorsOnDevice(this.DeviceMomento.State.SchemeTable, this));
            _deviceCustomItems = _customItemsFactory.CreateDeviceCustomItems(_deviceMomento.State);
            DeviceInitialized?.Invoke();
            _isDeviceInitialized = true;
        }

        /// <summary>
        /// пускатели на устройстве
        /// </summary>
        public List<IStarter> StartersOnDevice { get; }

        public List<IResistor> ResistorsOnDevice { get; }


        public TcpDeviceConnection TcpDeviceConnection
        {
            get { return _tcpDeviceConnection; }
        }



        public async void SetUpdatingMode(bool isFull)
        {
            _timerInterrigationService.StopInterrogation();
            _timerInterrigationService.SetIsFullUpdating(isFull);
            _timerInterrigationService.StartInterrogation();
        }

        public Action DeviceInitialized { get; set; }

        public Action DeviceValuesUpdated { get; set; }


        #endregion

        #region Implementation of ILogicalDevice

        public Guid DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        public string DeviceName
        {
            get { return _deviceName; }
        }

        public string DeviceDescription
        {
            get { return _deviceDescription; }
        }

        public IDeviceMomento DeviceMomento
        {
            get { return _deviceMomento; }
            set
            {
                _deviceMomento = value;
                _deviceName = _deviceMomento.State.DeviceName;
                _deviceDescription = _deviceMomento.State.DeviceDescription;
                DefectState.SetLogger(LogManager.GetLogger(DeviceName));
                _logger = LogManager.GetLogger(DeviceName);
                DeviceNumber = _deviceMomento.State.DeviceNumber;
                // InitFormatters();
                InitializeDriver();
            }
        }

        protected virtual void InitFormatters()
        {
            //
        }

        public IDriverMomento DriverMomento
        {
            get { return _driverMomento; }
            set
            {
                _driverMomento = value;

            }
        }


        private async void InitializeDriver()
        {
            var driver = await _runtimeModeDriversService
                .GetDriverById(Guid.Parse(this.DeviceMomento.State.RelatedDriverId));
            _applicationSettingsService.Load();
            DriverMomento = driver.CreateMomento();
            InitFormatters();
            _tcpDeviceConnection = _applicationConnectionService.CreateTcpDeviceConnection(
                DriverMomento.State.GetTcpAddress(), DriverMomento.State.GetTcpPort(),
                _applicationSettingsService.QueryTimeoutPeriod * 1000);
            _tcpDeviceConnection.ConnectionRestoredAction += async () =>
            {
                try
                {

                    _isOnline = true;
                    if (_isConnectionWasOnline)
                    {
                        _connectionLogger.ConnectionResroted(_logger);
                    }
                    _isConnectionWasOnline = true;
                }
                catch
                {

                }
            };

            _tcpDeviceConnection.ConnectionLostAction += () =>
            {
                if (_isOnline)
                {
                    _isDeviceInitialized = false;
                    _connectionLogger.ConnectionLost(_logger);
                    _isOnline = false;
                }
            };
        }

        public int DeviceNumber { get; set; }


        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            base.OnDisposing();
            _tcpDeviceConnection?.Dispose();
            _timerInterrigationService.Dispose();
            _tcpDeviceConnection = null;

        }

        #endregion

        #endregion
    }
}
