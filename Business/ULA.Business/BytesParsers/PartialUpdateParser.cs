using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Business.Strings;
using ULA.Business.Infrastructure.BytesParsers;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;

namespace ULA.Business.BytesParsers
{
    public class PartialUpdateParser : IRawBytesToDeviceStateParser
    {

        public PartialUpdateParser()
        {

        }


        #region Implementation of IRawBytesToDeviceStateParser

        public virtual void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice)
        {
            runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                .Value = runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                .Formatter.Format(rawBytes);
            bool state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.MANAGEMENT_DEFECT]
                .Formatter.Format(rawBytes);
            runtimeDevice.DefectState.IsManagementDefect = state;

            state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.MANAGEMENT_CHAINS_DEFECT]
               .Formatter.Format(rawBytes);
            runtimeDevice.DefectState.IsManagementChainsDefect = state;

            state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.FUSES_DEFECT]
               .Formatter.Format(rawBytes);
            runtimeDevice.DefectState.IsFusesDefect = state;

            state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.PROTECTION_DEFECT]
               .Formatter.Format(rawBytes);
            runtimeDevice.DefectState.IsProtectionDefect = state;

            state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.POWER_DEFECT]
               .Formatter.Format(rawBytes);
            runtimeDevice.DefectState.IsNoPowerDefect = state;

            if (runtimeDevice.DeviceMomento.State.SchemeTable == null) return;




            var resitors = runtimeDevice.ResistorsOnDevice;

            foreach (var resistor in resitors)
            {
                try
                {
                    var stateRes = runtimeDevice.DeviceMomento.State.DataTable.GetObjectByTag(
                         AddressesStrings.MODUL_STR + resistor.ModuleNumber + AddressesStrings.SPLITTER_STR +
                         AddressesStrings.DISKRET_STR + resistor.ResistorNumber + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR).Formatter.Format(rawBytes);
                    var failRes = runtimeDevice.DeviceMomento.State.DataTable.GetObjectByTag(
                        AddressesStrings.MODUL_STR + resistor.ModuleNumber + AddressesStrings.SPLITTER_STR +
                        AddressesStrings.DISKRET_STR + resistor.ResistorNumber + AddressesStrings.SPLITTER_STR + AddressesStrings.FAIL_STR).Formatter.Format(rawBytes);
                    resistor.IsOnState = (bool)stateRes;
                    resistor.IsDefectState = (bool)failRes;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);

                }
            }

            if (runtimeDevice.DeviceCustomItems.IsCascadeEnabled)
            {
                var cascades = runtimeDevice.DeviceCustomItems.Cascades;
                foreach (var cascade in cascades)
                {
                    var cascadeRow =
                        runtimeDevice.DeviceMomento.State.CustomDeviceSchemeTable.CascadeIndicatorsTable.GetObjectByTag(
                            cascade.SignalDescription);
                    var stateRes = runtimeDevice.DeviceMomento.State.DataTable.GetObjectByTag(
                        AddressesStrings.MODUL_STR + cascadeRow.ResistorModule + AddressesStrings.SPLITTER_STR +
                        AddressesStrings.DISKRET_STR + (cascadeRow.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR).Formatter.Format(rawBytes);
                    cascade.SignalState = (bool)stateRes;
                }
            }

            if (runtimeDevice.DeviceCustomItems.IsIndicatorsEnabled)
            {
                var indicators = runtimeDevice.DeviceCustomItems.Indicators;
                foreach (var indicator in indicators)
                {
                    var indicatorRow =
                        runtimeDevice.DeviceMomento.State.CustomDeviceSchemeTable.IndicatorsTable.GetObjectByTag(
                            indicator.SignalDescription);
                    var stateRes = runtimeDevice.DeviceMomento.State.DataTable.GetObjectByTag(
                        AddressesStrings.MODUL_STR + indicatorRow.ResistorModule + AddressesStrings.SPLITTER_STR +
                        AddressesStrings.DISKRET_STR + (indicatorRow.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR).Formatter.Format(rawBytes);
                    indicator.SignalState = (bool)stateRes;
                }
            }

            if (runtimeDevice.DeviceCustomItems.IsSignalsEnabled)
            {
                var signals = runtimeDevice.DeviceCustomItems.Signals;
                foreach (var signal in signals)
                {
                    var signalRow =
                        runtimeDevice.DeviceMomento.State.CustomDeviceSchemeTable.SignalsTable.GetObjectByTag(
                            signal.SignalDescription);
                    var stateRes = runtimeDevice.DeviceMomento.State.DataTable.GetObjectByTag(
                        AddressesStrings.MODUL_STR + signalRow.ResistorModule + AddressesStrings.SPLITTER_STR +
                        AddressesStrings.DISKRET_STR + (signalRow.ResistorNumber - 1) + AddressesStrings.SPLITTER_STR + AddressesStrings.STATE_STR).Formatter.Format(rawBytes);
                    signal.SignalState = (bool)stateRes;
                }
            }





            var deviceStarterRows = runtimeDevice.DeviceMomento.State.SchemeTable.GetChannelsEnumerable();
            foreach (var deviceStarterRow in deviceStarterRows)
            {
                IStarter starter =
                    runtimeDevice.StartersOnDevice.FirstOrDefault(
                        (st => st.StarterNumber == deviceStarterRow.StarterId));

                state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.STARTER_STATE_PATTERN + starter.ChannelNumber]
                     .Formatter.Format(rawBytes);
                starter.IsStarterOn = state;

                state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.STARTER_IS_MANUAL_MODE_PATTERN + starter.ChannelNumber]
                    .Formatter.Format(rawBytes);
                starter.IsInManualMode = state;

                state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.STARTER_IS_REPAIR + starter.ChannelNumber]
                    .Formatter.Format(rawBytes);
                starter.IsInRepairState = state;

                state = (bool)runtimeDevice.DeviceMomento.State.DataTable[DeviceStringKeys.DeviceTableTagKeys.STARTER_MANAGEMENT_CONTROL + starter.ChannelNumber]
                        .Formatter.Format(rawBytes);
                starter.ManagementFuseState = state;


            }
        }


        #endregion
    }
}
