using System.Text;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Exposes character information
    /// </summary>
    public interface ICharInfo
    {
        /// <summary>
        /// An encoding of a character
        /// </summary>
        Encoding Encoding { get; }
    }
}