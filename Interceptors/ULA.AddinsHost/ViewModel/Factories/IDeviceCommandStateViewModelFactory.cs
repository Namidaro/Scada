using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.ViewModel.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommandStateViewModelFactory
    {/// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
        IDeviceCommandStateViewModel CreateDeviceCommandStateViewModel(object command);
    }
}