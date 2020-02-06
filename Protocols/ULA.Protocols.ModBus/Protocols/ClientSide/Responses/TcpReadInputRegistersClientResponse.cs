namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses
{
    /// <summary>
    /// Represents a tcp modbus response with read input registers
    /// </summary>
    public class TcpReadInputRegistersClientResponse : TcpModbusClientResponse
    {
        #region [Properties]
        /// <summary>
        /// Data of raw message frame
        /// </summary>
        public byte[] RawData { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpReadInputRegistersClientResponse"/>
        /// </summary>
        /// <param name="transactionId">The unique transaction identifier for current request-response</param>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="numberOfPoints">The number of points for current request</param>
        /// <param name="data">A collection of data that has been read</param>
        public TcpReadInputRegistersClientResponse(ushort transactionId, byte functionCode, ushort numberOfPoints, byte[] data)
            : base(transactionId, functionCode, numberOfPoints)
        {
            this.RawData = data;
        }
        #endregion [Ctor's]
    }
}