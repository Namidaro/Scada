using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems
{
    public interface ICustomSignal:ITagNamedObject
    {
        int ResistorModule { get; set; }
        int ResistorNumber { get; set; }
    }
}