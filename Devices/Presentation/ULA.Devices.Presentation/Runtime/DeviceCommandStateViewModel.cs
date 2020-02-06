using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime
{
  public  class DeviceCommandStateViewModel:DisposableBindableBase, IDeviceCommandStateViewModel
    {
        private bool _isCommandSending;
        private bool _isCommandSent;
        private IDeviceCommand _model;
        private CommandTypesEnum _commandType;
        private bool? _isCommandSucceed;

        public DeviceCommandStateViewModel()
        {
            
        }



        #region Implementation of IDeviceCommandStateViewModel

        public CommandTypesEnum CommandType
        {
            get { return _commandType; }
        }

        public bool IsCommandSending
        {
            get { return _isCommandSending; }
            set
            {
                _isCommandSending = value; 
                RaisePropertyChanged();
            }
        }

        public bool IsCommandSent
        {
            get { return _isCommandSent; }
            set
            {
                _isCommandSent = value;
                RaisePropertyChanged();
            }
        }

        public bool? IsCommandSucceed
        {
            get { return _isCommandSucceed; }
            set
            {
                _isCommandSucceed = value;
                RaisePropertyChanged();
            }
        }


        public object Model
        {
            get { return _model; }
            set
            {
                _model = value as IDeviceCommand;
                _commandType = _model.CommandType;
                RaisePropertyChanged(nameof(CommandType));
                _model.CurrectCommandStateChanged +=()=>
                {
                    IsCommandSending = _model.IsCommandStartSending;
                    IsCommandSent = _model.IsCommandSent;
                    if (_model.IsCommandSucceed.HasValue)
                    {
                        IsCommandSucceed = _model.IsCommandSucceed.Value;
                    }
                }
                ;
            }
        }

        #endregion
    }
}
