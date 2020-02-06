using System;
using ULA.Business.Infrastructure.BytesParsers;

namespace ULA.Business.Infrastructure.Metadata
{
    /// <summary>
    /// 
    /// </summary>
    public class MetadataFromDevice
    {
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EntityMetadata MetadataForTag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DriverId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DriverDataId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IRawBytesToDeviceStateParser RawBytesToDeviceStateParser { get; set; }
    }
}
