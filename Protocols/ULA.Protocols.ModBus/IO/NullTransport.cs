using System;
using System.Threading;
using System.Threading.Tasks;

namespace YP.Toolkit.Protocols.ModBus.IO
{
    /// <summary>
    /// Represents a null-object pattern implenetation for transport functionality
    /// </summary>
    public class NullTransport : ITransport
    {
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
        public int ReadTimeout
        {
            get { return Timeout.Infinite; }
            set { throw new NotSupportedException(); }
        }
        /// <summary>
        /// Gets or sets the number of milliseconds before a timeout occurs when a write operation does not finish.
        /// </summary>
        public int WriteTimeout
        {
            get { return Timeout.Infinite; }
            set { throw new NotSupportedException(); }
        }
        /// <summary>
        /// Opens a source connection
        /// </summary>
        public void OpenConnection()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Closes a source connection
        /// </summary>
        public void CloseConnection()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Opens a source connection asynchronously
        /// </summary>
        public Task OpenConnectionAsync()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Closes a source connection asynchronously
        /// </summary>
        public Task CloseConnectionAsync()
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>The number of bytes read.</returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        public void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Writes a specified number of bytes to the port from an output buffer, starting at the specified offset asynchronously.
        /// </summary>
        /// <param name="buffer">The byte array that contains the data to write to the port.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to write.</param>
        /// <returns>A task to return</returns>
        public Task WriteAsync(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Reads a number of bytes from the input buffer and writes those bytes into a byte array at the specified offset asynchronoulsy.
        /// </summary>
        /// <param name="buffer">The byte array to write the input to.</param>
        /// <param name="offset">The offset in the buffer array to begin writing.</param>
        /// <param name="count">The number of bytes to read.</param>
        /// <returns>A task to return</returns>
        public Task<int> ReadAsync(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            //NONE
        }
        #endregion [ITransport members]
    }
}