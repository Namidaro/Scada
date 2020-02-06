using System;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents persistance service exception that will be thrown when an exception occures durin application data storing file parsing
    /// </summary>
    public class PersistanceServiceFileFormatException : ApplicationException
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="PersistanceServiceFileFormatException" />
        /// </summary>
        /// <param name="exception">
        ///     Anh instance of <see cref="Exception" /> that represents inner exception
        /// </param>
        public PersistanceServiceFileFormatException(Exception exception)
            : base(LocalizationResources.Instance.DataStorageFileFormatExceptionMessage, exception)
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="PersistanceServiceFileFormatException" />
        /// </summary>
        public PersistanceServiceFileFormatException() : this(null)
        {
        }

        #endregion [Ctor's]
    }
}