using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.Metadata;
using ULA.Interceptors;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.DeviceDomain.Commands
{
   public abstract class DeviceCommandBase:Disposable,IDeviceCommand
    {
        private ICommandSuccessRule _rule;
        private bool? _isCommandSucceed=null;
        private int _commandCheckTimes = 3;
        private int _currentCommandcheckCounter = 0;

        public DeviceCommandBase()
        {
            Starters=new List<IStarter>();
        }



        #region Implementation of IDeviceCommand

        public List<IStarter> Starters { get;  }
        public CommandTypesEnum CommandType { get; set; }
        public void SetRule(ICommandSuccessRule rule)
        {
            _rule = rule;
        }

        public abstract string[] CommandTags { get; }
        public abstract object[] CommandValues { get; }
        public bool IsCommandSent { get; set; }

        public virtual bool? IsCommandSucceed
        {
            get { return _isCommandSucceed; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCommandStartSending { get; set; }

        public EntityMetadata EntityMetadata { get; set; }

        public virtual void CheckIfCommandSucceed(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
           bool isCommandSucceed= _rule.IsCommandSucceed(runtimeDeviceViewModel);
            if (isCommandSucceed)
            {
                _isCommandSucceed = true;
                CurrectCommandStateChanged?.Invoke();
            }
            else if (!_isCommandSucceed.HasValue)
            {
                _currentCommandcheckCounter++;
                if (_currentCommandcheckCounter == _commandCheckTimes)
                {
                    _isCommandSucceed = false;
                    CurrectCommandStateChanged?.Invoke();
                }
            }
        
        }
        

        public Action CurrectCommandStateChanged { get; set; }


        
        #endregion

        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            CurrectCommandStateChanged = null;
            base.OnDisposing();
        }

        
        #endregion
    }
}
