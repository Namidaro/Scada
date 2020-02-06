namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Exposes device scheme data table Starter row functionally
    /// </summary>
    public interface IDeviceStarterRow
    {
        /// <summary>
        ///     StarterId
        /// </summary>
        int StarterId { get; set; }

        /// <summary>
        ///     Description of starter
        /// </summary>
        string StarterDescription { get; set; }
    }
}
