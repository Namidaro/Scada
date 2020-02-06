using System.Collections.Generic;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Common.AOM;

namespace ULA.Drivers.TcpFamily.TcpModbus
{
    /// <summary>
    ///     Represents tcp modbus driver functionality
    ///     Представляет функциональность Modbus драйвера
    /// </summary>
    public class TcpModbusDriver : ConfigLogicalDriverBase, IRuntimeLogicalDriver
    {
        #region [Override members]

        /// <summary>
        ///     Creates an instance of <see cref="AomEntityType" /> that represents current driver data table row template
        ///     Создает AomEntityType представляющий шаблон строки данных для modbus драйвера
        ///     (см. AOM паттерн)
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="AomEntityType" /> that represents current driver data table row template
        /// </returns>
        protected override AomEntityType CreateDataTableRowType()
        {
            return new AomEntityType(typeof (AomDataTableRowEntity),
                                     new AomPropertyTypeCollection(new List<AomPropertyType>
                                         {
                                             new AomPropertyType("Name", typeof (string)),
                                             new AomPropertyType("Address", typeof (ushort)),
                                             new AomPropertyType("Length", typeof (ushort)),
                                             new AomPropertyType("IsUpdatable", typeof (bool)),
                                             new AomPropertyType("Value", typeof(byte[]))
                                         }));
        }

        #endregion [Override members]
    }
}