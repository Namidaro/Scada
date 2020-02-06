using System;
using ULA.Localization;

namespace ULA.AddinsHost.Exceptions
{
    /// <summary>
    ///     Represents driver data table exception that will be thrown if there is already an entity with the same id
    ///     Представляет исключение повторения таблицы данных драйвера
    /// </summary>
    public class DriverDataTableDataRowExistsException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets the identifier of failed to add row
        /// </summary>
        public Guid RowId { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="DriverDataTableDataRowExistsException" />
        /// </summary>
        /// <param name="rowId">
        ///     An instance of <see cref="Guid" /> that represents the identifier of failed to add row
        /// </param>
        public DriverDataTableDataRowExistsException(Guid rowId)
        {
            this.RowId = rowId;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <returns>
        ///     The error message that explains the reason for the exception, or an empty string("").
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public override string Message
        {
            get
            {
                return string.Format(LocalizationResources.Instance.DriverDataTableDataRowExistsExceptionMessageFormat,
                                     this.RowId);
            }
        }

        #endregion [Override members]
    }
}