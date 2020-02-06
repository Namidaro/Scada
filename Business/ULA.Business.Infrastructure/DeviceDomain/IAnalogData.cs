using System;

namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAnalogData
    {
        /// <summary>
        /// 
        /// </summary>
        int? SignalLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DateTime? DateTimeFromDevice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Action AnalogDataUpdated { get; set; }


    }
}