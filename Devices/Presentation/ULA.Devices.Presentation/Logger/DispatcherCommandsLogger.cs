using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Log;
using ULA.Log.LoggerService;

namespace ULA.Devices.Presentation.Logger
{
   public class DispatcherCommandsLogger: LoggerServiceBase
    {
        public void SyncTime(NLog.Logger logger)
        {
            LogMessage("Синхронизация времени на устройстве",logger, LogMessageTypes.DISPATCHER_MESSAGE);
        }

        public void AutoModeAllStarters(NLog.Logger logger)
        {
            LogMessage("Включить авто-режим на всех пускателях", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void ManualModeAllStarters(NLog.Logger logger)
        {
            LogMessage("Включить ручной-режим на всех пускателях", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void RepairModeAllStarters(NLog.Logger logger)
        {
            LogMessage("Включить ремонт на всех пускателях", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void StopAllStarters(NLog.Logger logger)
        {
            LogMessage("Включить ремонт на всех пускателях", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }


        public void OffRepairModeAllStarters(NLog.Logger logger)
        {
            LogMessage("Отключить режим ремонта на всех пускателях", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

       public void NightLightingOn(NLog.Logger logger)
        {
            LogMessage("Включить ночное освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void NightLightingOff(NLog.Logger logger)
        {
            LogMessage("Отключить ночное освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
    public void EveninglightingLightingOn(NLog.Logger logger)
        {
            LogMessage("Включить вечернее освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void EveninglightingLightingOff(NLog.Logger logger)
        {
            LogMessage("Отключить вечернее освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

    public void FulllightingLightingOn(NLog.Logger logger)
        {
            LogMessage("Включить полное освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void FulllightingLightingOff(NLog.Logger logger)
        {
            LogMessage("Отключить полное освещение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
    public void BackLightOn(NLog.Logger logger)
        {
            LogMessage("Включить подсветку", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void BackLightOff(NLog.Logger logger)
        {
            LogMessage("Отключить подсветку.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

    public void IlluminationOn(NLog.Logger logger)
        {
            LogMessage("Включить иллюминацию", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void IlluminationOff(NLog.Logger logger)
        {
            LogMessage("Отключить иллюминацию.", logger, LogMessageTypes.DISPATCHER_MESSAGE);
        }



        public void EnergySavingOn(NLog.Logger logger)
        {
            LogMessage("Включить энергосбережение", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void EnergySavingOff(NLog.Logger logger)
        {
            LogMessage("Отключить энергосбережение.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }


    }
}
