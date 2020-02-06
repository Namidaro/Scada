using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents object metadata
    /// </summary>
    public class ReferencedTypeMetadata<TValue> : ISerializableTypeMetadata<TValue> where TValue : class, new()
    {
        #region [Constants]
        private const string NULL_METADATA_MESSAGE = "Can't create object metadata while there is no info about object members";
        #endregion [Constants]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ReferencedTypeMetadata{TValue}"/>
        /// 
        /// 
        /// <exception cref="MetadataException"></exception>
        /// 
        /// </summary>
        /// <param name="collection">The collection of object members</param>
        /// <param name="capacity"></param>
        // ReSharper disable ParameterTypeCanBeEnumerable.Local
        public ReferencedTypeMetadata(MemberMetadata[] collection, int capacity)
        {
            Guard.AgainstNullReference<MetadataException>(collection, "collection", NULL_METADATA_MESSAGE);

            this.Members = collection;
            this.Capacity = capacity;
            this.Serializer = new ObjectSerializer<TValue>(this);
        }
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets the collection of object members
        /// </summary>
        public MemberMetadata[] Members { get; private set; }
        #endregion [Properties]


        #region [ISerializableTypeMetadata<TValue> memebrs]
        /// <summary>
        /// Gets a size of type in bytes
        /// </summary>
        public int Capacity { get; private set; }
        /// <summary>
        /// Gets a type that a metadata is assosiated with
        /// </summary>
        public Type ValueType
        {
            get { return typeof(TValue); }
        }
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        ISerializer ISerializableTypeMetadata.Serializer
        {
            get { return Serializer; }
        }
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        public ISerializer<TValue> Serializer { get; private set; } 
        #endregion [ISerializableTypeMetadata<TValue> memebrs]
    }
}