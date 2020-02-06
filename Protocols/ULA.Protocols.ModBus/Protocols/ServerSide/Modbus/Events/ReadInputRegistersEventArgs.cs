using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events
{
    /// <summary>
    /// Represents a read input registers request arguments
    /// </summary>
    public class ReadInputRegistersEventArgs : ServerEventArgs<ReadInputRegistersServerRequest>
    {
        #region [Properties]
        /// <summary>
        /// Gets a number of registers to read
        /// </summary>
        public ushort NumberOfRegistersToRead
        {
            get { return this._request.NumberOfPoints; }
        }
        /// <summary>
        /// Gets or sets a collection of bytes that represents read registers
        /// </summary>
        public byte[] ReadRegisters { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instanc of <see cref="ReadInputRegistersEventArgs"/>
        /// </summary>
        /// <param name="request">An instance of request</param>
        internal ReadInputRegistersEventArgs(ReadInputRegistersServerRequest request)
            : base(request)
        { }
        #endregion [Ctor's]
    }
}