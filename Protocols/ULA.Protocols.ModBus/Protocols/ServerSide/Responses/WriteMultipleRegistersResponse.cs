using System;
using YP.Toolkit.Protocols.ModBus.Logger;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses
{
    /// <summary>
    /// Represents a write multiple registers server response functionality
    /// </summary>
    public class WriteMultipleRegistersResponse : ModbusServerResponseBase
    {
        #region [Private fields]
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets a start address of a data
        /// </summary>
        public ushort Address { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="WriteMultipleRegistersResponse"/>
        /// </summary>
        /// <param name="slaveAddress">Address of slave device</param>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="address">An address of current request</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        public WriteMultipleRegistersResponse(byte slaveAddress, byte functionCode, ushort address, ushort numberOfPoints)
            : base(slaveAddress, functionCode, numberOfPoints)
        {
            this.Address = address;
        }
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Builds a server message frame as byte array.
        /// </summary>
        /// <returns>Full frame</returns>
        public override byte[] BuildMessageFrame()
        {
            var frame = new[]
                {
                    this.SlaveAddress,
                    this.FunctionCode,
                    (byte)((this.Address >> 8) & 0xff),
                    (byte)this.Address,
                    (byte)((this.NumberOfPoints >> 8) & 0xff),
                    (byte)this.NumberOfPoints
                };
            this._logger.Log(String.Format("Request to write {0} words processed successfully", this.NumberOfPoints));
            return frame;
        }
        #endregion [Override members]
    }
}