using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents an exception for server modbus response
    /// </summary>
    class ServerResponseExeption : ExceptionBase
    {
        /// <summary>
        /// Creates an instance of <see cref="ServerResponseExeption"/>
        /// </summary>
        public ServerResponseExeption()
        { }
        /// <summary>
        /// Creates an instance of <see cref="ServerResponseExeption"/>
        /// </summary>
        /// <param name="message">An exception message</param>
        public ServerResponseExeption(string message)
            : base(message)
        { }
    }
}
