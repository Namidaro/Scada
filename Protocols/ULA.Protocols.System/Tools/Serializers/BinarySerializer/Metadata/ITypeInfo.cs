using System;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Exposes a mechanism for obtaining a type information
    /// </summary>
    public interface ITypeInfo
    {
        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        object GetInfo(Type infoType);
    }
}