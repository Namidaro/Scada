using System;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommandFactory
    {
        /// <summary>
        /// создать комманду синхранозации времени
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateSyncTimeCommand(DateTime dateTime, IRuntimeDevice runtimeDevice);

      /// <summary>
        /// создать комманду включения ночного освещения
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunNightlightingCommand(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// создать комманду выключения ночного освещения
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunEveninglightingCommand(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// создать комманду выключения ночного освещения
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopEveninglightingCommand(IRuntimeDevice runtimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopNightlightingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunFullLightingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopFullLightingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunBackLightLightingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopBackLightLightingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunIlluminationCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopIlluminationCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateRunAutoModeCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopAutoModeCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStartRepairCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopRepairCommand(IRuntimeDevice modelParentRuntimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopAllStartersCommand(IRuntimeDevice modelParentRuntimeDevice);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStartEnergySavingCommand(IRuntimeDevice modelParentRuntimeDevice);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelParentRuntimeDevice"></param>
        /// <returns></returns>
        IDeviceCommand CreateStopEnergySavingCommand(IRuntimeDevice modelParentRuntimeDevice);


    }

}