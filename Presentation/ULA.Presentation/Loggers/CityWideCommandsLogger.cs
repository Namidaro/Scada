using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Log;
using ULA.Log.LoggerService;

namespace ULA.Presentation.Loggers
{
    public class CityWideCommandsLogger: LoggerServiceBase
    {

        private bool _isCommandOnSelectedDevices = false;

        private string GetDevicesString()
        {
            return _isCommandOnSelectedDevices ? "выбранных" : "всех";
        }

        public void SetIsCommandOnSelectedDevices(bool isCommandOnSelectedDevices)
        {
            _isCommandOnSelectedDevices = isCommandOnSelectedDevices;
        }
        public void SyncTime(NLog.Logger logger)
        {
            LogMessage($"Синхронизация времени на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);
        }

        public void AutoModeAllStarters(NLog.Logger logger)
        {
            LogMessage($"Включить авто-режим на всех пускателях на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void ManualModeAllStarters(NLog.Logger logger)
        {
            LogMessage($"Включить ручной-режим на всех пускателях на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void RepairModeAllStarters(NLog.Logger logger)
        {
            LogMessage($"Включить ремонт на всех пускателях на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }


        public void OffRepairModeAllStarters(NLog.Logger logger)
        {
            LogMessage($"Отключить режим ремонта на всех пускателях на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void NightLightingOn(NLog.Logger logger)
        {
            LogMessage($"Включить ночное освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void NightLightingOff(NLog.Logger logger)
        {
            LogMessage($"Отключить ночное освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void EveninglightingLightingOn(NLog.Logger logger)
        {
            LogMessage($"Включить вечернее освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void EveninglightingLightingOff(NLog.Logger logger)
        {
            LogMessage($"Отключить вечернее освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void FulllightingLightingOn(NLog.Logger logger)
        {
            LogMessage($"Включить полное освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void FulllightingLightingOff(NLog.Logger logger)
        {
            LogMessage($"Отключить полное освещение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void BackLightOn(NLog.Logger logger)
        {
            LogMessage($"Включить подсветку на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void BackLightOff(NLog.Logger logger)
        {
            LogMessage($"Отключить подсветку на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void IlluminationOn(NLog.Logger logger)
        {
            LogMessage($"Включить иллюминацию на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void IlluminationOff(NLog.Logger logger)
        {
            LogMessage($"Отключить иллюминацию на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }
        public void EnergySavingOn(NLog.Logger logger)
        {
            LogMessage($"Включить энергосбережение на {GetDevicesString()} устройствах", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void EnergySavingOff(NLog.Logger logger)
        {
            LogMessage($"Отключить энергосбережение на {GetDevicesString()} устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

        public void SuncTimeAllDevices(NLog.Logger logger)
        {
            LogMessage($"Синхронизировать время на всех устройствах.", logger, LogMessageTypes.DISPATCHER_MESSAGE);

        }

    }
}
