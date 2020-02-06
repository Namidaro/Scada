using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes a data information
    /// </summary>
    public interface IMetadata
    {
        /// <summary>
        /// Gets a size of type in bytes
        /// </summary>
        int Capacity { get; }
        /// <summary>
        /// Gets a type that a metadata is assosiated with
        /// </summary>
        Type ValueType { get; }
    }
}