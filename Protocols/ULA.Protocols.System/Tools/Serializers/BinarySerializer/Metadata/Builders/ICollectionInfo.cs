namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Exposes a collection information
    /// </summary>
    public interface ICollectionInfo : ITypeInfo
    {
        /// <summary>
        /// Gets a length of a collection
        /// </summary>
        int Length { get; }
    }
}