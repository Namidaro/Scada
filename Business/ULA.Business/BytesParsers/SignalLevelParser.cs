using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.BytesParsers;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;

namespace ULA.Business.BytesParsers
{
  public  class SignalLevelParser: IRawBytesToDeviceStateParser
    {
        #region Implementation of IRawBytesToDeviceStateParser

        public void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
            runtimeDevice.AnalogData.SignalLevel = int.Parse(runtimeDevice.DeviceMomento.State
                .DataTable[DeviceStringKeys.DeviceTableTagKeys.SIGNAL_LEVEL].Formatter.Format(rawBytes).ToString());
            runtimeDevice.AnalogData.AnalogDataUpdated?.Invoke();
        }

        #endregion
    }
}
