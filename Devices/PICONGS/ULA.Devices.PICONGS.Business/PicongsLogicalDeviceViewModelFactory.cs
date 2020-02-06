using System;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business;
using ULA.Business.Infrastructure;
using ULA.Devices.PICONGS.Business.Factories;
using ULA.Devices.Presentation.Runtime;
using ULA.Interceptors.IoC;

namespace ULA.Devices.PICONGS.Business
{
    /// <summary>
    ///     Repressents PICONGS logical device factory functionality
    ///     Представляет фабрику для PICONGS устройств в разных режимах
    /// </summary>
    public class PicongsLogicalDeviceViewModelFactory : ILogicalDeviceViewModelFactory
    {
        #region [Private fields]

        private readonly IIoCContainer _container;
        private readonly PiconGsDeviceFactory _piconGsDeviceFactory;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="PicongsLogicalDeviceViewModelFactory" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> to use
        /// </param>
        public PicongsLogicalDeviceViewModelFactory(IIoCContainer container, PiconGsDeviceFactory piconGsDeviceFactory)
        {
            this._container = container;
            _piconGsDeviceFactory = piconGsDeviceFactory;
        }

        #endregion [Ctor's]

        #region [ILogicalDeviceViewModelFactory members]

        /// <summary>
        ///     Creates an instance of <see cref="IRuntimeDeviceViewModel" />
        /// </summary>
        /// <param name="deviceMomento"></param>
        /// <returns>
        ///     Created instance of <see cref="IRuntimeDeviceViewModel" />
        /// </returns>
        public IRuntimeDeviceViewModel CreateRuntimeLogicalDeviceViewModel(IDeviceMomento deviceMomento, Guid id)
        {
            RuntimeDeviceViewModel runtimeDeviceViewModel = this._container.Resolve<RuntimeDeviceViewModel>();
            runtimeDeviceViewModel.Model = _piconGsDeviceFactory.CreateLogicalDevice(deviceMomento);
            runtimeDeviceViewModel.Model.DeviceId = id;
            return runtimeDeviceViewModel;
        }

        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDevice" />
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="IConfigLogicalDevice" />
        /// </returns>
        public IConfigLogicalDevice CreateConfigLogicalDevice()
        {
            return this._container.Resolve<PiconGsConfigLogicalDevice>();
        }

        #endregion [ILogicalDeviceViewModelFactory members]
    }
}