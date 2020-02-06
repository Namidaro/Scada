using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.Metadata;
using ULA.Interceptors;

namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommand
    {
        /// <summary>
        /// 
        /// </summary>
        List<IStarter> Starters { get;  }

        /// <summary>
        /// 
        /// </summary>
        CommandTypesEnum CommandType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule"></param>
        void SetRule(ICommandSuccessRule rule);

        /// <summary>
        /// тэги комманды
        /// </summary>
        string[] CommandTags { get; }
        /// <summary>
        /// значения комманды
        /// </summary>
        object[] CommandValues { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCommandSent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool? IsCommandSucceed { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCommandStartSending { get; set; }

        

        /// <summary>
        /// 
        /// </summary>
        EntityMetadata EntityMetadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        void CheckIfCommandSucceed(IRuntimeDeviceViewModel runtimeDeviceViewModel);   
        
        /// <summary>
        /// событие на изменение состояние текущей комманды
        /// </summary>
        Action CurrectCommandStateChanged { get; set; }
    }
}