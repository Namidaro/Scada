using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using NModbus4.Device;
using ULA.Common;
using YP.Toolkit.System.Tools.Patterns;


namespace ULA.ApplicationConnectionService
{

    /// <summary>
    /// Класс соединения по протоколу TCP
    /// </summary>
    public class TcpDeviceConnection : Disposable
    {
        private TcpClient _tcpClient;
        private IModbusMaster _modbusMaster;
        private int _writeTimeOut = 10000;
        private int _readTimeOut = 10000;
        private readonly object _everyConnectionLockObject;
        private static ushort _transactionId;
        private SemaphoreSlim _semaphoreSlim;

        public Action ConnectionLostAction;
        public Action ConnectionRestoredAction;
        public Action TransactionCompleteAction;

        public string Host { get; }
        public int Port { get; }
        public bool LastTransactionSucceed { get; set; }

        public ObservableCollection<string> ConnectionExceptionList { get; }

        public TcpDeviceConnection(string host, int port)

        {
            ConnectionExceptionList = new ObservableCollection<string>();
            Host = host;
            Port = port;
            LastTransactionSucceed = false;
            _everyConnectionLockObject = new object();
            _transactionId = 0;
            _semaphoreSlim = new SemaphoreSlim(1, 1);
        }

        private async Task AddErrorInList(Exception exception, string requestName)
        {

            await Task.Run((() =>
            {
                StringBuilder sb = new StringBuilder();
                if (exception is SocketException)
                {
                    sb.Append(requestName);
                    sb.Append(" | ");
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(" | ");
                    sb.Append(((SocketException)exception).SocketErrorCode.ToString());
                    sb.Append(" | ");
                    sb.Append(((SocketException)exception).ErrorCode.ToString());
                    sb.Append(" | ");
                    //TODO: think what to do with long messages, and think what to present as message
                    sb.Append(((SocketException)exception).Message.ToString().Split(' ').Last());

                }
                else
                {
                    sb.Append(requestName);
                    sb.Append(" | ");
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(" | ");
                    sb.Append(" | ");
                    sb.Append((exception).Message.ToString());
                }
                DispatchService.Invoke((() =>
                {
                    if (ConnectionExceptionList.Count == 100) ConnectionExceptionList.RemoveAt(99);
                    ConnectionExceptionList.Insert(0, sb.ToString());
                }));
            }));
        }

        /// <summary>
        /// Открыть сокет
        /// </summary>
        /// <returns></returns>
        public async Task<bool> OpenConnectionSession(bool isReconnecting)
        {
            await _semaphoreSlim.WaitAsync();
            try
            {

                await Task.Run((() =>
                {
                    if (_tcpClient != null)
                    {
                        _tcpClient.Close();
                        _tcpClient = null;
                    }
                    _tcpClient = new TcpClient(Host, Port);
                    _tcpClient.SendTimeout = _writeTimeOut;
                    _tcpClient.ReceiveTimeout = _readTimeOut;

                    _modbusMaster = ModbusIpMaster.CreateIp(_tcpClient);
                    _modbusMaster.Transport.RetryOnOldResponseThreshold = 5;
                    _modbusMaster.Transport.Retries = 0;
                    _modbusMaster.Transport.ReadTimeout = _readTimeOut;
                    _modbusMaster.Transport.WriteTimeout = _writeTimeOut;

                }));
                LastTransactionSucceed = true;
                if (!isReconnecting)
                {
                    ConnectionRestoredAction?.Invoke();
                }

            }
            catch (Exception ex)
            {
                LastTransactionSucceed = false;
                if (!isReconnecting)
                {
                    ConnectionLostAction?.Invoke();
                    TransactionCompleteAction?.Invoke();
                    await AddErrorInList(ex, "Попытка открыть соединение");
                    //await AddErrorInList(ex, "Open Connection Session");
                }
                return false;
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            return true;
        }

        public async Task<byte[]> GetDataByAddressAsync(ushort startAddress,
            ushort numberOfPoints, string requestName)
        {
            // TcpMbReadResponse tcpMbResponse;
            byte[] resultBytes = null;
            try
            {
                TransactionCompleteAction?.Invoke();
                if (!LastTransactionSucceed) return null;
                ushort[] resUshorts = await TryExecuteReadQuery(startAddress, numberOfPoints);
                resultBytes = UshortArrayToByteArray(resUshorts);
            }
            catch
                (Exception j)
            {
                if (!(j is NullReferenceException))
                    AddErrorInList(j, requestName);
                LastTransactionSucceed = false;
                ConnectionLostAction?.Invoke();
                resultBytes = null;
            }
            return resultBytes;
        }


        public async Task<byte[]> ExecuteFunction12Async(byte moduleNum, string requestName, byte innerFunctionId)
        {
            // TcpMbReadResponse tcpMbResponse;
            byte[] resultBytes = null;
            try
            {
                TransactionCompleteAction?.Invoke();
                var receivedBytes = await _modbusMaster.ExecuteFunction12Async(1, moduleNum, innerFunctionId, 0);
                byte moduleByte = receivedBytes[0];
                byte innerFunByte = receivedBytes[1];
                byte numberOfBytesByte = receivedBytes[2];
                if ((moduleByte != moduleNum) || (innerFunByte != innerFunctionId))
                {
                    throw new Exception();
                }
                resultBytes = receivedBytes.Skip(3).ToArray();
            }
            catch
                (Exception j)
            {
                //AddErrorInList(j, requestName);
                //LastTransactionSucceed = false;
            }
            return resultBytes;
        }


        private async Task<ushort[]> TryExecuteReadQuery(ushort startAddress, ushort numberOfPoints)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();
                if (!LastTransactionSucceed) throw new Exception("");
                ushort[] bytes = await _modbusMaster.ReadHoldingRegistersAsync(1, startAddress, numberOfPoints);
                _semaphoreSlim.Release();
                return bytes;
            }
            catch (StackOverflowException s)
            {
                throw;
            }
            catch (Exception e)
            {
                _semaphoreSlim.Release();
                if (!LastTransactionSucceed) throw;

                //_modbusMaster.Dispose();
                //OpenConnectionSession(false);

                //for (loopIterator = 0; loopIterator < 2; loopIterator++)
                //{
                //    if (LastTransactionSucceed) break;
                //    await OpenConnectionSession(true);
                //}
                //loopIterator = 0;

                return null;

                //return await TryExecuteReadQuery(startAddress, numberOfPoints);
            }

        }

