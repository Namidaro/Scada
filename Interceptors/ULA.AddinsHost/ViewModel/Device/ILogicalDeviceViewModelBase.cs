using System.ComponentModel;
using ULA.AddinsHost.Business.Device;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogicalDeviceViewModelBase:INotifyPropertyChanged
    {
        /// <summary>
        /// описание
        /// </summary>
        string DeviceDescription { get; set; }
        /// <summary>
        /// имя
        /// </summary>
        string DeviceName { get; set; }
        /// <summary>
        /// номер устройства
        /// </summary>
        int DeviceNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        ILogicalDevice Model { get; set; }


    }
}