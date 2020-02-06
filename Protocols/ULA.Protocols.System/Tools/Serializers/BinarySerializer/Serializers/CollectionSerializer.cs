using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class CollectionSerializer<TElementType> : ISerializer
    {
        private readonly CollectionStrategySerialization _serializationStrategy;

        public CollectionSerializer(ISerializableTypeMetadata elementMetadata, int length)
        {
            Guard.AgainstNullReference<MetadataException>(elementMetadata, "elementMetadata");

            var typedMetadata = elementMetadata as ISerializableTypeMetadata<TElementType>;
            this._serializationStrategy = typedMetadata == null
                                              ? new CollectionStrategySerialization(elementMetadata, length)
                                              : new CollectionStrategySerialization<TElementType>(typedMetadata, length);
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
        public void Serialize(IBinaryWriter writer, object value)
        {
           this._serializationStrategy.Serialize(writer, value);
            
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
        public object Deserialize(IBinaryReader reader)
        {
            return this._serializationStrategy.Deserialize(reader);
        }
    }
}