using System;
using ULA.Business.Infrastructure.DTOs;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents common deviceViewModel exception
    /// </summary>
    public class LogicalDeviceException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="LogicalDeviceInformation" /> that represents information about failed deviceViewModel
        /// </summary>
        public LogicalDeviceInformation DeviceInfo { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDeviceException" />
        /// </summary>
        /// <param name="deviceInfo">
        ///     An instance of <see cref="LogicalDeviceInformation" /> to use
        /// </param>
        /// <param name="message">An optional parameter that represents the message of the exception</param>
        /// <param name="innerException">
        ///     An instance of <see cref="Exception" /> that represents inner exception information
        /// </param>
        public LogicalDeviceException(LogicalDeviceInformation deviceInfo, string message = null,
                                      Exception innerException = null) : base(message, innerException)
        {
            this.DeviceInfo = deviceInfo;
        }

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDeviceException" />
        /// </summary>
        /// <param name="message">An optional parameter that represents the message of the exception</param>
        /// <param name="innerException">
        ///     An instance of <see cref="Exception" /> that represents inner exception information
        /// </param>
        public LogicalDeviceException(string message = null,
                                      Exception innerException = null)
            : base(message, innerException)
        {
            this.DeviceInfo = null;
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
                var result = string.Empty;
                if (this.DeviceInfo != null)
                    result = string.Format(LocalizationResources.Instance.DeviceExceptionInfoPartMessageFormat,
                                           this.DeviceInfo.DeviceName);
                result = string.Format("{0} {1}",
                                       !string.IsNullOrEmpty(base.Message)
                                           ? base.Message
                                           : LocalizationResources.Instance.DeviceExceptionSimpleMessage, result);
                return result;
            }
        }

        #endregion [Override members]
    }
}