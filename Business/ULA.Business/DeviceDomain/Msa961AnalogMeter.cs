using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain
{
   public class Msa961AnalogMeter : IMsa961AnalogMeter
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
       
     
        #endregion
    }
}
