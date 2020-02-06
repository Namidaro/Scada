using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DTOs;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceLogLoadingService:IDisposable
    {



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<ILogInformation>> ReadNextLogFromDevice();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        void Initialize(IRuntimeDevice runtimeDevice);



    }
}