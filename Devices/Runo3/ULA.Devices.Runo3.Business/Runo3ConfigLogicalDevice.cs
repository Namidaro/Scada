using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.ConfigLogicalDevice;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.Formatters;

namespace ULA.Devices.Runo3.Business
{
    public class Runo3ConfigLogicalDevice : ConfigLogicalDevice
    {
        public Runo3ConfigLogicalDevice(IPersistanceService persistanceService) : base(persistanceService)
        {
        }

    }
}
