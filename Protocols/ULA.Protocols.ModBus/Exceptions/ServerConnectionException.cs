using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents an exception that occures when connection with end point has been lost
    /// </summary>
    public class ServerConnectionException : ExceptionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionException"/> class.
        /// </summary>
        public ServerConnectionException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerConnectionException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ServerConnectionException(string message) : base(message) { }

    }
}
