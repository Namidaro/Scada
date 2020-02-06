using System.Collections.Generic;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Modbus
{
    /// <summary>
    /// Represents a base class for modbus client protocol functionality
    /// </summary>
    public abstract class ModbusClientBase : ModbusBase
    {
        private const string RESPONSE_ERROR = "Can't obtain client correct response";

        #region [Private fields]
        private readonly object _syncObj = new object();
        private readonly AsyncSemaphore _semaphore = new AsyncSemaphore(1);
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ModbusClientBase"/>
        /// </summary>
        /// <param name="transport">An instance of a transport to use while processing a request</param>
        protected ModbusClientBase(ITransport transport)
            : base(transport)
        {  }
        #endregion [Ctor's]


        #region [Internal members]
        /// <summary>
        /// Processes modbus request message.
        /// </summary>
        /// <typeparam name="T">The type of response</typeparam>
        /// <param name="request">The modbus request message.</param>
        /// <returns>The result of message processing.</returns>
        internal virtual T ProcessRequest<T>(IModbusClientRequest request) where T : class, IModbusResponse
        {
            this.ThrowIfDisposed();
            T response;
            lock (this._syncObj)
            {
                this._transport.OpenConnection();
                this.WriteRequest(request);
                response = (T)this.ReadResponse(request);
                this._transport.CloseConnection();
            }
            if (response == null)
            {
                this._logger.Log(RESPONSE_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ClientModbusResponseException>(RESPONSE_ERROR);
            }
            request.ValidateResponse(response);
            return response;
        }
        /// <summary>
        /// Processes modbus request message.
        /// </summary>
        /// <typeparam name="T">The type of response</typeparam>
        /// <param name="request">The modbus request message.</param>
        /// <returns>The result of message processing.</returns>
        internal Task<T> ProcessRequestAsync<T>(IModbusClientRequest request) where T : class, IModbusResponse
        {
            this.ThrowIfDisposed();
            return Task.Factory.IterateWithResult<T>(OnProcessRequestAsync<T>(request));
        } 
        #endregion [Internal members]


        #region [Templated members]
        /// <summary>
        /// Reads a modbus response from a transport asynchronously. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An instance of a task that represents a modbus response that has recently been read.</returns>
        protected abstract Task<IModbusResponse> ReadResponseAsync(IModbusClientRequest request);
        /// <summary>
        /// Writes a modbus request to a transport asynchronously. 
        /// </summary>
        /// <param name="request">An instance of a task that represents a modbus request to write</param>
        protected abstract Task WriteRequestAsync(IModbusClientRequest request);
        /// <summary>
        /// Reads a modbus response from a transport. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>An instance of a modbus response that has recently been read.</returns>
        protected abstract IModbusResponse ReadResponse(IModbusClientRequest request);
        /// <summary>
        /// Writes a modbus request to a transport. 
        /// </summary>
        /// <param name="request">An instance of a modbus request to write</param>
        protected abstract void WriteRequest(IModbusClientRequest request);
        /// <summary>
        /// Represents template method for validation in derived class.
        /// </summary>
        /// <param name="request">An instance of modbus request.</param>
        /// <param name="response">An instance of modbus response to validate.</param>
        protected virtual void OnValidateResponse(IModbusClientRequest request, IModbusResponse response)
        {
            /*none*/
        }
        /// <summary>
        /// Represents asynchronous processing of a modbus request
        /// </summary>
        /// <param name="request">An instance of modbus request to process</param>
        /// <returns>An enumeration of asynchronous actions to process</returns>
        protected virtual IEnumerable<Task> OnProcessRequestAsync<T>(IModbusClientRequest request) where T : IModbusResponse
        {
            this.ThrowIfDisposed();
            yield return this._semaphore.WaitAsync();
            Task<IModbusResponse> readResult;
            try
            {
                yield return this._transport.OpenConnectionAsync();
                var writeResult = this.WriteRequestAsync(request);
                yield return writeResult;
                readResult = this.ReadResponseAsync(request);
                yield return readResult;
                yield return this._transport.CloseConnectionAsync();
            }
            finally
            {
                this._semaphore.Release();
            }
            if (readResult.Result == null)
            {
                this._logger.Log(RESPONSE_ERROR, Category.ERROR, Priority.HIGH);
                Guard.Throw<ClientModbusResponseException>(RESPONSE_ERROR);
            }
            request.ValidateResponse(readResult.Result);
            yield return Task.Factory.FromResult((T)readResult.Result);
        }  
        #endregion [Templated members]
    }
}