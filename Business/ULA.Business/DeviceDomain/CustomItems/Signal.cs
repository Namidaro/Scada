using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.DeviceDomain.CustomItems
{
  public  class Signal:ISignal
    {
        #region Implementation of ISignal

        public string SignalDescription { get; set; }
        public bool SignalState { get; set; }

        #endregion
    }
}
