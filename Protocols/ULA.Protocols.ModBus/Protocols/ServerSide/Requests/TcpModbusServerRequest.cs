using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests
{
    /// <summary>
    /// Represents a wrapper for tcp modbus request
    /// </summary>
    internal class TcpModbusServerRequest : IModbusServerRequest
    {
        #region [Private fields]
        private readonly IModbusServerRequest _innerRequest;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets or sets a transaction ID
        /// </summary>
        public ushort TransactionId { get; internal set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusServerRequest"/>
        /// </summary>
        /// <param name="innerRequest">An instance of inner modbus request to wrap</param>
        public TcpModbusServerRequest(IModbusServerRequest innerRequest)
        {
            Guard.AgainstNullReference(innerRequest, "innerModbusRequest");

            this._innerRequest = innerRequest;
        }
        #endregion [Ctor's]


        #region [IServerModbusRequest members]
        /// <summary>
        /// Initializes an instance of current request from a collection of bytes
        /// </summary>
        /// <param name="frame">A collection of bytes to initialize current request from</param>
        public void Initialize(byte[] frame)
        {
            this._innerRequest.Initialize(frame);
        }
        /// <summary>
        /// Processes a modbus server request
        /// </summary>
        /// <param name="serverEvents">An instance of server events provider</param>
        /// <returns>An instance of modbus server response for current request</returns>
        public IModbusServerResponse Process(IServerEventsExecutor serverEvents)
        {
            return this._innerRequest.Process(serverEvents);
        }
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode
        {
            get { return this._innerRequest.FunctionCode; }
        }
        /// <summary>
        /// Gets an address of a request
        /// </summary>
        public ushort Address
        {
            get { return this._innerRequest.Address; }
        }
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        public byte SlaveAddress
        {
            get { return this._innerRequest.SlaveAddress; }
        } 
        #endregion [IServerModbusRequest members]
    }
}