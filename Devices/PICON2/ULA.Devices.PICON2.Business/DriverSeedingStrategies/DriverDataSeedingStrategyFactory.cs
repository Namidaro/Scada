using ULA.Devices.PICON2.Business.Exceptions;

namespace ULA.Devices.PICON2.Business.DriverSeedingStrategies
{
    /// <summary>
    ///     Represetns data seeding strategy factory functionality
    ///     фабрика для создания инифиализации драйвера
    /// </summary>
    public static class DriverDataSeedingStrategyFactory
    {
        #region [Const]
        private const string TCP_MODBUS_DRIVER = "TCP_MB";
        #endregion

        #region [Public members]

        /// <summary>
        ///     Creates an instance of <see cref="IDriverDataSeedingStrategy" /> for specific driver type
        /// </summary>
        /// <param name="driverType">The type of driver to seed</param>
        /// <returns></returns>
        public static IDriverDataSeedingStrategy CreateSeedingStrategy(string driverType)
        {
            switch (driverType)
            {
                case TCP_MODBUS_DRIVER:
                    return new TcpModbusDriverDataSeedingStrategy();
                default:
                    throw new PICON2SetupDriverException(PICON2.Localization.PICON2Resources.Instance.NotWorkWithDriverType + driverType);
            }
        }

        #endregion [Public members]
    }
}