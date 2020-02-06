using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents a transport write exception
    /// </summary>
    public class TransportWriteException : ExceptionBase
    {
        /// <summary>
        /// Creates an instance of <see cref="TransportWriteException"/>
        /// </summary>
        public TransportWriteException()
        {  }
        /// <summary>
        /// Creates an instance of <see cref="TransportWriteException"/>
        /// </summary>
        /// <param name="message">An exception message</param>
        public TransportWriteException(string message):base(message)
        {  }
    }
}