using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULA.Devices.PICON2.Business.Business
{
 public   class Picon2ModuleInfo
    {
      public  string ModuleFirmwareVersion { get; set; }
      public  string ModemVersion { get; set; }
      public  string ModemFirmwareVersion { get; set; }
      public  string ModemIMEI { get; set; }
    }
}
