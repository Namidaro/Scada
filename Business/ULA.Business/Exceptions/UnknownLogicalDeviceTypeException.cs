using System;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents an application exception that occures when the system can't resolve the type of a deviceViewModel factory
    /// </summary>
    public class UnknownLogicalDeviceTypeException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets the name of deviceViewModel factory that wasn't resolved
        /// </summary>
        public string DeviceFactoryTypeName { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="UnknownLogicalDeviceTypeException" />
        /// </summary>
        /// <param name="deviceFactoryTypeName">The name of failed deviceViewModel factory</param>
        public UnknownLogicalDeviceTypeException(string deviceFactoryTypeName)
        {
            this.DeviceFactoryTypeName = deviceFactoryTypeName;
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
                return string.Format(LocalizationResources.Instance.UnknownDeviceTypeExceptionMessageFormat,
                                     this.DeviceFactoryTypeName);
            }
        }

        #endregion [Override members]
    }
}