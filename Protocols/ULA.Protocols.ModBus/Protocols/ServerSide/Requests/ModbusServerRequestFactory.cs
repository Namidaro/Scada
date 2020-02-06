using System;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Validation;


namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests
{
    /// <summary>
    /// Represents a modbus server request factory
    /// </summary>
    internal static class ModbusServerRequestFactory
    {
        #region [Private fields]
        private static readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]

        #region [Constants]
        private const int MIN_REQUEST_FRAME_LENGTH = 3;
        private const string FRAME_LENGTH_ERROR_MESSAGE = "Server. Argument 'frame' must have a length of at least 3 bytes.";
        #endregion [Constants]


        #region [Public members]
        /// <summary>
        /// Creates a instance of T
        /// </summary>
        /// <typeparam name="T">A type of a server request to create</typeparam>
        /// <param name="frame">A collection of bytes that represents a server request</param>
        /// <returns>An instance of created request to the server</returns>
        public static T CreateModbusMessage<T>(byte[] frame) where T : IModbusServerRequest, new()
        {
            IModbusServerRequest message = new T();
            message.Initialize(frame);

            return (T)message;
        }
        /// <summary>
        /// Creates an instance of a request to the server
        /// </summary>
        /// <param name="frame">A collection of bytes that represents a server request</param>
        /// <returns>An instance of created request to the server</returns>
        public static IModbusServerRequest CreateModbusRequest(byte[] frame)
        {
            if (frame.Length < MIN_REQUEST_FRAME_LENGTH)
            {
                _logger.Log(FRAME_LENGTH_ERROR_MESSAGE, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerRequestException>(FRAME_LENGTH_ERROR_MESSAGE, new FormatException());
            }

            IModbusServerRequest request;
            var functionCode = frame.Second();
            switch (functionCode)
            {
                case ProtocolConstants.READ_COILS:
                case ProtocolConstants.READ_INPUTS:
                case ProtocolConstants.READ_HOLDING_REGISTERS:
                case ProtocolConstants.WRITE_SINGLE_COIL:
                case ProtocolConstants.WRITE_SINGLE_REGISTER:
                case ProtocolConstants.WRITE_MULTIPLE_COILS:
                    throw new NotSupportedException();
                case ProtocolConstants.READ_INPUT_REGISTERS:
                    request = CreateModbusMessage<ReadInputRegistersServerRequest>(frame);
                    break;
                case ProtocolConstants.WRITE_MULTIPLE_REGISTERS:
                    request = CreateModbusMessage<WriteMultipleRegistersServerRequest>(frame);
                    break;
                default:
                    throw new NotSupportedException();
            }
            return request;
        }
        #endregion [Public members]
    }
}