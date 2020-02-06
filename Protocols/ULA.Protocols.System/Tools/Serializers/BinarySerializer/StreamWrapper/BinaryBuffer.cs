using System;
using System.Runtime.Serialization;
using System.Text;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper
{
    /// <summary>
    /// Represents a binary buffer functionality
    /// </summary>
    public class BinaryBuffer : IBinaryReader, IBinaryWriter
    {
        #region [Private members]
        private readonly byte[] _stream;
        private int _counter;
        #endregion [Private members]


        #region [Constants]
        private const int VALUE_FOR_INT16 = 2;
        private const int VALUE_FOR_BYTE = 1;
        private const string CAN_NOT_BE_NULL = "Can not be null";
        #endregion


        #region [BinaryBuffer members]

        /// <summary>
        /// Gets byte array, which contains information about serializable object
        /// </summary>
        public byte[] Data
        {
            get { return _stream; }
        }

        #endregion [BinaryBuffer members]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinaryBuffer"/>.
        /// 
        /// 
        /// <exception cref="Exception"></exception>
        /// 
        /// </summary>
        /// <param name="stream">Array of bytes to use</param>
        public BinaryBuffer(byte[] stream)
        {
            Guard.AgainstNullReference<YP.Toolkit.System.Exceptions.SerializationException>(stream, "stream", CAN_NOT_BE_NULL);

            this._stream = stream;
            this._counter = 0;
        }
        #endregion [Ctor's]


        #region [IBinaryReader members]
        /// <summary>
        /// Reads <see cref="Byte"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public byte ReadByte()
        {
            byte value = _stream[this._counter];
            this._counter += VALUE_FOR_BYTE;
            return value;
        }
        /// <summary>
        /// Reads <see cref="sbyte"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public sbyte ReadSByte()
        {
            var value = (sbyte)_stream[this._counter++];
            return value;
        }
        /// <summary>
        /// Reads <see cref="short"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public short ReadInt16()
        {
            short value = this.GetInt16FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="ushort"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public ushort ReadUInt16()
        {
            ushort value = this.GetUInt16FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="Int32"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public int ReadInt32()
        {
            int value = GetInt32FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="uint"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public uint ReadUInt32()
        {
            uint value = this.GetUInt32FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="long"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public long ReadInt64()
        {
            long value = this.GetInt64FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="ulong"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public ulong ReadUInt64()
        {
            ulong value = this.GetUInt64FromBytes();
            return value;
        }
        /// <summary>
        /// Reads <see cref="char"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public char ReadUnicodeChar(Encoding encoding)
        {
            var value = encoding.GetChars(_stream, _counter, VALUE_FOR_INT16);
            this._counter += VALUE_FOR_INT16;
            return value[0];
        }
        /// <summary>
        /// Reads <see cref="char"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public char ReadChar()
        {
            var value = (char)_stream[_counter++];
            return value;
        }
        /// <summary>
        /// Reads <see cref="String"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="capacity">Length of <see cref="String"/> value</param>
        /// <param name="encoding">Encoding of string</param>
        /// <returns>Read value</returns>
        public string ReadString(int capacity, Encoding encoding)
        {
            var chars = new char[capacity];
            for (int i = 0; i < capacity; i++)
            {
                chars[i] = (char)this._stream[this._counter++];
            }
            return new string(chars);
        }
        /// <summary>
        /// Reads <see cref="bool"/> from array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public bool ReadBoolean()
        {
            bool value = this.GetBooleanFromBytes();
            return value;
        }
        #endregion [IBinaryReader members]


        #region [IBinaryWriter members]
        /// <summary>
        /// Writes <see cref="Byte"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteByte(byte value)
        {
            _stream[this._counter++] = value;
        }
        /// <summary>
        /// Writes <see cref="sbyte"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteSByte(sbyte value)
        {
            unchecked
            {
                _stream[this._counter++] = (byte)value;
            }
        }
        /// <summary>
        /// Writes <see cref="short"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt16(short value)
        {
            this.GetBytesFromInt16(value);
        }
        /// <summary>
        /// Writes <see cref="ushort"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteUInt16(ushort value)
        {
            this.GetBytesFromUInt16(value);
        }
        /// <summary>
        /// Writes <see cref="Int32"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        ///  
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt32(int value)
        {
            this.GetBytesFromInt32(value);
        }
        /// <summary>
        /// Writes <see cref="uint"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteUInt32(uint value)
        {
            this.GetBytesFromUInt32(value);
        }
        /// <summary>
        /// Writes <see cref="long"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt64(long value)
        {
            this.GetBytesFromInt64(value);
        }
        /// <summary>
        /// Writes <see cref="ulong"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteUInt64(ulong value)
        {
            this.GetBytesFromUInt64(value);
        }
        /// <summary>
        /// Writes <see cref="char"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        public void WriteUnicodeChar(char value, Encoding encoding)
        {
            var charInBytes = encoding.GetBytes(new[] { value });
            this._stream[this._counter++] = charInBytes[0];
            this._stream[this._counter++] = charInBytes[1];
        }
        /// <summary>
        /// Writes <see cref="char"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        public void WriteChar(char value)
        {
            _stream[_counter++] = (byte)value;
        }
        /// <summary>
        /// Skips a namuber of bytes
        /// </summary>
        /// <param name="bytesToSkip">A number of bytes to skip</param>
        public void Skip(int bytesToSkip)
        {
            this._counter += bytesToSkip;
        }
        /// <summary>
        /// Writes <see cref="String"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="capacity">Capacity of <see cref="String"/> value</param>
        /// <param name="encoding">Encoding of string</param>
        public void WriteString(string value, int capacity, Encoding encoding)
        {
            foreach (var @char in value)
            {
                this._stream[this._counter++] = (byte)@char;
            }
            this._counter += (capacity - value.Length);
        }
        /// <summary>
        /// Writes <see cref="bool"/> value to array of bytes
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteBoolean(bool value)
        {
            this.GetBytesFromBoolean(value);
        }
        #endregion [IBinaryWriter members]


        #region [Help members]
        /// <summary>
        /// Convert array of bytes to <see cref="short"/> value
        /// </summary>
        /// <returns><see cref="short"/> value</returns>
        private short GetInt16FromBytes()
        {
            var value = (short)(this._stream[this._counter + 1] | (this._stream[this._counter] << 8));
            this._counter += VALUE_FOR_INT16;
            return value;
        }
        /// <summary>
        /// Convert array of bytes to <see cref="ushort"/> value
        /// </summary>
        /// <returns><see cref="ushort"/> value</returns>
        private ushort GetUInt16FromBytes()
        {
            return (ushort)this.GetInt16FromBytes();
        }
        /// <summary>
        /// Convert array of bytes to <see cref="Int32"/> value
        /// </summary>
        /// <returns><see cref="int"/> value</returns>
        private int GetInt32FromBytes()
        {
            int value = 0;
            value += (_stream[this._counter++] & 0xff) << 24;
            value += (_stream[this._counter++] & 0xff) << 16;
            value += (_stream[this._counter++] & 0xff) << 8;
            value += _stream[this._counter++] & 0xff;
            return value;
        }
        /// <summary>
        /// Convert array of bytes to <see cref="uint"/> value
        /// </summary>
        /// <returns><see cref="uint"/> value</returns>
        private uint GetUInt32FromBytes()
        {
            return (uint)this.GetInt32FromBytes();
        }
        /// <summary>
        /// Convert <see cref="Int32"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromInt32(int value)
        {
            this._stream[this._counter++] = (byte)((value >> 24) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 16) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 8) & 0xff);
            this._stream[this._counter++] = (byte)value;
        }
        /// <summary>
        /// Convert <see cref="uint"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromUInt32(uint value)
        {
            this.GetBytesFromInt32((int)value);
        }
        /// <summary>
        /// Convert <see cref="long"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromInt64(long value)
        {
            this._stream[this._counter++] = (byte)((value >> 56) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 48) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 40) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 32) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 24) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 16) & 0xff);
            this._stream[this._counter++] = (byte)((value >> 8) & 0xff);
            this._stream[this._counter++] = (byte)value;
        }
        /// <summary>
        /// Convert <see cref="ulong"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromUInt64(ulong value)
        {
            this.GetBytesFromInt64((long)value);
        }
        /// <summary>
        /// Convert array of bytes to <see cref="long"/> value
        /// </summary>
        /// <returns><see cref="long"/> value</returns>
        private long GetInt64FromBytes()
        {
            long value = 0;
            value += (this._stream[this._counter++] & 0xff) << 56;
            value += (this._stream[this._counter++] & 0xff) << 48;
            value += (this._stream[this._counter++] & 0xff) << 40;
            value += (this._stream[this._counter++] & 0xff) << 32;
            value += (this._stream[this._counter++] & 0xff) << 24;
            value += (this._stream[this._counter++] & 0xff) << 16;
            value += (this._stream[this._counter++] & 0xff) << 8;
            value += this._stream[this._counter++] & 0xff;
            return value;
        }
        /// <summary>
        /// Convert array of bytes to <see cref="ulong"/> value
        /// </summary>
        /// <returns><see cref="ulong"/> value</returns>
        private ulong GetUInt64FromBytes()
        {
            return (ulong)this.GetInt64FromBytes();
        }
        /// <summary>
        /// Convert <see cref="short"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromInt16(short value)
        {
            this._stream[this._counter++] = (byte)((value >> 8) & 0xff);
            this._stream[this._counter++] = (byte)value;
        }
        /// <summary>
        /// Convert <see cref="ushort"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromUInt16(ushort value)
        {
            this.GetBytesFromInt16((short)value);
        }
        /// <summary>
        /// Convert <see cref="bool"/> value to array of bytes
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns>Array of bytes</returns>
        private void GetBytesFromBoolean(bool value)
        {
            this._stream[this._counter++] = value ? (byte)1 : (byte)0;
        }
        /// <summary>
        /// Convert array of bytes to <see cref="bool"/> value
        /// </summary>
        /// <returns><see cref="bool"/> value</returns>
        private bool GetBooleanFromBytes()
        {
            return (this._stream[this._counter++]) != 0;
        }
        #endregion [Help members]
    }
}
