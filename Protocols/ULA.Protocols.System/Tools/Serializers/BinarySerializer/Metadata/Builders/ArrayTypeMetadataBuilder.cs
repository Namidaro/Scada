using System;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a <see cref="Array"/> type metadata builder
    /// </summary>
    public class ArrayTypeMetadataBuilder : IMetadataBuilder
    {
        #region [Private fields]
        private ICollectionInfo _collectionInfo;
        private readonly IMetadataBuilder _metadataBuilder;
        private int _length;
        private Type _elementType; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ArrayTypeMetadataBuilder"/>
        /// </summary>
        /// <param name="metadataBuilder">A metadata builder to use for elements of an array</param>
        public ArrayTypeMetadataBuilder(IMetadataBuilder metadataBuilder)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(metadataBuilder, "metadataBuilder", "Can't create enumeration metadata builder with null metadata builder");
            this._metadataBuilder = metadataBuilder;
        } 
        #endregion [Ctor's]


        #region [IMetadataBuilder members]
        /// <summary>
        /// Sets a type to build metadata for
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="type">A type to build metadata for</param>
        public void SetTypeToBuild(Type type)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(type, "type", "Can't create enumeration metadata builder for null-type");
            this.TypeToBuild = type;
            var elementType = type.GetElementType();
            Guard.AgainstNullReference<MetadataBuilderException>(elementType, "Ups something went wrong. Can't get enumeration element type");
            this._elementType = type.GetElementType();
        }
        /// <summary>
        /// Gets a type information accessor
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="informationAccessor">The instance of information  accessor</param>
        public void SetInfoAccessor(Func<Type, object> informationAccessor)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(informationAccessor, "collectionInfo", "Can't get collection information");
            var collectionInfo = informationAccessor(typeof (ICollectionInfo));
            Guard.AgainstNullReference<MetadataBuilderException>(collectionInfo, "Can't get collection information");
            this._collectionInfo = (ICollectionInfo)collectionInfo;
            this._length = this._collectionInfo.Length;
        }
        /// <summary>
        /// Builds metadata for a type
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <returns>Built metadata</returns>
        public ISerializableTypeMetadata Build()
        {
            this._metadataBuilder.SetTypeToBuild(this._elementType);
            this._metadataBuilder.SetInfoAccessor(this._collectionInfo.GetInfo);
            var elementMetadata = this._metadataBuilder.Build();
            var serializer = (ISerializer)Activator.CreateInstance(typeof(ArraySerializer<>).MakeGenericType(this._elementType),
                                         new object[] { elementMetadata, this._length });
            return new SerializableTypeMetadata(serializer, this.TypeToBuild, elementMetadata.Capacity * this._length);
        }
        /// <summary>
        /// Gets a type to build metadata for
        /// </summary>
        public Type TypeToBuild { get; private set; } 
        #endregion [IMetadataBuilder members]
    }
}