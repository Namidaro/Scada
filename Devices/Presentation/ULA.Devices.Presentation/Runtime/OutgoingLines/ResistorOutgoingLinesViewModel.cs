using System.Collections.ObjectModel;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime.OutgoingLines
{
   public class ResistorOutgoingLinesViewModel:DisposableBindableBase,IOutgoingLinesViewModel
    {
        public ResistorOutgoingLinesViewModel()
        {
            ResistorViewModels=new ObservableCollection<IResistorViewModel>();
        }



       public ObservableCollection<IResistorViewModel> ResistorViewModels { get; }

        #region Implementation of IOutgoingLinesViewModel

        public void Update()
        {
            ResistorViewModels.ForEach((model => model.UpdateStates()));
        }

        public void SetConnectionLost()
        {
            ResistorViewModels?.ForEach((model =>model.SetLostConnection() ));
        }

        #endregion
    }
}
