using System;
using ULA.Business.Infrastructure.DTOs;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents logical driver exception wht will be thrown when an unexpected exception is occured during driver creation process
    /// </summary>
    public class LogicalDriverCreationException : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="LogicalDriverInformation" /> that failed to be created
        /// </summary>
        public LogicalDriverInformation DriverInfo { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDriverCreationException" />
        /// </summary>
        /// <param name="driverInfo">
        ///     An instance of <see cref="LogicalDriverInformation" /> that failed to be createde
        /// </param>
        /// <param name="message">n optional parameter that represents the message of the exception</param>
        /// <param name="innerException"> An instance of <see cref="Exception" /> that represents inner exception information</param>
        public LogicalDriverCreationException(LogicalDriverInformation driverInfo, string message = null, Exception innerException = null)
            : base(message, innerException)
        {
            this.DriverInfo = driverInfo;
        }

        #endregion [Ctor's]

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
                if (this.DriverInfo != null)
                    result = string.Format(LocalizationResources.Instance.DriverExceptionInfoPartMessageFormat,
                                           this.DriverInfo);
                result = string.Format("{0} {1}",
                                       !string.IsNullOrEmpty(base.Message)
                                           ? base.Message
                                           : LocalizationResources.Instance.DriverExceptionSimpleMessage, result);
                return result;
            }
        }
    }
}