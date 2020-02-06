using System;
using YP.ToolKit.System.Exceptions;
using YP.ToolKit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.ToolKit.System.Validation;

namespace YP.ToolKit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents metadata that cover simple value, such as <see cref="Int32"/>, <see cref="Byte"/> and e.t.c.
    /// </summary>
    public class SimpleValueMetadata : MetadataBase
    {
        #region [Constants]
        private const string NULL_SERIALIZER_MESSAGE = "Can't create instance of metadata with Null serializer"; 
        #endregion [Constants]


        #region [Private fields]
        private readonly ISimpleValueSerializer _serializer;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="SimpleValueMetadata"/>
        /// </summary>
        /// <param name="serializer">The serializer to use</param>
        /// <param name="valueType">The type of value that metadata will cover</param>
        public SimpleValueMetadata(ISimpleValueSerializer serializer, Type valueType):base(valueType)
        {
            Guard.AgainstNullReference<MetadataException>(serializer, "serializer", NULL_SERIALIZER_MESSAGE);
            
            this._serializer = serializer;
        } 
        #endregion [Ctor's]


        #region [MetadataBase members]
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        public override ISerializer Serializer
        {
            get { return this._serializer; }
        }
        #endregion [MetadataBase members]
    }
}