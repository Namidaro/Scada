using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.AnalogMeters;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Devices.Presentation.View;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime.AnalogMeters
{
   public class MsaAnalogMeterViewModel:DisposableBindableBase, IMsaAnalogMeterViewModel
    {
        private readonly IAnalogTimerInterrogation _analogTimerInterrogation;
        private IMsa961AnalogMeter _model;
        private bool _isViewShowing;
        private double? _voltageA;
        private double? _voltageB;
        private double? _voltageC;
        private double? _currentA;
        private double? _currentB;
        private double? _currentC;
        private Window _nalogWnd;
        private IRuntimeDeviceViewModel _runtimeDeviceViewModel;

        public MsaAnalogMeterViewModel(IAnalogTimerInterrogation analogTimerInterrogation)
        {
            _analogTimerInterrogation = analogTimerInterrogation;
        }



        #region Implementation of IAnalogMeterViewModel

        public object Model
        {
            get { return _model; }
            set { _model = value as IMsa961AnalogMeter; }
        }

        public void Update()
        {
            CurrentA = _model.CurrentA;
            CurrentB = _model.CurrentB;
            CurrentC = _model.CurrentC;
            VoltageA = _model.VoltageA;
            VoltageB = _model.VoltageB;
            VoltageC = _model.VoltageC; 
        }

        public bool IsViewShowing
        {
            get
            {
                if (_nalogWnd == null) return false;
                return IsOpen(_nalogWnd);
            }
        }

        private bool IsOpen(Window window)
        {
            return Application.Current.Windows.Cast<Window>().Any(x => x == window);
        }
        public void ShowView()
        {
            Msa961AnalogTableView energomeraAnalogTableView = new Msa961AnalogTableView();


            CurrentA = null;
            CurrentB = null;
            CurrentC = null;
            VoltageA = null;
            VoltageB = null;
            VoltageC = null;
       


            _nalogWnd = new Window
            {
                Owner = Application.Current.MainWindow,
                Content = energomeraAnalogTableView,
                // Title = String.Format("Показания {0}", this.DeviceName),
                ResizeMode = ResizeMode.CanMinimize,
                SizeToContent = SizeToContent.Height,
                Width = 275
            };
            _nalogWnd.DataContext = this;
            _analogTimerInterrogation.StartInterrogation();
            _analogTimerInterrogation.InterrogationCycleComplete += () =>
            {
                Update();
            };
            _nalogWnd.Closing += (o, e) =>
            {
                _analogTimerInterrogation.StopInterrogation();
            };
            _nalogWnd.Show();
        }

        public void SetDevice(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            _runtimeDeviceViewModel = runtimeDeviceViewModel;
            _analogTimerInterrogation.SetDeviceForInterrogation(_runtimeDeviceViewModel.Model as IRuntimeDevice);
        }

        #endregion

        #region Implementation of IMsaAnalogMeterViewModel

        public double? VoltageA
        {
            get { return _voltageA; }
            set
            {
                _voltageA = value;
                RaisePropertyChanged();
            }
        }

        public double? VoltageB
        {
            get { return _voltageB; }
            set
            {
                _voltageB = value;
                RaisePropertyChanged();
            }
        }

        public double? VoltageC
        {
            get { return _voltageC; }
            set
            {
                _voltageC = value;
                RaisePropertyChanged();
            }
        }

        public double? CurrentA
        {
            get { return _currentA; }
            set
            {
                _currentA = value;
                RaisePropertyChanged();
            }
        }

        public double? CurrentB
        {
            get { return _currentB; }
            set
            {
                _currentB = value;
                RaisePropertyChanged();
            }
        }

        public double? CurrentC
        {
            get { return _currentC; }
            set
            {
                _currentC = value;
                RaisePropertyChanged();
            }
        }


        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            _nalogWnd?.Close();
            _nalogWnd = null;
            _analogTimerInterrogation?.Dispose();
            base.OnDisposing();
        }

        #endregion

        #endregion
    }
}
