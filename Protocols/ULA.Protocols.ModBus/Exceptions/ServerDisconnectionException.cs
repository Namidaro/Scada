using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents an exception that occures when performing actions on close connection
    /// </summary>
    public class ServerDisconnectionException : ExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerDisconnectionException"/> class.
        /// </summary>
        public ServerDisconnectionException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerDisconnectionException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ServerDisconnectionException(string message) : base(message) { }

    }
}
