using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes data metadata structure to serializable type
    /// </summary>
    public interface ISerializableTypeMetadata : IMetadata
    {
        /// <summary>
        /// Gets a serializer for a member to serialize
        /// </summary>
        ISerializer Serializer { get; }
    }
}