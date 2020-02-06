using System;
using System.Threading.Tasks;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAnalogDataViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        int? SignalLevel { get; }
        /// <summary>
        /// 
        /// </summary>
        DateTime? DateTimeFromDevice { get; }
        /// <summary>
        /// 
        /// </summary>
        Task UpdateSignalLevel();
        /// <summary>
        /// 
        /// </summary>
        object Model { get; set; }
    }
}