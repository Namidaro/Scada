using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime
{
    public class DefectStateViewModel : DisposableBindableBase, IDefectStateViewModel
    {
        private IDefectState _model;
        private bool? _isManagementDefect;
        private bool? _isManagementChainsDefect;
        private bool? _isFusesDefect;
        private bool? _isProtectionDefect;
        private bool? _isNoPowerDefect;


        public DefectStateViewModel()
        {
      
        }



        #region Implementation of IDefectStateViewModel

        public object Model
        {
            get { return _model; }
            set { _model = value as IDefectState; }
        }

        public void Update()
        {
            if (_model.IsFusesDefect != null) IsFusesDefect = _model.IsFusesDefect.Value;
            if (_model.IsManagementChainsDefect != null)
                IsManagementChainsDefect = _model.IsManagementChainsDefect.Value;
            if (_model.IsManagementDefect != null) IsManagementDefect = _model.IsManagementDefect.Value;
            if (_model.IsNoPowerDefect != null) IsNoPowerDefect = _model.IsNoPowerDefect.Value;
            if (_model.IsProtectionDefect != null) IsProtectionDefect = _model.IsProtectionDefect.Value;
        }

        public void SetLostConnection()
        {
            IsFusesDefect = null;
            IsManagementChainsDefect = null;
            IsManagementDefect = null;
            IsNoPowerDefect = null;
            IsProtectionDefect = null;
        }

        public bool? IsManagementDefect
        {
            get { return _isManagementDefect; }
            set
            {
                if (_isManagementDefect!=null&&_isManagementDefect.Equals(value)) return;
                _isManagementDefect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasAnyDefect));
            }
        }

        public bool? IsManagementChainsDefect
        {
            get { return _isManagementChainsDefect; }
            set
            {
                if (_isManagementChainsDefect != null && _isManagementChainsDefect.Equals(value)) return;

                _isManagementChainsDefect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasAnyDefect));

            }
        }

        public bool? IsFusesDefect
        {
            get { return _isFusesDefect; }
            set
            {
                if (_isFusesDefect != null&&_isFusesDefect.Equals(value)) return;

                _isFusesDefect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasAnyDefect));

            }
        }

        public bool? IsProtectionDefect
        {
            get { return _isProtectionDefect; }
            set
            {
                if (_isProtectionDefect != null && _isProtectionDefect.Equals(value)) return;

                _isProtectionDefect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasAnyDefect));

            }
        }

        public bool? IsNoPowerDefect
        {
            get { return _isNoPowerDefect; }
            set
            {
                if (_isNoPowerDefect != null && _isNoPowerDefect.Equals(value)) return;

                _isNoPowerDefect = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(HasAnyDefect));

            }
        }

        public bool HasAnyDefect => _model.HasAnyDefect;

        #endregion
    }
}
