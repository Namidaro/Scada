using System;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommandService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceCommand"></param>
        /// <returns></returns>
        Task AddDeviceCommandCreator(Func<IDeviceCommand> deviceCommand,IRuntimeDeviceViewModel runtimeDeviceViewModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDeviceViewModel"></param>
        Task TryExecuteCommandOnDevice(IRuntimeDeviceViewModel runtimeDeviceViewModel);
        
    }
}