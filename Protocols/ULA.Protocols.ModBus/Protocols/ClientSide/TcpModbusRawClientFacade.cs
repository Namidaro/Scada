using System;
using System.Threading.Tasks;
using YP.Toolkit.Protocols.ModBus.IO;
using YP.Toolkit.Protocols.ModBus.Logger;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Modbus;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Requests;
using YP.Toolkit.Protocols.ModBus.Protocols.ClientSide.Responses;
using YP.Toolkit.Protocols.ModBus.Resources;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide
{
    /// <summary>
    /// Represents a TCP client modbus facade functionality
    /// </summary>
    public class TcpModbusRawClientFacade : ModbusClientFacadeBase, IModbusRawClientFacade
    {
        #region [Constants]
        private const string START_RAED_INPUT_REGISTERS = "Start read input registers";
        private const string END_RAED_INPUT_REGISTERS = "Input registers has been read";
        private const string START_WRITE_MULTIPLE_REGISTERS = "Start write multiple registers";
        private const string END_WRITE_MULTIPLE_REGISTERS = "Multiple registers has been written";
        #endregion [Constants]
        

        #region [Private fields]
        private const byte SLAVE_ADDRESS = 1;
        private readonly ILogService _logger = LoggerServiceLocator.Current;
        #endregion [Private fields]


        #region [Factory members]
        /// <summary>
        /// Creates an instance of a TCP modbus master.
        /// </summary>
        /// <param name="host">The address of a remote slave to connect to.</param>
        /// <param name="port">The port of a remote slave to connect to.</param>
        /// <returns>The instance of a TCP modbus master.</returns>
        public static TcpModbusRawClientFacade CreateTcpModbus(string host, int port)
        {
            return TcpModbusRawClientFacade.CreateTcpModbus(new TcpIpSocketTransport(host, port));
        }
        /// <summary>
        /// Creates an instance of a TCP modbus master.
        /// </summary>
        /// <param name="transport">An instance of a streaming source to use as a transport.</param>
        /// <returns>The instance of a TCP modbus master.</returns>
        public static TcpModbusRawClientFacade CreateTcpModbus(ITransport transport)
        {
            Guard.AgainstNullReference(transport);
            return new TcpModbusRawClientFacade(new TcpModbusClient(transport));
        } 
        #endregion [Factory members]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="TcpModbusRawClientFacade"/>. 
        /// </summary>
        /// <param name="modbus">The modbus protocol functionality to use.</param>
        protected TcpModbusRawClientFacade(ModbusClientBase modbus)
            : base(modbus)
        { } 
        #endregion [Ctor's]


        #region [Synchronous model]
        /// <summary>
        /// Read from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status</returns>
        public byte[] ReadCoilsRaw(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status</returns>
        public byte[] ReadInputsRaw(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status</returns>
        public byte[] ReadHoldingRegistersRaw(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status</returns>
        public byte[] ReadInputRegistersRaw(ushort startAddress, ushort numberOfPoints)
        {
            this.ThrowIfDisposed();
            this._logger.Log(START_RAED_INPUT_REGISTERS);
            this.ValidateNumberOfPoints(numberOfPoints, ProtocolConstants.MAXIMUM_REGISTER_REQUEST_RESPONSE_SIZE);
            var request = new TcpReadInputRegistersClientRequest(SLAVE_ADDRESS, startAddress, numberOfPoints);
            var response = this._modbus.ProcessRequest<TcpReadInputRegistersClientResponse>(request);
            this._logger.Log(END_RAED_INPUT_REGISTERS);
            return response.RawData;
        }
        /// <summary>
        /// Write a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleCoilRaw(ushort coilAddress, byte[] value)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Write a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public void WriteSingleRegisterRaw(ushort registerAddress, byte[] value)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Write a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleRegistersRaw(ushort startAddress, byte[] data)
        {
            this.ThrowIfDisposed();
            this._logger.Log(START_WRITE_MULTIPLE_REGISTERS);
            this.ValidateData(data, ProtocolConstants.MAXIMUM_WRITE_REGISTER_REQUEST_RESPONSE_DATA_SIZE);
            var request = new TcpWriteMultipleRegistersClientRequest(SLAVE_ADDRESS, startAddress, data, (ushort)(data.Length / 2));
            this._modbus.ProcessRequest<TcpWriteMultipleRegistersClientResponse>(request);
            this._logger.Log(END_WRITE_MULTIPLE_REGISTERS);
        }
        /// <summary>
        /// Force each coil in a sequence of coils to a provided value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public void WriteMultipleCoilsRaw(ushort startAddress, bool[] data)
        {
            throw new NotSupportedException();
        } 
        #endregion [Synchronous model]


        #region [Asynchronous model]
        /// <summary>
        /// Read from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status</returns>
        public Task<byte[]> ReadCoilsRawAsync(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status</returns>
        public Task<byte[]> ReadInputsRawAsync(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status</returns>
        public Task<byte[]> ReadHoldingRegistersRawAsync(ushort startAddress, ushort numberOfPoints)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Read contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status</returns>
        public Task<byte[]> ReadInputRegistersRawAsync(ushort startAddress, ushort numberOfPoints)
        {
            this.ThrowIfDisposed();
            this._logger.Log(START_RAED_INPUT_REGISTERS);
            this.ValidateNumberOfPoints(numberOfPoints, ProtocolConstants.MAXIMUM_REGISTER_REQUEST_RESPONSE_SIZE);
            var request = new TcpReadInputRegistersClientRequest(SLAVE_ADDRESS, startAddress, numberOfPoints);
            return this._modbus.ProcessRequestAsync<TcpReadInputRegistersClientResponse>(request)
                .ContinueWith(task =>
                    {
                        this._logger.Log(END_RAED_INPUT_REGISTERS);
                        return task.Result.RawData;
                    });
        }
        /// <summary>
        /// Write a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        public Task WriteSingleCoilRawAsync(ushort coilAddress, byte[] value)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Write a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        public Task WriteSingleRegisterRawAsync(ushort registerAddress, byte[] value)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// Write a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public Task WriteMultipleRegistersRawAsync(ushort startAddress, byte[] data)
        {
            this.ThrowIfDisposed();
            this._logger.Log(START_WRITE_MULTIPLE_REGISTERS);
            this.ValidateData(data, ProtocolConstants.MAXIMUM_WRITE_REGISTER_REQUEST_RESPONSE_DATA_SIZE);
            var request = new TcpWriteMultipleRegistersClientRequest(SLAVE_ADDRESS, startAddress, data, (ushort)(data.Length / 2));
            var result = this._modbus.ProcessRequestAsync<TcpWriteMultipleRegistersClientResponse>(request);
            result.ContinueWith(task => this._logger.Log(END_WRITE_MULTIPLE_REGISTERS));
            return result;
        }
        /// <summary>
        /// Force each coil in a sequence of coils to a provided value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        public Task WriteMultipleCoilsRawAsync(ushort startAddress, bool[] data)
        {
            throw new NotSupportedException();
        } 
        #endregion [Asynchronous model]
    }
}