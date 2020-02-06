using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.Presentation.Infrastructure.ViewModels.CustomItems
{
    public interface ISignalDefinitionsViewModel
    {
        string Description { get; set; }
        int ResistorModule { get; set; }
        int ResistorNumber { get; set; }
        ICustomSignal Model { get; set; }
    }
}