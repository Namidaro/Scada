using System;
using System.Runtime.Serialization;
using ULA.Localization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents an entity type in AOM notation
    ///     Представляет тип AOM сущности в AOM нотации
    /// </summary>
    [DataContract]
    public class AomEntityType
    {
        #region [Const]

        private const string PROPERTY_TYPES_COLLECTION_NAME = "propertyTypes";
        private const string TYPE_FULL_NAME = "type";

        #endregion

        #region [Private fields]

        [DataMember(Name = PROPERTY_TYPES_COLLECTION_NAME)]
        private AomPropertyTypeCollection _collection;
        private Type _type;
        [DataMember(Name = TYPE_FULL_NAME)]
        private string _typeFullName;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets the name of current entity type
        ///     Имя типа
        /// </summary>
        public string Name
        {
            get { return this.TypeOf.FullName; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="Type" /> that represents the type of current entity
        ///     Тип сущности
        /// </summary>
        public Type TypeOf
        {
            get { return this._type ?? (this._type = Type.GetType(this._typeFullName)); }
        }

        /// <summary>
        ///     Gets an instance of <see cref="AomPropertyTypeCollection" /> that represents a collection of properties for current entity type
        ///     Свойства типа сущности
        /// </summary>
        public AomPropertyTypeCollection Properties
        {
            get { return this._collection; }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomEntityType" />. It is used ONLY for deserialization purpose.
        /// </summary>
        private AomEntityType()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomEntityType" />
        /// </summary>
        /// <param name="type">
        ///     An instance of <see cref="Type" /> that represents current entity
        /// </param>
        /// <param name="properties">
        ///     An instance of <see cref="AomPropertyTypeCollection" /> that represetns a collection of properties that this entity has
        /// </param>
        public AomEntityType(Type type, AomPropertyTypeCollection properties)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type",
                                                LocalizationResources.Instance.AomEntityTypeNullTypeExceptionMessage);
            }

            this._type = type;
            this._typeFullName = type.AssemblyQualifiedName;
            this._collection = properties ?? new AomPropertyTypeCollection(new AomPropertyType[0]);
        }

        #endregion [Ctor's]
    }
}