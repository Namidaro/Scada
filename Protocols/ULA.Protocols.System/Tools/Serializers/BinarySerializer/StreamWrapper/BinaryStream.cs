using System;
using System.IO;
using YP.ToolKit.System.Exceptions;
using YP.ToolKit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.ToolKit.System.Validation;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using CoreStream = System.IO.Stream;

namespace YP.ToolKit.System.Tools.Serializers.BinarySerializer.StreamWrapper
{
    /// <summary>
    /// Represents <see cref="CoreStream"/> wrapper
    /// </summary>
    public class BinaryStream : IBinaryReader, IBinaryWriter
    {
        #region [Constants]
        private const string NULL_STREAM_MESSAGE = "Can't wrap Null stream"; 
        #endregion [Constants]


        #region [Private fields]
        private readonly CoreStream _stream; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinaryStream"/>
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="stream">The stream to use</param>
        public BinaryStream(CoreStream stream)
        {
            Guard.AgainstNullReference<SerializationException>(stream, "stream", NULL_STREAM_MESSAGE);

            this._stream = stream;
        } 
        #endregion [Ctor's]


        #region [IBinaryReader members]
        /// <summary>
        /// Reads <see cref="Int32"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public int ReadInt32()
        {
            try
            {
                using (var reader = new BinaryReader(this._stream))
                    return reader.ReadInt32();
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
            return -1;
        }
        /// <summary>
        /// Reads <see cref="byte"/> from inputed stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <returns>Read value</returns>
        public byte ReadByte()
        {
            try
            {
                using (var reader = new BinaryReader(this._stream))
                    return reader.ReadByte();
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
            return 0;
        } 
        #endregion [IBinaryReader members]


        #region [IBinaryWriter members]
        /// <summary>
        /// Writes <see cref="int"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteInt32(int value)
        {
            try
            {
                using (var writer = new BinaryWriter(this._stream))
                    writer.Write(value);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
        }
        /// <summary>
        /// Writes <see cref="byte"/> value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="value">The value to write</param>
        public void WriteByte(byte value)
        {
            try
            {
                using (var writer = new BinaryWriter(this._stream))
                    writer.Write(value);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
        } 
        #endregion [IBinaryWriter members]
    }
}