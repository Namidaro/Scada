using System;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.IO
{
    /// <summary>
    /// Represents a transport that keeps connection alive while there is no connection issue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KeepConnectionTransport<T> : TransportBase where T : TransportBase
    {
        #region [Constants]
        private const string CONNECTION_ERROR = "Client. Occurred error while connection";
        private const string DISCONNECTION_ERROR = "Client. Occurred error while close connection";
        private const string READ_ERROR = "Client. Occurred error while read bytes";
        private const string WRITE_ERROR = "Client. Occurred error while write bytes";
        private const string START_HOLDING_CONNECTION = "Start holding connection";
        private const string STOP_HOLDING_CONNECTION_SESSION = "Stop holding connection session";
        #endregion [Constants]
        

        #region [Private fields]
        private T _innerTransport;
        private volatile bool _lastConnectionSucceeded;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="KeepConnectionTransport{T}"/>
        /// </summary>
        /// <param name="innerTransport">An instance of an inner transport to use</param>
        public KeepConnectionTransport(T innerTransport)
        {
            Guard.AgainstNullReference(innerTransport, "innerTransport");
            this._innerTransport = innerTransport;
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        public override int ReadTimeout
        {
            get { return this._innerTransport.ReadTimeout; }
            set { this._innerTransport.ReadTimeout = value; }
        }
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public override int WriteTimeout
        {
            get { return this._innerTransport.WriteTimeout; }
            set { this._innerTransport.WriteTimeout = value; }
        }
        /// <summary>
        /// Opens a source connection
        /// </summary>
        protected override void OnOpenConnection()
        {
            if (this._lastConnectionSucceeded) return;
            try
            {
             //   this._innerTransport.OpenConnection();
                this._lastConnectionSucceeded = true;
            }
            catch (ConnectionLostException exc)
            {
                this._logger.Log(CONNECTION_ERROR, exc, Category.ERROR, Priority.HIGH);
                this._lastConnectionSucceeded = false;
                Guard.Throw<ConnectionLostException>(CONNECTION_ERROR, exc);
            }
        }
        /// <summary>
        /// Closes a source connection
        /// </summary>
        protected override void OnCloseConnection()
        {
            if (!this._lastConnectionSucceeded)
                this._innerTransport.CloseConnection();
        }
        /// <summary>
        /// Opens a source connection asynchronously
        /// </summary>
        protected override Task OnOpenConnectionAsync()
        {
            if (this._lastConnectionSucceeded) return CompletedTask.Default;
            return this._innerTransport.OpenConnectionAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        this._lastConnectionSucceeded = false;
                        if (task.Exception != null)
                        {
                            this._logger.Log(CONNECTION_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                            Guard.Throw<ConnectionLostException>(CONNECTION_ERROR, task.Exception);
                        }
                    }
                    this._lastConnectionSucceeded = true;
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Closes a source connection asynchronously
        /// </summary>
        protected override Task OnCloseConnectionAsync()
        {
            if (this._lastConnectionSucceeded) return CompletedTask.Default;
            return this._innerTransport.CloseConnectionAsync().ContinueWith(task =>
                {
                    if (!task.IsFaulted) return;
                    this._lastConnectionSucceeded = false;
                    if (task.Exception != null)
                    {
                        this._logger.Log(DISCONNECTION_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<DisconnectionException>(DISCONNECTION_ERROR, task.Exception);
                    }
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
            try
            {
                return this._innerTransport.Read(buffer, offset, count);
            }
            catch (Exception exc)
            {
                this._logger.Log(READ_ERROR, exc, Category.ERROR, Priority.HIGH);
                this._lastConnectionSucceeded = false;
                Guard.Throw<TransportReadException>(READ_ERROR, exc);
                return -1;
            }
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        protected override void OnWrite(byte[] buffer, int offset, int count)
        {
            try
            {
                this._innerTransport.Write(buffer, offset, count);
            }
            catch (Exception exc)
            {
                this._logger.Log(WRITE_ERROR, exc, Category.ERROR, Priority.HIGH);
                this._lastConnectionSucceeded = false;
                Guard.Throw<TransportWriteException>(WRITE_ERROR, exc);
            }
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
            return this._innerTransport.WriteAsync(buffer, offset, count).ContinueWith(task =>
                {
                    if (!task.IsFaulted) return;
                    this._lastConnectionSucceeded = false;
                    if (task.Exception != null)
                    {
                        this._logger.Log(WRITE_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<TransportWriteException>(WRITE_ERROR, task.Exception);
                    }
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
            return this._innerTransport.ReadAsync(buffer, offset, count).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        this._lastConnectionSucceeded = false;
                        if (task.Exception != null)
                        {
                            this._logger.Log(READ_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                            Guard.Throw<TransportReadException>(READ_ERROR, task.Exception); 
                        }
                        return -1;
                    }
                    return task.Result;
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.ThrowIfDisposed();
            this._innerTransport.CloseConnection();
            this._innerTransport.Dispose();
            this._innerTransport = null;
        }
        #endregion [Override members]


        #region [Public members]

        /// <summary>
        /// Closes a source connection session
        /// </summary>
        public void CloseConnectionSession()
        {
            this.CloseConnection();
            this._logger.Log(STOP_HOLDING_CONNECTION_SESSION);
        }
        /// <summary>
        /// Opens a source connection session asynchronously
        /// </summary>
        public Task OpenConnectionSessionAsync()
        {
            this._logger.Log(START_HOLDING_CONNECTION);
            return this._isConnected ? CompletedTask.Default : this.OpenConnectionAsync();
        }
        /// <summary>
        /// Closes a source connection session asynchronously
        /// </summary>
        public Task CloseConnectionSessionAsync()
        {
            if (!this._isConnected) return CompletedTask.Default;
            return this._innerTransport.CloseConnectionAsync().ContinueWith(task =>
                {
                    this._lastConnectionSucceeded = false;
                    if (!task.IsFaulted)
                    {
                        this._logger.Log(STOP_HOLDING_CONNECTION_SESSION);
                        return;
                    }
                    if (task.Exception != null)
                    {
                        this._logger.Log(DISCONNECTION_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<DisconnectionException>(DISCONNECTION_ERROR, task.Exception);
                    }
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        #endregion [Public members]
    }
}