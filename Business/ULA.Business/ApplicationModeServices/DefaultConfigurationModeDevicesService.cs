using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ULA.AddinsHost;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Exceptions;
using ULA.Business.Image;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DTOs;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.AsyncServices;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace ULA.Business.ApplicationModeServices
{
    /// <summary>
    ///     Represents a default configuration mode facade service functionaltiy
    ///     Представляет сервис(фасад) для устройств в режиме конфигурации. 
    ///     Основная цель CRUD устройств в xml (дал конфига).
    /// </summary>
    public class DefaultConfigurationModeDevicesService : Disposable, IConfigurationModeDevicesService
    {
        #region [Private fields]
        
        private IDictionary<Guid, IConfigLogicalDevice> _devicesCache;
        private IConfigurationModeDriversService _driversService;
        private readonly IUnityContainer _container;
        private IPersistanceService _persistanceService;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultConfigurationModeDevicesService" />
        /// </summary>
        /// <param name="persistanceService">
        ///     An instance of <see cref="IPersistanceService" /> to use
        /// </param>
        /// <param name="driversService">
        ///     An instance of <see cref="IConfigurationModeDriversService" /> to use
        /// </param>
        /// <param name="container"></param>
        /// Конструктор.
        public DefaultConfigurationModeDevicesService(IPersistanceService persistanceService,IConfigurationModeDriversService driversService,IUnityContainer container)
        {
            this._persistanceService = persistanceService;
            this._driversService = driversService;
            _container = container;
            this.Initialization = this.OnInitializating();
        }

        #endregion [Ctor's]

        #region [IConfigurationModeFacadeService members]

        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        /// </summary>
        /// Таска для асинхронной иницмализации
        public Task Initialization { get; private set; }

        /// <summary>
        ///     Gets all logical devices by a criterion asynchronously
        ///     Асинхронно вренет все конфиг устройства из xml
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task<IEnumerable<IConfigLogicalDevice>> GetAllDevicesAsync()
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            return this._devicesCache.Values.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IBackgroundPicture GetBackgroundPicture()
        {
            BackgroundPicture bp = new BackgroundPicture();
            bp.Load();
            return bp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SetBackgroundPictureAsync(IBackgroundPicture picture)
        {
            Task t = new Task((()=> { picture.Save(false); }));
            t.Start();
            await t;
        }

        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDevice" /> asynchronously
        /// Создает новое устройство на основе информации о нём полученной в параметрах
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task<IConfigLogicalDevice> CreateDeviceAsync(LogicalDeviceInformation deviceInfo)
        {
            this.ThrowIfDisposed();
            await this.Initialization;
            Guard.AgainstNullReference(deviceInfo, "deviceInfo");
            Guid? driverId = null;
            if (!string.IsNullOrEmpty(deviceInfo.DriverTypeName))
            {
                try
                {
                    var driver = await this._driversService.CreateDriverAsync(new LogicalDriverInformation
                    {
                        DriverType = deviceInfo.DriverTypeName,
                        DriverTcpPort = deviceInfo.DriverTcpPort,
                        DriverTcpAddress = deviceInfo.DriverTcpAddress
                    });
                    driverId = driver.DriverId;
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message);
                }
            }
            
            IConfigLogicalDevice logicalDevice = null;

            try
            {
                logicalDevice = await this.CreateLogicalDeviceInternalAsync(deviceInfo, driverId);

                int number = 0;
                if (this._devicesCache.Count > 0)
                {
                    var numbers = this._devicesCache.Values.Select(
                        device => device.CreateMomento().State.DeviceNumber).ToList();
                    for (int i = 0; i < 104; i++)
                    {
                        if (!numbers.Contains(i))
                        {
                            number = i;
                            break;
                        }
                    }
                }
                logicalDevice.CreateMomento().State.AnalogMeterType=deviceInfo.AnalogMeterTypeName;
                logicalDevice.CreateMomento().State.DeviceType= deviceInfo.DeviceTypeName;
                logicalDevice.CreateMomento().State.DeviceNumber=number;
                logicalDevice.CreateMomento().State.Starter1Description= deviceInfo.StarterDescriptions[0];
                logicalDevice.CreateMomento().State.Starter2Description = deviceInfo.StarterDescriptions[1];
                logicalDevice.CreateMomento().State.Starter3Description = deviceInfo.StarterDescriptions[2];

                logicalDevice.CreateMomento().State.TransformKoefA = deviceInfo.TransformKoefA;
                logicalDevice.CreateMomento().State.TransformKoefB = deviceInfo.TransformKoefB;
                logicalDevice.CreateMomento().State.TransformKoefC = deviceInfo.TransformKoefC;

                logicalDevice.CreateMomento().State.ChannelNumberOfStarter1 = deviceInfo.ChannelNumberOfStarter1;
                logicalDevice.CreateMomento().State.ChannelNumberOfStarter2 = deviceInfo.ChannelNumberOfStarter2;
                logicalDevice.CreateMomento().State.ChannelNumberOfStarter3 = deviceInfo.ChannelNumberOfStarter3;

            }
            catch (Exception ex)
            {
                if (driverId.HasValue)
                    this.RemoveDriver(driverId.Value);
                throw ex;
            }

            try
            {
                
                var context = await this._persistanceService.GetDevicePersistableContextAsync(logicalDevice.DeviceId);
                context.SaveDeviceMomentoAsync(logicalDevice.CreateMomento());
                this._devicesCache.Add(logicalDevice.DeviceId, logicalDevice);
            }
            catch (Exception error)
            {
                if (driverId.HasValue)
                    this._driversService.RemoveDriverAsync(driverId.Value).Start();
                throw new LogicalDeviceException(deviceInfo, innerException: error);
            }

            return logicalDevice;
        }

        /// <summary>
        ///     Removes an instance of <see cref="IConfigLogicalDevice" /> from the system asynchronously
        /// Асинхронно Удаляет устройство из xml
        /// </summary>
        /// <param name="device">
        ///     An instance of <see cref="IConfigLogicalDevice" /> to remove. Use DeviceId only.
        ///     Сущность конфиг устройства, которая будет удалена.(Вместе с драйвером этого устройства)
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task RemoveDeviceAsync(IConfigLogicalDevice device)
        {
            this.ThrowIfDisposed();
            await this.Initialization;
            try
            {
                await this._persistanceService.RemoveDevicePersistanbleContextAsync(device.DeviceId);
                var stringDriverId = device.CreateMomento().State.RelatedDriverId;
                await this._driversService.RemoveDriverAsync(Guid.Parse(stringDriverId));
                this._devicesCache.Remove(device.DeviceId);
            }
            catch (Exception tException)
            {
                throw tException; //new LogicalDeviceException();
            }
        }

        /// <summary>
        ///     Updates an instance of <see cref="IConfigLogicalDevice" /> in the system registry asynchronously
        /// Обновит данное устройство.
        /// </summary>
        /// <param name="deviceInfo">
        ///     An instance of <see cref="LogicalDeviceInformation" /> with data to update
        /// Новые данные для обновляемого устройства
        /// </param>
        /// <param name="editingDevice">
        ///     An instance of <see cref="IConfigLogicalDevice" /> to update
        /// Устройство, которое будет обновляться
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task<IConfigLogicalDevice> UpdateDeviceAsync(LogicalDeviceInformation deviceInfo,
            IConfigLogicalDevice editingDevice)
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            Guard.AgainstNullReference(deviceInfo, "deviceInfo");
            
            var drivers = await this._driversService.GetAllDriversAsync();
            if (!editingDevice.DeviceName.Equals(deviceInfo.DeviceName) &&
                this._devicesCache.Any(dev => dev.Value.DeviceName.Equals(deviceInfo.DeviceName)))
            {
                throw new InvalidOperationException("Устройство с таким именем уже существует");
            }
            var editingDriver =
                await this._driversService.GetDriverById(Guid.Parse(
                    editingDevice.CreateMomento().State.RelatedDriverId));
            if (!editingDriver.CreateMomento().State.GetTcpAddress().Equals(deviceInfo.DriverTcpAddress) &&
                drivers.Any(dr => dr.CreateMomento().State.GetTcpAddress().Equals(deviceInfo.DriverTcpAddress)))
            {
                throw new InvalidOperationException("Устройство с таким IP-адресом уже существует");
            }
            
            await this.RemoveDeviceAsync(editingDevice);

            IConfigLogicalDevice logicalDevice = null;
            Guid? driverId = null;
            if (!string.IsNullOrEmpty(deviceInfo.DriverTypeName))
            {
                try
                {
                    
                    var driver = await this._driversService.CreateDriverAsync(new LogicalDriverInformation
                    {
                        DriverType = deviceInfo.DriverTypeName,
                        DriverTcpPort = deviceInfo.DriverTcpPort,
                        DriverTcpAddress = deviceInfo.DriverTcpAddress
                    });
                    driverId = driver.DriverId;
                    
                    logicalDevice = await this.CreateLogicalDeviceInternalAsync(deviceInfo, driverId);
                    logicalDevice.CreateMomento().State.Starter1Description = deviceInfo.StarterDescriptions[0];
                    logicalDevice.CreateMomento().State.Starter2Description = deviceInfo.StarterDescriptions[1];
                    logicalDevice.CreateMomento().State.Starter3Description = deviceInfo.StarterDescriptions[2];
                    logicalDevice.CreateMomento().State.TransformKoefA = deviceInfo.TransformKoefA;
                    logicalDevice.CreateMomento().State.TransformKoefB = deviceInfo.TransformKoefB;
                    logicalDevice.CreateMomento().State.TransformKoefC = deviceInfo.TransformKoefC;
                    logicalDevice.CreateMomento().State.ChannelNumberOfStarter1 = deviceInfo.ChannelNumberOfStarter1;
                    logicalDevice.CreateMomento().State.ChannelNumberOfStarter2 = deviceInfo.ChannelNumberOfStarter2;
                    logicalDevice.CreateMomento().State.ChannelNumberOfStarter3 = deviceInfo.ChannelNumberOfStarter3;
                    var context =
                        await this._persistanceService.GetDevicePersistableContextAsync(logicalDevice.DeviceId);
                    logicalDevice.CreateMomento().State.SchemeTable = editingDevice.CreateMomento().State.SchemeTable;
                    int number = editingDevice.CreateMomento().State.DeviceNumber;
                    logicalDevice.CreateMomento().State.DeviceNumber = number;

                    context.SaveDeviceMomentoAsync(logicalDevice.CreateMomento());
                    this._devicesCache.Remove(logicalDevice.DeviceId);
                    this._devicesCache.Add(logicalDevice.DeviceId, logicalDevice);
                }
                catch (Exception ex)
                {
                    throw new LogicalDeviceException("Редактирование невозможно", ex);
                }
            }

            return logicalDevice;
        }

        /// <summary>
        ///     Updates an instance of <see cref="IConfiguratedDeviceSchemeTable"/> in <see cref="IConfigLogicalDevice" /> in the system registry asynchronously
        /// Асинхронно обновляет схему устройства
        /// </summary>
        /// <param name="device">An instance of <see cref="LogicalDeviceInformation" /> to update. 
        /// Устройство в котором будет обновляться схема</param>
        /// <param name="scheme">New scheme. Instance of <see cref="IConfiguratedDeviceSchemeTable"/>
        /// Новая схема для устройства</param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task UpdateDeviceSchemeAsync(IConfigLogicalDevice device, IConfiguratedDeviceSchemeTable scheme)
        {
            var context = await this._persistanceService.GetDevicePersistableContextAsync(device.DeviceId);
            device.CreateMomento().State.SchemeTable = scheme;
            context.SaveDeviceMomentoAsync(device.CreateMomento());
        }

        /// <summary>
        ///     Сохраняет новый номер позиции устройства в хранилище
        /// </summary>
        /// <param name="device">Устройство</param>
        /// <returns>задача</returns>
        public async Task UpdateDevicePositionAsync(IConfigLogicalDevice device,int newNumber)
        {
            var context = await this._persistanceService.GetDevicePersistableContextAsync(device.DeviceId);
            device.CreateMomento().State.DeviceNumber = newNumber;
            context.SaveDeviceMomentoAsync(device.CreateMomento());
        }

        #endregion [IConfigurationModeFacadeService members]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.Initialization.ContinueWith(task =>
            {
                this.OnDispose();
                base.OnDisposing();
            });
        }

        #endregion [Override members]

        #region [Help members]

        /// <summary>
        ///     Асинхронно удаляет драйвер из xml.
        /// </summary>
        /// <param name="guid">GUid драйвера</param>
        private async void RemoveDriver(Guid guid)
        {
            await this._driversService.RemoveDriverAsync(guid);
        }

        /// <summary>
        /// Освобождаем ресурсы
        /// </summary>
        private void OnDispose()
        {
            this._persistanceService = null;
            this._driversService = null;
        }

        /// <summary>
        ///     Инициализация
        /// </summary>
        /// <returns></returns>
        private async Task OnInitializating()
        {

            await AsyncInitialization.EnsureInitializedAsync(this._persistanceService, this._driversService);

            this._devicesCache = new ConcurrentDictionary<Guid, IConfigLogicalDevice>();
            await this.UpdateCacheInternalAsync(this._devicesCache);
        }

        /// <summary>
        ///     Перенос устройств с xml в кэш этого класса
        /// </summary>
        /// <param name="devicesCache">Кэш</param>
        /// <returns></returns>
        private async Task UpdateCacheInternalAsync(IDictionary<Guid, IConfigLogicalDevice> devicesCache)
        {
            devicesCache.Clear();

            var contexts = await this._persistanceService.GetDevicePersistableContextsAsync();
            foreach (var devicePersistableContext in contexts)
            {
                try
                {
                    var deviceId = devicePersistableContext.Key;
                    var deviceMomento = await devicePersistableContext.Value.GetMomentoAsync();
                    var device = await this.RestoreDeviceFromMomentoAsync(deviceId, deviceMomento);
                    this._devicesCache.Add(deviceId, device);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        ///     Создает устройство на основе данных из xml по Id-ку и инициализирует его состояние данных
        /// </summary>
        /// <param name="deviceId">ID-шник устройства</param>
        /// <param name="deviceMomento">Состояние(данные) устройство (Можешь почитать про шаблон Momento)</param>
        /// <returns></returns>
        private async Task<IConfigLogicalDevice> RestoreDeviceFromMomentoAsync(Guid deviceId,
            IDeviceMomento deviceMomento)
        {
            if (deviceMomento == null) throw new ArgumentNullException("deviceMomento");
            var deviceType = deviceMomento.State.DeviceType;

            var deviceFactory = _container.Resolve<ILogicalDeviceViewModelFactory>(deviceType);
            if (deviceFactory == null) throw new UnknownLogicalDeviceTypeException(deviceType);

            var logicalDevice = deviceFactory.CreateConfigLogicalDevice();
            if (logicalDevice == null) throw new LogicalDeviceException();

            await logicalDevice.SetMomentoAsync(deviceMomento);
            ((IConfigLogicalDevice) logicalDevice).DeviceId = deviceId;

            return logicalDevice;
        }


        /// <summary>
        ///     Создает устройство на основе данных о нем
        /// </summary>
        /// <param name="deviceInfo">Инфа о устростве</param>
        /// <param name="driverId">Id-шник драйвера данного устройства устройства (Если есть)</param>
        /// <returns></returns>
        private async Task<IConfigLogicalDevice> CreateLogicalDeviceInternalAsync(LogicalDeviceInformation deviceInfo,
            Guid? driverId = null)
        {
            if ((await this.GetAllDevicesAsync()).Any(a => a.DeviceName.Equals(deviceInfo.DeviceName)))
                throw new LogicalDeviceAlreadyExistsException(deviceInfo);
          
            var deviceFactory = _container.Resolve<ILogicalDeviceViewModelFactory>(deviceInfo.DeviceTypeName);
            if (deviceFactory == null) throw new UnknownLogicalDeviceTypeException(deviceInfo.DeviceTypeName);

            var logicalDevice = deviceFactory.CreateConfigLogicalDevice();
            if (logicalDevice == null) throw new LogicalDeviceException(deviceInfo);

            try
            {
                var deviceContextBuilder = new AomDeviceContextEntityBuilder();
                deviceContextBuilder.SetDeviceName(deviceInfo.DeviceName);
                deviceContextBuilder.SetDeviceDescription(deviceInfo.DeviceDescription);
                deviceContextBuilder.SetDeviceType(deviceInfo.DeviceTypeName);
                deviceContextBuilder.SeAnalogMeterType(deviceInfo.AnalogMeterTypeName);
                deviceContextBuilder.SetCustomFiders(deviceInfo.CustomFidersOnDevice);
                deviceContextBuilder.SetCustomIndicators(deviceInfo.CustomIndicatorsOnDevice);
                deviceContextBuilder.SetCustomSignals(deviceInfo.CustomSignalsOnDevice);
                deviceContextBuilder.SetCustomCascade(deviceInfo.CascadeIndicatorsOnDevice);
                deviceContextBuilder.SetAnalogMeterKoefs(deviceInfo.TransformKoefA,deviceInfo.TransformKoefB,deviceInfo.TransformKoefC);

                if (driverId.HasValue) deviceContextBuilder.SetDriverId(driverId.Value.ToString());

                await logicalDevice.InitializeAsync(deviceContextBuilder.Build());
                logicalDevice.DeviceId = Guid.NewGuid();
            }
            catch (Exception error)
            {
                throw new LogicalDeviceException(deviceInfo, innerException: error);
            }

            return logicalDevice;
        }

        #endregion [Help members]
    }
}