using System;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests
{
    /// <summary>
    /// Represents a tcp modbus request for reading input registers
    /// </summary>
    public class TcpReadInputRegistersClientRequest : TcpModbusClientRequest
    {
        #region [Constants]
        private const int TRANSACTION_ID_OFFSET = 0;
        private const int FUNCTION_CODE_OFFSET = 7;
        private const int NUMBER_OF_POINTS_OFFSET = 8;
        private const int DATA_OFFSET = 9;
        #endregion [Constants]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref=" TcpReadInputRegistersClientRequest"/>
        /// </summary>
        /// <param name="startAddress">An address of current request</param>
        /// <param name="slaveAddress">Address of slave device</param>
        /// <param name="numberOfPoints">The number of points for current request</param>
        public TcpReadInputRegistersClientRequest(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
            : base(slaveAddress, ProtocolConstants.READ_INPUT_REGISTERS, startAddress, numberOfPoints)
        {
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Creates an instance of a response for current request
        /// </summary>
        /// <param name="requestedResponse">A raw response data</param>
        /// <returns>An instance of a response for current request</returns>
        protected override IModbusResponse OnCreateResponseFromFrame(byte[] requestedResponse)
        {
            if (requestedResponse.Length < 20)
            {
                
            }
            var transactionId =
                (ushort)
                (requestedResponse[TcpReadInputRegistersClientRequest.TRANSACTION_ID_OFFSET + 1] |
                 requestedResponse[TcpReadInputRegistersClientRequest.TRANSACTION_ID_OFFSET] << 8);
            var functionCode = requestedResponse[TcpReadInputRegistersClientRequest.FUNCTION_CODE_OFFSET];
            var numberOfPoints = (ushort)requestedResponse[TcpReadInputRegistersClientRequest.NUMBER_OF_POINTS_OFFSET];
            var data = new byte[numberOfPoints];
            Array.Copy(requestedResponse, DATA_OFFSET, data, 0, numberOfPoints);
            return new TcpReadInputRegistersClientResponse(transactionId, functionCode, numberOfPoints, data);
        }
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        protected override void OnValidateResponse(IModbusResponse response)
        {
            base.OnValidateResponse(response);
            Guard.AgainstIsFalse<ClientModbusResponseException>(this.NumberOfPoints == (response.NumberOfPoints / ProtocolConstants.BYTES_IN_WORD));
        }
        /// <summary>
        /// Build ProtocolDataUnits part of frame 
        /// </summary>
        /// <returns>The composition of the slave address, function code and message data</returns>
        protected override byte[] GetProtocolDataUnits()
        {
           return new[]
                {
                    this.SlaveAddress,
                    this.FunctionCode,
                    (byte)((Address >> 8) & 0xff),
                    (byte)Address,
                    (byte)((NumberOfPoints >> 8) & 0xff),
                    (byte)NumberOfPoints
                };
        }
        #endregion [Override members]
    }
}