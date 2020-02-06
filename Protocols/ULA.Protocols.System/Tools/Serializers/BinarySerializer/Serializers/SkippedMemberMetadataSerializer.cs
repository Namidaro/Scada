using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.StreamWrapper;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers
{
    /// <summary>
    /// Represents a member metadata serializer
    /// </summary>
    public class SkippedMemberMetadataSerializer : MemberMetadataSerializer
    {
        #region [Private fields]
        private readonly int _skippedBytes; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="SkippedMemberMetadataSerializer"/>
        /// </summary>
        /// <param name="memberMetadata">An instance of <see cref="MemberMetadata"/> to use</param>
        public SkippedMemberMetadataSerializer(MemberMetadata memberMetadata)
            : base(memberMetadata)
        {
            this._skippedBytes = this._memberMetadata.Capacity;
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Serializes a member's value
        /// </summary>
        /// <param name="writer">An instance of binary writer to use</param>
        /// <param name="accessor">An instance of an object to get value from</param>
        public override void Serialize(IBinaryWriter writer, object accessor)
        {
            writer.Skip(this._skippedBytes);
        }
        /// <summary>
        /// Deserializes a value for a member
        /// </summary>
        /// <param name="reader">An instance of binary reader to use</param>
        /// <param name="accessor">An instance of an object to set value to</param>
        public override void Deserialize(IBinaryReader reader, object accessor)
        {
            reader.Skip(this._skippedBytes);
        } 
        #endregion [Override members]
    }
}