using ULA.Interceptors;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommandStateViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        CommandTypesEnum CommandType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsCommandSending { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCommandSent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool? IsCommandSucceed { get; set; }



        /// <summary>
        /// 
        /// </summary>
        object Model { get; set; }
    }
}