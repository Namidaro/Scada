using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.Business.LoggerServices;
using ULA.Log;
using ULA.Log.LoggerService;

namespace ULA.Business.Infrastructure.LoggerServices
{

    public class StarterLoggerService: LoggerServiceBase
    {


        /// <summary>
        /// событие изменения свойства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StarterStateChanged(Logger logger, bool state, int starterNumber)
        {
            var stateSting = state ? "включенное" : "отключенное";
            LogMessage($"Пускатель {starterNumber} перешел в {stateSting} состояние",logger,LogMessageTypes.STARTER_MESSAGE);
        }

        public void IsAutoModeStateChanged(Logger logger, bool state, int starterNumber)
        {
            var stateSting = state ? "автоматический" : "ручной";
            LogMessage($"Пускатель {starterNumber} перешел в {stateSting} режим", logger, LogMessageTypes.STARTER_MESSAGE);
        }

        public void IsRepairStateChanged(Logger logger, bool state, int starterNumber)
        {
            var stateSting = state ? "перешел" : "вышел из";
            var stateAddStr= state ? "" : "а";
            LogMessage($"Пускатель {starterNumber} {stateSting} режим{stateAddStr} ремонта", logger, LogMessageTypes.STARTER_MESSAGE);
        }


      
    }
}

