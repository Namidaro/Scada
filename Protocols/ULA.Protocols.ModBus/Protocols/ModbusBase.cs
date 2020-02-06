using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols
{
    /// <summary>
    /// Represents a base modbus protocol functionality
    /// </summary>
    public abstract class ModbusBase : Disposable
    {
        #region [Protected fields]
        /// <summary>
        /// An instance of a transport to use in order to obtain data from an end point
        /// </summary>
        protected ITransport _transport;
        #endregion [Protected fields]


        #region [Properties]
        /// <summary>
        /// Gets or sets the read timeout of the source transport.
        /// </summary>
        public int ReadTimeout
        {
            get { return this._transport.ReadTimeout; }
            set { this._transport.ReadTimeout = value; }
        }
        /// <summary>
        /// Gets or sets the write timeout for current transport.
        /// </summary>
        public int WriteTimeout
        {
            get { return this._transport.WriteTimeout; }
            set { this._transport.WriteTimeout = value; }
        }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ModbusBase"/>
        /// </summary>
        /// <param name="transport">An instance of a transport to use while processing a request</param>
        protected ModbusBase(ITransport transport)
        {
            Guard.AgainstNullReference(transport, "transport");
            this._transport = transport;
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.ThrowIfDisposed();
            if (this._transport != null)
                this._transport.Dispose();
            this._transport = null;
            base.OnDisposing();
        }
        #endregion [Override members]
    }
}