using Microsoft.Practices.Prism.Logging;
using NLog;
using ULA.Interceptors;

namespace ULA.Business.Infrastructure.DeviceDomain
{
    /// <summary>
    ///  модель пускателя
    /// </summary>
    public interface IStarter
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
        /// неопределенное состояние
        /// </summary>
        bool IsInUndefinedState { get; set; }


        int ChannelNumber { get; set; }


        /// <summary>
        /// 
        /// </summary>
        LightingModeEnum StarterLightingMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool? ManagementFuseState { get; set; }

     //   StarterAddressKeys StarterAddressKeys { get;  }
        void SetLogger(Logger logger);
    }

    //public class StarterAddressKeys
    //{
    //    public string SwitchingStateKey { get; set; }
    //    public string SwitchingCommandKey { get; set; }

    //    public string ManualModeStateKey { get; set; }
    //    public string ManualModeCommandKey { get; set; }

    //    public string RepairStateKey { get; set; }
    //    public string RepairCommandKey { get; set; }

    //    public string ManagementFuseStateKey { get; set; }

    //}



}