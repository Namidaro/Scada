using System;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// сервис загрузки данных из устройства
    /// </summary>
    public interface IDataLoadingService:IDisposable
    {
        /// <summary>
        /// обновить данные частично
        /// </summary>
        /// <returns></returns>
        Task<bool> UpdateDataFromDevicePartly(IRuntimeDevice runtimeDevice);
        /// <summary>
        /// обновить данные полностью
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        Task<bool> UpdateDataFromDeviceFull(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        Task<bool> UpdateSignalLevel(IRuntimeDevice runtimeDevice);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        Task<bool> UpdateDeviceSignature(IRuntimeDevice runtimeDevice);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        Task<bool> UpdateAnalogs(IRuntimeDevice runtimeDevice);



    }
}