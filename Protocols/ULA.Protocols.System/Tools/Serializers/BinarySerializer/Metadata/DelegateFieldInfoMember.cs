using System;
using System.Reflection;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;
using YP.Toolkit.System.Tools.StrongTypedReflection;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents a base field information obtainer mechansm that is based on delegate access to a field
    /// </summary>
    /// <typeparam name="TAccessor"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class DelegateFieldInfoMember<TAccessor, TValue> : IMemberInfo<TValue>
    {
        #region [Private fields]
        private readonly Func<TAccessor, TValue> _getter;
        private readonly Action<TAccessor, TValue> _setter;
        private readonly Type _memberType; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="DelegateFieldInfoMember{TAccessor,TValue}"/>
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="fieldInformation">The instance that represents a field information via .Net reflection mechanism</param>
        protected DelegateFieldInfoMember(FieldInfo fieldInformation)
        {
            Guard.AgainstNullReference<MetadataBuilderException>(fieldInformation, "fieldInformation");

            this._getter = TypeMembersHelper.CreateFieldGetter<TAccessor, TValue>(fieldInformation);
            this._setter = TypeMembersHelper.CreateFieldSetter<TAccessor, TValue>(fieldInformation);
            this._memberType = fieldInformation.FieldType;
        } 
        #endregion [Ctor's]


        #region [IMemberInfo members]
        /// <summary>
        /// Gets the type of a member
        /// </summary>
        /// <returns>The type of member</returns>
        public Type GetMemberType()
        {
            return this._memberType;
        }
        /// <summary>
        /// Gets the value of a member
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        public TValue GetValue(object valueAccessor)
        {
            return this._getter((TAccessor)valueAccessor);
        }
        /// <summary>
        /// Sets the value to an accessor
        /// </summary>
        /// <param name="valueAccessor">The accessor to set value to</param>
        /// <param name="value">The value to set to accessor</param>
        public void SetValue(object valueAccessor, TValue value)
        {
            this._setter((TAccessor)valueAccessor, value);
        }
        /// <summary>
        /// Gets the information of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type of information to get</typeparam>
        /// <returns>Resulting information</returns>
        public T GetInfo<T>() where T : class
        {
            return this.GetInfo(typeof(T)) as T;
        }
        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        public abstract object GetInfo(Type infoType);
        /// <summary>
        /// Gets the value of a member
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        object IMemberInfo.GetValue(object valueAccessor)
        {
            return this.GetValue(valueAccessor);
        }
        /// <summary>
        /// Sets the value to an accessor
        /// </summary>
        /// <param name="valueAccessor">The accessor to set value to</param>
        /// <param name="value">The value to set to accessor</param>
        void IMemberInfo.SetValue(object valueAccessor, object value)
        {
            this.SetValue(valueAccessor, (TValue)value);
        }
        #endregion [IMemberInfo members]
    }
}