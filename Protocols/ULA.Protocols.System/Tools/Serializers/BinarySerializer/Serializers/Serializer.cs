using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal abstract class Serializer<TValue> : ISerializer
    {
        public void Serialize(IBinaryWriter writer, object value)
        {
            this.SerializeGeneric(writer, (TValue)value);
        }
        public object Deserialize(IBinaryReader reader)
        {
            return this.DeserializeGeneric(reader);
        }


        public abstract TValue DeserializeGeneric(IBinaryReader reader);
        public abstract void SerializeGeneric(IBinaryWriter writer, TValue value);
    }
}