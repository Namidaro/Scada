using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DTOs;
using ULA.Common.AsyncServices;

namespace ULA.Business.Infrastructure.ApplicationModes
{
    /// <summary>
    ///     Exposes a runtime mode business logic facade service functionality
    /// </summary>
    public interface IRuntimeModeDevicesServices : IAsyncInitializationService, IDisposable
    {
        /// <summary>
        ///     Gets all logical devices by a criterion asynchronously
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task<IEnumerable<IRuntimeDeviceViewModel>> GetAllDevicesAsync();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBackgroundPicture GetBackgroundPicture();
    }
}
