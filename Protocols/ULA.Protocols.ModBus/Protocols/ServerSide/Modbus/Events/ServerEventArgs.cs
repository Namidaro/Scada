using System;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Represents a server event arguments
    /// </summary>
    /// <typeparam name="TRequest">A type of server request</typeparam>
    public abstract class ServerEventArgs<TRequest> : EventArgs where TRequest : IModbusServerRequest
    {
        #region [Protected fields]
        /// <summary>
        /// An instance of request
        /// </summary>
        protected TRequest _request;
        #endregion [Protected fields]


        #region [Properties]
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public ushort FunctionCode
        {
            get { return this._request.FunctionCode; }
        }
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        public byte SlaveAddress
        {
            get { return this._request.SlaveAddress; }
        }
        /// <summary>
        /// Gets an address of a request
        /// </summary>
        public ushort Address
        {
            get { return this._request.Address; }
        }
        /// <summary>
        /// Gets or sets a value that indicats whether a request has been processed or not
        /// </summary>
        public bool Handled { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ServerEventArgs{TRequest}"/>
        /// </summary>
        /// <param name="request">An instance of request</param>
        protected ServerEventArgs(TRequest request)
        {
            this._request = request;
            this.Handled = false;
        }
        #endregion [Ctor's]
    }
}