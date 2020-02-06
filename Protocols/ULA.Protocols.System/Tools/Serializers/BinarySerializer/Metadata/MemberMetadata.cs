using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents serializable member's metadata
    /// </summary>
    public class MemberMetadata : IMetadata
    {
        #region [Constants]
        private const string NULL_METADATA_MESSAGE = "Can't create member metadata with Null inner metadata";
        private const string NULL_MEMBER_INFO_MESSAGE = "Can't create member metadata with Null member information";
        #endregion [Constants]


        #region [Private fields]
        private readonly ISerializableTypeMetadata _memberMetadata;
        private readonly IMemberInfo _memberInfo;
        private MemberMetadataSerializer _serializer;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="MemberMetadata"/>
        /// </summary>
        /// <param name="memberMetadata">The inner data metadata to use</param>
        /// <param name="memberInfo">The information of a member to serialize</param>
        public MemberMetadata(ISerializableTypeMetadata memberMetadata, IMemberInfo memberInfo)
        {
            Guard.AgainstNullReference<MetadataException>(memberMetadata, "memberMetadata", NULL_METADATA_MESSAGE);
            Guard.AgainstNullReference<MetadataException>(memberInfo, "memberInfo", NULL_MEMBER_INFO_MESSAGE);

            this._memberMetadata = memberMetadata;
            this._memberInfo = memberInfo;
        }
        #endregion [Ctor's]

        
        #region [Properties]
        /// <summary>
        /// Gets the information of member that is under metadata
        /// </summary>
        public IMemberInfo MemberInfo
        {
            get { return this._memberInfo; }
        }
        /// <summary>
        /// Gets the successor metadata
        /// </summary>
        public ISerializableTypeMetadata SuccessorMetadata
        {
            get { return this._memberMetadata; }
        }
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        public MemberMetadataSerializer Serializer
        {
            get { return this._serializer; }
        }
        #endregion [Properties]


        #region [IMetadata members]
        /// <summary>
        /// Gets a size of type in bytes
        /// </summary>
        public int Capacity
        {
            get { return this._memberMetadata.Capacity; }
        }
        /// <summary>
        /// Gets a type that a metadata is assosiated with
        /// </summary>
        public Type ValueType
        {
            get { return this._memberMetadata.ValueType; }
        } 
        #endregion [IMetadata members]


        #region [Public members]
        /// <summary>
        /// Sets a serializer to use
        /// </summary>
        /// <param name="serializer">An instance of serializer to use</param>
        public void SetSerializer(MemberMetadataSerializer serializer)
        {
            this._serializer = serializer;
        }
        #endregion [Public members]
    }
}