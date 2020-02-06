using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class ArraySerializer<TElementType> : ISerializer
    {
        private readonly CollectionSerializationStrategyBase _serializationStrategy;

        public ArraySerializer(ISerializableTypeMetadata elementMetadata, int length)
        {
            var typedMetadata = elementMetadata as ISerializableTypeMetadata<TElementType>;

            this._serializationStrategy = typedMetadata == null ? (CollectionSerializationStrategyBase)
                                                new ArraySerializationStrategy(elementMetadata, length)
                                              : new ArraySerializationStrategy<TElementType>(typedMetadata, length);
        }

        public void Serialize(IBinaryWriter writer, object value)
        {
            this._serializationStrategy.Serialize(writer, value);
        }
        public object Deserialize(IBinaryReader reader)
        {
            return this._serializationStrategy.Deserialize(reader);
        }
    }
}