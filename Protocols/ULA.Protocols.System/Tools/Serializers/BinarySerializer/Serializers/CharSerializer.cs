using System;
using System.Text;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class CharSerializer : ISerializer<char>
    {
        private const int VALUE_FOR_UNICODE_CHAR = 2;
        private const int VALUE_FOR_CHAR = 1;

        private readonly int _capacity;
        private readonly Encoding _encoding;

        /// <summary>
        /// Creates an instance of <see cref="CharSerializer"/>
        /// </summary>
        /// <param name="capacity">A capacity of a char</param>
        /// <param name="encoding"></param>
        public CharSerializer(int capacity, Encoding encoding)
        {
            this._capacity = capacity;
            this._encoding = encoding;
        }


        #region [ISerializer<char> members]
        /// <summary>
        /// Deserializes a value from input stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="reader">The reader to use in order to get value</param>
        /// <returns>Deserializable value</returns>
        public char Deserialize(IBinaryReader reader)
        {
            try
            {
                if (this._capacity == VALUE_FOR_UNICODE_CHAR)
                {
                    return reader.ReadUnicodeChar(this._encoding);
                }
                if (this._capacity == VALUE_FOR_CHAR)
                {
                    return reader.ReadChar();
                }
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
            return (char)0;
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
        public void Serialize(IBinaryWriter writer, char value)
        {
            try
            {
                //TODO: Something is wrong
                if (this._capacity == VALUE_FOR_UNICODE_CHAR)
                {
                    writer.WriteUnicodeChar(value, this._encoding);
                }
                if (this._capacity == VALUE_FOR_CHAR)
                {
                    writer.WriteChar(value);
                }
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
        void ISerializer.Serialize(IBinaryWriter writer, object value)
        {
            this.Serialize(writer, (char)value);
        }
        /// <summary>
        /// Deserializes a value from input stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="reader">The reader to use in order to get value</param>
        /// <returns>Deserializable value</returns>
        object ISerializer.Deserialize(IBinaryReader reader)
        {
            return this.Deserialize(reader);
        } 
        #endregion [ISerializer<char> members]
    }
}
