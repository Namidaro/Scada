using System;
using System.Collections.Generic;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;

namespace ULA.Business.Infrastructure.DataServices
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRawBytesToDeviceStateParserService:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadataFromDevice"></param>
        /// <param name="runtimeDevice"></param>
        /// <param name="rawBytes"></param>
        void ApplyReceivedBytesToDevice(MetadataFromDevice metadataFromDevice, IRuntimeDevice runtimeDevice,
            byte[] rawBytes);


    }
}