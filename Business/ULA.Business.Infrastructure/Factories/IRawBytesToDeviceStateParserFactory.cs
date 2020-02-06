using ULA.Business.Infrastructure.BytesParsers;

namespace ULA.Business.Infrastructure.Factories
{/// <summary>
/// 
/// </summary>
    public interface IRawBytesToDeviceStateParserFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreatePartialModeParser();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreateDateTimeParser();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreateSignalLevelParser();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreateDeviceSignatureParser();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreatAnalogParser();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRawBytesToDeviceStateParser CreatAnalogDateTimeParser();

    }
}