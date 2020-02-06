using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.Keys;
using ULA.Common.AOM;
using ULA.Common.Formatters;

namespace ULA.AddinsHost.Business.Device.DataTables
{
    /// <summary>
    ///     Represents device data row functionality
    ///     Представляет строку данных из таблицы данных устройства
    /// </summary>
    ///
    [DataContract]
    public class DefaultDataRow :AomEntity, IDeviceDataTableRow
    {
        #region [Private fields]

        private object _value;
        private bool _isPreviousValueNull;
        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets or sets the tag name of current data row
        ///     Тэг(имя) строки
        /// </summary>
        public string Tag
        {
            get { return Properties[DeviceEntityKeys.TAG].Value as string; }
            set { Properties[DeviceEntityKeys.TAG].Value = value; }
        }

        /// <summary>
        ///     Gets or sets the driver unique identifier od the value that is associated with current value
        ///     Id строки данных драйвера включающую эту строку
        /// </summary>
  
        public Guid DriverDataId
        {
            get { return Guid.Parse(Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_DATADRIVER_ID].Value as string); }
            set { Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_DATADRIVER_ID].Value = value.ToString(); }
        }

        /// <summary>
        ///     Gets or sets an instance of <see cref="IValueFormatter" /> for current value
        ///     Сущность для форматирования данного
        /// </summary>
  
        public IValueFormatter Formatter
        {
            get { return Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_FORMATTER].Value as IValueFormatter; }
            set
            {
                Properties[DeviceEntityKeys.DeviceDataTable.DATATABLE_FORMATTER].Value = value; 
                
            }
        }

     

        /// <summary>
        ///     Get a value 
        ///     Значение
        /// </summary>
        public object Value
        {
            get { return this._value; }
            set
            {
                if (this._value != null && this._value.Equals(value)) return;
                _isPreviousValueNull = _value == null;
                this._value = value;
            }
        }

        #endregion [Properties]
 
        /// <summary>
        /// 
        /// </summary>
        public DefaultDataRow() : base(DefaultDataRow.CreateTypeMetadata())
        {
        }



        private static AomEntityType CreateTypeMetadata()
        {
            return new AomEntityType(typeof(AomDeviceContextEntity), new AomPropertyTypeCollection(new AomPropertyType[]
            {
                new AomPropertyType(DeviceEntityKeys.TAG, typeof (string)),
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_DATADRIVER_ID, typeof (string)),
                new AomPropertyType(DeviceEntityKeys.DeviceDataTable.DATATABLE_FORMATTER, typeof (IValueFormatter)),

            }));
        }


    }
}