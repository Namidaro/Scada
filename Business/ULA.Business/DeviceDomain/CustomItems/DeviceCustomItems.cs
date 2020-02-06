using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain.CustomItems
{
public class DeviceCustomItems:IDeviceCustomItems
    {
        public DeviceCustomItems()
        {
            Cascades=new List<ICascade>();
            Signals=new List<ISignal>();
            Indicators=new List<IIndicator>();
        }


        #region Implementation of IDeviceCustomItems

        public bool IsSignalsEnabled { get; set; }
        public bool IsIndicatorsEnabled { get; set; }
        public bool IsCascadeEnabled { get; set; }
        public List<ICascade> Cascades { get; set; }
        public List<ISignal> Signals { get; set; }
        public List<IIndicator> Indicators { get; set; }

        #endregion
    }
}
