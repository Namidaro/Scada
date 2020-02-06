using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Modbus
{
    /// <summary>
    /// Represents a tcp modbus protocol functionality
    /// </summary>
    public class TcpModbusClient : ModbusClientBase
    {
        #region [Constants]
        private const ushort START_TRANSACTIONID_VALUE = 1;
        #endregion [Constants]


        #region [Private fields]
        private ushort _currentTransactionId;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusClient"/>
        /// </summary>
        /// <param name="transport"></param>
        public TcpModbusClient(ITransport transport)
            : base(transport)
        { } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Represents template method for validation in derived class.
        /// </summary>
        /// <param name="request">An instance of modbus request.</param>
        /// <param name="response">An instance of modbus response to validate.</param>
        protected override void OnValidateResponse(IModbusClientRequest request, IModbusResponse response)
        {
            base.OnValidateResponse(request, response);
            var tcpModbusRequest = (TcpModbusClientRequest)request;
            var tcpModbusResponse = (TcpModbusClientResponse)response;

            Guard.AgainstIsFalse<ClientModbusResponseException>(
                tcpModbusRequest.TransactionId == tcpModbusResponse.TransactionId,
                ResourceConstants.TRANSACTION_ID_ERROR_MESSAGE);
        }
        /// <summary>
        /// Reads a modbus response from a transport asynchronously. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An instance of a task that represents a modbus response that has recently been read.</returns>
        protected override Task<IModbusResponse> ReadResponseAsync(IModbusClientRequest request)
        {
            return Task.Factory.IterateWithResult<IModbusResponse>(this.OnReadResponseAsync(request));
        }
        /// <summary>
        /// Writes a modbus request to a transport asynchronously. 
        /// </summary>
        /// <param name="request">An instance of a task that represents a modbus request to write</param>
        protected override Task WriteRequestAsync(IModbusClientRequest request)
        {
            return Task.Factory.Iterate(this.OnWriteRequestAsync(request));
        }
        /// <summary>
        /// Reads a modbus response from a transport. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An instance of a modbus response that has recently been read.</returns>
        protected override IModbusResponse ReadResponse(IModbusClientRequest request)
        {
            var requestResponse = this.ReadResponseInternal();
            return request.CreateResponseFromFrame(requestResponse);
        }
        /// <summary>
        /// Writes a modbus request to a transport. 
        /// </summary>
        /// <param name="request">An instance of a modbus request to write</param>
        protected override void WriteRequest(IModbusClientRequest request)
        {
            var tcpModbusRequest = (TcpModbusClientRequest)request;
            tcpModbusRequest.TransactionId = this.GetNewTransactionId();

            var frameToWrite = request.BuildRequestFrame();
            this._transport.Write(frameToWrite, 0, frameToWrite.Length);
        } 
        #endregion [Override members]


        #region [Help members]
        private IEnumerable<Task> OnReadResponseAsync(IModbusClientRequest request)
        {
            var bytesToRead = ProtocolConstants.MBAP_HEADER_LENGTH;
            var mbapHeader = new byte[bytesToRead];
            var numBytesRead = 0;
            while (numBytesRead != ProtocolConstants.MBAP_HEADER_LENGTH)
            {
                var readTask = this._transport.ReadAsync(mbapHeader, numBytesRead, bytesToRead - numBytesRead);
                yield return readTask;
                numBytesRead = readTask.Result;
                if (numBytesRead == 0)
                {
                    this._logger.Log(ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ClientModbusResponseException>(
                        ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE);
                }
            }
            bytesToRead = (ushort)(mbapHeader[5] | (mbapHeader[4] << 8));
            var frame = new byte[bytesToRead];
            numBytesRead = 0;
            while (numBytesRead != bytesToRead)
            {
                var readTask = this._transport.ReadAsync(frame, numBytesRead, bytesToRead - numBytesRead);
                yield return readTask;
                numBytesRead = readTask.Result; 
                if (numBytesRead == 0)
                {
                    this._logger.Log(ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ClientModbusResponseException>(
                        ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE);
                }
            }
            var response = request.CreateResponseFromFrame(mbapHeader.CombineArray(frame));
            yield return Task.Factory.FromResult(response);
        }
        private IEnumerable<Task> OnWriteRequestAsync(IModbusClientRequest request)
        {
            var tcpModbusRequest = (TcpModbusClientRequest)request;
            tcpModbusRequest.TransactionId = this.GetNewTransactionId();
            var frameToWrite = request.BuildRequestFrame();
            var writeTask = this._transport.WriteAsync(frameToWrite, 0, frameToWrite.Length);
            yield return writeTask;
        }
        /// <summary>
        /// Gets a new transaction id.
        /// </summary>
        /// <returns>The resulting transaction id.</returns>
        private ushort GetNewTransactionId()
        {
            this._currentTransactionId = this._currentTransactionId == UInt16.MaxValue ?
                    START_TRANSACTIONID_VALUE : ++this._currentTransactionId;
            return this._currentTransactionId;
        }
        /// <summary>
        /// Reads request or response from a streaming source as message frame.
        /// </summary>
        /// <returns>The resulting request or response message frame.</returns>
        private byte[] ReadResponseInternal()
        {
            // read header
            var mbapHeader = this.ReadFromTransport(ProtocolConstants.MBAP_HEADER_LENGTH);
            // read message
            var frameLength = (ushort)(mbapHeader[5] | (mbapHeader[4] << 8));
            var messageFrame = this.ReadFromTransport(frameLength);
            var frame = mbapHeader.CombineArray(messageFrame);
            return frame;
        }
        private byte[] ReadFromTransport(int bytesToRead)
        {
            var result = new byte[bytesToRead];
            var numBytesRead = 0;
            while (numBytesRead != bytesToRead)
            {
                numBytesRead += this._transport.Read(result, numBytesRead, bytesToRead - numBytesRead);
                if (numBytesRead == 0)
                {
                    this._logger.Log(ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE, Category.ERROR, Priority.HIGH);
                    Guard.Throw<ClientModbusResponseException>(
                        ResourceConstants.TCP_MODBUS_READ_RESULT_WITH_ZERO_BYTES_MESSAGE);
                }
            }
            return result;
        }
        #endregion [Help members]
    }
}