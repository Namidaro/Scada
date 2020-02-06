using System;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business;
using ULA.Business.Infrastructure;
using ULA.Devices.Presentation;
using ULA.Devices.Presentation.Runtime;
using ULA.Devices.Runo3.Business.Interfaces;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Runo3.Business
{
    /// <summary>
    ///     Repressents Runo3 logical deviceViewModel factory functionality
    ///     Представляет фабрику для РУНО-3 устройств в разных режимах
    /// </summary>
    public class Runo3LogicalDeviceViewModelFactory : ILogicalDeviceViewModelFactory
    {
        #region [Private fields]

        private readonly IIoCContainer _container;
        private readonly IRuno3DeviceFactory _runo3DeviceFactory;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="Runo3LogicalDeviceViewModelFactory" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> to use
        /// </param>
        public Runo3LogicalDeviceViewModelFactory(IIoCContainer container, IRuno3DeviceFactory runo3DeviceFactory)
        {
            this._container = container;
            _runo3DeviceFactory = runo3DeviceFactory;
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
        public IRuntimeDeviceViewModel CreateRuntimeLogicalDeviceViewModel(IDeviceMomento deviceMomento,Guid id)
        {
            RuntimeDeviceViewModel runtimeDeviceViewModel=this._container.Resolve<RuntimeDeviceViewModel>();
            runtimeDeviceViewModel.Model = _runo3DeviceFactory.CreateLogicalDevice(deviceMomento);
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
            return this._container.Resolve<Runo3ConfigLogicalDevice>();
        }

        #endregion [ILogicalDeviceViewModelFactory members]
    }
}