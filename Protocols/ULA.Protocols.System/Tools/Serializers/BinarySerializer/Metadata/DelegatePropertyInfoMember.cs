using System;
using System.Reflection;
using YP.Toolkit.System.Validation;
using YP.Toolkit.System.Tools.StrongTypedReflection;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents a base property information obtainer mechanism that is based on delegate access to a field
    /// </summary>
    /// <typeparam name="TAccessor"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class DelegatePropertyInfoMember<TAccessor, TValue> : IMemberInfo<TValue>
    {
        #region [Private fields]
        private readonly Func<TAccessor, TValue> _getter;
        private readonly Action<TAccessor, TValue> _setter;
        #endregion [Private fields]


        #region [Ctor's]
        protected DelegatePropertyInfoMember(PropertyInfo propertyInformation)
        {
            Guard.AgainstNullReference(propertyInformation, "propertyInformation");

            this._getter = TypeMembersHelper.CreatePropertyGetter<TAccessor, TValue>(propertyInformation);
            this._setter = TypeMembersHelper.CreatePropertySetter<TAccessor, TValue>(propertyInformation);
        }
        #endregion [Ctor's]


        #region [IMemberInfo members]
        /// <summary>
        /// Gets the type of a member
        /// </summary>
        /// <returns>The type of member</returns>
        public Type GetMemberType()
        {
            return typeof(TValue);
        }
        /// <summary>
        /// Gets the value of a member
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        public TValue GetValue(object valueAccessor)
        {
            return this._getter((TAccessor)valueAccessor);
        }
        /// <summary>
        /// Sets the value to an accessor
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to set value to</param>
        /// <param name="value">The value to set to accessor</param>
        public void SetValue(object valueAccessor, TValue value)
        {
            this._setter((TAccessor)valueAccessor, value);
        }
        /// <summary>
        /// Gets the value of a member
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to get value from</param>
        /// <returns>The value accepted from the value accessor</returns>
        object IMemberInfo.GetValue(object valueAccessor)
        {
            return this.GetValue(valueAccessor);
        }
        /// <summary>
        /// Sets the value to an accessor
        /// 
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// 
        /// </summary>
        /// <param name="valueAccessor">The accessor to set value to</param>
        /// <param name="value">The value to set to accessor</param>
        void IMemberInfo.SetValue(object valueAccessor, object value)
        {
            this.SetValue(valueAccessor, (TValue)value);
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
        #endregion [IMemberInfo members]
    }
}