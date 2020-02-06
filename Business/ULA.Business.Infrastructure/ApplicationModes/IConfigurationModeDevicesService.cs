using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Presentation;
using ULA.Business.Infrastructure.DTOs;
using ULA.Common.AsyncServices;

namespace ULA.Business.Infrastructure.ApplicationModes
{
    /// <summary>
    ///     Exposes a configuration mode business logic facade service functionality
    /// </summary>
    public interface IConfigurationModeDevicesService : IAsyncInitializationService, IDisposable
    {
        /// <summary>
        ///     Gets all logical devices by a criterion asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IEnumerable<IConfigLogicalDevice>> GetAllDevicesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBackgroundPicture GetBackgroundPicture();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SetBackgroundPictureAsync(IBackgroundPicture picture);

        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDevice" /> asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IConfigLogicalDevice> CreateDeviceAsync(LogicalDeviceInformation deviceInfo);

        /// <summary>
        ///     Removes an instance of <see cref="IConfigLogicalDevice" /> from the system asynchronously
        /// </summary>
        /// <param name="device">
        ///     An instance of <see cref="IConfigLogicalDevice" /> to remove
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task RemoveDeviceAsync(IConfigLogicalDevice device);

        /// <summary>
        ///     Updates an instance of <see cref="IConfigLogicalDevice" /> in the system registry asynchronously
        /// </summary>
        /// <param name="device">
        ///     An instance of <see cref="LogicalDeviceInformation" /> to update
        /// </param>
        /// <param name="editingDevice">
        ///     An instance of <see cref="IConfigLogicalDevice" /> to update
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IConfigLogicalDevice> UpdateDeviceAsync(LogicalDeviceInformation device, IConfigLogicalDevice editingDevice);

        /// <summary>
        ///     Updates an instance of <see cref="IConfiguratedDeviceSchemeTable"/> in <see cref="IConfigLogicalDevice" /> in the system registry asynchronously
        /// </summary>
        /// <param name="device">An instance of <see cref="LogicalDeviceInformation" /> to update</param>
        /// <param name="scheme">New scheme. Instance of <see cref="IConfiguratedDeviceSchemeTable"/></param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task UpdateDeviceSchemeAsync(IConfigLogicalDevice device, IConfiguratedDeviceSchemeTable scheme);

        /// <summary>
        ///     Сохраняет новый номер позиции устройства в хранилище
        /// </summary>
        /// <param name="device">Устройство</param>
        /// <returns>задача</returns>
        Task UpdateDevicePositionAsync(IConfigLogicalDevice device,int newNumber);
    }
}