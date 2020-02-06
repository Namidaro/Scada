using System;
using System.Linq;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// Represents object serializer functionality
    /// </summary>
    internal class ObjectSerializer<TValue> : ISerializer<TValue> where TValue : class, new()
    {
        #region [Constants]
        private const string NULL_METADATA_MESSAGE = "Can't create object serializer for Null object metadata";
        private const string DESERIALIZATION_EXCEPTION_MESSAGE = "There is an error while object de-serialization. Look for inner exception to get more information.";
        private const string SERIALIZATION_EXCEPTION_MESSAGE = "There is an error while object serialization. Look for inner exception to get more information.";
        private const string METADA_TYPE_CONFLICT_MESSAGE = "The type to serialize is not equals to the metadata type information";
        #endregion [Constants]


        #region [Private fields]
        private readonly ReferencedTypeMetadata<TValue> _metadata;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ObjectSerializer{TValue}"/>
        /// 
        /// 
        /// <exception cref="MetadataException"></exception>
        /// 
        /// </summary>
        /// <param name="metadata">The instance of <see cref="ReferencedTypeMetadata{TValue}"/> to use</param>
        public ObjectSerializer(ReferencedTypeMetadata<TValue> metadata)
        {
            Guard.AgainstNullReference<MetadataException>(metadata, "metadata", NULL_METADATA_MESSAGE);

            this._metadata = metadata;
        }
        #endregion [Ctor's]


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
        public TValue Deserialize(IBinaryReader reader)
        {
            try
            {
                var result = Activator.CreateInstance<TValue>();
                foreach (var memberMetadata in this._metadata.Members)
                {
                    var serializer = memberMetadata.Serializer;
                    serializer.Deserialize(reader, result);
                }
                return result;
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(DESERIALIZATION_EXCEPTION_MESSAGE, exception);
            }
            return null;
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
        public void Serialize(IBinaryWriter writer, TValue value)
        {
            try
            {
                if (value == null)
                {
                    writer.Skip(this._metadata.Capacity);
                    return;
                }
                foreach (var member in this._metadata.Members)
                {
                    member.Serializer.Serialize(writer, value);
                }
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SERIALIZATION_EXCEPTION_MESSAGE, exception);
            }
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
        /// <summary>
        /// Serializes a value to output stream
        /// 
        /// 
        /// <exception cref="SerializationException"></exception>
        /// 
        /// </summary>
        /// <param name="writer">The write to which value to serialize</param>
        /// <param name="value">The value to serialize</param>
        void ISerializer.Serialize(IBinaryWriter writer, object value)
        {
            this.Serialize(writer, (TValue)value);
        }
        #endregion [ISerializer members]
    }
}