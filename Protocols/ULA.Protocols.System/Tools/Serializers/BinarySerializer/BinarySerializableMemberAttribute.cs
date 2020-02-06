using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer
{
    /// <summary>
    /// Represents metadata that is indicated that a field or property could be binary serializable
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class BinarySerializableMemberAttribute : Attribute
    {
        #region [Properties]
        /// <summary>
        /// Gets or sets the member offset for serialization
        /// </summary>
        public uint Offset { get; set; }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinarySerializableMemberAttribute"/>
        /// </summary>
        /// <param name="offset">The offset of a member for serialization</param>
        public BinarySerializableMemberAttribute(uint offset)
        {
            this.Offset = offset;
        } 
        #endregion [Ctor's]
    }
}