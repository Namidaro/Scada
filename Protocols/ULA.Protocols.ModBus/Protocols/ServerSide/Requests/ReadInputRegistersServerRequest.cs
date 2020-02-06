using System;
using System.Linq;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.System.Validation;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Events;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;
using YP.Toolkit.System.SystemExtensions;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests
{
    /// <summary>
    /// Represents a read input registers request 
    /// </summary>
    public class ReadInputRegistersServerRequest : IModbusServerRequest
    {
        #region [Constants]
        private const string ERROR_FRAME_LENGTH_PATTERN = "Server. Message frame must contain at least {0} bytes of data.";
        private const string PROCESS_ERROR = "Server. The request has been not processed";
        private const int MINIMUM_FRAME_SIZE = 6;
        #endregion [Constants]


        #region [Private fields]
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [IServerModbusRequest members]
        /// <summary>
        /// Initializes an instance of current request from a collection of bytes
        /// </summary>
        /// <param name="frame">A collection of bytes to initialize current request from</param>
        public void Initialize(byte[] frame)
        {
            if (frame.Length < MINIMUM_FRAME_SIZE)
            {
                var errorMessage = string.Format(ERROR_FRAME_LENGTH_PATTERN, MINIMUM_FRAME_SIZE);
                this._logger.Log(errorMessage, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerRequestException>(errorMessage);
            }
            this.SlaveAddress = frame.First();
            this.FunctionCode = frame.Second();
            this.Address = (ushort)(frame.Fourth() | frame.Third() << 8);
            this.NumberOfPoints = (ushort)(frame.Sixth() | frame.Fifth() << 8);
        }
        /// <summary>
        /// Processes a modbus server request
        /// </summary>
        /// <param name="serverEvents">An instance of server events provider</param>
        /// <returns>An instance of modbus server response for current request</returns>
        IModbusServerResponse IModbusServerRequest.Process(IServerEventsExecutor serverEvents)
        {
            this._logger.Log(String.Format("Received a request to read {0} words", this.NumberOfPoints));
            var args = new ReadInputRegistersEventArgs(this);
            serverEvents.OnReadInputRegistersRequest(args);
            if (!args.Handled)
            {
                this._logger.Log(PROCESS_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerRequestException>(PROCESS_ERROR);
            }
            return new ReadInputRegistersResponse(this.SlaveAddress, this.FunctionCode, this.NumberOfPoints, args.ReadRegisters);
        }
        /// <summary>
        /// Gets the number of points to read
        /// </summary>
        public ushort NumberOfPoints { get; set; }
        /// <summary>
        /// Gets the number of modbus function
        /// </summary>
        public byte FunctionCode { get; private set; }
        /// <summary>
        /// Gets an address of a request
        /// </summary>
        public ushort Address { get; private set; }
        /// <summary>
        /// Gets address of slave device
        /// </summary>
        public byte SlaveAddress { get; private set; } 
        #endregion [IServerModbusRequest members]
    }
}