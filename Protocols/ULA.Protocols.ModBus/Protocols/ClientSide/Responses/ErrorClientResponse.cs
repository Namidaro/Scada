namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses
{
    /// <summary>
    /// Represents a error client response
    /// </summary>
    public class ErrorClientResponse : IModbusResponse
    {
        /// <summary>
        /// Gets a start address of a data
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// Gets or sets a unique transaction identifier for current request-response
        /// </summary>
        public ushort TransactionId { get; set; }
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode { get; set; }
        /// <summary>
        /// Gets the number of points for current request
        /// </summary>
        public ushort NumberOfPoints { get; set; }
        /// <summary>
        /// Data of raw message frame
        /// </summary>
        public byte[] RawData { get; set; }
    }
}