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
    /// Represents a write multiple input registers modbus server request
    /// </summary>
    public class WriteMultipleRegistersServerRequest : IModbusServerRequest
    {
        #region [Private fields]
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Constants]
        private const string FRAME_LENGTH_ERROR = "Server. Message frame does not contain enough bytes.";
        private const string PROCESS_ERROR = "Server. The request has been not processed";
        private const int MINIMUM_FRANE_SIZE = 7; 
        #endregion [Constants]


        #region [Properties]
        /// <summary>
        /// Gets a number of points to write
        /// </summary>
        public ushort NumberOfPoints { get; private set; }
        /// <summary>
        /// Gets a number of points in bytes to write
        /// </summary>
        public byte ByteCount { get; private set; }
        /// <summary>
        /// Gets a collection of bytes to write
        /// </summary>
        public byte[] Data { get; private set; } 
        #endregion [Properties]


        #region [IServerModbusRequest members]
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
        /// <summary>
        /// Initializes an instance of current request from a collection of bytes
        /// </summary>
        /// <param name="frame">A collection of bytes to initialize current request from</param>
        public void Initialize(byte[] frame)
        {
            if (frame.Length < MINIMUM_FRANE_SIZE + frame.Seventh())
            {
                this._logger.Log(FRAME_LENGTH_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerRequestException>(FRAME_LENGTH_ERROR, new FormatException());
            }

            this.SlaveAddress = frame.First();
            this.FunctionCode = frame.Second();
            this.Address = (ushort)(frame.Fourth() | frame.Third() << 8);
            this.NumberOfPoints = (ushort)(frame.Sixth() | frame.Fifth() << 8);
            this.ByteCount = frame.Seventh();
            this.Data = frame.Slice(7, this.ByteCount).ToArray();
        }
        /// <summary>
        /// Processes a modbus server request
        /// </summary>
        /// <param name="serverEvents">An instance of server events provider</param>
        /// <returns>An instance of modbus server response for current request</returns>
        IModbusServerResponse IModbusServerRequest.Process(IServerEventsExecutor serverEvents)
        {
            this._logger.Log(String.Format("Received a request to write {0} words", this.NumberOfPoints));
            var args = new WriteMultipleRegistersEventArgs(this);
            serverEvents.OnWriteMultipleRegistersRequest(args);
            if (!args.Handled)
            {
                this._logger.Log(PROCESS_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ServerRequestException>(PROCESS_ERROR);
            }
            return new WriteMultipleRegistersResponse(this.SlaveAddress, this.FunctionCode, this.Address, this.NumberOfPoints);
        }
        #endregion [IServerModbusRequest members]
    }
}