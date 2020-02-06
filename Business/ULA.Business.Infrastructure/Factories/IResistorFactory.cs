using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.Factories
{
    /// <summary>
    /// 
    /// </summary>
   public interface IResistorFactory
    {
        List<IResistor> CreateResistorsOnDevice(IConfiguratedDeviceSchemeTable configuratedDeviceSchemeTable,IRuntimeDevice runtimeDevice);

    }
}
