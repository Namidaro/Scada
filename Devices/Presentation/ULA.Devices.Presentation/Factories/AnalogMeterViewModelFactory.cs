using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Presentation.Factories
{
  public  class AnalogMeterViewModelFactory: IAnalogMeterViewModelFactory
    {
        private readonly IIoCContainer _container;

        public AnalogMeterViewModelFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of IAnalogMeterViewModelFactory

        public IAnalogMeterViewModel CreateAnalogMeterViewModel(IRuntimeDeviceViewModel runtimeDeviceViewModel, object analogMeter)
        {
            IAnalogMeter analogMeterModel= analogMeter as IAnalogMeter;

            IAnalogMeterViewModel analogMeterViewModel =
                _container.Resolve<IAnalogMeterViewModel>(analogMeterModel.AnalogMeterType);
            analogMeterViewModel.Model = analogMeterModel;
            analogMeterViewModel.SetDevice(runtimeDeviceViewModel);
            return analogMeterViewModel;
        }

        #endregion
    }
}
