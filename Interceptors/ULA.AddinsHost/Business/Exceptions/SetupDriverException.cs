using System;

namespace ULA.AddinsHost.Business.Exceptions
{
    /// <summary>
    ///     Represents PICONGS device exception that will be thrown if any error occures during driver setup phase
    /// </summary>
    public class SetupDriverException : ApplicationException
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="SetupDriverException" />
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public SetupDriverException(string message)
            : base(message)
        {
        }

        #endregion [Ctor's]
    }
}