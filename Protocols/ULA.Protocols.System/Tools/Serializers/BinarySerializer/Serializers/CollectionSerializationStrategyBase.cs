using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal abstract class CollectionSerializationStrategyBase
    {
        protected readonly ISerializableTypeMetadata _elementMetadata;
        protected readonly int _length;

        protected CollectionSerializationStrategyBase(ISerializableTypeMetadata elementMetadata, int length)
        {
            this._elementMetadata = elementMetadata;
            this._length = length;
        }


        public void Serialize(IBinaryWriter writer, object value)
        {
            try
            {
                this.OnSerialize(writer, value);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
            }
        }
        public object Deserialize(IBinaryReader reader)
        {
            try
            {
                return this.OnDeserialize(reader);
            }
            catch (Exception exception)
            {
                Guard.Throw<SerializationException>(SerializerMessanger.UNEXPECTED_SERIALIZATION_EXCEPTION_MESSAGE,
                                                    exception);
                return null;
            }
        }

        protected void SkipIfNecessary(IBinaryWriter writer, int currentCount)
        {
            if (currentCount < this._length)
                writer.Skip((this._length - currentCount) * this._elementMetadata.Capacity);
        }

        protected abstract object OnDeserialize(IBinaryReader reader);
        protected abstract void OnSerialize(IBinaryWriter writer, object value);
    }
}