using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Represents a write multiple registers request arguments
    /// </summary>
    public class WriteMultipleRegistersEventArgs : ServerEventArgs<WriteMultipleRegistersServerRequest>
    {
        #region [Properties]
        /// <summary>
        /// Gets a number of registers that should be written
        /// </summary>
        public ushort RegistersCountToWrite
        {
            get { return this._request.NumberOfPoints; }
        }
        /// <summary>
        /// Gets a number of registers in byte representation that should be written
        /// </summary>
        public ushort RegistersCountInBytesToWrite
        {
            get { return this._request.ByteCount; }
        }
        /// <summary>
        /// Gets a collection of registers that represents as collection of bytes to write
        /// </summary>
        public byte[] RegistersToWrite
        {
            get { return this._request.Data; }
        } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="WriteMultipleRegistersEventArgs"/>
        /// </summary>
        /// <param name="request">An instance of request</param>
        internal WriteMultipleRegistersEventArgs(WriteMultipleRegistersServerRequest request)
            : base(request)
        { } 
        #endregion [Ctor's]
    }
}