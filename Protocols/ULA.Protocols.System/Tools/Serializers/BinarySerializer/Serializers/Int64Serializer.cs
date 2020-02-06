using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// 
    /// </summary>
    internal class Int64Serializer : ISerializer<Int64>
    {
        #region [ISerializer members]
        /// <summary>
        /// Deserializes a value from input stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="reader">The reader to use in order to get value</param>
        /// <returns>Deserializable value</returns>
        public long Deserialize(IBinaryReader reader)
        {
            try
            {
                return reader.ReadInt64();
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
            return 0;
        }
        /// <summary>
        /// Serializes a value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="writer">The write to which value to serialize</param>
        /// <param name="value">The value to serialize</param>
        public void Serialize(IBinaryWriter writer, long value)
        {
            try
            {
                writer.WriteInt64(value);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
        }
        /// <summary>
        /// Serializes a value to output stream
        /// </summary>
        /// <param name="writer">The write to which value to serialize</param>
        /// <param name="value">The value to serialize</param>
        public void Serialize(IBinaryWriter writer, object value)
        {
            this.Serialize(writer, (Int64)value);
        }
        /// <summary>
        /// Deserializes a value from input stream
        /// </summary>
        /// <param name="reader">The reader to use in order to get value</param>
        /// <returns>Deserializable value</returns>
        object ISerializer.Deserialize(IBinaryReader reader)
        {
            return this.Deserialize(reader);
        }
        #endregion [ISerializer members]
    }
}
