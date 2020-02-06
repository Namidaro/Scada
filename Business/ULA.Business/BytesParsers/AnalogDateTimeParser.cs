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
  public  class AnalogDateTimeParser: IRawBytesToDeviceStateParser
    {
        #region Implementation of IRawBytesToDeviceStateParser

        public void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
            if (runtimeDevice.AnalogMeter != null)
            {

                if (runtimeDevice.AnalogMeter.AnalogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE))
                {

                    IEnergomeraAnalogMeter energomeraAnalogMeter = runtimeDevice.AnalogMeter as IEnergomeraAnalogMeter;
                    energomeraAnalogMeter.Date =
                        runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.ANALOG_METER_DATE].Formatter.Format(rawBytes).ToString();

                    energomeraAnalogMeter.Time =
                        runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.ANALOG_METER_TIME].Formatter.Format(rawBytes).ToString();
                

                }


            }
            runtimeDevice.AnalogData.AnalogDataUpdated?.Invoke();

        }
        #endregion
    }
}
