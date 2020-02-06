using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests
{
    /// <summary>
    /// Represents a tcp modbus request basic functionality
    /// </summary>
    public abstract class TcpModbusClientRequest : ModbusClientRequestBase
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets a unique transaction identifier for current request-response
        /// </summary>
        public ushort TransactionId { get; set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="TcpModbusClientRequest"/>
        /// </summary>
        /// <param name="functionCode">The number of modbus function that represent current request</param>
        /// <param name="slaveAddress">An address of slave device</param>
        /// <param name="address">An address of current request</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        protected TcpModbusClientRequest(byte slaveAddress, byte functionCode, ushort address, ushort numberOfPoints)
            : base(functionCode, slaveAddress, address, numberOfPoints)
        { } 
        #endregion [Ctor's]

        
        #region [Override members]
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        protected override void OnValidateResponse(IModbusResponse response)
        {
            base.OnValidateResponse(response);
            Guard.AgainstIsFalse<ClientModbusResponseException>(
                this.TransactionId == ((TcpModbusClientResponse) response).TransactionId,
                ResourceConstants.TRANSACTION_ID_ERROR_MESSAGE);
        }

        /// <summary>
        /// Builds a modbus message frame as byte array.
        /// </summary>
        /// <returns>Full frame</returns>
        public override byte[] BuildRequestFrame()
        {
            var protocolDataUnit = this.GetProtocolDataUnits();
            var length = protocolDataUnit.Length;
            var mbapHeader = new byte[]
                {
                    (byte) ((this.TransactionId >> 8) & 0xff), //transaction id 
                    (byte) this.TransactionId, //transaction id
                    0, //protocol id
                    0, //protocol id
                    (byte) ((length >> 8) & 0xff), //request length
                    (byte) length //request length
                };
            return mbapHeader.CombineArray(protocolDataUnit);
        }
        #endregion [Override members]


        #region [Abstract members]
        /// <summary>
        /// Build ProtocolDataUnits part of frame 
        /// </summary>
        /// <returns>The composition of the slave address, function code and message data</returns>
        protected abstract byte[] GetProtocolDataUnits();
        #endregion [Abstract members]

    }
}