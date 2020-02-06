using System;
using System.Linq;
using System.Runtime.Serialization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents an entity in AOM notation
    ///     Сущность в AOM нотации
    /// </summary>
    [DataContract]
    public class AomEntity
    {
        #region [Const]

        private const string PROPERTY_TYPES_COLLECTION_NAME = "properties";
        private const string ENTITY_TYPES_NAME = "type";

        #endregion
        
        #region [Private fields]

        [DataMember(Name = PROPERTY_TYPES_COLLECTION_NAME)]
        private AomPropertyCollection _collection;
        [DataMember(Name = ENTITY_TYPES_NAME)]
        private AomEntityType _type;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="AomEntityType" /> that represents the type of current instance
        ///     Тип AOM сущности
        /// </summary>
        public AomEntityType EntityTypeOf
        {
            get { return this._type; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="AomPropertyCollection" /> that represents a collection of properties that this entity has
        ///     коллекция свойств AOM сущности
        /// </summary>
        public AomPropertyCollection Properties
        {
            get { return this._collection; }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomEntity" />. It is used ONLY for deserialization purpose.
        /// </summary>
        private AomEntity()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomEntity" />.
        /// </summary>
        /// <param name="type">
        ///     An instance of <see cref="AomEntityType" /> that represetns the type of current entity
        /// </param>
        public AomEntity(AomEntityType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this._type = type;
            this._collection =
                new AomPropertyCollection(type.Properties.ToDictionary(k => k.Key, v => new AomProperty(v.Value)));
        }

        #endregion [Ctor's]
    }
}