using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents a serializable type metadata
    /// </summary>
    internal class SerializableTypeMetadata : ISerializableTypeMetadata
    {
        #region [Constants]
        private const string NULL_SERIALIZER_MESSAGE = "Can't create instance of metadata with Null serializer";
        private const string NULL_TYPE_MESSAGE = "Can't create instance of metadat with Null type";
        #endregion [Constants]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="SerializableTypeMetadata"/>
        /// </summary>
        /// <param name="serializer">An instance of a serializer to use</param>
        /// <param name="type">A type that a metadata is assosiated with</param>
        /// <param name="capacity">A size of type in bytes</param>
        public SerializableTypeMetadata(ISerializer serializer, Type type, int capacity)
        {
            Guard.AgainstNullReference<MetadataException>(serializer, "serializer", NULL_SERIALIZER_MESSAGE);
            Guard.AgainstNullReference<MetadataException>(type, "type", NULL_TYPE_MESSAGE);

            this.Serializer = serializer;
            this.ValueType = type;
            this.Capacity = capacity;
        } 
        #endregion [Ctor's]


        #region [ISerializableTypeMetadata members]
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        public ISerializer Serializer { get; private set; }
        /// <summary>
        /// Gets a type that a metadata is assosiated with
        /// </summary>
        public Type ValueType { get; set; }
        /// <summary>
        /// Gets a size of type in bytes
        /// </summary>
        public int Capacity { get; private set; } 
        #endregion [ISerializableTypeMetadata members]
    }
}
