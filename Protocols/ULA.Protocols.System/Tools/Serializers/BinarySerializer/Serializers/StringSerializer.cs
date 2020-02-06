using System;
using System.Text;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// Represents <see cref="String"/> value serializer
    /// </summary>
    internal class StringSerializer : ISerializer
    {
        #region [Private members]
        private readonly Encoding _encoding;
        private readonly int _capacity;
        #endregion [Private members]


        #region [Ctor's]

        /// <summary>
        /// Creates an instance of <see cref="StringSerializer"/>
        /// </summary>
        /// <param name="capacity">Length of string to serialize</param>
        /// <param name="encoding">Encoding of string to serialize</param>
        public StringSerializer(int capacity, Encoding encoding)
        {
            this._encoding = encoding;
            this._capacity = capacity;
        }
        #endregion [Ctor's]


        #region [ISerializer members]
        /// <summary>
        /// Serializes a value to output stream
        /// </summary>
        /// <param name="writer">The write to which value to serialize</param>
        /// <param name="value">The value to serialize</param>
        public void Serialize(IBinaryWriter writer, object value)
        {
            try
            {
                if (value == null)
                {
                    writer.Skip(this._capacity);
                    return;
                }
                writer.WriteString((string)value, this._capacity, this._encoding);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE, exception);
            }
        }

        /// <summary>
        /// Deserializes a value from input stream
        /// </summary>
        /// <param name="reader">The reader to use in order to get value</param>
        /// <returns>Deserializable value</returns>
        public object Deserialize(IBinaryReader reader)
        {
            try
            {
                return reader.ReadString(this._capacity, this._encoding);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_DESERIALIZATION_EXCEPTION_MESSAGE, exception);
            }
            return null;
        }

        #endregion
    }
}
