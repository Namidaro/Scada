using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Devices.Presentation.Interfaces
{
    public interface IStarterViewModelFactory
    {
        IStarterViewModel CreateStarterViewModel(IStarter starter);
    }
}
