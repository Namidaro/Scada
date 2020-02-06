using System;

namespace ULA.Devices.Runo3.Business.Exceptions
{
    /// <summary>
    ///     Represents Runo3 deviceViewModel exception that will be thrown if any error occures during driver setup phase
    /// </summary>
    public class Runo3SetupDriverException : ApplicationException
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="Runo3SetupDriverException" />
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public Runo3SetupDriverException(string message)
            : base(message)
        {
        }

        #endregion [Ctor's]
    }
}