using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.Common.AsyncServices;

namespace ULA.Business.Infrastructure.PersistanceServices
{
    /// <summary>
    ///     Exposes logical deviceViewModel persistance functionality
    /// </summary>
    public interface IPersistanceService : IAsyncInitializationService, IDisposable
    {
        /// <summary>
        ///     Gets a collection of all deviceViewModel persistable contexts
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task<IEnumerable<KeyValuePair<Guid, IDevicePersistableContext>>> GetDevicePersistableContextsAsync();

        /// <summary>
        ///     Gets an instance of <see cref="IDevicePersistableContext" /> that represents persisting context for a deviceViewModel asynchronously
        /// </summary>
        /// <param name="deviceId">
        ///     An instance of <see cref="Guid" /> that represents current deviceViewModel unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task<IDevicePersistableContext> GetDevicePersistableContextAsync(Guid deviceId);

        /// <summary>
        ///     Gets a collection of all drivers persistable contexts
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task<IEnumerable<KeyValuePair<Guid, IDriverPersistableContext>>> GetDriversPersistableContextAsynk();

        /// <summary>
        ///     Gets an instance of <see cref="IDriverPersistableContext" /> that represents persisting context for a driver asynchronously
        /// </summary>
        /// <param name="driverId">
        ///     An instance of <see cref="Guid" /> that represents current driver unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task <IDriverPersistableContext> GetDriverPersistableContextAsync(Guid driverId);

        /// <summary>
        ///     Remove deviceViewModel from persistable context asynchronously
        /// </summary>
        /// <param name="deviceId">
        ///     An instance of <see cref="Guid" /> that represents current deviceViewModel unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task RemoveDevicePersistanbleContextAsync(Guid deviceId);

        /// <summary>
        ///     Remove driver from persistable context asynchronously
        /// </summary>
        /// <param name="driverId">A instance of <see cref="Guid"/> that represents current deviceViewModel unique identifier</param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        Task RemoveDriverPersistableContextAsynk(Guid driverId);
    }
}