using System;
using System.Runtime.Serialization;
using System.Text;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper
{
    /// <summary>
    /// Exposes binary reader functionality
    /// </summary>
    public interface IBinaryReader
    {
        /// <summary>
        /// Reads <see cref="Int32"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        int ReadInt32();
        /// <summary>
        /// Reads <see cref="byte"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        byte ReadByte();
        /// <summary>
        /// Reads <see cref="string"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        string ReadString(int length, Encoding encoding);
        /// <summary>
        /// Reads <see cref="long"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        long ReadInt64();
        /// <summary>
        /// Reads <see cref="short"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        short ReadInt16();
        /// <summary>
        /// Reads <see cref="bool"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        bool ReadBoolean();
        /// <summary>
        /// Reads <see cref="sbyte"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        sbyte ReadSByte();
        /// <summary>
        /// Reads <see cref="ushort"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        ushort ReadUInt16();
        /// <summary>
        /// Reads <see cref="uint"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        uint ReadUInt32();
        /// <summary>
        /// Reads <see cref="ulong"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        ulong ReadUInt64();
        /// <summary>
        /// Reads <see cref="char"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        char ReadUnicodeChar(Encoding encoding);
        /// <summary>
        /// Reads <see cref="char"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        char ReadChar();
        /// <summary>
        /// Skips a namuber of bytes
        /// </summary>
        /// <param name="bytesToSkip">A number of bytes to skip</param>
        void Skip(int bytesToSkip);
    }
}