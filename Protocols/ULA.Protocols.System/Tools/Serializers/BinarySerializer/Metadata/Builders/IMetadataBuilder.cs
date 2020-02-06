using System;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Exposes metadata builder functionality
    /// </summary>
    public interface IMetadataBuilder
    {
        /// <summary>
        /// Sets a type to build metadata for
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">A type to build metadata for</param>
        void SetTypeToBuild(Type type);
        /// <summary>
        /// Gets a type information accessor
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="informationAccessor">The instance of information  accessor</param>
        void SetInfoAccessor(Func<Type, object> informationAccessor);
        /// <summary>
        /// Builds metadata for a type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Built metadata</returns>
        ISerializableTypeMetadata Build();
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        Type TypeToBuild { get; }
    }
}