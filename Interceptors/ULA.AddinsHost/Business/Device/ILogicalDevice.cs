using System;
using ULA.AddinsHost.Business.Driver;

namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    ///     Exposes basic logical device functionality
    ///     Описывает базовый функционал логического устройства
    /// </summary>
    public interface ILogicalDevice : IDisposable
    {
        /// <summary>
        ///     Gets or sets an instance of <see cref="Guid" /> that represents device unique identifier
        ///     Id устройства
        /// </summary>
        Guid DeviceId { get; set; }

        /// <summary>
        ///     Gets the name of the device
        ///     Имя устройства
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        ///     Gets the desciption of the device
        ///     Описание устройства
        /// </summary>
        string DeviceDescription { get; }
        /// <summary>
        /// 
        /// </summary>
        IDeviceMomento DeviceMomento { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IDriverMomento DriverMomento { get; set; }



        /// <summary>
        /// номер устройства
        /// </summary>
        int DeviceNumber { get; set; }
    }
}