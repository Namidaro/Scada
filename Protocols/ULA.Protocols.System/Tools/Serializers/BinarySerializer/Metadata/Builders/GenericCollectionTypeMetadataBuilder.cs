using System;
using System.Linq;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Serializers;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders
{
    /// <summary>
    /// Represents a generic collection type metadata builder
    /// </summary>
    public class GenericCollectionTypeMetadataBuilder : IMetadataBuilder
    {
        #region [Private fields]
        private ICollectionInfo _collectionInfo;
        private readonly IMetadataBuilder _metadataBuilder;
        private int _length;
        private Type _elementType; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="GenericCollectionTypeMetadataBuilder"/>
        /// </summary>
        /// <param name="metadataBuilder"></param>
        public GenericCollectionTypeMetadataBuilder(IMetadataBuilder metadataBuilder)
        {
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
        /// <param name="type"></param>
        public void SetTypeToBuild(Type type)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(type, "type", "Can't create enumeration metadata builder for null-type");
            var elementType = type.GetGenericArguments().FirstOrDefault();
            Guard.AgainstNullReference<MetadataBuilderException>(elementType, "Ups something went wrong. Can't get enumeration element type");
            this.TypeToBuild = type;
            this._elementType = elementType;
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
            Guard.AgainstNullReference<MetadataBuilderException>(informationAccessor, "informationAccessor", "Can't get collection information");
            var collectionInformation = informationAccessor(typeof(ICollectionInfo));
            Guard.AgainstNullReference<MetadataBuilderException>(collectionInformation, "Can't get collection information");
            this._collectionInfo = (ICollectionInfo)collectionInformation;
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
            var serializer = (ISerializer)Activator.CreateInstance(typeof(CollectionSerializer<>).MakeGenericType(this._elementType),
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