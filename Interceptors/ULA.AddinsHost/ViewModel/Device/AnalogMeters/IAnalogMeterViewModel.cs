using System;

namespace ULA.AddinsHost.ViewModel.Device
{
    public interface IAnalogMeterViewModel:IDisposable
    {
        object Model { get; set; }
        void Update();
        bool IsViewShowing { get;  }
        void ShowView();
        void SetDevice(IRuntimeDeviceViewModel runtimeDeviceViewModel);
    }
}