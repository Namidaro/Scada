using ULA.AddinsHost.Business.Device.Context;

namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    ///     Exposes device momento structure
    /// </summary>
    public interface IDeviceMomento
    {
        /// <summary>
        ///     Gets the state of a momento object
        /// </summary>
        IDeviceContext State { get; }

    }
}