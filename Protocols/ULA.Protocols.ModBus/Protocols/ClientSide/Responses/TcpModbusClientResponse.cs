namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses
{
    /// <summary>
    /// Represents a tcp modbus response basic functionality
    /// </summary>
    public class TcpModbusClientResponse : ModbusClientResponseBase
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets a unique transaction identifier for current request-response
        /// </summary>
        public ushort TransactionId { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="TcpModbusClientResponse"/>
        /// </summary>
        /// <param name="transactionId">The unique transaction identifier for current request-response</param>
        /// <param name="functionCode">The number of modbus function that represent current request</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        public TcpModbusClientResponse(ushort transactionId, byte functionCode, ushort numberOfPoints)
            : base(functionCode, numberOfPoints)
        {
            this.TransactionId = transactionId;
        }
        #endregion [Ctor's]

    }
}