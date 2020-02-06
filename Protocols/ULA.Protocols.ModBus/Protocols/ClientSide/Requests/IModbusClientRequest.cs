namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests
{
    /// <summary>
    /// Exposes client modbus request functionality
    /// </summary>
    public interface IModbusClientRequest : IModbusRequest
    {
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        void ValidateResponse(IModbusResponse response);
        /// <summary>
        /// Creates an instance of a response for current request
        /// </summary>
        /// <param name="requestedResponse">A raw response data</param>
        /// <returns>An instance of a response for current request</returns>
        IModbusResponse CreateResponseFromFrame(byte[] requestedResponse);
        /// <summary>
        /// Build message frame for request
        /// </summary>
        /// <returns>Message frame</returns>
        byte[] BuildRequestFrame();
    }
}