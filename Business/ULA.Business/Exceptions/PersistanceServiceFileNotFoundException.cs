using System;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents persistance service exception that will be thrown when no application file for data storing is found
    /// </summary>
    public class PersistanceServiceFileNotFoundException : ApplicationException
    {
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
            get { return LocalizationResources.Instance.DataStorageFilePathExceptionMessage; }
        }

        #endregion [Override members]
    }
}