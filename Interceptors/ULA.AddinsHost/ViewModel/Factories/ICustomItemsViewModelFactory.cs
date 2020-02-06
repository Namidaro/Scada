using ULA.AddinsHost.ViewModel.Device.CustomItems;

namespace ULA.AddinsHost.ViewModel.Factories
{
    public interface ICustomItemsViewModelFactory
    {
        ICustomItemsViewModel CreateCustomItemsViewModel(object deviceCustomItems);
    }
}