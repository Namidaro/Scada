using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.ViewModel.Factories
{
    public interface IResistorViewModelFactory
    {
        IResistorViewModel CreateResistorViewModel(object resistor,bool isEnabled);
    }
}