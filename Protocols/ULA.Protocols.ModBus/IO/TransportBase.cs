using System;
using System.Threading;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.IO
{
    /// <summary>
    /// Represents a base trasport class funtionality
    /// </summary>
    public abstract class TransportBase : Disposable, ITransport
    {
        #region [Constants]
        private const string CONNECTION_ERROR = "Client. An error occured while connecting to end point";
        private const string DISCONNECTION_ERROR = "Client. An error occured while disconnecting from end point";
        private const string NOT_CONNECTED_READ_ERROR = "Client. Can't perform read action, because a connection hasn't been extablished";
        private const string READ_ERROR = "Client. An error occured while processing read operation";
        private const string NOT_CONNECTED_WRITE_ERROR = "Client. Can't perform write action, because a connection hasn't been extablished";
        private const string WRITE_ERROR = "Client. An error occured while processing write operation";
        #endregion [Constants]

        
        #region [Private fields]
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Protected fields]
        /// <summary>
        /// Indicates whether a connection to an endpoint is established or not
        /// </summary>
        protected volatile bool _isConnected;
        #endregion [Protected fields]


        #region [ITransport members]
        /// <summary>
        /// Indicates that no timeout should occur.
        /// </summary>
        public int InfiniteTimeout
        {
            get { return Timeout.Infinite; }
        }
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a read operation does not finish.
        /// </summary>
        public abstract int ReadTimeout { get; set; }
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public abstract int WriteTimeout { get; set; }
        /// <summary>
        /// Opens a source connection
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        public void OpenConnection()
        {
            this.ThrowIfDisposed();
            if(this._isConnected) return;
            try
            {
                this.OnOpenConnection();
                this._isConnected = true;
            }
            catch (Exception error)
            {
                this._isConnected = false;
                this._logger.Log(CONNECTION_ERROR, error, Category.ERROR, Priority.HIGH);
                Guard.Throw<ConnectionLostException>(CONNECTION_ERROR, error);
            }
        }
        /// <summary>
        /// Closes a source connection
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        public void CloseConnection()
        {
            this.ThrowIfDisposed();
            if (!this._isConnected) return;
            try
            {
                this.OnCloseConnection();
            }
            catch (Exception error)
            {
                this._logger.Log(DISCONNECTION_ERROR, error, Category.ERROR, Priority.HIGH);
                Guard.Throw<ConnectionLostException>(DISCONNECTION_ERROR, error);
            }
            finally
            {
                this._isConnected = false;
            }
        }
        /// <summary>
        /// Opens a source connection asynchronously
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        public Task OpenConnectionAsync()
        {
            this.ThrowIfDisposed();
            if (this._isConnected) return CompletedTask.Default;
            return this.OnOpenConnectionAsync().ContinueWith(task =>
                {
                    this._isConnected = true;
                    if (!task.IsFaulted) return;
                    this._isConnected = false;
                    this._logger.Log(CONNECTION_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ConnectionLostException>("An error occured while connecting to end point asynchronously", task.Exception);
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Closes a source connection asynchronously
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        public Task CloseConnectionAsync()
        {
            this.ThrowIfDisposed();
            if (!this._isConnected) return CompletedTask.Default;
            return this.OnCloseConnectionAsync().ContinueWith(task =>
                {
                    this._isConnected = false;
                    if (task.IsFaulted)
                    {
                        this._logger.Log(DISCONNECTION_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<ConnectionLostException>("An error occured while disconnecting from end point asynchronously", task.Exception);
                    }
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset.
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            this.ThrowIfDisposed();
            if (!this._isConnected)
            {
                this._logger.Log(NOT_CONNECTED_READ_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<DisconnectionException>(NOT_CONNECTED_READ_ERROR);
            }
            try
            {
                return this.OnRead(buffer, offset, count);
            }
            catch (Exception error)
            {
                this._logger.Log(READ_ERROR, error, Category.ERROR, Priority.HIGH);
                Guard.Throw<ConnectionLostException>(READ_ERROR, error);
                return -1;
            }
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset.
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            this.ThrowIfDisposed();
            if (!this._isConnected)
            {
                this._logger.Log(NOT_CONNECTED_WRITE_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<DisconnectionException>(NOT_CONNECTED_WRITE_ERROR);
            }
            try
            {
                this.OnWrite(buffer, offset, count);
            }
            catch (Exception error)
            {
                this._logger.Log(WRITE_ERROR, error, Category.ERROR, Priority.HIGH);
                Guard.Throw<ConnectionLostException>(WRITE_ERROR, error);
            }
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset asynchronously.
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>A task to return</returns>
        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            this.ThrowIfDisposed();
            if (!this._isConnected)
            {
                this._logger.Log(NOT_CONNECTED_WRITE_ERROR, Category.ERROR, Priority.HIGH);
                return Task.Factory.FromException(new DisconnectionException(NOT_CONNECTED_WRITE_ERROR));
            }
            return this.OnWriteAsync(buffer, offset, count).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        this._logger.Log(WRITE_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<ConnectionLostException>("An error occured while processing write operation asynchronously", task.Exception);
                    }
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset asynchronoulsy.
        /// 
        /// 
        /// <exception cref="ConnectionLostException">Will be thrown when there is any exception while processing current action</exception>
        /// 
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A task to return</returns>
        public Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            this.ThrowIfDisposed();
            if (!this._isConnected)
            {
                this._logger.Log(NOT_CONNECTED_READ_ERROR, Category.ERROR, Priority.HIGH);
                return Task<int>.Factory.FromException(new DisconnectionException(NOT_CONNECTED_READ_ERROR));
            }
            return this.OnReadAsync(buffer, offset, count).ContinueWith(task =>
                {
                    if (!task.IsFaulted) return task.Result;
                    this._logger.Log(READ_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ConnectionLostException>("An error occured while processsing read operation asynchronously", task.Exception);
                    return -1;
                }, TaskContinuationOptions.ExecuteSynchronously);
        }
        /// <summary>
        /// Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.ThrowIfDisposed();
            this.CloseConnection();
            base.OnDisposing();
        }
        #endregion [ITransport members]


        #region [Abstract members]
        /// <summary>
        /// Opens a source connection
        /// </summary>
        protected abstract void OnOpenConnection();
        /// <summary>
        /// Closes a source connection
        /// </summary>
        protected abstract void OnCloseConnection();
        /// <summary>
        /// Opens a source connection asynchronously
        /// </summary>
        protected abstract Task OnOpenConnectionAsync();
        /// <summary>
        /// Closes a source connection asynchronously
        /// </summary>
        protected abstract Task OnCloseConnectionAsync();
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        protected abstract int OnRead(byte[] buffer, int offset, int count);
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        protected abstract void OnWrite(byte[] buffer, int offset, int count);
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset asynchronously.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>A task to return</returns>
        protected abstract Task OnWriteAsync(byte[] buffer, int offset, int count);
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset asynchronoulsy.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A task to return</returns>
        protected abstract Task<int> OnReadAsync(byte[] buffer, int offset, int count); 
        #endregion [Abstract members]
    }
}