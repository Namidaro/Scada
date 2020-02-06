namespace YP.Toolkit.Protocols.ModBus.Protocols
{
    /// <summary>
    /// Exposes modbus request functionality
    /// </summary>
    public interface IModbusRequest
    {
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        byte FunctionCode { get; }
        /// <summary>
        /// Gets an address of a request
        /// </summary>
        ushort Address { get; }
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        byte SlaveAddress { get; }
    }
}