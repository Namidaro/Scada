using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;

namespace ULA.AddinsHost.ViewModel.Factories
{
    public interface IOutgoingLinesViewModelFactory
    {
        IOutgoingLinesViewModel CreateOutgoingLinesViewModel(object runtimeDevice);
    }
}