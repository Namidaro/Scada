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
   public class DateTimeParser: IRawBytesToDeviceStateParser
    {
        #region Implementation of IRawBytesToDeviceStateParser

        public void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
          //var r1=  (int) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_YEAR]
          //      .Formatter.Format(rawBytes);
          //  var r2 = int.Parse((string) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MONTH]
          //      .Formatter.Format(rawBytes));
          //  var r3 = int.Parse((string) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_DAY]
          //      .Formatter.Format(rawBytes));
          //  var r4 = int.Parse((string) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_HOUR]
          //      .Formatter.Format(rawBytes));
          //  var r5 = int.Parse((string) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MINUTE]
          //      .Formatter.Format(rawBytes));
          //  var r6 = int.Parse((string) runtimeDevice.DeviceMomento.State
          //      .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_SECOND]
          //      .Formatter.Format(rawBytes));




            runtimeDevice.AnalogData.DateTimeFromDevice = new DateTime(
                (int) runtimeDevice.DeviceMomento.State
                    .DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_YEAR]
                    .Formatter.Format(rawBytes),
               int.Parse((string) runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MONTH]
                    .Formatter.Format(rawBytes)),
                int.Parse((string)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_DAY]
                    .Formatter.Format(rawBytes)),
                    int.Parse((string)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_HOUR]
                    .Formatter.Format(rawBytes)),
                        int.Parse((string)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_MINUTE]
                    .Formatter.Format(rawBytes)),
                            int.Parse((string)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceDateTimeTagKeys.DATETIME_SECOND]
                    .Formatter.Format(rawBytes))
            );
            runtimeDevice.AnalogData.AnalogDataUpdated?.Invoke();

        }

        #endregion
    }
}
