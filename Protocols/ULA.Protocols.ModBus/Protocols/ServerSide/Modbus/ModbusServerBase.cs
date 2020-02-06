using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus
{
    /// <summary>
    /// Represents a basic server modbus functionality
    /// </summary>
    public abstract class ModbusServerBase : ModbusBase, IModbusServerHandler
    {
        #region [Private fields]
        private readonly IServerEventsProvider _serverEventsProvider;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// An address of a slave device
        /// </summary>
        public byte SlaveAddress { get; private set; }
        /// <summary>
        /// Gets an instance of server events provider
        /// </summary>
        public IServerEventsProvider ServerEvents
        {
            get { return this._serverEventsProvider; }
        }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ModbusServerBase"/>
        /// </summary>
        /// <param name="slaveAddress">An address of a slave device</param>
        /// <param name="transport">An instance of a transport to use while processing a request</param>
        protected ModbusServerBase(byte slaveAddress, ITransport transport)
            : base(transport)
        {
            this.SlaveAddress = slaveAddress;
            this._serverEventsProvider = new ServerEventsProvider(this);
        }
        #endregion [Ctor's]


        #region [IServerModbusHandler members]
        /// <summary>
        /// Applies an incomming request
        /// </summary>
        /// <param name="request">An instance of incomming request to process</param>
        /// <returns>An instance of the response for the incomming request</returns>
        IModbusServerResponse IModbusServerHandler.ApplyRequest(IModbusServerRequest request)
        {
            return this.OnApplyRequest(request);
        }
        #endregion [IServerModbusHandler members]


        #region [Abstract members]
        /// <summary>
        /// Starts listenning to connections
        /// </summary>
        public abstract void StartListen();
        /// <summary>
        /// Stops listenning to connections
        /// </summary>
        public abstract void StopListen();
        #endregion [Abstract members]


        #region [Templated members]
        /// <summary>
        /// Applies an incomming request
        /// </summary>
        /// <param name="request">An instance of incomming request to process</param>
        /// <returns>An instance of the response for the incomming request</returns>
        protected virtual IModbusServerResponse OnApplyRequest(IModbusServerRequest request)
        {
            return request.Process((IServerEventsExecutor)this.ServerEvents);
        }
        #endregion [Templated members]


        #region [Override members]
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this._serverEventsProvider.Dispose();
            base.OnDisposing();
        } 
        #endregion [Override members]
    }
}