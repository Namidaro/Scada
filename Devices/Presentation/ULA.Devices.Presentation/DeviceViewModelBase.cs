using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Devices.Presentation
{
  public abstract class DeviceViewModelBase:DisposableBindableBase, ILogicalDeviceViewModelBase
    {
        protected ILogicalDevice _model;
        private int _deviceNumber;
        private string _deviceName;
        private string _deviceDescription;

        #region Implementation of ILogicalDeviceViewModelBase

        public string DeviceDescription
        {
            get { return _deviceDescription; }
            set
            {
                _deviceDescription = value;
                RaisePropertyChanged();
            }
        }

        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;
                RaisePropertyChanged();
            }
        }

        public int DeviceNumber
        {
            get { return _deviceNumber; }
            set
            {
                _deviceNumber = value; 
                RaisePropertyChanged();
            }
        }


        public ILogicalDevice Model
        {
            get { return _model; }
            set
            {
                SetModel(value);
               

            }
        }

        protected virtual void SetModel(ILogicalDevice model)
        {
            _model = model;
            DeviceNumber = _model.DeviceNumber;
            DeviceName = _model.DeviceName;
            DeviceDescription = _model.DeviceDescription;
        
        }

     




        #endregion
    }
}
