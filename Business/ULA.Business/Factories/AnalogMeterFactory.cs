using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Interceptors.IoC;

namespace ULA.Business.Factories
{
  public  class AnalogMeterFactory: IAnalogMeterFactory
    {
        private readonly IIoCContainer _container;

        public AnalogMeterFactory(IIoCContainer container)
        {
            _container = container;
        }

        #region Implementation of IAnalogMeterFactory

        public IAnalogMeter CreateAnalogMeter(string analogMeterType)
        {
            if (analogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.NO) ||
                analogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE))
            {
                IAnalogMeter analogMeter = _container.Resolve<IAnalogMeter>(analogMeterType);
                analogMeter.AnalogMeterType = analogMeterType;

                return analogMeter;
            }
            else if(analogMeterType.Equals(DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE))
            {
                IAnalogMeter analogMeter = _container.Resolve<IAnalogMeter>(analogMeterType);
                analogMeter.AnalogMeterType = analogMeterType;

                return analogMeter;
            }
            return null;
        }

        #endregion
    }
}
