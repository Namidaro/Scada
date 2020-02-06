namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses
{
    /// <summary>
    /// Represents a base modbus response functionality
    /// </summary>
    public abstract class ModbusClientResponseBase : IModbusResponse
    {
        #region [Properties]
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
        /// Initializes an instance of <see cref="ModbusClientResponseBase"/>
        /// </summary>
        /// <param name="functionCode">The number of modbus function</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        protected ModbusClientResponseBase(byte functionCode, ushort numberOfPoints)
        {
            this.FunctionCode = functionCode;
            this.NumberOfPoints = numberOfPoints;
        }
        #endregion [Ctor's]
    }
}