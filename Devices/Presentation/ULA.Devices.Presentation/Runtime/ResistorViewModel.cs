using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Devices.Presentation.Runtime
{
   public class ResistorViewModel:BindableBase, IResistorViewModel
    {
        private bool? _isDefectState;
        private bool? _isOnState;
        private int _parentStarterId;
        private IResistor _model;
        private int _moduleNumber;
        private int _resistorNumber;
        private string _shortParentStarterDescription;
        private bool? _isStarterManagementFuseStateOn;
        private bool _isEnabled;

        #region Implementation of IResistorViewModel

        public bool? IsDefectState
        {
            get { return _isDefectState; }
        }

        public bool? IsStarterManagementFuseStateOn


        {
            get { return _isStarterManagementFuseStateOn; }
        }

        public bool? IsOnState
        {
            get { return _isOnState; }
        }

        public int ParentStarterId
        {
            get { return _parentStarterId; }
        }

        public string ShortParentStarterDescription
        {
            get { return _shortParentStarterDescription; }
        }

        public object Model
        {
            get { return _model; }
            set
            {
                _model = value as IResistor;
                _parentStarterId = _model.ParentStarter.StarterNumber;
                RaisePropertyChanged(nameof(ParentStarterId));
                _moduleNumber = _model.ModuleNumber;
                RaisePropertyChanged(nameof(ModuleNumber));
                _resistorNumber = _model.ResistorNumber+1;
                RaisePropertyChanged(nameof(ResistorNumber));
                if (_model.ParentStarter.StarterDescription != null)
                {
                    if (_model.ParentStarter.StarterDescription.Length > 0)
                    {
                        _shortParentStarterDescription = _model.ParentStarter.StarterDescription[0].ToString();
                        RaisePropertyChanged(nameof(ShortParentStarterDescription));

                    }
                }
            }
        }

        public int ModuleNumber
        {
            get { return _moduleNumber; }
        }

        public int ResistorNumber
        {
            get { return _resistorNumber; }
        }

        public void SetLostConnection()
        {
            if (!IsEnabled) return;
            if (_isDefectState !=null)
            {
                _isDefectState = null;
                RaisePropertyChanged(nameof(IsDefectState));
            }

            if (_isOnState != null)
            {
                _isOnState = null;
                RaisePropertyChanged(nameof(IsOnState));
            }
            if (_isStarterManagementFuseStateOn !=null)
            {
                _isStarterManagementFuseStateOn =null;
                RaisePropertyChanged(nameof(IsStarterManagementFuseStateOn));
            }
        }

        public void UpdateStates()
        {
            if(!IsEnabled)return;
            if (_isDefectState != _model.IsDefectState)
            {
                _isDefectState = _model.IsDefectState;
                RaisePropertyChanged(nameof(IsDefectState));
            }

            if (_isOnState != _model.IsOnState)
            {
                _isOnState = _model.IsOnState;
                RaisePropertyChanged(nameof(IsOnState));
            }
            if (_isStarterManagementFuseStateOn != _model.ParentStarter.ManagementFuseState)
            {
                _isStarterManagementFuseStateOn = _model.ParentStarter.ManagementFuseState;
                RaisePropertyChanged(nameof(IsStarterManagementFuseStateOn));
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
