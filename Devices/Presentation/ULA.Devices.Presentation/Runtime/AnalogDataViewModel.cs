using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.TimerInterrogation;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime
{
   public class AnalogDataViewModel:DisposableBindableBase, IAnalogDataViewModel
    {
        private int? _signalLevel;
        private DateTime? _dateTimeFromDevice;
        private IAnalogData _model;


        public AnalogDataViewModel()
        {
            _dateTimeFromDevice = null;
            _signalLevel = null;
        }


        #region Implementation of IAnalogDataViewModel

        public int? SignalLevel
        {
            get { return _signalLevel; }
        }

        public DateTime? DateTimeFromDevice
        {
            get { return _dateTimeFromDevice; }
        }

        public Task UpdateSignalLevel()
        {
            throw new AbandonedMutexException();
        }

        public object Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                {
                    _model.AnalogDataUpdated = null;
                }
                _model = value as IAnalogData;
                _model.AnalogDataUpdated += () =>
                {
                    if (_dateTimeFromDevice == null || !_dateTimeFromDevice.Equals(_model.DateTimeFromDevice))
                    {
                        _dateTimeFromDevice = _model.DateTimeFromDevice;
                        RaisePropertyChanged(nameof(DateTimeFromDevice));
                    }
                    if (_signalLevel == null || !_signalLevel.Equals(_model.SignalLevel))
                    {
                        _signalLevel = _model.SignalLevel;
                        RaisePropertyChanged(nameof(SignalLevel));
                    }
                    
                };
            }
        }

        #endregion


        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            _model.AnalogDataUpdated = null;
            base.OnDisposing();
        }

        #endregion
    }
}
