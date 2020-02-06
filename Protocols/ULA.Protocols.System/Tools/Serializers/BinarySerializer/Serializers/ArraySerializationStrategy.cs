using System;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class ArraySerializationStrategy : CollectionSerializationStrategyBase
    {

        public ArraySerializationStrategy(ISerializableTypeMetadata elementMetadata, int length)
            : base(elementMetadata, length)
        { }


        protected override void OnSerialize(IBinaryWriter writer, object value)
        {
            var enumeration = (Array)value;

            var serializer = this._elementMetadata.Serializer;
            var currentCount = 0;
            foreach (var item in enumeration)
            {
                if (currentCount == this._length) return;
                serializer.Serialize(writer, item);
                currentCount++;
            }
            this.SkipIfNecessary(writer, currentCount);
        }

        protected override object OnDeserialize(IBinaryReader reader)
        {
            var result = Array.CreateInstance(this._elementMetadata.ValueType, this._length);
            var serializer = this._elementMetadata.Serializer;
            for (var i = 0; i < this._length; i++)
                result.SetValue(serializer.Deserialize(reader), i);
            return result;
        }
    }
}