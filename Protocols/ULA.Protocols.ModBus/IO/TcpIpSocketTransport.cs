using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.Validation;


namespace YP.Toolkit.Protocols.ModBus.IO
{
    /// <summary>
    /// Represents a tcp/ip socket transport functionality
    /// </summary>
    public class TcpIpSocketTransport : TransportBase
    {
        #region [Constants]
        private const string START_CONNECTED_PATTERN = "Start connect to Host:{0} Port:{1}";
        private const string START_DISCONNECTED_PATTERN = "Start disconnect from Host:{0} Port:{1}";
        private const string END_CONNECTED_PATTERN = "Connected to Host:{0} Port:{1}";
        private const string END_DISCONNECTED_PATTERN = "Disconnected from Host:{0} Port:{1}";
        private const string CONNECTION_ERROR_PATTERN = "Can't connected to Host:{0} Port:{1}";
        private const string DISCONNECTION_ERROR_PATTERN = "Can't close connection to Host:{0} Port:{1}";
        private const string READ_ERROR_PATTERN = "Can't read bytes from Host:{0} Port:{1}";
        private const string WRITE_ERROR_PATTERN = "Can't write bytes to Host:{0} Port:{1}";
        #endregion [Constants]


        #region [Private fields]
        private Socket _socket;
        private readonly int _port;
        private readonly string _host;
        private int _readTimeout = 10000;
        private int _writeTimeout = 10000;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpIpSocketTransport"/>
        /// </summary>
        /// <param name="host">The name of a remote host</param>
        /// <param name="port">The port number of a remote host</param>
        public TcpIpSocketTransport(string host, int port)
        {
            this._port = port;
            this._host = host;
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        public override int ReadTimeout
        {
            get { return _readTimeout; }
            set { _readTimeout = value; }
        }
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public override int WriteTimeout
        {
            get { return _writeTimeout; }
            set { _writeTimeout = value; }
        }
        /// <summary>
        /// Opens a source connection
        /// </summary>
        protected override void OnOpenConnection()
        {
            this._logger.Log(String.Format(START_CONNECTED_PATTERN, this._host, this._port));
            this.OnCloseConnection();
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP)
                {
                    SendTimeout = this.WriteTimeout,
                    ReceiveTimeout = this.ReadTimeout
                };
            this._socket.Connect(this._host, this._port);
            this._logger.Log(String.Format(END_CONNECTED_PATTERN, this._host, this._port));
        }
        /// <summary>
        /// Closes a source connection
        /// </summary>
        protected override void OnCloseConnection()
        {
            if (this._socket == null) return;
            this._logger.Log(String.Format(START_DISCONNECTED_PATTERN, this._host, this._port));
            this._socket.Dispose();
            this._socket = null;
            this._logger.Log(String.Format(END_DISCONNECTED_PATTERN, this._host, this._port));
        }
        /// <summary>
        /// Opens a source connection asynchronously
        /// </summary>
        protected override Task OnOpenConnectionAsync()
        {

            return this.OnCloseConnectionAsync()
                .ContinueWith(task =>
                {
                    this._logger.Log(String.Format(START_CONNECTED_PATTERN, this._host, this._port));
                    this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP)
                        {
                            SendTimeout = this.WriteTimeout,
                            ReceiveTimeout = this.ReadTimeout
                        };
                }, TaskContinuationOptions.ExecuteSynchronously)
                .Then(() => Task.Factory.FromAsync(this._socket.BeginConnect, this._socket.EndConnect,
                             this._host, this._port, null).ContinueWith(t =>
                             {
                                 if (t.IsFaulted)
                                 {
                                     this._logger.Log(String.Format(CONNECTION_ERROR_PATTERN, this._host, this._port), t.Exception, Category.ERROR, Priority.HIGH);
                                     Guard.Throw<ConnectionLostException>(CONNECTION_ERROR_PATTERN, t.Exception);
                                 }
                                 this._logger.Log(String.Format(END_CONNECTED_PATTERN, this._host, this._port));

                             }));
        }
        /// <summary>
        /// Closes a source connection asynchronously 
        /// </summary>
        protected override Task OnCloseConnectionAsync()
        {
            if (this._socket == null) return CompletedTask.Default;
            this._logger.Log(String.Format(START_DISCONNECTED_PATTERN, this._host, this._port));
            return Task.Factory.FromAsync(this._socket.BeginDisconnect, this._socket.EndDisconnect, true, null)
                .ContinueWith(task =>
                    {
                        this._socket.Dispose();
                        this._socket = null;
                        if (task.IsFaulted)
                        {
                            this._logger.Log(String.Format(DISCONNECTION_ERROR_PATTERN, this._host, this._port), task.Exception, Category.ERROR, Priority.HIGH);
                            Guard.Throw<ConnectionLostException>(DISCONNECTION_ERROR_PATTERN, task.Exception);
                        }
                        this._logger.Log(String.Format(END_DISCONNECTED_PATTERN, this._host, this._port));

                    }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        protected override int OnRead(byte[] buffer, int offset, int count)
        {
            return this._socket.Receive(buffer, offset, count, SocketFlags.None);
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        protected override void OnWrite(byte[] buffer, int offset, int count)
        {
            var result = this._socket.Send(buffer, offset, count, SocketFlags.None);
            if (result == count)
                return;
            this._logger.Log(String.Format(WRITE_ERROR_PATTERN, this._host, this._port), Category.ERROR, Priority.HIGH);
            Guard.Throw<TransportWriteException>("The numbers of sent bytes are different");
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset asynchronously.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>A task to return</returns>
        protected override Task OnWriteAsync(byte[] buffer, int offset, int count)
        {
            return Task<int>.Factory.FromAsync(this.BeginSendInternal, this.EndSendInternal, buffer, offset, count, null)
                .ContinueWith(task =>
                    {
                        if (task.IsFaulted && task.Exception != null)
                        {
                            this._logger.Log(String.Format(WRITE_ERROR_PATTERN, this._host, this._port), task.Exception, Category.ERROR, Priority.HIGH);
                            Guard.Throw<ConnectionLostException>(WRITE_ERROR_PATTERN, task.Exception);
                        }
                        if (task.Result == count)
                            return;
                        this._logger.Log(String.Format(WRITE_ERROR_PATTERN, this._host, this._port), Category.ERROR, Priority.HIGH);
                        Guard.Throw<TransportWriteException>("The numbers of sent bytes are different");
                    }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset asynchronoulsy.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A task to return</returns>
        protected override Task<int> OnReadAsync(byte[] buffer, int offset, int count)
        {
            return Task<int>.Factory.FromAsync(this.BeginReceiveInternal, this.EndReceiveInternal, buffer, offset, count, null)
                .ContinueWith(task =>
            {
                if (task.IsFaulted && task.Exception != null)
                {
                    this._logger.Log(String.Format(READ_ERROR_PATTERN, this._host, this._port), task.Exception, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ConnectionLostException>(READ_ERROR_PATTERN, task.Exception);
                }
                return task.Result;
            }, TaskContinuationOptions.ExecuteSynchronously);
        }
        #endregion [Override members]


        #region [Help members]
        private IAsyncResult BeginSendInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._socket.BeginSend(buffer, offset, count, SocketFlags.None, callback, state);
        }
        private int EndSendInternal(IAsyncResult result)
        {
            return this._socket.EndSend(result);
        }
        private IAsyncResult BeginReceiveInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._socket.BeginReceive(buffer, offset, count, SocketFlags.None, callback, state);
        }
        private int EndReceiveInternal(IAsyncResult result)
        {
            return this._socket.EndReceive(result);
        }
        #endregion  [Help members]
    }
}