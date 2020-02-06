using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.ApplicationConnectionService;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;
using ULA.Interceptors;
using YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination;

namespace ULA.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandSendingService : ICommandSendingService
    {

        private IDataWritingService _dataWritingService;
        private IDeviceCommand _previousDeviceCommand;


        public CommandSendingService(IDataWritingService dataWritingService)
        {
            _dataWritingService = dataWritingService;
        }


        #region Implementation of ICommandSendingService
    
        #endregion

        #region Implementation of ICommandSendingService

        public async Task<bool> TryExecuteCommand(IDeviceCommand deviceCommand,IRuntimeDevice runtimeDevice)
        {
            deviceCommand.IsCommandStartSending = true;
            deviceCommand.CurrectCommandStateChanged?.Invoke();
            _dataWritingService.SetDevice(runtimeDevice);
            if (!await _dataWritingService.WriteValues(deviceCommand.EntityMetadata, deviceCommand.CommandTags,
                deviceCommand.CommandValues))
            {
                return false;
            }
            deviceCommand.IsCommandSent = true;
            deviceCommand.CurrectCommandStateChanged?.Invoke();
            return true;
       }

  

        #endregion
    }
}
