using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Configuration
{
    /// <summary>
    /// Exposes binary serialization configuration functionality
    /// </summary>
    internal interface IConfiguration
    {
        /// <summary>
        /// Registeres metadata with specific <see cref="metadataBuilder"/>
        /// </summary>
        /// <param name="metadataBuilder">The metadata builder</param>
        void RegisterMetadata([NotNull] IMetadataBuilder metadataBuilder);
    }
}