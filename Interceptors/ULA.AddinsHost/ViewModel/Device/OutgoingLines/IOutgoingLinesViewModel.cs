using System;

namespace ULA.AddinsHost.ViewModel.Device.OutgoingLines
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutgoingLinesViewModel:IDisposable
    {
        void Update();
        void SetConnectionLost();
    }
}