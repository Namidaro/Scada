using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests
{
    /// <summary>
    /// Represents a base modbus request functionality
    /// </summary>
    public abstract class ModbusClientRequestBase : IModbusClientRequest
    {
        #region [Properties]
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode { get; set; }

        /// <summary>
        /// Gets a start address of a data
        /// </summary>
        public ushort Address { get; set; }
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        public byte SlaveAddress { get; private set; }
        /// <summary>
        /// Gets the number of points for current request
        /// </summary>
        public ushort NumberOfPoints { get; private set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ModbusClientRequestBase"/>
        /// </summary>
        /// <param name="functionCode">The number of modbus function that represent current request</param>
        /// <param name="slaveAddress">An address of slave device</param>
        /// <param name="address">An address of current request</param>
        /// <param name="numberOfPoints">the number of points for current request</param>
        protected ModbusClientRequestBase(byte functionCode, byte slaveAddress, ushort address, ushort numberOfPoints)
        {
            this.FunctionCode = functionCode;
            this.SlaveAddress = slaveAddress;
            this.Address = address;
            this.NumberOfPoints = numberOfPoints;
        } 
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        public void ValidateResponse(IModbusResponse response)
        {
            // ReSharper disable PossibleNullReferenceException
            Guard.AgainstNullReference<ClientModbusResponseException>(response);
            Guard.AgainstIsFalse<ClientModbusResponseException>(this.FunctionCode == response.FunctionCode,
                ResourceConstants.NOT_EQUALS_FUNCTIONCODES_MESSAGE);
            // ReSharper restore PossibleNullReferenceException
            this.OnValidateResponse(response);
        }
        /// <summary>
        /// Creates an instance of a response for current request
        /// </summary>
        /// <param name="requestedResponse">A raw response data</param>
        /// <returns>An instance of a response for current request</returns>
        public IModbusResponse CreateResponseFromFrame(byte[] requestedResponse)
        {
            return this.OnCreateResponseFromFrame(requestedResponse);
        }
        /// <summary>
        /// Build message frame for request
        /// </summary>
        /// <returns>Message frame</returns>
        public abstract byte[] BuildRequestFrame();
        #endregion [Public members]


        #region [Templated members]
        /// <summary>
        /// Creates an instance of a response for current request
        /// </summary>
        /// <param name="requestedResponse">A raw response data</param>
        /// <returns>An instance of a response for current request</returns>
        protected abstract IModbusResponse OnCreateResponseFromFrame(byte[] requestedResponse);
        /// <summary>
        /// Validates a response for current request
        /// </summary>
        /// <param name="response">An instance of a modbus response to validate</param>
        protected virtual void OnValidateResponse(IModbusResponse response)
        {
            /*none*/
        } 
        #endregion [Templated members]
    }
}