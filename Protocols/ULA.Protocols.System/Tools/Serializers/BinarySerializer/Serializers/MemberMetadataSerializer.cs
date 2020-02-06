using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// Represents a member metadata serializer
    /// </summary>
    public class MemberMetadataSerializer
    {
        #region [Private fields]
        // ReSharper disable PossibleNullReferenceException
        protected readonly MemberMetadata _memberMetadata; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="MemberMetadataSerializer"/>
        /// </summary>
        /// <param name="memberMetadata">An instance of <see cref="MemberMetadata"/> to use</param>
        public MemberMetadataSerializer(MemberMetadata memberMetadata)
        {
            this._memberMetadata = memberMetadata;
        } 
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Serializes a member's value
        /// </summary>
        /// <param name="writer">An instance of binary writer to use</param>
        /// <param name="accessor">An instance of an object to get value from</param>
        public virtual void Serialize(IBinaryWriter writer, object accessor)
        {
            var memberInfo = this._memberMetadata.MemberInfo;
            var valueToSerialize = memberInfo.GetValue(accessor);
            this._memberMetadata.SuccessorMetadata.Serializer.Serialize(writer, valueToSerialize);
        }
        /// <summary>
        /// Deserializes a value for a member
        /// </summary>
        /// <param name="reader">An instance of binary reader to use</param>
        /// <param name="accessor">An instance of an object to set value to</param>
        public virtual void Deserialize(IBinaryReader reader, object accessor)
        {
            var memberInfo = this._memberMetadata.MemberInfo;
            var value = this._memberMetadata.SuccessorMetadata.Serializer.Deserialize(reader);
            memberInfo.SetValue(accessor, value);
        } 
        #endregion [Public members]
    }
}