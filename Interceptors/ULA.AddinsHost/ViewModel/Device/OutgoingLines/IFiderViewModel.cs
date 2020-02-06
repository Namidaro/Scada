using System.Collections.ObjectModel;

namespace ULA.AddinsHost.ViewModel.Device.OutgoingLines
{
    public interface IFiderViewModel
    {
        ObservableCollection<IResistorViewModel> ResistorViewModels { get; }
        string FiderSignature { get; set; }

        void Update();

        void SetLostConnection();
    }
}