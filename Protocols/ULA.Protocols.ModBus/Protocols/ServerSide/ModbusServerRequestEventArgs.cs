using System;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide
{
    /// <summary>
    /// Represets a server modbus request event arguments
    /// </summary>
    public class ModbusServerRequestEventArgs : EventArgs
    {
        #region [Private fields]
        private readonly IModbusServerRequest _request; 
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public IModbusRequest Request
        {
            get { return this._request; }
        } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ModbusServerRequestEventArgs"/>
        /// </summary>
        /// <param name="request">An instance of incomming request</param>
        internal ModbusServerRequestEventArgs(IModbusServerRequest request)
        {
            this._request = request;
        } 
        #endregion [Ctor's]
    }
}