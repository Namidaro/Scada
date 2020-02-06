using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ULA.AddinsHost.Exceptions;
using ULA.Common.AOM;

namespace ULA.AddinsHost.Business.Driver.DataTables
{
    /// <summary>
    ///     Represents driver data table functionality
    ///     Представляет таблицу данных драйвера
    /// </summary>
    [DataContract(Name = "dataTable")]
    public class DriverDataTable : IDriverDataTable
    {
        #region [Private fields]

        [DataMember(Name = "tableContent")] private List<AomDataTableRowEntity> _content =
            new List<AomDataTableRowEntity>();

        [DataMember(Name = "dataTableRowTamplate")] private AomEntityType _dataTableRowType;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomEntity" />. It is used ONLY for deserialization purpose.
        /// </summary>
        private DriverDataTable()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="DriverDataTable" />
        /// </summary>
        /// <param name="dataTableRowType">
        ///     An instance of <see cref="AomEntityType" /> that represents the template for data table row
        /// </param>
        public DriverDataTable(AomEntityType dataTableRowType)
        {
            this._dataTableRowType = dataTableRowType;
        }

        #endregion [Ctor's]

        #region [IDriverDataTable members]

        /// <summary>
        ///     Creates an instance of <see cref="AomDataTableRowEntity" /> that represents the row of current data table
        ///     Создает строку данных для таблицы данных драйвера
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="AomDataTableRowEntity" /> that represents the row of current data table
        /// </returns>
        public AomDataTableRowEntity CreateRow()
        {
            return new AomDataTableRowEntity(Guid.NewGuid(), this._dataTableRowType);
        }

        /// <summary>
        ///     Pushes an instance of <see cref="AomDataTableRowEntity" /> to the collection
        ///     Вставляет строку данных в таблицу данных драйвера
        /// </summary>
        /// <param name="dataRow">
        ///     An instance of <see cref="AomDataTableRowEntity" /> to push to a collection
        /// </param>
        public void PushRow(AomDataTableRowEntity dataRow)
        {
            if (this._content.Any(a => a.Id.Equals(dataRow.Id)))
                throw new DriverDataTableDataRowExistsException(dataRow.Id);
            this._content.Add(dataRow);
        }

        /// <summary>
        ///     Get from current driver data table by id
        ///     Возвращает строку данных из таблицы данных драйвера
        /// </summary>
        /// <param name="rowId">Id of required row</param>
        /// <returns>Represent data row from current driver</returns>
        public AomDataTableRowEntity GetRowById(Guid rowId)
        {
            if (this._content.Any(a => a.Id.Equals(rowId)))
            {
                return this._content.First(result => result.Id.Equals(rowId));
            }
            else
            {
                throw new ArgumentException("Data row with Id "+rowId.ToString()+"not exist");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowName"></param>
        /// <returns></returns>
        public AomDataTableRowEntity GetRowByName(string rowName)
        {
            if (this._content.Any(a => a.Properties.Any(pair =>pair.Key=="Name"&&pair.Value.Value.Equals(rowName) )))
            {
                return this._content.First(a => a.Properties.Any(pair => pair.Key == "Name" && pair.Value.Value.Equals(rowName)));
            }
            else
            {
                throw new ArgumentException("Data row with name " + rowName.ToString() + "not exist");
            }
        }

        #endregion [IDriverDataTable members]

        #region [Help members]

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            /*none*/
        }

        #endregion [Help members]
    }
}