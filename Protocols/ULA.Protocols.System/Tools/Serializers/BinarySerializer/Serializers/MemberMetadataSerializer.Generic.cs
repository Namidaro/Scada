using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    internal class MemberMetadataSerializer<TValue> : MemberMetadataSerializer
    {
        private readonly IMemberInfo<TValue> _memberInfo;
        private readonly ISerializer<TValue> _serializer;

        public MemberMetadataSerializer(MemberMetadata member)
            : base(member)
        {
            this._memberInfo = member.MemberInfo as IMemberInfo<TValue>;
            this._serializer = member.SuccessorMetadata.Serializer as ISerializer<TValue>;
        }


        public override void Serialize(IBinaryWriter writer, object value)
        {
            // ReSharper disable PossibleNullReferenceException
            //var memberInfo = (IMemberInfo<TValue>)this._memberMetadata.MemberInfo;
            var valueToSerialize = this._memberInfo.GetValue(value);
            //var serializer = (ISerializer<TValue>)this._memberMetadata.SuccessorMetadata.Serializer;
            this._serializer.Serialize(writer, valueToSerialize);
            // ReSharper restore PossibleNullReferenceException
        }

        public override void Deserialize(IBinaryReader reader, object accessor)
        {
            // ReSharper disable PossibleNullReferenceException
            //var memberInfo = this._memberMetadata.MemberInfo as IMemberInfo<TValue>;
            //var serializer = this._memberMetadata.SuccessorMetadata.Serializer as ISerializer<TValue>;
            var result = this._serializer.Deserialize(reader);
            this._memberInfo.SetValue(accessor, result);
            // ReSharper disable PossibleNullReferenceException
        }
    }
}