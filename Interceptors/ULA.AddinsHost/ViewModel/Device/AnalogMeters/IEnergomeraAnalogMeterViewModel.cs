namespace ULA.AddinsHost.ViewModel.Device
{
    public interface IEnergomeraAnalogMeterViewModel:IAnalogMeterViewModel
    {
        double? VoltageA { get; set; }
        double? VoltageB { get; set; }

        double? VoltageC { get; set; }

        double? CurrentA { get; set; }
        double? CurrentB { get; set; }
        double? CurrentC { get; set; }


        double? PowerA { get; set; }
        double? PowerB { get; set; }
        double? PowerC { get; set; }

        double? StoredEnergy { get; set; }
        double? StoredEnergyForMonth { get; set; }
        double? StoredEnergyForDay { get; set; }


        string Date { get; set; }
        string Time { get; set; }

        bool IsThreeEnergiesShowing { get; set; }
    }
}