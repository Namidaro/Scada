using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents metadata that is indicated that a field or property must be skipped during binary serialization
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BinarySkipMemberAttribute : BinarySerializableMemberAttribute
    {
        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinarySkipMemberAttribute"/>
        /// </summary>
        /// <param name="offset">The offset of a member for serialization</param>
        public BinarySkipMemberAttribute(uint offset)
            : base(offset)
        { } 
        #endregion [Ctor's]
    }
}