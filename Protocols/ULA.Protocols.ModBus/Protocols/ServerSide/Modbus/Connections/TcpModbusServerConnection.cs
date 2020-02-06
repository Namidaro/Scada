using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.Exceptions;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Requests;
using YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.SystemExtensions;
using YP.Toolkit.System.Tools.Patterns;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ServerSide.Modbus.Connections
{
    /// <summary>
    /// Represents a tcp based modbus server
    /// </summary>
    internal class TcpModbusServerConnection : Disposable
    {
        #region [Constants]
        private const string BEGIN_PROCESS_ERROR = "Occurred error while reciving data from server";
        private const string LOST_CONNECTION = "Server. Client closed connection";
        #endregion [Constants]


        #region [Private fields]
        private readonly Socket _connectedSocket;
        private readonly ITcpModbusServerHandler _serverHandler;
        private readonly string _endPoint;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets the name of a connection point
        /// </summary>
        public string EndPoint
        {
            get
            {
                return this._endPoint;
            }
        }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusServerConnection"/>
        /// </summary>
        /// <param name="connectedSocket">An instance of connected client socket</param>
        /// <param name="serverHandler">An instance of server requests handler</param>
        public TcpModbusServerConnection(Socket connectedSocket, ITcpModbusServerHandler serverHandler)
        {
            Guard.AgainstNullReference(connectedSocket, "connectedSocket");
            Guard.AgainstNullReference(serverHandler, "serverTcpModbus");

            this._connectedSocket = connectedSocket;
            this._serverHandler = serverHandler;
            this._endPoint = connectedSocket.RemoteEndPoint.ToString();
        }
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Begins processing requests
        /// </summary>
        public void BeginProcess(CancellationTokenSource cts)
        {
            Task.Factory.Iterate(this.BeginRecieveInternal()).ContinueWith(task =>
                {
                    if (task.IsFaulted && task.Exception != null)
                    {
                        this._logger.Log(BEGIN_PROCESS_ERROR, task.Exception, Category.ERROR, Priority.HIGH);
                        Guard.Throw<ServerConnectionException>(BEGIN_PROCESS_ERROR, task.Exception);
                    }
                    cts.Token.ThrowIfCancellationRequested();
                    this.BeginProcess(cts);
                });
        }
        #endregion [Public members]


        #region [Help members]
        private IEnumerable<Task> BeginRecieveInternal()
        {
            var bytesToRead = ProtocolConstants.MBAP_HEADER_LENGTH;
            var mbapHeader = new byte[bytesToRead];
            var numBytesRead = 0;
            while (numBytesRead != ProtocolConstants.MBAP_HEADER_LENGTH)
            {
                var readTask = this.OnReadDataAsync(mbapHeader, numBytesRead, bytesToRead - numBytesRead);
                yield return readTask;
                numBytesRead = readTask.Result;
                if (numBytesRead != 0) continue;
                this._logger.Log(LOST_CONNECTION);
                this._serverHandler.RemoveConnection(this.EndPoint);
                yield break;
            }
            bytesToRead = (ushort)(mbapHeader.Sixth() | (mbapHeader.Fifth() << 8));
            var frame = new byte[bytesToRead];
            numBytesRead = 0;
            while (numBytesRead != bytesToRead)
            {
                var readTask = this.OnReadDataAsync(frame, numBytesRead, bytesToRead - numBytesRead);
                yield return readTask;
                numBytesRead = readTask.Result;
                if (numBytesRead != 0) continue;
                this._logger.Log(LOST_CONNECTION);
                this._serverHandler.RemoveConnection(this.EndPoint);
                yield break;
            }
            var processTask = this.ProcessRequest(mbapHeader, frame);
            yield return processTask;
            var responseRaw = processTask.Result.BuildMessageFrame();
            yield return this.OnWriteDataAsync(responseRaw, 0, responseRaw.Length);
        }
        private Task<IModbusServerResponse> ProcessRequest(byte[] mbapHeader, byte[] frame)
        {
            var request = new TcpModbusServerRequest(ModbusServerRequestFactory.CreateModbusRequest(frame))
                {
                    TransactionId = (ushort)(mbapHeader.Second() | (mbapHeader.First() << 8))
                };
            var response = new TcpModbusServerResponse(this._serverHandler.ApplyRequest(request))
                {
                    TransactionId = request.TransactionId
                };
            return Task.Factory.FromResult((IModbusServerResponse)response);
        }
        private Task<int> OnReadDataAsync(byte[] buffer, int offset, int length)
        {
            return Task<int>.Factory.FromAsync(this.BeginReceiveInternal, this.EndReceiveInternal, buffer, offset, length, null);
        }
        private Task<int> OnWriteDataAsync(byte[] buffer, int offset, int length)
        {
            return Task<int>.Factory.FromAsync(this.BeginSendInternal, this.EndSendInternal, buffer, offset, length, null);
        }
        private IAsyncResult BeginSendInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._connectedSocket.BeginSend(buffer, offset, count, SocketFlags.None, callback, state);
        }
        private int EndSendInternal(IAsyncResult result)
        {
            return this._connectedSocket.EndSend(result);
        }
        private IAsyncResult BeginReceiveInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return this._connectedSocket.BeginReceive(buffer, offset, count, SocketFlags.None, callback, state);
        }
        private int EndReceiveInternal(IAsyncResult result)
        {
            return this._connectedSocket.EndReceive(result);
        }
        #endregion [Help members]
    }
}