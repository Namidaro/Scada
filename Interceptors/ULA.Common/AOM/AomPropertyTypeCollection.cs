using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ULA.Localization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents a hash map for <see cref="AomPropertyType" /> in AOM notation
    ///     Представляет Hashmap для <see cref="AomPropertyType" /> в AOM нотации
    /// </summary>
    [CollectionDataContract
        (Name = ENTITY_NAME,
        ItemName = PROPERTY_TYPE_ITEM_NAME,
        KeyName = PROPERTY_NAME_KEY_NAME,
        ValueName = PROPERTY_TYPE_METADATA_VALUE_NAME)]
    public class AomPropertyTypeCollection : Dictionary<string, AomPropertyType>
    {
        #region [Const]
        private const string ENTITY_NAME = "entityPropertiesType";
        private const string PROPERTY_TYPE_ITEM_NAME = "propertyType";
        private const string PROPERTY_NAME_KEY_NAME = "propertyName";
        private const string PROPERTY_TYPE_METADATA_VALUE_NAME = "propertyTypeMetadata";
        #endregion

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyTypeCollection" />. It is used ONLY for deserialization purpose
        /// </summary>
        // ReSharper disable UnusedMember.Local
        private AomPropertyTypeCollection()
            // ReSharper restore UnusedMember.Local
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyTypeCollection" />
        /// </summary>
        /// <param name="collection">
        ///     An collection of <see cref="AomPropertyType" />
        /// </param>
        public AomPropertyTypeCollection(IEnumerable<AomPropertyType> collection)
            : base(AomPropertyTypeCollection.InitializeFromCollection(collection))
        {
        }

        #endregion [Ctor's]

        #region [Help members]

        private static IDictionary<string, AomPropertyType> InitializeFromCollection(
            IEnumerable<AomPropertyType> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection",
                                                LocalizationResources.Instance.AomPropertyNullCollectionExceptionMessage);
            }
            return collection.ToDictionary(k => k.Name, v => v);
        }

        #endregion [Help members]
    }
}