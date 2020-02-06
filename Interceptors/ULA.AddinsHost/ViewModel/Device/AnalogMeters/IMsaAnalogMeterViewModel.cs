namespace ULA.AddinsHost.ViewModel.Device.AnalogMeters
{
    public interface IMsaAnalogMeterViewModel : IAnalogMeterViewModel
    {
        double? VoltageA { get; set; }
        double? VoltageB { get; set; }

        double? VoltageC { get; set; }

        double? CurrentA { get; set; }
        double? CurrentB { get; set; }
        double? CurrentC { get; set; }

    }
}