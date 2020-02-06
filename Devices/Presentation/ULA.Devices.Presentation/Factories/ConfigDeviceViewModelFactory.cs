using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
  public  class ConfigDeviceViewModelFactory: IConfigDeviceViewModelFactory
    {
        private readonly IIoCContainer _container;

        public ConfigDeviceViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of IConfigDeviceViewModelFactory

        public IConfigDeviceViewModel CreateConfigDeviceViewModel(IConfigLogicalDevice configLogicalDevice)
        {
            IConfigDeviceViewModel configDeviceViewModel = _container.Resolve<IConfigDeviceViewModel>();
            configDeviceViewModel.Model = configLogicalDevice;
            return configDeviceViewModel;
        }

        #endregion
    }
}
