using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.ViewModel.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStarterViewModel
    {
        /// <summary>
        /// описпание пускателя
        /// </summary>
        string StarterDescription { get; set; }
        /// <summary>
        /// номер пускателя
        /// </summary>
        int StarterNumber { get; set; }

        /// <summary>
        /// режим ремонта
        /// </summary>
        bool? IsInRepairState { get; set; }

        /// <summary>
        /// ручной режим
        /// </summary>
        bool? IsInManualMode { get; set; }

        /// <summary>
        /// включенное состояние
        /// </summary>
        bool? IsStarterOn { get; set; }
        /// <summary>
        /// состояние управления
        /// </summary>
        bool? ManagementFuseState { get; set; }

        /// <summary>
        /// неопределенное состояние
        /// </summary>
        bool IsInUndefinedState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// 
        IDeviceCommandStateViewModel DeviceCommandStateViewModel { get; set; }


       // CommandTypesEnum? CommandOnStarter 

            /// <summary>
            /// 
            /// </summary>
       object Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Update();
        /// <summary>
        /// 
        /// </summary>
        void SetLostConnection();
    }
}