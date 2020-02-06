using System;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents an application exception that occures when the system can't resolve the type of a driver factory
    /// </summary>
    public class UnknownLogicalDriverTypeException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets the name of driver factory that wasn't resolved
        /// </summary>
        public string DriverType { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="UnknownLogicalDriverTypeException" />
        /// </summary>
        /// <param name="driverType">The name of failed driver factory</param>
        public UnknownLogicalDriverTypeException(string driverType)
        {
            this.DriverType = driverType;
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
                return string.Format(LocalizationResources.Instance.UnknownDriverTypeExceptionMessageFormat,
                                     this.DriverType);
            }
        }

        #endregion [Override members]
    }
}