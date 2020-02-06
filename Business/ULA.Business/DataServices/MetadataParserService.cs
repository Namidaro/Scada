using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public class MetadataParserService :Disposable, IMetadataParserService
    {
        private IRawBytesToDeviceStateParserFactory _rawBytesToDeviceStateParserFactory;


        public MetadataParserService(IRawBytesToDeviceStateParserFactory rawBytesToDeviceStateParserFactory)
        {
            _rawBytesToDeviceStateParserFactory = rawBytesToDeviceStateParserFactory;
        }


        #region Implementation of IMetadataParserService

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        public List<MetadataFromDevice> GetPartlyUpdateMetadata(IRuntimeDevice runtimeDevice)
        {
            List<MetadataFromDevice> metadataFromDevice = new List<MetadataFromDevice>();


            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.COMMAND_MANAGENT_ID_NAME);
            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            metadataFromDevice.Add(new MetadataFromDevice
            {
                Tag = DeviceStringKeys.DeviceTableTagKeys.COMMAND_MANAGENT_ID_NAME,
                DeviceId = runtimeDevice.DeviceId,
                DriverDataId =driverDataTable.Id,
                MetadataForTag = new EntityMetadata
                {
                    StartAddress = (ushort) address,
                    NumberOfPoints = (ushort) (countAddresses)
                },
                RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreatePartialModeParser()
                
            });
            return metadataFromDevice;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        public List<MetadataFromDevice> GetFullUpdateMetadata(IRuntimeDevice runtimeDevice)
        {
            List<MetadataFromDevice> metadataFromDevice = GetPartlyUpdateMetadata(runtimeDevice);


            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.DATETIME_ID_NAME);
            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            metadataFromDevice.Add(new MetadataFromDevice
            {
                Tag = DeviceStringKeys.DeviceTableTagKeys.DATETIME_ID_NAME,
                DeviceId = runtimeDevice.DeviceId,
                DriverDataId = driverDataTable.Id,
                MetadataForTag = new EntityMetadata
                {
                    StartAddress = (ushort)address,
                    NumberOfPoints = (ushort)(countAddresses)
                },
                RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreateDateTimeParser()

            });
            return metadataFromDevice;

        }

        public List<MetadataFromDevice> GetSignalUpdateMetadata(IRuntimeDevice runtimeDevice)
        {
            List<MetadataFromDevice> metadataFromDevice = new List<MetadataFromDevice>();


            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.SIGNAL_LEVEL_DATA_ID_NAME);
            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            metadataFromDevice.Add(new MetadataFromDevice
            {
                Tag = DeviceStringKeys.DeviceTableTagKeys.SIGNAL_LEVEL_DATA_ID_NAME,
                DeviceId = runtimeDevice.DeviceId,
                DriverDataId = driverDataTable.Id,
                MetadataForTag = new EntityMetadata
                {
                    StartAddress = (ushort)address,
                    NumberOfPoints = (ushort)(countAddresses)
                },
                RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreateSignalLevelParser()

            });
            return metadataFromDevice;
        }

        public List<MetadataFromDevice> GetDeviceSignatureMetadata(IRuntimeDevice runtimeDevice)
        {
            List<MetadataFromDevice> metadataFromDevice = new List<MetadataFromDevice>();


            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.DEVICE_SIGNATURE_DATA_ID_NAME);
            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            metadataFromDevice.Add(new MetadataFromDevice
            {
                Tag = DeviceStringKeys.DeviceTableTagKeys.DEVICE_SIGNATURE_DATA_ID_NAME,
                DeviceId = runtimeDevice.DeviceId,
                DriverDataId = driverDataTable.Id,
                MetadataForTag = new EntityMetadata
                {
                    StartAddress = (ushort)address,
                    NumberOfPoints = (ushort)(countAddresses)
                },
                RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreateDeviceSignatureParser()

            });
            return metadataFromDevice;
        }

        public List<MetadataFromDevice> GetAnalogsMetadata(IRuntimeDevice runtimeDevice)
        {
            List<MetadataFromDevice> metadataFromDevice = new List<MetadataFromDevice>();


            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceAnalogMetersTagKeys.ANALOG_ID);
            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            metadataFromDevice.Add(new MetadataFromDevice
            {
                Tag = DeviceStringKeys.DeviceAnalogMetersTagKeys.ANALOG_ID,
                DeviceId = runtimeDevice.DeviceId,
                DriverDataId = driverDataTable.Id,
                MetadataForTag = new EntityMetadata
                {
                    StartAddress = (ushort)address,
                    NumberOfPoints = (ushort)(countAddresses)
                },
                RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreatAnalogParser()

            });


            if (runtimeDevice.DeviceMomento.State.AnalogMeterType != DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE)
            {
                var driverDataTable1 = runtimeDevice.DriverMomento.State.DataContainer
                    .GetValue<IDriverDataTable>("DataTable")
                    .GetRowByName(DeviceStringKeys.DeviceAnalogMetersTagKeys.METER_DATE_TIME);
                var countAddresses1 = Convert.ToInt16(driverDataTable1.Properties["Length"].Value);
                var address1 = driverDataTable1.Properties["Address"].Value;
                metadataFromDevice.Add(new MetadataFromDevice
                {
                    Tag = DeviceStringKeys.DeviceAnalogMetersTagKeys.METER_DATE_TIME,
                    DeviceId = runtimeDevice.DeviceId,
                    DriverDataId = driverDataTable.Id,
                    MetadataForTag = new EntityMetadata
                    {
                        StartAddress = (ushort) address1,
                        NumberOfPoints = (ushort) (countAddresses1)
                    },
                    RawBytesToDeviceStateParser = _rawBytesToDeviceStateParserFactory.CreatAnalogDateTimeParser()

                });
            }


            return metadataFromDevice;
        }

        #endregion


        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            _rawBytesToDeviceStateParserFactory = null;
            base.OnDisposing();
        }

        #endregion
    }
}
