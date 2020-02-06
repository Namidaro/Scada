using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Metadata;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DataServices
{
    public class RawBytesToDeviceStateParserService : Disposable, IRawBytesToDeviceStateParserService
    {
        public RawBytesToDeviceStateParserService()
        {

        }

        #region Implementation of IRawBytesToDeviceStateParserService
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadataFromDevice"></param>
        /// <param name="runtimeDevice"></param>
        /// <param name="rawBytes"></param>
        public void ApplyReceivedBytesToDevice(MetadataFromDevice metadataFromDevice, IRuntimeDevice runtimeDevice, byte[] rawBytes)
        {
            try
            {
                metadataFromDevice.RawBytesToDeviceStateParser.ParseRawBytesToDeviceState(rawBytes, runtimeDevice);
            }
            catch (Exception e)
            {

            }
        }

        #endregion
    }
}
