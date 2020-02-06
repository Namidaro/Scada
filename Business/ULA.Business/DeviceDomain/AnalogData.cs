using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain
{
  public  class AnalogData: IAnalogData
    {

        public AnalogData()
        {
            DateTimeFromDevice = null;
            SignalLevel = null;
        }


        #region Implementation of IAnalogData

        public int? SignalLevel { get; set; }
        public DateTime? DateTimeFromDevice { get; set; }
        public Action AnalogDataUpdated { get; set; }

        #endregion
    }
}
