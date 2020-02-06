using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Helpers;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata;
using YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Configuration
{
    /// <summary>
    /// Represents a basic binary serialization configuration functionality
    /// </summary>
    public abstract class BinarySerializationConfiguration
    {
        #region [Constants]
        private const string NULL_METADATA_BUILDER_MESSAGE = "The metadata data builder can't be null while registration metadata";
        private const string PRIMITIVE_TYPE_MESSAGE = "Can't registrate metadata for primitive or enumerable of primitives types";
        private const string METADATA_CONFLICT_MESSAGE_PATTERN = "The type {0} has already been registered";
        #endregion [Constants]


        #region [Private fields]
        protected IDictionary<Type, ISerializableTypeMetadata> _collection;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="BinarySerializationConfiguration"/>
        /// </summary>
        protected BinarySerializationConfiguration()
            : this(new ConcurrentDictionary<Type, ISerializableTypeMetadata>())
        { }
        /// <summary>
        /// Creates an instance of <see cref="BinarySerializationConfiguration"/>
        /// </summary>
        /// <param name="collection">The collection to store registered metadata</param>
        protected BinarySerializationConfiguration(IDictionary<Type, ISerializableTypeMetadata> collection)
        {
            this._collection = collection;
        }
        #endregion [Ctor's]


        #region [IConfiguration members]
        /// <summary>
        /// Registeres metadata with specific <see cref="metadataBuilder"/>
        /// 
        /// 
        /// <exception cref="MetadataRegistrationException"></exception>
        /// <exception cref="MetadataAlreadyExistsException"></exception>
        /// 
        /// </summary>
        /// <param name="metadataBuilder">The metadata builder</param>
        protected internal void RegisterMetadata([NotNull] IMetadataBuilder metadataBuilder)
        {
            Guard.AgainstNullReference<MetadataRegistrationException>(metadataBuilder, "metadataBuilder",
                                                                     NULL_METADATA_BUILDER_MESSAGE);

            var metadataType = metadataBuilder.TypeToBuild;

            if (TypeHelper.IsPrimitiveType(metadataType))
                Guard.Throw<MetadataRegistrationException>(PRIMITIVE_TYPE_MESSAGE);

            if (this._collection.ContainsKey(metadataBuilder.TypeToBuild))
                Guard.Throw<MetadataAlreadyExistsException>(string.Format(METADATA_CONFLICT_MESSAGE_PATTERN, metadataBuilder.TypeToBuild));

            this._collection.Add(metadataType, metadataBuilder.Build());
        }
        #endregion [IConfiguration members]


        #region [Help members]
        /// <summary>
        /// Gets a collection of metadata mapping data
        /// </summary>
        /// <returns>The collection of metadata mapping data</returns>
        protected internal IEnumerable<KeyValuePair<Type, ISerializableTypeMetadata>> GetConfiguratedMetadata()
        {
            return this._collection.ToDictionary(k => k.Key, v => v.Value);
        } 
        #endregion [Help members]
    }
}