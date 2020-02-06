using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    ///     Exposes logical device factory functionality
    ///     Описывает функционал фабрики догических устройств
    /// </summary>
    public interface ILogicalDeviceFactory
    {
        /// <summary>
        ///     Creates an instance of <see cref="IRuntimeDeviceViewModel" />
        /// </summary>
        /// <param name="deviceMomento"></param>
        /// <returns>
        ///     Created instance of <see cref="IRuntimeDeviceViewModel" />
        /// </returns>
        ILogicalDevice CreateLogicalDevice(IDeviceMomento deviceMomento);

       
    }
}