using System.Threading.Tasks;

namespace ULA.AddinsHost.ViewModel.Device
{
    public interface IQueryIndicatorViewModel
    {
        Task BeginIndication();
        double IndicatorOpacity { get; set; }
    }
}
