using System.Collections.Generic;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class CollectionStrategySerialization<TValue> : CollectionStrategySerialization
    {
        protected readonly ISerializer<TValue> _serializer;

        public CollectionStrategySerialization(ISerializableTypeMetadata<TValue> elementMetadata, int length)
            : base(elementMetadata, length)
        {
            this._serializer = elementMetadata.Serializer;
            }

        protected override void OnSerialize(IBinaryWriter writer, object value)
        {
            // ReSharper disable PossibleNullReferenceException
            var currentCount = 0;
            var enumeration = value as IEnumerable<TValue>;
            foreach (var element in enumeration)
            {
                if (currentCount == this._length) return;
                this._serializer.Serialize(writer, element);
                currentCount++;
            }
            this.SkipIfNecessary(writer, currentCount);
            // ReSharper restore PossibleNullReferenceException
        }

        protected override object OnDeserialize(IBinaryReader reader)
        {
            // ReSharper disable PossibleNullReferenceException
            var result = new List<TValue>(this._length);
            for (var i = 0; i < this._length; i++)
                result.Add(this._serializer.Deserialize(reader));
            return result;
            // ReSharper restore PossibleNullReferenceException
        }
    }
}