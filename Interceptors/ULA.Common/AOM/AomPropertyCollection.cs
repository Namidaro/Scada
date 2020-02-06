using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents a collection of properties in AOM notation
    ///     Представляет коллекцию свойств в AOM нотации
    /// </summary>
    [CollectionDataContract
        (Name = PROPERTIES_NAME,
        ItemName = PROPERTY_ITEM_NAME,
        KeyName = PROPERTY_NAME_KEY_NAME,
        ValueName = PROPERTY_VALUE_VALUE_NAME)]
    public class AomPropertyCollection : Dictionary<string, AomProperty>
    {
        #region [Const]
        private const string PROPERTIES_NAME = "properties";
        private const string PROPERTY_ITEM_NAME = "property";
        private const string PROPERTY_NAME_KEY_NAME = "propertyName";
        private const string PROPERTY_VALUE_VALUE_NAME = "propertyValue";
        #endregion

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyCollection" />. It is used ONLY for deserialization purpose.
        /// </summary>
        // ReSharper disable UnusedMember.Local
        private AomPropertyCollection()
            // ReSharper restore UnusedMember.Local
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyCollection" />
        /// </summary>
        /// <param name="collection">
        ///     A map for <see cref="AomProperty" />
        /// </param>
        public AomPropertyCollection(IDictionary<string, AomProperty> collection)
            : base(collection)
        {
        }

        #endregion [Ctor's]
    }
}