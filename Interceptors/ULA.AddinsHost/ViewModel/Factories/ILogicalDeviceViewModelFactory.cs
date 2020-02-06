using System;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.ViewModel.Factories
{
    /// <summary>
    ///     Exposes logical device factory functionality
    ///     Описывает функционал фабрики догических устройств
    /// </summary>
    public interface ILogicalDeviceViewModelFactory
    {
        /// <summary>
        ///     Creates an instance of <see cref="IRuntimeDeviceViewModel" />
        /// </summary>
        /// <param name="deviceMomento"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Created instance of <see cref="IRuntimeDeviceViewModel" />
        /// </returns>
        IRuntimeDeviceViewModel CreateRuntimeLogicalDeviceViewModel(IDeviceMomento deviceMomento,Guid id);

        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDevice" />
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="IConfigLogicalDevice" />
        /// </returns>
        IConfigLogicalDevice CreateConfigLogicalDevice();
    }
}