using System;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses
{
    /// <summary>
    /// Represents a server response
    /// </summary>
    internal class ReadInputRegistersResponse : ModbusServerResponseBase
    {
        #region [Constants]
        private const string FRAME_LENGTH_ERROR = "Server. Message frame does not contain enough bytes.";
        private const int NUMBER_OF_SERVISE_DATA = 3;
        private const int RESPONSE_DATA_OFFSET = 3;
        #endregion [Constants]
        

        #region [Private fields]
        private readonly byte[] _readRegisters;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ReadInputRegistersResponse"/>
        /// </summary>
        /// <param name="slaveAddress">An address of slave device</param>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="numberOfPoints">The number of points for current request</param>
        /// <param name="readRegisters">A collection of bytes that represents read registers</param>
        public ReadInputRegistersResponse(byte slaveAddress, byte functionCode, ushort numberOfPoints, byte[] readRegisters)
            :base(slaveAddress, functionCode, numberOfPoints)
        {
            this._readRegisters = readRegisters;
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Builds a server message frame as byte array.
        /// </summary>
        /// <returns>Full frame</returns>
        public override byte[] BuildMessageFrame()
        {
            var numberOfPointsLength = this.NumberOfPoints * ProtocolConstants.BYTES_IN_WORD;
            var messageFrame = new byte[ReadInputRegistersResponse.NUMBER_OF_SERVISE_DATA + numberOfPointsLength];

            messageFrame[0] = this.SlaveAddress;
            messageFrame[1] = this.FunctionCode;
            messageFrame[2] = (byte) numberOfPointsLength;

            if (_readRegisters.Length > numberOfPointsLength)
            {
                this._logger.Log(FRAME_LENGTH_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerResponseExeption>(FRAME_LENGTH_ERROR, new FormatException());
            }
            Array.Copy(this._readRegisters, 0, messageFrame, ReadInputRegistersResponse.RESPONSE_DATA_OFFSET, this._readRegisters.Length);
            this._logger.Log(String.Format("Request to read {0} words processed successfully", this.NumberOfPoints));
            return messageFrame;
        }
        #endregion [Override members]
    }
}