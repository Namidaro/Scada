namespace ULA.Business.Infrastructure.DeviceDomain
{
    public interface IMsa961AnalogMeter : IAnalogMeter
    {
        double? VoltageA { get; set; }
        double? VoltageB { get; set; }

        double? VoltageC { get; set; }

        double? CurrentA { get; set; }
        double? CurrentB { get; set; }
        double? CurrentC { get; set; }
    }
}