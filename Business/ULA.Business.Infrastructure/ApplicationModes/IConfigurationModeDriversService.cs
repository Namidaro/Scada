using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver;
using ULA.Business.Infrastructure.DTOs;
using ULA.Common.AsyncServices;

namespace ULA.Business.Infrastructure.ApplicationModes
{
    /// <summary>
    ///     Exposes drivers logic service for the configuration mode
    /// </summary>
    public interface IConfigurationModeDriversService : IAsyncInitializationService, IDisposable
    {
        /// <summary>
        ///     Creates an instance of <see cref="IConfigLogicalDriver" /> asynchronously
        /// </summary>
        /// <param name="logicalDriverInformation">
        ///     An instance of <see cref="LogicalDriverInformation" /> to obtain all information about deviceViewModel to create
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IConfigLogicalDriver> CreateDriverAsync(LogicalDriverInformation logicalDriverInformation);

        /// <summary>
        ///     Gets all configuration logical drivers that registered in the system asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IEnumerable<IConfigLogicalDriver>> GetAllDriversAsync();

        /// <summary>
        ///     Вернет драйвер по Id. Если драйвера с таким Id нет - то null
        /// </summary>
        /// <param name="id">Id драйвера</param>
        /// <returns>Task с драйвером.</returns>
        Task<IConfigLogicalDriver> GetDriverById(Guid id);

        /// <summary>
        ///     Removes an instance of <see cref="IConfigLogicalDriver" /> from the system asynchronously
        /// </summary>
        /// <param name="driverId">
        ///     An instance of <see cref="IConfigLogicalDriver" /> to remove
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task RemoveDriverAsync(Guid driverId);

        /// <summary>
        ///     Updates an instance of <see cref="IConfigLogicalDriver" /> in the system registry asynchronously
        /// </summary>
        /// <param name="driver">
        ///     An instance of <see cref="LogicalDeviceInformation" /> to update
        /// </param>
        /// <param name="editingDriver">
        ///     An instance of <see cref="IConfigLogicalDriver" /> to update
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IConfigLogicalDriver> UpdateDriverAsync(LogicalDriverInformation driver, IConfigLogicalDriver editingDriver);
    }
}