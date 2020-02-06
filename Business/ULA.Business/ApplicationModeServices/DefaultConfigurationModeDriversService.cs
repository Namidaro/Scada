using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ULA.AddinsHost;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.Business.Driver.Context;
using ULA.Business.Exceptions;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DTOs;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.AsyncServices;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace ULA.Business.ApplicationModeServices
{
    /// <summary>
    ///     Represents default drivers logic service functionality
    ///     Представляет сервис(фасад) для драйверов устройств в режиме конфигурации. 
    ///     Основная цель CRUD драйверов в xml (дал конфига).
    /// </summary>
    public class DefaultConfigurationModeDriversService : Disposable, IConfigurationModeDriversService
    {
        #region [Private fields]
        
        private IPersistanceService _persistanceService;
        private readonly IUnityContainer _container;
        private IDictionary<Guid, IConfigLogicalDriver> _driversCache;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultConfigurationModeDriversService" />
        /// </summary>
        /// <param name="persistanceService">
        ///     An instance of <see cref="IPersistanceService" /> to use
        /// </param>
        /// <param name="container"></param>
        public DefaultConfigurationModeDriversService(IPersistanceService persistanceService,
                                                    IUnityContainer container)
        {
            this._persistanceService = persistanceService;
            _container = container;

            this.Initialization = this.OnInitializating();
        }

        #endregion [Ctor's]

        #region [IConfigurationModeDriversService members]

        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        /// </summary>
        /// Таска для асинхронной иницмализации
        public Task Initialization { get; private set; }

        /// <summary>
        ///     Gets all configuration logical drivers that registered in the system asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        /// Асинхронно вренет все драйвера из xml
        public async Task<IEnumerable<IConfigLogicalDriver>> GetAllDriversAsync()
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            return this._driversCache.Values;
        }

        /// <summary>
        ///     Вернет драйвер по Id. Если драйвера с таким Id нет - то null
        /// </summary>
        /// <param name="id">Id драйвера</param>
        /// <returns>Task с драйвером.</returns>
        public async Task<IConfigLogicalDriver> GetDriverById(Guid id)
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            if (this._driversCache.ContainsKey(id))
            {
                return this._driversCache[id];
            }
            return null;
        }

        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDriver" /> asynchronously
        /// </summary>
        /// <param name="logicalDriverInformation">
        ///     An instance of <see cref="LogicalDriverInformation" /> to obtain all information about deviceViewModel to create
        /// Инфа о драйвере
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation
        /// Таска с драйвером</returns>
        /// Созданёт драйвер устройства и сохраняет его в xml
        public async Task<IConfigLogicalDriver> CreateDriverAsync(LogicalDriverInformation logicalDriverInformation)
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            Guard.AgainstNullReference(logicalDriverInformation, "deviceInfo");

            var driver = await this.CreateDriverInternalAsync(logicalDriverInformation);
            try
            {
                var driverContext = await this._persistanceService.GetDriverPersistableContextAsync(driver.DriverId);
                await driverContext.SaveDriverMomentoAsync(driver.CreateMomento());
                this._driversCache.Add(driver.DriverId, driver);
                return driver;
            }
            catch (Exception error)
            {
                throw new LogicalDriverCreationException(logicalDriverInformation, innerException: error);
            }
        }

        /// <summary>
        ///     Removes an instance of <see cref="IConfigLogicalDriver" /> from the system asynchronously
        /// </summary>
        /// <param name="driverId">
        ///     An instance of <see cref="IConfigLogicalDriver" /> to remove
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        /// Удаляет драйвер
        public async Task RemoveDriverAsync(Guid driverId)
        {
            this.ThrowIfDisposed();
            await this.Initialization;
            try
            {
                await this._persistanceService.RemoveDriverPersistableContextAsynk(driverId);
                this._driversCache.Remove(driverId);
            }
            catch (Exception)
            {
                throw new LogicalDeviceException();
            }
        }

        /// <summary>
        ///     Updates an instance of <see cref="IConfigLogicalDriver" /> in the system registry asynchronously
        /// </summary>
        /// <param name="driverInfo">
        ///     An instance of <see cref="LogicalDeviceInformation" /> to update
        /// </param>
        /// <param name="editingDriver">
        ///     An instance of <see cref="IConfigLogicalDriver" /> to update
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        /// Обновляет драйвер
        public async Task<IConfigLogicalDriver> UpdateDriverAsync(LogicalDriverInformation driverInfo,
            IConfigLogicalDriver editingDriver)
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            Guard.AgainstNullReference(driverInfo, "deviceInfo");


            await this.RemoveDriverAsync(editingDriver.DriverId);
            var logicalDriver = await this.CreateDriverInternalAsync(driverInfo);

            try
            {
                var context = await this._persistanceService.GetDriverPersistableContextAsync(logicalDriver.DriverId);
                await context.SaveDriverMomentoAsync(logicalDriver.CreateMomento());
                this._driversCache.Remove(logicalDriver.DriverId);
                this._driversCache.Add(logicalDriver.DriverId, logicalDriver);
            }
            catch (Exception)
            {
                throw new LogicalDeviceException();
            }

            return logicalDriver;
        }

        #endregion [IConfigurationModeDriversService members]

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

        //Инициализация класса
        private async Task OnInitializating()
        {
            await AsyncInitialization.EnsureInitializedAsync(this._persistanceService);

            this._driversCache = new Dictionary<Guid, IConfigLogicalDriver>();
            await this.UpdateCacheInternalAsynk(this._driversCache);
        }

        //Создаёт драйвер объект драйвера из информации о нем
        private async Task<IConfigLogicalDriver> CreateDriverInternalAsync(LogicalDriverInformation driverInfo)
        {
            var context = this.MapDriverInfoToDriverContext(driverInfo);

            if ((await this.GetAllDriversAsync()).Any(a => a.Equals(context)))
                throw new LogicalDriverAlreadyExistsExcetpion(driverInfo);

            var driverFactory = _container.Resolve<ILogicalDriverFactory>(driverInfo.DriverType);
            if (driverFactory == null) throw new UnknownLogicalDriverTypeException(driverInfo.DriverType);

            var logicalDriver = driverFactory.CreateConfigLogicalDriver();
            if (logicalDriver == null) throw new LogicalDriverCreationException(driverInfo);

            try
            {
                await logicalDriver.InitializeAsync(context);
                ((IConfigLogicalDriver)logicalDriver).DriverId = Guid.NewGuid();
            }
            catch (Exception error)
            {
                throw new LogicalDriverCreationException(driverInfo, innerException: error);
            }

            return logicalDriver;
        }

        /// <summary>
        ///     создаёт контекст драйвера и инициализирует его
        /// </summary>
        /// <param name="driverInfo"></param>
        /// <returns></returns>
        private IDriverContext MapDriverInfoToDriverContext(LogicalDriverInformation driverInfo)
        {
            //TODO: When more than one driver type exists this builder should be refactored!!!!
            var contextBuilder = new AomTcpDriverContextEntityBuilder();
            contextBuilder.SetDriverType(driverInfo.DriverType);
            contextBuilder.SetTcpAddress(driverInfo.DriverTcpAddress);
            contextBuilder.SetTcpPortNumber(driverInfo.DriverTcpPort);

            return contextBuilder.Build();
        }

        private void OnDispose()
        {
            this._persistanceService = null;
            this._driversCache.Clear();
            this._driversCache = null;
        }

        /// <summary>
        ///     Инициализирует кэш этого сервиса на основе xml конфига
        /// </summary>
        /// <param name="paramDriversCache">наш кэш</param>
        /// <returns>таска</returns>
        private async Task UpdateCacheInternalAsynk(IDictionary<Guid, IConfigLogicalDriver> paramDriversCache)
        {
            paramDriversCache.Clear();
            var contexts = await this._persistanceService.GetDriversPersistableContextAsynk();
            foreach (var driverPersistableContext in contexts)
            {
                try
                {
                    var driverId = driverPersistableContext.Key;
                    var driverMemento = await driverPersistableContext.Value.GetDriverMomentoAsync();
                    var driver = this.RestoreDriverFromMomento(driverId, driverMemento);
                    this._driversCache.Add(driverId, driver);
                }
                catch (Exception exception)
                {

                    throw exception; //new Exception("Load driver asynk");
                }
            }
        }

        /// <summary>
        ///     На основе id-ка достает и хранителя(Momento) создает новыё драйвер
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="driverMomento"></param>
        /// <returns></returns>
        private IConfigLogicalDriver RestoreDriverFromMomento(Guid driverId, IDriverMomento driverMomento)
        {
            if (driverMomento == null) throw new ArgumentNullException("driverMomento");
            var driverType = driverMomento.State.DriverType;

            var driverFactory = _container.Resolve<ILogicalDriverFactory>(driverType);
            if (driverFactory == null) throw new UnknownLogicalDeviceTypeException(driverType);

            var logicalDriver = driverFactory.CreateConfigLogicalDriver();
            if (logicalDriver == null) throw new LogicalDriverCreationException(new LogicalDriverInformation());

            logicalDriver.SetMomento(driverMomento);
            ((IConfigLogicalDriver)logicalDriver).DriverId = driverId;

            return logicalDriver;
        }

        #endregion [Help members]
    }
}