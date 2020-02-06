using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.ViewModel.Factories
{
    public interface IAnalogMeterViewModelFactory
    {
        IAnalogMeterViewModel CreateAnalogMeterViewModel(IRuntimeDeviceViewModel runtimeDeviceViewModel,
            object analogMeter);
    }
}