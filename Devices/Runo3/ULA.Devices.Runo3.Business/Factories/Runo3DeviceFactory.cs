using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Driver;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Devices.Runo3.Business.Interfaces;
using ULA.Interceptors.IoC;

namespace ULA.Devices.Runo3.Business.Factories
{
   public class Runo3DeviceFactory: IRuno3DeviceFactory
    {
        private readonly IIoCContainer _container;
        private readonly IAnalogMeterFactory _analogMeterFactory;

        public Runo3DeviceFactory(IIoCContainer container,IAnalogMeterFactory analogMeterFactory)
        {
            _container = container;
            _analogMeterFactory = analogMeterFactory;
        }


        #region Implementation of ILogicalDeviceFactory

        public ILogicalDevice CreateLogicalDevice(IDeviceMomento deviceMomento)
        {
            IRuno3Device runo3Device= _container.Resolve<IRuno3Device>();
            runo3Device.DeviceMomento = deviceMomento;
            if (deviceMomento.State.AnalogMeterType != DeviceStringKeys.DeviceAnalogMetersTagKeys.NO)
            {
                runo3Device.AnalogMeter = _analogMeterFactory.CreateAnalogMeter(deviceMomento.State.AnalogMeterType);
            }
            return runo3Device;
        }

        #endregion
    }
}
