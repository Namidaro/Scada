namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Exposes device scheme data table Resistor row functionally
    /// </summary>
    public interface IDeviceResistorRow
    {
        /// <summary>
        ///     Id of resistor
        /// </summary>
        int ResistorId { get; set; }
        /// <summary>
        /// modul of resistor
        /// </summary>
        int ResistorModul{ get; set; }
        /// <summary>
        /// diskret of resistor
        /// </summary>
        int ResistorDiskret { get; set; }
        /// <summary>
        ///     Description of resistor
        /// </summary>
        string ResistorDescription { get; set; }

        /// <summary>
        ///     Id os parent starter
        /// </summary>
        int? ParentStarterId { get; set; }
    }
}
