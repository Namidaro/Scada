using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.LoggerServices;
using ULA.Interceptors;

namespace ULA.Business
{
    /// <summary>
    ///  модель пускателя
    /// </summary>
    public class Starter : IStarter
    {
        private readonly StarterLoggerService _starterLoggerService;
        private Logger _logger;
        private bool? _isStarterOn;
        private bool? _isInManualMode;
        private bool? _isInRepairState;
        private bool? _managementFuseState;
        private bool? _isCommandStateOn;

        //   private StarterAddressKeys _starterAddressKeys;

        public Starter(StarterLoggerService starterLoggerService)
        {
            _starterLoggerService = starterLoggerService;
          //  _starterAddressKeys=new StarterAddressKeys();
        }


        #region Implementation of IStarter

        /// <summary>
        /// описпание пускателя
        /// </summary>
        public string StarterDescription { get; set; }

        /// <summary>
        /// номер пускателя
        /// </summary>
        public int StarterNumber { get; set; }

        /// <summary>
        /// режим ремонта
        /// </summary>
        public bool? IsInRepairState
        {
            get { return _isInRepairState; }
            set
            {
                if ((_isInRepairState != null) && (_isInRepairState == value)) return;
                if (_isInRepairState != null)
                {
                    _starterLoggerService.IsRepairStateChanged(_logger, value.Value, StarterNumber);
                }
                _isInRepairState = value;
            }
        }

        /// <summary>
        /// ручной режим
        /// </summary>
        public bool? IsInManualMode
        {
            get { return _isInManualMode; }
            set
            {

                if ((_isInManualMode != null) && (_isInManualMode == value)) return;
                if (_isInManualMode != null)
                {
                    _starterLoggerService.IsAutoModeStateChanged(_logger, !value.Value, StarterNumber);
                }
                _isInManualMode = value;

            }
        }

        /// <summary>
        /// включенное состояние
        /// </summary>
        public bool? IsStarterOn
        {
            get { return _isStarterOn; }
            set
            {
               if((_isStarterOn!=null)&&(_isStarterOn==value))return;
                if (_isStarterOn!=null)
                {
                    _starterLoggerService.StarterStateChanged(_logger,value.Value,StarterNumber);
                }
                _isStarterOn = value;
            }
        }
       

        /// <summary>
        /// неопределенное состояние
        /// </summary>
        public bool IsInUndefinedState { get; set; }

        public int ChannelNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LightingModeEnum StarterLightingMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? ManagementFuseState
        {
            get { return _managementFuseState; }
            set
            {
                _managementFuseState = value;
            }

        }

    //    public StarterAddressKeys StarterAddressKeys
    //    {
     //       get { return _starterAddressKeys; }
      //  }

        public void SetLogger(Logger logger)
        {
            _logger = logger;
        }

        #endregion
    }
}
