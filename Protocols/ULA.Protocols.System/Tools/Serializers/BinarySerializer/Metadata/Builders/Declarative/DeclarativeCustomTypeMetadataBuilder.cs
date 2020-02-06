using System;
using System.Linq;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents a declarative custom type metadata builder. 
    /// It can process ony referenced types for now
    /// </summary>
    public class DeclarativeCustomTypeMetadataBuilder : DeclarativeMetadataBuilderBase
    {
        #region [Constants]
        private const string BAD_CUSTOM_TYPE_MESSAGE = "Only custom referenced types are currently supported"; 
        #endregion [Constants]


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
            //TODO: For now we don't provide the ability to work with custom structure types
            Guard.AgainstIsFalse<MetadataBuilderException>(this._type.IsClass /*|| this._type.IsValueType*/, BAD_CUSTOM_TYPE_MESSAGE);

            return this.BuildMetadataForObjectType(this._type);
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
            var newMetadataBuilder = new DeclarativeCustomTypeMetadataBuilder();
            newMetadataBuilder.SetTypeToBuild(type);
            return newMetadataBuilder;
        }
        #endregion [DeclarativeMetadataBuilderBase members]


        #region [Help members]
        /// <summary>
        /// Builds metadata for referenced type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">The type to build metadata for</param>
        /// <returns>Built metadata</returns>
        protected virtual ISerializableTypeMetadata BuildMetadataForObjectType(Type type)
        {
            Guard.AgainstIsFalse<MetadataBuilderException>(
                Attribute.IsDefined(type, typeof(BinarySerializableAttribute)), NOT_MARKED_TYPE_MESAGE, new NotSupportedException());
            Guard.AgainstIsTrue<MetadataBuilderException>(type.IsAbstract, ABSTRACT_TYPE_MESSAGE, new NotSupportedException());
            Guard.AgainstNullReference<MetadataBuilderException>(
                type.GetConstructor(Type.EmptyTypes), NO_DEFAULT_CONSTRUCTOR_MESSAGE, new NotSupportedException());

            var members = this.CreateMemberCollection(type);
            var resultedCapacity = members.Sum(s => s.Capacity);

            return (ISerializableTypeMetadata)Activator.CreateInstance(typeof(ReferencedTypeMetadata<>).MakeGenericType(type),
                                         new object[] { members, resultedCapacity });
        } 
        #endregion [Help members]
    }
}