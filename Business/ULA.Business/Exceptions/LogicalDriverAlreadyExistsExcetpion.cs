using System;
using ULA.Business.Infrastructure.DTOs;
using ULA.Localization;

namespace ULA.Business.Exceptions
{
    /// <summary>
    ///     Represents a logical driver exception that will be thrown when another deviceViewModel with the same characteristics already exists
    /// </summary>
    public class LogicalDriverAlreadyExistsExcetpion : ApplicationException
    {
        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="LogicalDriverInformation" /> that represents failed driver/>
        /// </summary>
        public LogicalDriverInformation LogicalDriverInformation { get; private set; }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="LogicalDriverAlreadyExistsExcetpion" />
        /// </summary>
        /// <param name="logicalDriverInformation">
        ///     An instance of <see cref="LogicalDriverInformation" /> that represents failed driver
        /// </param>
        public LogicalDriverAlreadyExistsExcetpion(LogicalDriverInformation logicalDriverInformation)
        {
            this.LogicalDriverInformation = logicalDriverInformation;
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
                return string.Format(LocalizationResources.Instance.DriverAlreadyExistsExceptionMessageFormat,
                                     this.LogicalDriverInformation.DriverTcpAddress, this.LogicalDriverInformation.DriverTcpPort);
            }
        }

        #endregion [Override members]
    }
}