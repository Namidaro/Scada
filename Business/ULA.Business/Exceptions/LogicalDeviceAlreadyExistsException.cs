using System;
using ULA.Business.Infrastructure.DTOs;
using ULA.Localization;

//В этой папке специальные ошибки для этого неё
namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents an application exception that occured when trying to create a deviceViewModel with dublicate name
    /// </summary>
    public class LogicalDeviceAlreadyExistsException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="LogicalDeviceInformation" /> that represents a failed deviceViewModel information
        /// </summary>
        public LogicalDeviceInformation DeviceInfo { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDeviceAlreadyExistsException" />
        /// </summary>
        /// <param name="deviceInfo">
        ///     An instance of <see cref="LogicalDeviceInformation" /> that represents the failed deviceViewModel information
        /// </param>
        public LogicalDeviceAlreadyExistsException(LogicalDeviceInformation deviceInfo)
        {
            this.DeviceInfo = deviceInfo;
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
                return string.Format(LocalizationResources.Instance.DeviceAlreadyExistsExceptionMessageFormat,
                                     this.DeviceInfo.DeviceName);
            }
        }

        #endregion [Override members]
    }
}