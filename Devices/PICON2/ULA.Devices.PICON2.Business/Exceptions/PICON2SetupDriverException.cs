using System;

namespace ULA.Devices.PICON2.Business.Exceptions
{
    /// <summary>
    ///     Represents PICON2 deviceViewModel exception that will be thrown if any error occures during driver setup phase
    /// </summary>
    public class PICON2SetupDriverException : ApplicationException
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="PICON2SetupDriverException" />
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public PICON2SetupDriverException(string message)
            : base(message)
        {
        }

        #endregion [Ctor's]
    }
}