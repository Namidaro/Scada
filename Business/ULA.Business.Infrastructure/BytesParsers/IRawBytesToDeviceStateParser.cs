using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.BytesParsers
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRawBytesToDeviceStateParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawBytes"></param>
        /// <param name="runtimeDevice"></param>
        void ParseRawBytesToDeviceState(byte[] rawBytes, IRuntimeDevice runtimeDevice);
    }
}