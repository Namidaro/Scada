using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime
{
   public class StarterViewModel:DisposableBindableBase, IStarterViewModel
    {
        private IStarter _model;
        private string _starterDescription;
        private int _starterNumber;
        private bool? _isInRepairState;
        private bool? _isInManualMode;
        private bool? _isStarterOn;
        private bool? _managementFuseState;
        private bool _isInUndefinedState;
        private IDeviceCommandStateViewModel _deviceCommandStateViewModel;
        private bool? _isCommandStateOn;

        public StarterViewModel()
        {
             IsInManualMode = null;
            IsInRepairState = null;
            IsStarterOn = null;
            ManagementFuseState =null;
            IsInUndefinedState = true;
        }



        #region Implementation of IStarterViewModel

        public string StarterDescription
        {
            get { return _starterDescription; }
            set
            {
                if (_starterDescription != null&&_starterDescription.Equals(value)) return;

                _starterDescription = value;
                RaisePropertyChanged();
            }
        }

        public int StarterNumber
        {
            get { return _starterNumber; }
            set
            {
                if (value != null && _starterNumber.Equals(value)) return;

                _starterNumber = value;
                RaisePropertyChanged();

            }
        }

        public bool? IsInRepairState
        {
            get { return _isInRepairState; }
            set
            {
                if (_isInRepairState != null && _isInRepairState.Equals(value)) return;

                _isInRepairState = value;
                RaisePropertyChanged();

            }
        }

        public bool? IsInManualMode
        {
            get { return _isInManualMode; }
            set
            {
                if (_isInManualMode != null && _isInManualMode.Equals(value)) return;

                _isInManualMode = value;
                RaisePropertyChanged();

            }
        }

        public bool? IsStarterOn
        {
            get { return _isStarterOn; }
            set
            {
                if (_isStarterOn != null && _isStarterOn.Equals(value)) return;

                _isStarterOn = value;
                RaisePropertyChanged();

            }
        }

        public bool? ManagementFuseState
        {
            get { return _managementFuseState; }
            set
            {
                if (_managementFuseState != null && _managementFuseState.Equals(value)) return;

                _managementFuseState = value;
                RaisePropertyChanged();

            }
        }
      

        public bool IsInUndefinedState
        {
            get { return _isInUndefinedState; }
            set
            {
                if(value != null && _isInUndefinedState.Equals(value))return;
                _isInUndefinedState = value;
                RaisePropertyChanged();

            }
        }

        public IDeviceCommandStateViewModel DeviceCommandStateViewModel
        {
            get { return _deviceCommandStateViewModel; }
            set
            {
                _deviceCommandStateViewModel = value;
                RaisePropertyChanged();
            }
        }

        public object Model
        {
            get { return _model; }
            set
            {
                _model = value as IStarter;
                StarterDescription = _model.StarterDescription;
                StarterNumber = _model.StarterNumber;
                RaisePropertyChanged();
            }
        }

        public void Update()
        {
            if(_model==null)return;
            IsInManualMode = _model.IsInManualMode;
            IsInRepairState = _model.IsInRepairState;
            IsStarterOn = _model.IsStarterOn;
            ManagementFuseState = _model.ManagementFuseState;
            IsInUndefinedState = false;
        }

        public void SetLostConnection()
        {
            IsInManualMode = null;
            IsInRepairState = null;
            IsStarterOn = null;
            ManagementFuseState =null;
            IsInUndefinedState = true;
        }

        #endregion
    }
}
