namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus
{
    /// <summary>
    /// Exposes tcp modbus server requests handling functionality
    /// </summary>
    public interface ITcpModbusServerHandler : IModbusServerHandler
    {
        /// <summary>
        /// Removes a connection from the server
        /// </summary>
        /// <param name="endPoint">An endpoint address to remove</param>
        void RemoveConnection(string endPoint);
    }
}