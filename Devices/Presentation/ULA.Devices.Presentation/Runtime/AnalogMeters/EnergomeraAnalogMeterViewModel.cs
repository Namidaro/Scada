using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Devices.Presentation.View;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation.Runtime.AnalogMeters
{
  public  class EnergomeraAnalogMeterViewModel:DisposableBindableBase, IEnergomeraAnalogMeterViewModel
    {
        private readonly IAnalogTimerInterrogation _analogTimerInterrogationService;
        private double? _voltageA;
        private double? _voltageB;
        private double? _voltageC;
        private double? _currentA;
        private double? _currentB;
        private double? _currentC;
        private double? _powerA;
        private double? _powerB;
        private double? _powerC;
        private double? _storedEnergy;
        private double? _storedEnergyForMonth;
        private double? _storedEnergyForDay;
        private IEnergomeraAnalogMeter _model;
        private Window _nalogWnd;

        private bool _isViewShowing;
        private IRuntimeDeviceViewModel _runtimeDeviceViewModel;
        private string _date;
        private string _time;
        private bool _isThreeEnergiesShowing;

        public EnergomeraAnalogMeterViewModel(IAnalogTimerInterrogation analogTimerInterrogationService)
        {
            _analogTimerInterrogationService = analogTimerInterrogationService;
        }




        #region Implementation of IEnergomeraAnalogMeterViewModel

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

        public double? PowerA
        {
            get { return _powerA; }
            set
            {
                _powerA = value;
                RaisePropertyChanged();
            }
        }

        public double? PowerB
        {
            get { return _powerB; }
            set
            {
                _powerB = value; 
                RaisePropertyChanged();
            }
        }

        public double? PowerC
        {
            get { return _powerC; }
            set
            {
                _powerC = value; 
                RaisePropertyChanged();
            }
        }

        public double? StoredEnergy
        {
            get { return _storedEnergy; }
            set
            {
                _storedEnergy = value; 
                RaisePropertyChanged();
            }
        }

        public double? StoredEnergyForMonth
        {
            get { return _storedEnergyForMonth; }
            set
            {
                _storedEnergyForMonth = value;
                RaisePropertyChanged();
            }
        }

        public double? StoredEnergyForDay
        {
            get { return _storedEnergyForDay; }
            set
            {
                _storedEnergyForDay = value;
                RaisePropertyChanged();
            }
        }

        public string Date
        {
            get { return _date; }
            set
            {
                _date = value; 
                RaisePropertyChanged();
            }
        }

        public string Time
        {
            get { return _time; }
            set
            {
                _time = value; 
                RaisePropertyChanged();
            }
        }

        public bool IsThreeEnergiesShowing
        {
            get { return _isThreeEnergiesShowing; }
            set
            {
                _isThreeEnergiesShowing = value; 
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Implementation of IAnalogMeterViewModel

        public object Model
        {
            get { return _model; }
            set { _model = value as IEnergomeraAnalogMeter; }
        }

        public void Update()
        {
            CurrentA = _model.CurrentA;
            CurrentB = _model.CurrentB;
            CurrentC = _model.CurrentC;
            VoltageA = _model.VoltageA;
            VoltageB = _model.VoltageB;
            VoltageC = _model.VoltageC;
            PowerA = _model.PowerA;
            PowerB = _model.PowerB;
            PowerC = _model.PowerC;
            StoredEnergy = _model.StoredEnergy;
            StoredEnergyForDay = _model.StoredEnergyForDay;
            StoredEnergyForMonth = _model.StoredEnergyForMonth;
            Date = _model.Date;
            Time = _model.Time;
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
            EnergomeraAnalogTableView energomeraAnalogTableView=new EnergomeraAnalogTableView();


            CurrentA = null;
            CurrentB = null;
            CurrentC = null;
            VoltageA = null;
            VoltageB = null;
            VoltageC = null;
            PowerA = null;
            PowerB = null;
            PowerC = null;
            StoredEnergy = null;
            StoredEnergyForDay = null;
            StoredEnergyForMonth = null;
            Date = null;
            Time = null;


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
            _analogTimerInterrogationService.StartInterrogation();
            _analogTimerInterrogationService.InterrogationCycleComplete += () =>
            {
                Update();
            };
            _nalogWnd.Closing += (o,e) =>
            {
                _analogTimerInterrogationService.StopInterrogation();
            };
            _nalogWnd.Show();
        }

        public void SetDevice(IRuntimeDeviceViewModel runtimeDeviceViewModel)
        {
            _runtimeDeviceViewModel = runtimeDeviceViewModel;
            _analogTimerInterrogationService.SetDeviceForInterrogation(_runtimeDeviceViewModel.Model as IRuntimeDevice);
            if (_runtimeDeviceViewModel.DeviceSignature != null)
            {
                if (_runtimeDeviceViewModel.DeviceSignature.Contains("RUNO") &&
                    ((_runtimeDeviceViewModel.DeviceSignature.Contains("UC")) ||
                     (_runtimeDeviceViewModel.DeviceSignature.Contains("M"))))
                {
                    IsThreeEnergiesShowing = true;
                }
                //TODO: check signature for piconGS ver.27+ to enable/disable three energies showing
                else
                if ((_runtimeDeviceViewModel.DeviceSignature.Contains("gs") || _runtimeDeviceViewModel.DeviceSignature.Contains("GS")) &&
                    (Convert.ToInt32(_runtimeDeviceViewModel.DeviceSignature.Split('.').Last()) >= 27))
                {
                    IsThreeEnergiesShowing = true;
                }
                else
                {
                    IsThreeEnergiesShowing = false;
                }
            }
        }

        #endregion

        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            _nalogWnd?.Close();
            _nalogWnd = null;
            _analogTimerInterrogationService?.Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}
