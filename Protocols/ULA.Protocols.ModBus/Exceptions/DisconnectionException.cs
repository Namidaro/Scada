using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents an exception that occures while performing actions on disconnected endpoint
    /// </summary>
    public class DisconnectionException : ExceptionBase
    {
        /// <summary>
        /// Creates an instance of <see cref="DisconnectionException"/>
        /// </summary>
        public DisconnectionException()
        {  }
        /// <summary>
        /// Creates an instance of <see cref="DisconnectionException"/>
        /// </summary>
        /// <param name="message">An exception message</param>
        public DisconnectionException(string message):base(message)
        {  }
    }
}