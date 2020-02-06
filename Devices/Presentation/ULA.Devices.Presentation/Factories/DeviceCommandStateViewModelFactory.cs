using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
  public  class DeviceCommandStateViewModelFactory: IDeviceCommandStateViewModelFactory
    {
        private readonly IIoCContainer _container;

        public DeviceCommandStateViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }

        #region Implementation of IDeviceCommandStateViewModelFactory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDeviceCommandStateViewModel CreateDeviceCommandStateViewModel(object command)
        {
            IDeviceCommandStateViewModel deviceCommandStateViewModel =
                _container.Resolve<IDeviceCommandStateViewModel>();
            deviceCommandStateViewModel.Model = command;
            return deviceCommandStateViewModel;
        }

        #endregion
    }
}
