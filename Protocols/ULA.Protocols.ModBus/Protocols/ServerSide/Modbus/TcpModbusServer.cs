using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Connections;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus
{
    /// <summary>
    /// Represents a tcp server that uses modbus protocol
    /// </summary>
    public class TcpModbusServer : ModbusServerBase, ITcpModbusServerHandler
    {
        private bool disconnected = false;
        #region [Private fields]
        private readonly string _host;
        private readonly int _port;
        private Socket _socket;
        private bool _isLaunched;
        private readonly object _syncObject = new object();
        private readonly IDictionary<string, TcpModbusServerConnection> _connections =
            new ConcurrentDictionary<string, TcpModbusServerConnection>();
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        private CancellationTokenSource cancellationToken;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusServer"/>
        /// </summary>
        /// <param name="host">A host address to listen to</param>
        /// <param name="port">A port to listen to</param>
        /// <param name="slaveAddress">An address of a slave device</param>
        public TcpModbusServer(string host, int port, byte slaveAddress)
            : base(slaveAddress, new NullTransport())
        {
            Guard.AgainstEmptyStringOrNull(host, "host");

            this._host = host;
            this._port = port;
            cancellationToken = new CancellationTokenSource();
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Starts listenning to connections
        /// </summary>
        public override void StartListen()
        {
            this.ThrowIfDisposed();
            if (this._isLaunched) return;
            lock (this._syncObject)
            {
                if (this._isLaunched) return;
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP)
                {
                    SendTimeout = this.WriteTimeout,
                    ReceiveTimeout = this.ReadTimeout
                };
                this._socket.Bind(new IPEndPoint(IPAddress.Parse(this._host), this._port));
                this._socket.Listen(100);
                this._logger.Log(String.Format("Server start listen clients requests on Host:{0} Port:{1}", this._host, this._port));
                this.OnStartListenInternal();
            }
        }
        /// <summary>
        /// Stops listenning to connections
        /// </summary>
        public override void StopListen()
        {
            this.ThrowIfDisposed();
            if (!this._isLaunched) return;
            lock (this._syncObject)
            {
                if (!this._isLaunched) return;
                this._socket.Dispose();
                this._socket = null;
                this._isLaunched = false;
                this._logger.Log(String.Format("Server has stoped listen clients requests on Host:{0} Port:{1}", this._host, this._port));
            }
        }
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.StopListen();
            base.OnDisposing();
        } 
        #endregion [Override members]


        #region [IServerTcpModbusHandler members]
        /// <summary>
        /// Removes a connection from the server
        /// </summary>
        /// <param name="endPoint">An endpoint address to remove</param>
        void ITcpModbusServerHandler.RemoveConnection(string endPoint)
        {
            this.ThrowIfDisposed();
            if (!disconnected)
            {
                TcpModbusServerConnection connection;
                if (!this._connections.TryGetValue(endPoint, out connection))
                {
                    var errorMessage = String.Format(CultureInfo.InvariantCulture,
                                                     "EndPoint {0} cannot be removed, it does not exist.", endPoint);
                    this._logger.Log(errorMessage, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ServerDisconnectionException>(errorMessage);
                }
                this._connections.Remove(endPoint);
                connection.Dispose();
                cancellationToken.Cancel();
                this._logger.Log(String.Format("Connection with Host:{0} Port:{1} removed from the server", this._host,
                                               this._port));
                this.disconnected = true;
            }
        } 
        #endregion [IServerTcpModbusHandler members]


        #region [Help members]
        private void OnStartListenInternal()
        {
            Task.Factory.FromAsync<Socket>(this._socket.BeginAccept, this._socket.EndAccept, null)
                .ContinueWith(task =>
                    {
                        var masterConnection = new TcpModbusServerConnection(task.Result, this);
                        masterConnection.BeginProcess(cancellationToken);
                        this._connections.Add(task.Result.RemoteEndPoint.ToString(), masterConnection);
                        this._logger.Log(String.Format("Client with Host:{0} and Port:{1} connected to the server", this._host, this._port));
                        this.OnStartListenInternal();
                    }, TaskContinuationOptions.ExecuteSynchronously);
        } 
        #endregion [Help members]
    }
}
