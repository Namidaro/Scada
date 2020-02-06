namespace YP.Toolkit.Protocols.ModBus.Protocols
{
    /// <summary>
    /// Exposes modbus response functionality
    /// </summary>
    public interface IModbusResponse
    {
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        byte FunctionCode { get; set; }
        /// <summary>
        /// Gets the number of points for current request
        /// </summary>
        ushort NumberOfPoints { get; set; }
    }
}