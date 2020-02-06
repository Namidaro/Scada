using System;
using System.Collections;
using System.Collections.Generic;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class CollectionStrategySerialization : CollectionSerializationStrategyBase
    {
        private readonly Type _collectionType;


        public CollectionStrategySerialization(ISerializableTypeMetadata elementMetadata, int length)
            : base(elementMetadata, length)
        {
            this._collectionType = typeof(List<>).MakeGenericType(this._elementMetadata.ValueType);
        }


        protected override object OnDeserialize(IBinaryReader reader)
        {
            var result = (IList)Activator.CreateInstance(this._collectionType, this._elementMetadata);
            var serializer = this._elementMetadata.Serializer;
            for (var i = 0; i < this._length; i++)
                result.Add(serializer.Deserialize(reader));
            return result;
        }
        protected override void OnSerialize(IBinaryWriter writer, object value)
        {
            // ReSharper disable PossibleNullReferenceException
            var currentCount = 0;
            var enumeration = value as IEnumerable;
            var serializer = this._elementMetadata.Serializer;
            foreach (var element in enumeration)
            {
                if (currentCount == this._length) return;
                serializer.Serialize(writer, element);
                currentCount++;
            }
            this.SkipIfNecessary(writer, currentCount);

        }
        
    }
}