using ULA.AddinsHost.Business.Driver.DataTables;

namespace ULA.Devices.PICON2.Business.DriverSeedingStrategies
{
    /// <summary>
    ///     Exposes driver data seeding strategy functionality
    /// </summary>
    public interface IDriverDataSeedingStrategy
    {
        /// <summary>
        ///     Seed current driver data table with data
        /// </summary>
        /// <param name="driverDataTable">
        ///     An instance of <see cref="IDriverDataTable" /> that represents current driver data table to seed with data
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DriverDataSeedingResult" /> that represents seeding result
        /// </returns>
        DriverDataSeedingResult Seed(IDriverDataTable driverDataTable);
    }
}