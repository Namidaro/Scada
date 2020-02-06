using System;
using System.Collections.ObjectModel;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime.OutgoingLines
{
   public class FidersOutgoingLinesViewModel:DisposableBindableBase,IOutgoingLinesViewModel
    {

        public FidersOutgoingLinesViewModel()
        {
            FiderViewModels = new ObservableCollection<IFiderViewModel>();
        }


        #region Implementation of IOutgoingLinesViewModel

        public void Update()
        {
            FiderViewModels.ForEach((model) =>
            {
                model.Update();
            });
        }

        public void SetConnectionLost()
        {
            FiderViewModels.ForEach((model) =>
            {
                model.SetLostConnection();
            });
        }


        public ObservableCollection<IFiderViewModel> FiderViewModels { get; }

        #endregion
    }
}
