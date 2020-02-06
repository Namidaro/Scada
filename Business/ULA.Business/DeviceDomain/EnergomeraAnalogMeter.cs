using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain
{
   public class EnergomeraAnalogMeter: IEnergomeraAnalogMeter
    {
        
        #region Implementation of IAnalogMeter

        public string AnalogMeterType { get; set; }

        #endregion


        #region Implementation of IEnergomeraAnalogMeter

        public double? VoltageA { get; set; }
        public double? VoltageB { get; set; }
        public double? VoltageC { get; set; }
        public double? CurrentA { get; set; }
        public double? CurrentB { get; set; }
        public double? CurrentC { get; set; }
        public double? PowerA { get; set; }
        public double? PowerB { get; set; }
        public double? PowerC { get; set; }
        public double? StoredEnergy { get; set; }
        public double? StoredEnergyForMonth { get; set; }
        public double? StoredEnergyForDay { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
        #endregion
    }
}
