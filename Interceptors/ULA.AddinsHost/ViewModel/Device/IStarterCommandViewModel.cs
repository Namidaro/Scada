using ULA.Interceptors;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStarterCommandViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        CommandTypesEnum FinalCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCommandWaitingForAnswer { get; set; }
    }
}