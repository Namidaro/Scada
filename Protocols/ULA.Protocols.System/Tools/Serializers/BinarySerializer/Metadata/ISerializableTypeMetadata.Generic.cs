using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes data metadata structure to serializable type
    /// </summary>
    internal interface ISerializableTypeMetadata<TValue> : ISerializableTypeMetadata
    {
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        new ISerializer<TValue> Serializer { get; }
    }
}