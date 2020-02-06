using System;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.TimerInterrogation
{

    /// <summary>
    /// 
    /// </summary>
    public interface ITimerInterrigationService:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsInterrogationInProcess { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> StartInterrogation();

        /// <summary>
        /// 
        /// </summary>
        void StopInterrogation();

        /// <summary>
        /// 
        /// </summary>
        Action InterrogationCycleComplete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        void SetDeviceForInterrogation(IRuntimeDevice runtimeDevice);

    
    }
}