using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;

namespace ULA.Devices.Presentation.Runtime.OutgoingLines
{
    public class FiderViewModel : IFiderViewModel
    {

        public FiderViewModel()
        {
            ResistorViewModels = new ObservableCollection<IResistorViewModel>();
        }
        public ObservableCollection<IResistorViewModel> ResistorViewModels { get; }
        public string FiderSignature { get; set; }
        public void Update()
        {
            ResistorViewModels.ForEach((model => model.UpdateStates()));
        }

        public void SetLostConnection()
        {
            ResistorViewModels.ForEach((model => model.SetLostConnection()));
        }
    }
}
