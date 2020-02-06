using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests
{
    /// <summary>
    /// Exposes a modbus server request functionality
    /// </summary>
    public interface IModbusServerRequest : IModbusRequest
    {
        /// <summary>
        /// Initializes an instance of current request from a collection of bytes
        /// </summary>
        /// <param name="frame">A collection of bytes to initialize current request from</param>
        void Initialize(byte[] frame);
        /// <summary>
        /// Processes a modbus server request
        /// </summary>
        /// <param name="serverEvents">An instance of server events provider</param>
        /// <returns>An instance of modbus server response for current request</returns>
        IModbusServerResponse Process(IServerEventsExecutor serverEvents);
    }
}