using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.Protocols.ModBus.Exceptions
{
    /// <summary>
    /// Represents a transport write exception
    /// </summary>
    public class TransportReadException : ExceptionBase
    {
        /// <summary>
        /// Creates an instance of <see cref="TransportReadException"/>
        /// </summary>
        public TransportReadException()
        {}

        /// <summary>
        /// Creates an instance of <see cref="TransportReadException"/>
        /// </summary>
        /// <param name="message">An exception message</param>
        public TransportReadException(string message)
            : base(message)
        {}
    }
}
