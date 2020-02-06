namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses
{
    /// <summary>
    /// Represents a base modbus server response functionality
    /// </summary>
    public abstract class ModbusServerResponseBase : IModbusServerResponse
    {
        #region [Properties]
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        public byte SlaveAddress { get; set; }
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode { get; set; }
        /// <summary>
        /// Gets the number of points for current request
        /// </summary>
        public ushort NumberOfPoints { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ModbusServerResponseBase"/>
        /// </summary>
        /// <param name="slaveAddress">Address of slave device</param>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        protected ModbusServerResponseBase(byte slaveAddress, byte functionCode, ushort numberOfPoints)
        {
            this.SlaveAddress = slaveAddress;
            this.FunctionCode = functionCode;
            this.NumberOfPoints = numberOfPoints;
        }
        #endregion [Ctor's]


        #region [IModbusServerResponse members]
        /// <summary>
        /// Builds a server message frame as byte array.
        /// </summary>
        /// <returns>Full frame</returns>
        public abstract byte[] BuildMessageFrame();
        #endregion [IModbusServerResponse members]
    }
}
