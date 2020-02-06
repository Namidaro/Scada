using System;
using ULA.AddinsHost.Business.Driver.DataTables;

namespace ULA.Devices.PICON2.Business.DriverSeedingStrategies
{
    /// <summary>
    ///     Класс инициализирующий данные TcpModbus драйвера
    /// </summary>
    public class TcpModbusDriverDataSeedingStrategy : IDriverDataSeedingStrategy
    {
        #region [Const]

        private const string NAME_PROPERTY = "Name";
        private const string ADDRESS_PROPERTY = "Address";
        private const string LENGTH_PROPERTY = "Length";
        private const string UPDATABLE_PROPERTY = "IsUpdatable";

        private const string COMMAND_MANAGENT_ID_NAME = "CommandManagent";
        private const string DATE_TIME_ID_NAME = "DateTime";
        private const string FUSE_ID_NAME = "Fuse";
        private const string DEFECT_ID_NAME = "ErrorMonitor";
        private const string LOCALD_DATE_TIME_ID_NAME = "LocalDateTime";
        private const string ANALOG_DATA_ID_NAME = "Analog";
        private const string RESISTOR_ID_NAME = "Resistor";
        private const string DEFECT_RESISTOR_ID_NAME = "DefectResistor";
        private const string METER_DATE_TIME = "MeterDateTime";

        #endregion

        #region [Private fields]

        private IDriverDataTable _driverDataTable;

        #endregion [Private fields]

        #region [IDriverDataSeedingStrategy members]

        /// <summary>
        ///     Seed current driver data table with data
        /// </summary>
        /// <param name="driverDataTable">
        ///     An instance of <see cref="IDriverDataTable" /> that represents current driver data table to seed with data
        /// </param>
        /// <returns>
        ///     An instance of <see cref="DriverDataSeedingResult" /> that represents seeding result
        /// </returns>
        public DriverDataSeedingResult Seed(IDriverDataTable driverDataTable)
        {
            var result = new DriverDataSeedingResult();
            if (driverDataTable == null) throw new ArgumentNullException(PICON2.Localization.PICON2Resources.Instance.DriverDataTable);
            this._driverDataTable = driverDataTable;
            this.OnSeeding(result);

            return result;
        }

        #endregion [IDriverDataSeedingStrategy members]

        #region [Templated members]

        /// <summary>
        ///     Seed current driver data table with data
        ///     инициализирует драйвер техническими данными
        /// </summary>
        /// <param name="result">
        ///     An instance of <see cref="DriverDataSeedingResult" /> that represents seeding result to populate
        /// </param>
        protected virtual void OnSeeding(DriverDataSeedingResult result)
        {
            result.DateTimeId = this.AddDriverData(DATE_TIME_ID_NAME, 0x1008, 8, true);
            result.CommandManagementId = this.AddDriverData(COMMAND_MANAGENT_ID_NAME, 0x0000, 13, true);
            result.AnalogId = this.AddDriverData(ANALOG_DATA_ID_NAME, 0x000E, 18, true);
            result.MeterDateTimeId = this.AddDriverData(METER_DATE_TIME, 0x210, 16, true);
        }

        #endregion [Templated members]

        #region [Help members]

        /// <summary>
        ///     Добавляет в таблицу данных драйвера новую строку с техническимим данными переданными в параметрах
        /// </summary>
        /// <param name="name">Имя строки данных драйвера</param>
        /// <param name="address">Адррес начала блока данных в устройстве</param>
        /// <param name="length">Длина блока данных</param>
        /// <param name="isUpdatable">Являются ли эти данные обновляемыми(могу ли обновляться на стороне устройства)</param>
        /// <returns>Id строки данного в таблице данных драйвера</returns>
        private Guid AddDriverData(string name, ushort address, ushort length, bool isUpdatable)
        {
            var row = this._driverDataTable.CreateRow();
            row.Properties[NAME_PROPERTY].Value = name;
            row.Properties[ADDRESS_PROPERTY].Value = address;
            row.Properties[LENGTH_PROPERTY].Value = length;
            row.Properties[UPDATABLE_PROPERTY].Value = isUpdatable;
            this._driverDataTable.PushRow(row);

            return row.Id;
        }

        #endregion [Help members]
    }
}