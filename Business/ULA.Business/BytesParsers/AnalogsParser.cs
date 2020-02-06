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
  public  class AnalogsParser : IRawBytesToDeviceStateParser
    {
        #region Implementation of IRawBytesToDeviceStateParser

        public void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
            if (runtimeDevice.AnalogMeter != null)
            {

                if (runtimeDevice.AnalogMeter.AnalogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE))
                {

                    IEnergomeraAnalogMeter energomeraAnalogMeter = runtimeDevice.AnalogMeter as IEnergomeraAnalogMeter;
                    energomeraAnalogMeter.VoltageA =
                        (double) runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_A].Formatter.Format(rawBytes);

                    energomeraAnalogMeter.VoltageB =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_B].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.VoltageC =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_C].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.CurrentA =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_A].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.CurrentB =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_B].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.CurrentC =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_С].Formatter.Format(rawBytes);


                    energomeraAnalogMeter.PowerA =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.POWER_A].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.PowerB =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.POWER_B].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.PowerC =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.POWER_C].Formatter.Format(rawBytes);



                    energomeraAnalogMeter.StoredEnergy =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.STORED_ENERGY].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.StoredEnergyForDay =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.STORED_ENERGY_FOR_DAY].Formatter.Format(rawBytes);
                    energomeraAnalogMeter.StoredEnergyForMonth =
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.STORED_ENERGY_FOR_MOUNTH].Formatter.Format(rawBytes);

                }

                if (runtimeDevice.AnalogMeter.AnalogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE))
                {
                    int koefA = runtimeDevice.DeviceMomento.State.TransformKoefA;
                    int koefB = runtimeDevice.DeviceMomento.State.TransformKoefB;
                    int koefC = runtimeDevice.DeviceMomento.State.TransformKoefC;



                    IMsa961AnalogMeter msa961AnalogMeter = runtimeDevice.AnalogMeter as IMsa961AnalogMeter;
                    msa961AnalogMeter.VoltageA =2*(double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_A_MSA961].Formatter.Format(rawBytes);

                    msa961AnalogMeter.VoltageB =2*
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_B_MSA961].Formatter.Format(rawBytes);
                    msa961AnalogMeter.VoltageC =2*
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.VOLTAGE_C_MSA961].Formatter.Format(rawBytes);
                    msa961AnalogMeter.CurrentA =GetCurrent(koefA,
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_A_MSA961].Formatter.Format(rawBytes));
                    msa961AnalogMeter.CurrentB = GetCurrent(koefB,
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_B_MSA961].Formatter.Format(rawBytes));
                    msa961AnalogMeter.CurrentC = GetCurrent(koefC,
                        (double)runtimeDevice.DeviceMomento.State.DataTable[
                            DeviceStringKeys.DeviceAnalogMetersTagKeys.CURRENT_С_MSA961].Formatter.Format(rawBytes));

                }
            }
            runtimeDevice.AnalogData.AnalogDataUpdated?.Invoke();

        }


      private  double GetCurrent(int koef, double valueFormatted)
      {
          return (valueFormatted / 10) * (koef / 5);
      }

        #endregion
    }
}