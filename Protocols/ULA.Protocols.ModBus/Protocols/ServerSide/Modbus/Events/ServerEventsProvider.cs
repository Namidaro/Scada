using System;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Represents a server events provider functionality
    /// </summary>
    internal class ServerEventsProvider : Disposable, IServerEventsProvider, IServerEventsExecutor
    {
        #region [Private fields]
        private ModbusServerBase _currentServer; 
        #endregion [Private fields]


        #region [IServerEventsProvider members]
        /// <summary>
        /// Occures on read inut registers request
        /// </summary>
        public event EventHandler<ReadInputRegistersEventArgs> OnReadInputRegistersRequest;
        /// <summary>
        /// Occures on write multiple registers request
        /// </summary>
        public event EventHandler<WriteMultipleRegistersEventArgs> OnWriteMultipleRegistersRequest; 
        #endregion [IServerEventsProvider members]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ServerEventsProvider"/>
        /// </summary>
        /// <param name="currentServer">An instance of <see cref="ModbusServerBase"/> to use</param>
        internal ServerEventsProvider(ModbusServerBase currentServer)
        {
            Guard.AgainstNullReference(currentServer, "currentServer");

            this._currentServer = currentServer;
        } 
        #endregion [Ctor's]


        #region [Internal members]
        /// <summary>
        /// Executes OnReadInputRegistersRequest event
        /// </summary>
        /// <param name="args">An instance of OnReadInputRegistersRequest arguments</param>
        void IServerEventsExecutor.OnReadInputRegistersRequest(ReadInputRegistersEventArgs args)
        {
            this.ThrowIfDisposed();
            this.OnReadInputRegistersRequest.Raise(this._currentServer, args);
        }
        /// <summary>
        /// Executes OnWriteMultipleRegistersRequest event
        /// </summary>
        /// <param name="args">An instance of OnWriteMultipleRegistersRequest arguments</param>
        void IServerEventsExecutor.OnWriteMultipleRegistersRequest(WriteMultipleRegistersEventArgs args)
        {
            this.ThrowIfDisposed();
            this.OnWriteMultipleRegistersRequest.Raise(this._currentServer, args);
        } 
        #endregion [Internal members]


        #region [Disposable members]
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this._currentServer = null;
            base.OnDisposing();
        } 
        #endregion [Disposable members]
    }
}