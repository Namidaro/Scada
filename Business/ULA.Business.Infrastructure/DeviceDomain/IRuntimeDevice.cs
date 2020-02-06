using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Driver.Context;
using ULA.ApplicationConnectionService;
using ULA.Business.Infrastructure.DataServices;

namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    /// модель устройства в режиме реального времени
    /// </summary>
    public interface IRuntimeDevice : ILogicalDevice
    {

        Task UpdateSignature();
        Task UpdateSignal();

        /// <summary>
        /// 
        /// </summary>
        string DeviceSignature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IDeviceDataCache DeviceDataCache { get; }

        /// <summary>
        /// 
        /// </summary>
        IDefectState DefectState { get; }


        /// <summary>
        /// 
        /// </summary>
        IDeviceCustomItems DeviceCustomItems { get; }



        IAnalogMeter AnalogMeter { get; set; }

        bool IsDeviceInitialized { get; }

        /// <summary>
        /// 
        /// </summary>
        IAnalogData AnalogData { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();


        /// <summary>
        /// пускатели на устройстве
        /// </summary>
        List<IStarter> StartersOnDevice { get; }

        /// <summary>
        /// 
        /// </summary>
        List<IResistor> ResistorsOnDevice { get; }
            /// <summary>
        /// 
        /// </summary>
        TcpDeviceConnection TcpDeviceConnection { get; }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isFull"></param>
        void SetUpdatingMode(bool isFull);
        /// <summary>
        /// 
        /// </summary>
        Action DeviceInitialized { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Action DeviceValuesUpdated { get; set; }

        
    }
}
