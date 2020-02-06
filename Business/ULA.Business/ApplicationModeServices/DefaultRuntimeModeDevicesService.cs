using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ULA.AddinsHost;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Exceptions;
using ULA.Business.Image;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.AsyncServices;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.ApplicationModeServices
{
    /// <summary>
    ///     Represents a default runtime mode facade service functionaltiy
    /// Смотреть описание <see cref="DefaultConfigurationModeDevicesService"/> - тот же функционал, только для Runtime девайса
    /// </summary>
    public class DefaultRuntimeModeDevicesService : Disposable, IRuntimeModeDevicesServices
    {
        #region [Private fields]
        
        private IDictionary<Guid, IRuntimeDeviceViewModel> _devicesCache;
        private IRuntimeModeDriversService _driversService;
        private IPersistanceService _persistanceService;
        private readonly IUnityContainer _container;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DefaultRuntimeModeDevicesService" />
        /// </summary>
        /// <param name="persistanceService">
        ///     An instance of <see cref="IPersistanceService" /> to use
        /// </param>
        /// <param name="container"></param>
        /// <param name="driversService">
        ///     An instance of <see cref="DefaultRuntimeModeDevicesService" /> to use
        /// </param>
        public DefaultRuntimeModeDevicesService(IPersistanceService persistanceService,
         IUnityContainer container,
            IRuntimeModeDriversService driversService)
        {
            this._persistanceService = persistanceService;
            _container = container;
            this._driversService = driversService;
            this.Initialization = this.OnInitializating();
        }

        #endregion [Ctor's]

        #region [IRuntimeModeFacadeService members]

        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        /// </summary>
        public Task Initialization { get; private set; }

        /// <summary>
        ///     Gets all logical devices by a criterion asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task<IEnumerable<IRuntimeDeviceViewModel>> GetAllDevicesAsync()
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


        #endregion [IRuntimeModeFacadeService members]

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

        private void OnDispose()
        {
            this._persistanceService = null;
            this._driversService = null;
        }

        private async Task OnInitializating()
        {
            await AsyncInitialization.EnsureInitializedAsync(this._persistanceService, this._driversService);

            this._devicesCache = new ConcurrentDictionary<Guid, IRuntimeDeviceViewModel>();
            await this.UpdateCacheInternalAsync(this._devicesCache);
        }

        private async Task UpdateCacheInternalAsync(IDictionary<Guid, IRuntimeDeviceViewModel> devicesCache)
        {
            /*TODO: during the next refactoring stage this method should be launched only one time until it is done. While this method working no other threads can re-launch this method.*/
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
                catch (Exception ex)
                {

                    ex.ToString();
                    throw new Exception("UpdateCacheInternalAsync in DefaultRuntimeModeDevicesService");
                }
            }
        }

        private async Task<IRuntimeDeviceViewModel> RestoreDeviceFromMomentoAsync(Guid deviceId,
            IDeviceMomento deviceMomento)
        {

            if (deviceMomento == null) throw new ArgumentNullException("deviceMomento");
            var deviceType = deviceMomento.State.DeviceType;

            var deviceFactory = _container.Resolve<ILogicalDeviceViewModelFactory>(deviceType);
            if (deviceFactory == null) throw new UnknownLogicalDeviceTypeException(deviceType);

            var runtimeDeviceViewModel = deviceFactory.CreateRuntimeLogicalDeviceViewModel(deviceMomento,deviceId);
            if (runtimeDeviceViewModel == null) throw new LogicalDeviceException();

            return runtimeDeviceViewModel;
        }

        #endregion [Help members]
    }
}
