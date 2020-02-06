using System;
using System.Linq;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;


namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a member metadata builder
    /// </summary>
    public class MemberMetadataBuilder
    {
        #region [Private fields]
        private readonly IMemberInfo _memberInfo;
        private readonly IMetadataBuilder _successorMetadataBuilder;
        private readonly bool _isSkipped;
        #endregion [Private fields]


        #region [Ctor's]

        /// <summary>
        /// Creates an instance of <see cref="MemberMetadataBuilder"/>
        /// </summary>
        /// <param name="memberInfo">The member information to use</param>
        /// <param name="successorMetadataBuilder">A succesor metadata builder to use</param>
        /// <param name="isSkipped">A value that indicates whether a value must be skipped during binary serialization</param>
        public MemberMetadataBuilder(IMemberInfo memberInfo,
            IMetadataBuilder successorMetadataBuilder, bool isSkipped = false)
        {
            this._memberInfo = memberInfo;
            this._successorMetadataBuilder = successorMetadataBuilder;
            _isSkipped = isSkipped;
        }
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Builds a metadata for a member
        /// </summary>
        /// <returns></returns>
        public MemberMetadata Build()
        {
            var metadata = this._successorMetadataBuilder.Build();
            var result = new MemberMetadata(metadata, this._memberInfo);
            MemberMetadataSerializer serializer;
            if (!this._isSkipped)
            {
                if (metadata.GetType().IsGenericType)
                {
                    serializer = (MemberMetadataSerializer)Activator.CreateInstance(typeof(MemberMetadataSerializer<>).MakeGenericType(
                                metadata.GetType().GetGenericArguments().First()), result);
                }
                else
                    serializer = new MemberMetadataSerializer(result);
            }
            else
            {
                serializer = new SkippedMemberMetadataSerializer(result);
            }
            result.SetSerializer(serializer);
            return result;
        }
        #endregion [Public members]
    }
}