using System;

namespace ULA.Devices.PICON2.Business.DriverSeedingStrategies
{
    /// <summary>
    ///     Represents driver data seeding context
    ///     Представляет id контекста полей драйвера 
    /// </summary>
    public class DriverDataSeedingResult
    {
        #region [Properties]
        /// <summary>
        ///     Gets or sets the unique identifier of the real date time
        /// </summary>
        public Guid DateTimeId { get; set; }

        /// <summary>
        ///     Gets or sets the unique identifier of the command management
        ///     поля управления
        /// </summary>
        public Guid CommandManagementId { get; set; }

        /// <summary>
        ///     Gets or sets the unique identifier of the values ​​of currents, voltages, power
        ///     данные счетчика(аналоги)
        /// </summary>
        public Guid AnalogId { get; set; }

        /// <summary>
        /// дата и время счетчика
        /// </summary>
        public Guid MeterDateTimeId { get; set; }

        #endregion [Properties]
    }
}