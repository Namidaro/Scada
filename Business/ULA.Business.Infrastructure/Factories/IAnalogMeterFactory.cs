using ULA.Business.Infrastructure.DeviceDomain;

namespace ULA.Business.Infrastructure.Factories
{
    public interface IAnalogMeterFactory
    {
        IAnalogMeter CreateAnalogMeter(string analogMeterType);
    }
}