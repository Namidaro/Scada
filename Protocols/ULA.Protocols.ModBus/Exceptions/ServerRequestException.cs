using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents an exception for server modbus request
    /// </summary>
    class ServerRequestException : ExceptionBase
    {
        /// <summary>
        /// Creates an instance of <see cref="ServerRequestException"/>
        /// </summary>
        public ServerRequestException()
        { }
        /// <summary>
        /// Creates an instance of <see cref="ServerRequestException"/>
        /// </summary>
        /// <param name="message">An exception message</param>
        public ServerRequestException(string message)
            : base(message)
        { }
    }
}
