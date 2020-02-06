using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.Log;
using ULA.Log.LoggerService;

namespace ULA.Business.LoggerServices
{
  public  class ConnectionLogger:LoggerServiceBase
    {
        public void ConnectionLost(Logger logger)
        {
            LogMessage("Связь потеряна",logger,LogMessageTypes.CONNECTION_MESSAGE);
        }

        public void ConnectionResroted(Logger logger)
        {
            LogMessage("Связь восстановлена", logger, LogMessageTypes.CONNECTION_MESSAGE);
        }
    }
}
