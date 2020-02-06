using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses
{
    /// <summary>
    /// Represents a tcp server modbus response functionality
    /// </summary>
    internal class TcpModbusServerResponse : IModbusServerResponse
    {
        #region [Private fields]
        private readonly IModbusServerResponse _innerResponse;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets or sets a transaction ID for current response
        /// </summary>
        public ushort TransactionId { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusServerResponse"/>
        /// </summary>
        /// <param name="innerResponse">An instance of inner response to wrap</param>
        public TcpModbusServerResponse(IModbusServerResponse innerResponse)
        {
            Guard.AgainstNullReference(innerResponse, "innerResponse");

            this._innerResponse = innerResponse;
        }
        #endregion [Ctor's]


        #region [IServerModbusResponse members]
        /// <summary>
        /// Builds a message frame as a collection of bytes
        /// </summary>
        /// <returns>Built message frame as collection of bytes</returns>
        public byte[] BuildMessageFrame()
        {
            var basic = this._innerResponse.BuildMessageFrame();
            var dataLength = basic.Length;
            var mbap = new[]
                {
                    (byte) ((this.TransactionId >> 8) & 0xff),
                    (byte) this.TransactionId,
                    (byte) 0,
                    (byte) 0,
                    (byte) ((dataLength >> 8) & 0xff),
                    (byte) dataLength
                };
            return mbap.CombineArray(basic);
        }
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode
        {
            get { return this._innerResponse.FunctionCode; }
            set { this._innerResponse.FunctionCode = value; }
        }
        /// <summary>
        /// Gets the number of points for current request
        /// </summary>
        public ushort NumberOfPoints
        {
            get { return this._innerResponse.NumberOfPoints; }
            set { this._innerResponse.NumberOfPoints = value; }
        }
        #endregion [IServerModbusResponse members]
    }
}