using ULA.AddinsHost.Business.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Devices.PICON2.Business.Factories
{
   public class Picon2DeviceFactory : ILogicalDeviceFactory
    {
        private readonly IIoCContainer _container;
        private readonly IAnalogMeterFactory _analogMeterFactory;

        public Picon2DeviceFactory(IIoCContainer container,IAnalogMeterFactory analogMeterFactory)
        {
            _container = container;
            _analogMeterFactory = analogMeterFactory;
        }


        #region Implementation of ILogicalDeviceFactory

        public ILogicalDevice CreateLogicalDevice(IDeviceMomento deviceMomento)
        {
            IRuntimeDevice runo3Device = _container.Resolve<Picon2RuntimeDevice>();
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
