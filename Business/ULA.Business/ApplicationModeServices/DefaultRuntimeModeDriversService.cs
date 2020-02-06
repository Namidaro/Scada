using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ULA.AddinsHost;
using ULA.AddinsHost.Business.Driver;
using ULA.Business.Exceptions;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.DTOs;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.AsyncServices;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.ApplicationModeServices
{
    /// <summary>
    ///     Represents default drivers logic service functionality in runtime mode
    /// Смотреть описание <see cref="DefaultConfigurationModeDriversService"/> - тот же функционал, только для Runtime драйвера
    /// </summary>
    public class DefaultRuntimeModeDriversService : Disposable, IRuntimeModeDriversService
    {
        #region [Private fields]

        private bool _isInitialized = false;
        private IPersistanceService _persistanceService;
        private readonly IUnityContainer _container;
        private IDictionary<Guid, IRuntimeLogicalDriver> _driversCache;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultRuntimeModeDriversService" />
        /// </summary>
        /// <param name="persistanceService">
        ///     An instance of <see cref="IPersistanceService" /> to use
        /// </param>
        /// <param name="container"></param>
        public DefaultRuntimeModeDriversService(IPersistanceService persistanceService,
                                                      IUnityContainer container)
        {
            this._persistanceService = persistanceService;
            _container = container;

            this.Initialization = this.OnInitializating();
            this._driversCache = new Dictionary<Guid, IRuntimeLogicalDriver>();
        }

        #endregion [Ctor's]

        #region [IRuntimeModeDriversService members]

        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        /// </summary>
        public Task Initialization { get; private set; }

        /// <summary>
        ///     Gets all runtime logical drivers that registered in the system asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task<IEnumerable<IRuntimeLogicalDriver>> GetAllDriversAsync()
        {
            this.ThrowIfDisposed();
            await this.Initialization;
            return this._driversCache.Values;
        }

        public async Task<IRuntimeLogicalDriver> GetDriverById(Guid id)
        {
            this.ThrowIfDisposed();
            await this.Initialization;

            if (this._driversCache.ContainsKey(id))
            {
                return this._driversCache[id];
            }
            return null;
        }

        #endregion [IRuntimeModeDriversService members]

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

        private async Task OnInitializating()
        {
            if(_isInitialized)return;
            await AsyncInitialization.EnsureInitializedAsync(this._persistanceService);
            this._driversCache = new Dictionary<Guid, IRuntimeLogicalDriver>();
            await this.UpdateCacheInternalAsynk(this._driversCache);
            _isInitialized = true;
        }

        private async Task UpdateCacheInternalAsynk(IDictionary<Guid, IRuntimeLogicalDriver> paramDriversCache)
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
                catch (Exception)
                {
                    throw new Exception("Load driver asynk");
                }
            }
        }

        private IRuntimeLogicalDriver RestoreDriverFromMomento(Guid driverId, IDriverMomento driverMomento)
        {
            if (driverMomento == null) throw new ArgumentNullException("driverMomento");
            var driverType = driverMomento.State.DriverType;

            var driverFactory = _container.Resolve<ILogicalDriverFactory>(driverType);
            if (driverFactory == null) throw new UnknownLogicalDeviceTypeException(driverType);

            var logicalDriver = driverFactory.CreateRuntimeLogicalDriver();
            if (logicalDriver == null) throw new LogicalDriverCreationException(new LogicalDriverInformation());

            logicalDriver.SetMomento(driverMomento);
            ((IRuntimeLogicalDriver)logicalDriver).DriverId = driverId;

            return logicalDriver;
        }

        private void OnDispose()
        {
            this._persistanceService = null;
            this._driversCache.Clear();
            this._driversCache = null;
        }

        #endregion [Help members]
    }
}
