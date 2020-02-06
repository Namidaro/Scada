using System;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests
{
    /// <summary>
    /// Represents a tcp modbus request for writting multiple registers
    /// </summary>
    public class TcpWriteMultipleRegistersClientRequest : TcpModbusClientRequest
    {
        #region [Constants]
        private const int TRANSACTION_ID_OFFSET = 0;
        private const int FUNCTION_CODE_OFFSET = 7;
        private const int START_ADDRESS_OFFSET = 8;
        private const int NUMBER_OF_POINTS_OFFSET = 10;
        private const int RESPONSE_DATA_OFFSET = 7;
        private const int NUMBER_OF_SERVICE_DATA = 7;
        #endregion [Constants]


        #region [Private fields]
        private readonly byte[] _data;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpWriteMultipleRegistersClientRequest"/>
        /// </summary>
        /// <param name="slaveAddress">Address of slave device</param>
        /// <param name="startAddress">An address of current request</param>
        /// <param name="data">A collection of data to write</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        public TcpWriteMultipleRegistersClientRequest(byte slaveAddress, ushort startAddress, byte[] data, ushort numberOfPoints)
            : base(slaveAddress, ProtocolConstants.WRITE_MULTIPLE_REGISTERS, startAddress, numberOfPoints)
        {
            this._data = data;
        }
        #endregion [Ctor's]


        #region [Override members]
 
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        protected override void OnValidateResponse(IModbusResponse response)
        {
            base.OnValidateResponse(response);
            var typedResponse = (TcpWriteMultipleRegistersClientResponse)response;
            Guard.AgainstIsFalse<ClientModbusResponseException>(this.NumberOfPoints == typedResponse.NumberOfPoints);
            Guard.AgainstIsFalse<ClientModbusResponseException>(this.Address == typedResponse.Address);

        }
        /// <summary>
        /// Build ProtocolDataUnits part of frame 
        /// </summary>
        /// <returns>The composition of the slave address, function code and message data</returns>
        protected override byte[] GetProtocolDataUnits()
        {
            var numberOfBytes = this.NumberOfPoints * ProtocolConstants.BYTES_IN_WORD;
            var protocolDataUnits = new byte[TcpWriteMultipleRegistersClientRequest.NUMBER_OF_SERVICE_DATA + numberOfBytes];

            protocolDataUnits[0] = this.SlaveAddress;
            protocolDataUnits[1] = this.FunctionCode;
            protocolDataUnits[2] = (byte)((this.Address >> 8) & 0xff);
            protocolDataUnits[3] = (byte)this.Address;
            protocolDataUnits[4] = (byte)(((this.NumberOfPoints) >> 8) & 0xff);
            protocolDataUnits[5] = (byte)(this.NumberOfPoints);
            protocolDataUnits[6] = (byte)(numberOfBytes);
            Array.Copy(_data, 0, protocolDataUnits, TcpWriteMultipleRegistersClientRequest.RESPONSE_DATA_OFFSET, numberOfBytes);
            return protocolDataUnits;
        }

        /// <summary>
        /// Creates an instance of a response for current request
        /// </summary>
        /// <param name="requestedResponse">A raw response data</param>
        /// <returns>An instance of a response for current request</returns>
        protected override IModbusResponse OnCreateResponseFromFrame(byte[] requestedResponse)
        {
            if (requestedResponse.Length < 12)
            {
                
            }
            var transactionId =
                (ushort)
                (requestedResponse[TcpWriteMultipleRegistersClientRequest.TRANSACTION_ID_OFFSET + 1] |
                 requestedResponse[TcpWriteMultipleRegistersClientRequest.TRANSACTION_ID_OFFSET] << 8);
            var startAddress =
                (ushort)
                (requestedResponse[TcpWriteMultipleRegistersClientRequest.START_ADDRESS_OFFSET + 1] |
                 requestedResponse[TcpWriteMultipleRegistersClientRequest.START_ADDRESS_OFFSET] << 8);
            var numberOfPoints =
                (ushort)
                (requestedResponse[TcpWriteMultipleRegistersClientRequest.NUMBER_OF_POINTS_OFFSET + 1] |
                 requestedResponse[TcpWriteMultipleRegistersClientRequest.NUMBER_OF_POINTS_OFFSET] << 8);
            var functionCode = requestedResponse[TcpWriteMultipleRegistersClientRequest.FUNCTION_CODE_OFFSET];
            return new TcpWriteMultipleRegistersClientResponse(transactionId, functionCode, numberOfPoints, startAddress);
        }
        #endregion [Override members]
    }
}