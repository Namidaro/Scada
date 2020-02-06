using System;

namespace ULA.Devices.PICONGS.Business.Exceptions
{
    /// <summary>
    ///     Represents PICONGS deviceViewModel exception that will be thrown if any error occures during driver setup phase
    /// </summary>
    public class PICONGSSetupDriverException : ApplicationException
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="PICONGSSetupDriverException" />
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public PICONGSSetupDriverException(string message)
            : base(message)
        {
        }

        #endregion [Ctor's]
    }
}