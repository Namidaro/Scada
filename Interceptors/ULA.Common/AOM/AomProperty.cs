using System;
using System.Runtime.Serialization;

namespace ULA.Common.AOM
{
    /// <summary>
    ///     Represents a property instance in AOM notation
    ///     Представляет свойство в AOM нотации
    /// </summary>
    [DataContract]
    public class AomProperty
    {
        #region [Const]

        private const string PROPERTY_TYPES_NAME = "type";
        private const string OBJECT_VALUE_NAME= "value";

        #endregion

        #region [Private fields]

        [DataMember(Name = PROPERTY_TYPES_NAME)]
        private AomPropertyType _type;
        [DataMember(Name = OBJECT_VALUE_NAME)]
        private object _value;

        #endregion [Private fields]

        #region [Properties]

        /// <summary>
        ///     Gets or sets the value of the property
        ///     Значение 
        /// </summary>
        public object Value
        {
            get { return this._value; }
            set
            {
                this.SetCurrentValue(value);
                
            }
        }

        /// <summary>
        ///     Gets the type of the property
        ///     Тип
        /// </summary>
        public AomPropertyType TypeOf
        {
            get { return this._type; }
        }

        #endregion [Properties]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AomProperty" />. It is used ONLY for deserialization purpose.
        /// </summary>
        private AomProperty()
        {
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomProperty" />.
        /// </summary>
        /// <param name="type">
        ///     An instance of <see cref="AomPropertyType" /> that represents the type of the property
        /// </param>
        public AomProperty(AomPropertyType type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this._type = type;
        }

        /// <summary>
        ///     Creates an instance of <see cref="AomProperty" />
        ///     
        /// </summary>
        /// <param name="type">
        ///     An instance of <see cref="AomPropertyType" /> that represents the type of the property
        /// </param>
        /// <param name="value">The value of the property</param>
        public AomProperty(AomPropertyType type, object value)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            this._type = type;
            this.SetCurrentValue(value);
        }

        #endregion [Ctor's]

        #region [Help members]

        private void SetCurrentValue(object value)
        {
            if (this._value == null)
                this._value = value;
            else
            {
                if( (this._value.GetType() == this._type.TypeOf)||(_value.GetType().GetInterface(_type.TypeOf.ToString())!=null))

                    this._value = value;
                else

                    throw new ArgumentException();
            }
        }

        #endregion [Help members]
    }
}