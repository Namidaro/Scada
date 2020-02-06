namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses
{
    /// <summary>
    /// Represents a modbus server response functoinality
    /// </summary>
    public interface IModbusServerResponse : IModbusResponse
    {
        /// <summary>
        /// Builds a message frame as a collection of bytes
        /// </summary>
        /// <returns>Built message frame as collection of bytes</returns>
        byte[] BuildMessageFrame();
    }
}