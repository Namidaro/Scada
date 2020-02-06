using System.Text;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper
{
    /// <summary>
    /// Exposes binary stream writer
    /// </summary>
    public interface IBinaryWriter
    {
        /// <summary>
        /// Writes <see cref="int"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteInt32(int value);
        /// <summary>
        /// Writes <see cref="byte"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteByte(byte value);
        /// <summary>
        /// Writes <see cref="string"/> value to output stream.
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        /// <param name="length">The length of string</param>
        /// <param name="encoding">Encoding of string</param>
        void WriteString(string value, int length, Encoding encoding);
        /// <summary>
        /// Writes <see cref="long"/> value to output stream.
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteInt64(long value);
        /// <summary>
        /// Writes <see cref="short"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteInt16(short value);
        /// <summary>
        /// Writes <see cref="bool"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteBoolean(bool value);
        /// <summary>
        /// Writes <see cref="sbyte"/> value to output stream
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteSByte(sbyte value);
        /// <summary>
        /// Writes <see cref="ushort"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteUInt16(ushort value);
        /// <summary>
        /// Writes <see cref="uint"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteUInt32(uint value);
        /// <summary>
        /// Writes <see cref="ulong"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        void WriteUInt64(ulong value);
        /// <summary>
        /// Writes <see cref="char"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        void WriteUnicodeChar(char value, Encoding encoding);
        /// <summary>
        /// Writes <see cref="char"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        void WriteChar(char value);
        /// <summary>
        /// Skips a namuber of bytes
        /// </summary>
        /// <param name="bytesToSkip">A number of bytes to skip</param>
        void Skip(int bytesToSkip);
    }
}