        private async Task TryExecuteWriteQuery(ushort startAddress, ushort[] data)
        {
            try
            {
                await _semaphoreSlim.WaitAsync();

                await _modbusMaster.WriteMultipleRegistersAsync(1, startAddress, data);
                _semaphoreSlim.Release();
            }
            catch (Exception e)
            {
                _semaphoreSlim.Release();
                for (int i = 0; i < 2; i++)
                {
                    await OpenConnectionSession(true);
                    if (LastTransactionSucceed) break;
                }
                if (!LastTransactionSucceed) throw;
                await TryExecuteWriteQuery(startAddress, data);
            }
        }



        public async Task<bool> PostDataByAddressAsync(ushort startAddress,
        byte[] data, string requestName)
        {
            try
            {
                TransactionCompleteAction?.Invoke();
                await TryExecuteWriteQuery(startAddress, ByteArrayToUshortArray(data));

            }
            catch (Exception j)
            {
                AddErrorInList(j, requestName);
                LastTransactionSucceed = false;
                ConnectionLostAction?.Invoke();
                return false;
            }

            return true;
        }

        public void SetTimeout(int newValue)
        {
            _readTimeOut = newValue;
            _writeTimeOut = newValue;
        }



        /// <summary>
        /// Конвертирует массив слов в массив байт
        /// </summary>
        /// <param name="words"> Массив слов.</param>
        /// <param name="bDirect">Порядок байт. true - обычный, false - ст.байт меняем местом с мл.байтом.</param>
        /// <returns>Массив байт.</returns>
        private byte[] UshortArrayToByteArray(ushort[] words)
        {
            byte[] buffer = new byte[words.Length * 2];
            for (int i = 0, j = 0; i < words.Length; i++)
            {
                buffer[j++] = HIBYTE(words[i]);
                buffer[j++] = LOBYTE(words[i]);
            }
            return buffer;
        }



        /// <summary>
        /// Возвращает младший байт слова
        /// </summary>
        /// <param name="v">Слово.</param>
        /// <returns>Мл.байт</returns>
        public static byte LOBYTE(int v)
        {
            return (byte)(v & 0xff);
        }

        /// <summary>
        /// Возвращает старший байт слова.
        /// </summary>
        /// <param name="v">Слово.</param>
        /// <returns>Ст.байт</returns>
        public static byte HIBYTE(int v)
        {
            return (byte)(v >> 8);
        }


        private ushort[] ByteArrayToUshortArray(byte[] byteArray)
        {
            if (byteArray.Length % 2 != 0)
            {
                byte[] buffer = byteArray;
                byteArray = new byte[byteArray.Length + 1];
                Array.ConstrainedCopy(buffer, 0, byteArray, 0, buffer.Length);
            }
            ushort[] ushorts = new ushort[byteArray.Length / 2];
            int ind = 0;
            for (int i = 0; i < ushorts.Length; i++)
            {
                ushorts[i] = TwoBytesToUshort(byteArray[ind], byteArray[ind + 1]);
                ind += sizeof(ushort);
            }
            return ushorts;
        }

        /// <summary>
        /// Конвертирует 2 байта в слово
        /// </summary>
        /// <param name="highByte">Ст.байт</param>
        /// <param name="lowByte">Мл.байт</param>
        /// <returns>Слово.</returns>
        public static ushort TwoBytesToUshort(byte highByte, byte lowByte)
        {
            UInt16 ret = (UInt16)highByte;
            return (ushort)((ushort)(ret << 8) + (ushort)lowByte);
        }

        #region Overrides of Disposable

        protected override void OnDisposing()
        {
            ConnectionLostAction = null;
            ConnectionRestoredAction = null;
            _modbusMaster?.Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}