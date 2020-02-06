namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigDeviceViewModelFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IConfigDeviceViewModel CreateConfigDeviceViewModel(IConfigLogicalDevice configLogicalDevice);
    }
}