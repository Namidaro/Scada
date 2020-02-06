using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.Business.Infrastructure.LoggerServices;
using ULA.Log;
using ULA.Log.LoggerService;

namespace ULA.Business.LoggerServices
{
    public class DefectLoggerService : LoggerServiceBase
    {

        public void PowerChainsDefectChanged(bool isManagementDefect, Logger logger)
        {
            var stateSting = isManagementDefect ? "неисправно" : "исправно";
            LogMessage($"Цепи питания: {stateSting}", logger, LogMessageTypes.FAIL_MESSAGE);
        }

        public void ProtectionDefectChanged(bool isProtectionDefect, Logger logger)
        {
            var stateSting = isProtectionDefect ? "нарушение" : "в норме";
            LogMessage($"Охрана: {stateSting}", logger, LogMessageTypes.FAIL_MESSAGE);
        }

        public void ManagementDefectChanged(bool isManagementDefect, Logger logger)
        {
            var stateSting = isManagementDefect ? "местное" : "дистанционное";
            LogMessage($"Ключ ЦУ: {stateSting}", logger, LogMessageTypes.FAIL_MESSAGE);
        }
        public void FusesDefectChanged(bool isResistorDefect, Logger logger)
        {
            var stateSting = isResistorDefect ? "неисправно" : "исправно";
            LogMessage($"Предохранители: {stateSting}", logger, LogMessageTypes.FAIL_MESSAGE);
        }
        public void ManagmentChainsDefectChanged(bool isManagementChainsDefect, Logger logger)
        {
            var stateSting = isManagementChainsDefect ? "неисправно" : "исправно";
            LogMessage($"Управление пускателями:: {stateSting}", logger, LogMessageTypes.FAIL_MESSAGE);
        }

    }
}
