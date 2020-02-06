using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus
{
    /// <summary>
    /// Exposes modbus server requests handling functionality
    /// </summary>
    public interface IModbusServerHandler
    {
        /// <summary>
        /// Applies an incomming request
        /// </summary>
        /// <param name="request">An instance of incomming request to process</param>
        /// <returns>An instance of the response for the incomming request</returns>
        IModbusServerResponse ApplyRequest(IModbusServerRequest request);
    }
}