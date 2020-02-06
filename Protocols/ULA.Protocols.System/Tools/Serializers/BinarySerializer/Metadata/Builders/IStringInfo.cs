using System.Text;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Exposes a string information
    /// </summary>
    public interface IStringInfo
    {
        /// <summary>
        /// Gets an encoding of a string
        /// </summary>
        Encoding Encoding { get; }
        /// <summary>
        /// Gets a length of a string
        /// </summary>
        int Length { get; }
    }
}