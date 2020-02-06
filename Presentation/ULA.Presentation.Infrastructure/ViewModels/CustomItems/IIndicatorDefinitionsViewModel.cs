using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.Presentation.Infrastructure.ViewModels.CustomItems
{
    public interface IIndicatorDefinitionsViewModel
    {
        string Description { get; set; }
        int ResistorModule { get; set; }
        int ResistorNumber { get; set; }
        ICustomIndicator Model { get; set; }
    }
}