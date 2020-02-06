using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.Presentation.Infrastructure.ViewModels.CustomItems
{
    public interface ICascadeDefinitionsViewModel
    {
        string Description { get; set; }
        int ResistorModule { get; set; }
        int ResistorNumber { get; set; }
        ICascadeIndicator Model { get; set; }
    }
}