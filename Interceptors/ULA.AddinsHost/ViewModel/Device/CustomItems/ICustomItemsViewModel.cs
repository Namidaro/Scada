using System.Collections.Generic;

namespace ULA.AddinsHost.ViewModel.Device.CustomItems
{
    public interface ICustomItemsViewModel
    {

       
        bool IsSignalsEnabled { get; set; }
        bool IsIndicatorsEnabled { get; set; }
        bool IsCascadeEnabled { get; set; }

        List<ICascadeViewModel> CascadeViewModels { get; set; }
        List<ISignalViewModel> SignalViewModels { get; set; }
        List<IIndicatorViewModel> IndicatorViewModels { get; set; }
        
        void Update();

        void SetLostConnection();
    }
}