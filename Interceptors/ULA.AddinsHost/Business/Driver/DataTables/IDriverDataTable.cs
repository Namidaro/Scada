using System;

namespace ULA.AddinsHost.Business.Driver.DataTables
{
    /// <summary>
    ///     Exposes driver data table fucntionality
    ///     Описывает таблицу данных драйвера
    /// </summary>
    public interface IDriverDataTable
    {
        /// <summary>
        ///     Creates an instance of <see cref="AomDataTableRowEntity" /> that represents the row of current data table
        ///     Создает строку в таблице данных драйвера
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="AomDataTableRowEntity" /> that represents the row of current data table
        /// </returns>
        AomDataTableRowEntity CreateRow();

        /// <summary>
        ///     Pushes an instance of <see cref="AomDataTableRowEntity" /> to the collection
        ///     Вставляет строку данных в таблицу данных драйвера
        /// </summary>
        /// <param name="dataRow">
        ///     An instance of <see cref="AomDataTableRowEntity" /> to push to a collection
        /// </param>
        void PushRow(AomDataTableRowEntity dataRow);

        /// <summary>
        ///     Get from current driver data table by id
        ///     Возвращают строку данных из таблицы данных драйвера по Id строки
        /// </summary>
        /// <param name="rowId">Id of required row</param>
        /// <returns>Represent data row from current driver</returns>
        AomDataTableRowEntity GetRowById(Guid rowId);


        /// <summary>
        ///     Get from current driver data table by id
        ///     Возвращают строку данных из таблицы данных драйвера по Id строки
        /// </summary>
        /// <param name="rowName">Id of required row</param>
        /// <returns>Represent data row from current driver</returns>
        AomDataTableRowEntity GetRowByName(string rowName);
    }
}