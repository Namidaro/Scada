using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.ApplicationConnectionService;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors;

namespace ULA.Business.Infrastructure.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandSendingService
    {

        /// <summary>
        /// попытаться выполнить команду
        /// </summary>
        Task<bool> TryExecuteCommand(IDeviceCommand deviceCommand,IRuntimeDevice runtimeDevice);

     
    }
}