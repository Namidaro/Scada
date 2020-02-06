using System;
using System.Runtime.Serialization;
using ULA.Localization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents an entity that describes property in AOM notation
    ///     Представляет сущность типа которая описывает свойство в AOM нотации
    /// </summary>
    [DataContract]
    public class AomPropertyType
    {
        #region [Const]

        private const string FULL_TYPES_NAME = "type";
        private const string ENTITY_NAME = "name";

        #endregion
        #region [Private fields]

        [DataMember(Name = FULL_TYPES_NAME)]
        private string _fullTypeName;
        [DataMember(Name = ENTITY_NAME)]
        private string _name;
        private Type _type;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets the name of a property that this entity describes
        ///     Имя свойства(типа) описывающего сущность
        /// </summary>
        public string Name
        {
            get { return this._name; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="Type" /> that represents the type of the described property
        ///     Тип описывающий свойство
        /// </summary>
        public Type TypeOf
        {
            get { return this._type ?? (this._type = Type.GetType(this._fullTypeName)); }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyType" />. It is used ONLY for deserialization purpose
        /// </summary>
        private AomPropertyType()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomPropertyType" />
        /// </summary>
        /// <param name="name">The name of a property that this entity describes</param>
        /// <param name="propertyValueType">
        ///     An instance of <see cref="Type" /> that represents the type of the described property
        /// </param>
        public AomPropertyType(string name, Type propertyValueType)
        {
            AomPropertyType.ValidateConstructorInputs(name, propertyValueType);

            this._name = name;
            this._fullTypeName = propertyValueType.AssemblyQualifiedName;
            this._type = propertyValueType;
        }

        #endregion [Ctor's]

        #region [Help members]

        private static void ValidateConstructorInputs(string name, Type propertyValueType)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(
                    LocalizationResources.Instance.AomPropertyTypePropertyNameExceptionMessage, "name");
            }
            if (propertyValueType == null)
            {
                throw new ArgumentNullException(
                    "propertyValueType", LocalizationResources.Instance.AomPropertyTypeNullPropertyTypeExceptionMessage);
            }
        }

        #endregion [Help members]
    }
}