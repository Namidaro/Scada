using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class ArraySerializationStrategy<TValue> : CollectionStrategySerialization<TValue>
    {
        public ArraySerializationStrategy(ISerializableTypeMetadata<TValue> elementMetadata, int length)
            : base(elementMetadata, length)
        { }

        protected override object OnDeserialize(IBinaryReader reader)
        {
            // ReSharper disable PossibleNullReferenceException
            var result = new TValue[this._length];
            for (var i = 0; i < this._length; i++)
                result[i] = this._serializer.Deserialize(reader);
            return result;

            // ReSharper restore PossibleNullReferenceException
        }
    }
}