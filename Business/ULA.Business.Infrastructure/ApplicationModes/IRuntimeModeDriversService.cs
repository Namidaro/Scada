using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver;
using ULA.Business.Infrastructure.DTOs;
using ULA.Common.AsyncServices;

namespace ULA.Business.Infrastructure.ApplicationModes
{
    /// <summary>
    ///     Exposes drivers logic service for the runtime mode
    /// </summary>
    public interface IRuntimeModeDriversService : IAsyncInitializationService, IDisposable
    {
        /// <summary>
        ///     Gets all configuration logical drivers that registered in the system asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IEnumerable<IRuntimeLogicalDriver>> GetAllDriversAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IRuntimeLogicalDriver> GetDriverById(Guid id);

    }
}
