using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    internal class SerializableTypeMetadata<TValue> : SerializableTypeMetadata, ISerializableTypeMetadata<TValue>
    {
        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="SerializableTypeMetadata{TValue}"/>
        /// </summary>
        /// <param name="serializer">The serializer to use</param>
        /// <param name="capacity">Capacity of type</param>
        public SerializableTypeMetadata(ISerializer<TValue> serializer, int capacity)
            : base(serializer, typeof(TValue), capacity)
        { }
        #endregion [Ctor's]

        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        public new ISerializer<TValue> Serializer
        {
            get { return (ISerializer<TValue>)base.Serializer; }
        }
    }
}