namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses
{
    /// <summary>
    /// Represents a tcp modbus response of writting multiple registers
    /// </summary>
    public class TcpWriteMultipleRegistersClientResponse : TcpModbusClientResponse
    {
        #region [Properties]
        /// <summary>
        /// Gets a start address of a data
        /// </summary>
        public ushort Address { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpReadInputRegistersClientResponse"/>
        /// </summary>
        /// <param name="transactionId">The unique transaction identifier for current request-response</param>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="numberOfPoints">The number of points for current request</param>
        /// <param name="startAddress">Start address of a data</param>
        public TcpWriteMultipleRegistersClientResponse(ushort transactionId, byte functionCode, ushort numberOfPoints, ushort startAddress)
            : base(transactionId, functionCode, numberOfPoints)
        {
            this.Address = startAddress;
        }
        #endregion [Ctor's]
    }
}