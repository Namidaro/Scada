using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Interceptors.Application;

namespace ULA.Business.DataServices
{
    public class DefectAcknowledgingService : IDefectAcknowledgingService
    {
        private readonly IApplicationSettingsService _applicationSettingsService;
        private IRuntimeDevice _runtimeDevice;
        private byte[] _previousAckBytes;
        private byte[] _currentAckBytes;
        private bool _isFailAcknowledged;


        public DefectAcknowledgingService(IApplicationSettingsService applicationSettingsService)
        {
            _applicationSettingsService = applicationSettingsService;
            _isFailAcknowledged = false;
        }


        #region Implementation of IDefectAcknowledgingService

        public void Initialize(IRuntimeDevice runtimeDevice)
        {
            runtimeDevice.DeviceValuesUpdated += CheckData;
            _runtimeDevice = runtimeDevice;
        }

        private void CheckData()
        {
            if (_previousAckBytes == null)
            {
                _currentAckBytes = _runtimeDevice.DeviceMomento.State
                    .DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                    .Value as byte[];
                if(_currentAckBytes==null)return;
                MakeBytesEqual();
            }
            else
            {
                if (!IsBytesEquals(_previousAckBytes, _runtimeDevice.DeviceMomento.State
                    .DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                    .Value as byte[]))
                {

                    _currentAckBytes = _runtimeDevice.DeviceMomento.State
                        .DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                        .Value as byte[];
                   
                    if (!_runtimeDevice.DefectState.HasAnyDefect)
                    {
                        if (_isFailAcknowledged)
                        {
                            MakeBytesEqual();
                        }
                        if (_applicationSettingsService.AcknowledgeEnabled)
                        {
                            MakeBytesEqual();
                        }
                    }
                    _isFailAcknowledged = false;
                }
                else
                {
                    if (_applicationSettingsService.AcknowledgeEnabled)
                    {
                        _currentAckBytes = _runtimeDevice.DeviceMomento.State
                            .DataTable[DeviceStringKeys.DeviceTableTagKeys.ACKNOWLEDGE_VALUE]
                            .Value as byte[];
                    }
                }
                AcknowledgeValueChanged?.Invoke();
            }
        }


        private void MakeBytesEqual()
        {
            _previousAckBytes = new byte[] { _currentAckBytes[0], _currentAckBytes[1] };

        }

        private bool IsBytesEquals()
        {

            if (_currentAckBytes[0].Equals(_previousAckBytes[0]) &&
                _currentAckBytes[1].Equals(_previousAckBytes[1])) return true;
            return false;
        }

        private bool IsBytesEquals(byte[] bytes1, byte[] bytes2)
        {

            if (bytes1[0].Equals(bytes2[0]) &&
                bytes1[1].Equals(bytes2[1])) return true;
            return false;
        }



        public bool IsFailNeedsAcknowledge()
        {
            if ((_currentAckBytes == null) || (_previousAckBytes == null)) return false;
            return !IsBytesEquals();
        }

        public void AcknowledgeFail()
        {
            MakeBytesEqual();
            _isFailAcknowledged = true;
            AcknowledgeValueChanged?.Invoke();

        }

        public Action AcknowledgeValueChanged { get; set; }

        #endregion
    }
}
