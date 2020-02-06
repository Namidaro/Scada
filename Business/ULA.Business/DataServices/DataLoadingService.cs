using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class DataLoadingService :Disposable, IDataLoadingService
    {
        private  IMetadataParserService _metadataParserService;
        private  IRawBytesToDeviceStateParserService _rawBytesToDeviceStateParserService;


        private List<MetadataFromDevice> _partlyModeMetadata;
        private List<MetadataFromDevice> _fullModeMetadata;
        private List<MetadataFromDevice> _signalMetadata;
        private List<MetadataFromDevice> _signatureMetadata;
        private List<MetadataFromDevice> _analogMetadata;


        public DataLoadingService(IMetadataParserService metadataParserService, IRawBytesToDeviceStateParserService rawBytesToDeviceStateParserService)
        {
            _metadataParserService = metadataParserService;
            _rawBytesToDeviceStateParserService = rawBytesToDeviceStateParserService;
            _partlyModeMetadata = new List<MetadataFromDevice>();
            _fullModeMetadata = new List<MetadataFromDevice>();
            _signalMetadata=new List<MetadataFromDevice>();
            _signatureMetadata=new List<MetadataFromDevice>();
            _analogMetadata=new List<MetadataFromDevice>();
        }

        #region Implementation of IDataLoadingService

        public async Task<bool> UpdateDataFromDevicePartly(IRuntimeDevice runtimeDevice)
        {
            if (_partlyModeMetadata.Count == 0)
            {
              
                _partlyModeMetadata = _metadataParserService.GetPartlyUpdateMetadata(runtimeDevice);
            }
            foreach (var metadataFromDevice in _partlyModeMetadata)
            {
                if (!await LoadDataFromDevice(metadataFromDevice, runtimeDevice)) return false;
            }
            return true;
        }

        public async Task<bool> UpdateDataFromDeviceFull(IRuntimeDevice runtimeDevice)
        {
            if (_fullModeMetadata.Count == 0)
            {
                _fullModeMetadata = _metadataParserService.GetFullUpdateMetadata(runtimeDevice);
            }
            foreach (var metadataFromDevice in _fullModeMetadata)
            {
                if (!await LoadDataFromDevice(metadataFromDevice, runtimeDevice)) return false;
            }
            return true;
        }

        public async Task<bool> UpdateSignalLevel(IRuntimeDevice runtimeDevice)
        {
            if (_signalMetadata.Count == 0)
            {
                _signalMetadata = _metadataParserService.GetSignalUpdateMetadata(runtimeDevice);
            }
            foreach (var metadataFromDevice in _signalMetadata)
            {
                if (!await LoadDataFromDevice(metadataFromDevice, runtimeDevice)) return false;
            }
            return true;
        }

        public async Task<bool> UpdateDeviceSignature(IRuntimeDevice runtimeDevice)
        {
            if (_signatureMetadata.Count == 0)
            {
                _signatureMetadata = _metadataParserService.GetDeviceSignatureMetadata(runtimeDevice);
            }
            foreach (var metadataFromDevice in _signatureMetadata)
            {
                if (!await LoadDataFromDevice(metadataFromDevice, runtimeDevice)) return false;
                runtimeDevice.DeviceValuesUpdated?.Invoke();
            }
            return true;
        }

        public async Task<bool> UpdateAnalogs(IRuntimeDevice runtimeDevice)
        {
            if (_analogMetadata.Count == 0)
            {
                _analogMetadata = _metadataParserService.GetAnalogsMetadata(runtimeDevice);
            }
            foreach (var metadataFromDevice in _analogMetadata)
            {
                if (!await LoadDataFromDevice(metadataFromDevice, runtimeDevice)) return false;
            }
            return true;
        }


        private async Task<bool> LoadDataFromDevice(MetadataFromDevice metadataFromDevice, IRuntimeDevice runtimeDevice)
        {
            if (IsDisposed) return false;
            byte[] res = await runtimeDevice.TcpDeviceConnection.GetDataByAddressAsync(metadataFromDevice.MetadataForTag.StartAddress,
                metadataFromDevice.MetadataForTag.NumberOfPoints, metadataFromDevice.Tag);
            if (res == null) return false;
            runtimeDevice.DeviceDataCache.SaveBytesByMetadata(metadataFromDevice.MetadataForTag, res);
            _rawBytesToDeviceStateParserService?.ApplyReceivedBytesToDevice(metadataFromDevice, runtimeDevice, res);
            return true;
        }


        #endregion

        #region Overrides of Disposable

        protected override void OnDisposing()
        {


            _metadataParserService.Dispose();
            _rawBytesToDeviceStateParserService.Dispose();
            _metadataParserService = null;
            _rawBytesToDeviceStateParserService = null;
            _partlyModeMetadata = null;
            _fullModeMetadata = null;
            _signalMetadata = null;
            ;
            _signatureMetadata = null;
            base.OnDisposing();
        }

        #endregion
    }
}
