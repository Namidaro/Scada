using System.Threading.Tasks;

namespace YP.Toolkit.Protocols.ModBus.Protocols.ClientSide
{
    /// <summary>
    /// Exposes a Raw protocol data transfromation
    /// </summary>
    public interface IModbusRawClientFacade
    {
        #region [Synchronous methods]
        /// <summary>
        /// Read from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status</returns>
        byte[] ReadCoilsRaw(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status</returns>
        byte[] ReadInputsRaw(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status</returns>
        byte[] ReadHoldingRegistersRaw(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status</returns>
        byte[] ReadInputRegistersRaw(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Write a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        void WriteSingleCoilRaw(ushort coilAddress, byte[] value);
        /// <summary>
        /// Write a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        void WriteSingleRegisterRaw(ushort registerAddress, byte[] value);
        /// <summary>
        /// Write a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        void WriteMultipleRegistersRaw(ushort startAddress, byte[] data);
        /// <summary>
        /// Force each coil in a sequence of coils to a provided value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        void WriteMultipleCoilsRaw(ushort startAddress, bool[] data);  
        #endregion [Synchronous methods]


        #region [Asynchronous Methods]
        /// <summary>
        /// Read from 1 to 2000 contiguous coils status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of coils to read.</param>
        /// <returns>Coils status</returns>
        Task<byte[]> ReadCoilsRawAsync(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read from 1 to 2000 contiguous discrete input status.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of discrete inputs to read.</param>
        /// <returns>Discrete inputs status</returns>
        Task<byte[]> ReadInputsRawAsync(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read contiguous block of holding registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Holding registers status</returns>
        Task<byte[]> ReadHoldingRegistersRawAsync(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Read contiguous block of input registers.
        /// </summary>
        /// <param name="startAddress">Address to begin reading.</param>
        /// <param name="numberOfPoints">Number of holding registers to read.</param>
        /// <returns>Input registers status</returns>
        Task<byte[]> ReadInputRegistersRawAsync(ushort startAddress, ushort numberOfPoints);
        /// <summary>
        /// Write a single coil value.
        /// </summary>
        /// <param name="coilAddress">Address to write value to.</param>
        /// <param name="value">Value to write.</param>
        Task WriteSingleCoilRawAsync(ushort coilAddress, byte[] value);
        /// <summary>
        /// Write a single holding register.
        /// </summary>
        /// <param name="registerAddress">Address to write.</param>
        /// <param name="value">Value to write.</param>
        Task WriteSingleRegisterRawAsync(ushort registerAddress, byte[] value);
        /// <summary>
        /// Write a block of 1 to 123 contiguous registers.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        Task WriteMultipleRegistersRawAsync(ushort startAddress, byte[] data);
        /// <summary>
        /// Force each coil in a sequence of coils to a provided value.
        /// </summary>
        /// <param name="startAddress">Address to begin writing values.</param>
        /// <param name="data">Values to write.</param>
        Task WriteMultipleCoilsRawAsync(ushort startAddress, bool[] data); 
        #endregion [Asynchronous Methods]
    }
}