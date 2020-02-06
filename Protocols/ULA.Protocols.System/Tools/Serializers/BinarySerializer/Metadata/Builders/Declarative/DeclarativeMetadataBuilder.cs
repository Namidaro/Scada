using System;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents a common engine for building metadata declaratively
    /// </summary>
    internal class DeclarativeMetadataBuilder : DeclarativeMetadataBuilderBase
    {
        #region [DeclarativeMetadataBuilderBase members]
        /// <summary>
        /// Builds metadata for a type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Built metadata</returns>
        public override ISerializableTypeMetadata Build()
        {
            var builder = this.GetMetadataBuilder(this._infoAccessor);
            return builder.Build();
        }
        /// <summary>
        /// Creates a metadata bulder for embedded custom type (the factory method pattern)
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">The type to create metadata builder for</param>
        /// <returns>The current metadata builder for the input type</returns>
        protected override IMetadataBuilder CreateTypeMetadataBuilder(Type type)
        {
            var builder = new DeclarativeCustomTypeMetadataBuilder();
            builder.SetTypeToBuild(type);
            return builder;
        } 
        #endregion [DeclarativeMetadataBuilderBase members]
    }
}