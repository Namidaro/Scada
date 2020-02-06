using System;
using System.Reflection;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata
{
    /// <summary>
    /// Represents field information as member
    /// </summary>
    internal abstract class FieldInfoMember : IMemberInfo
    {
        #region [Private fields]
        protected readonly FieldInfo _fieldInfo;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="FieldInfoMember"/>
        /// 
        /// 
        /// <exception cref="NullReferenceException"></exception>
        /// 
        /// </summary>
        /// <param name="fieldInfo">The field information to use</param>
        protected FieldInfoMember(FieldInfo fieldInfo)
        {
            Guard.AgainstNullReference(fieldInfo, "fieldInfo");
            this._fieldInfo = fieldInfo;
        } 
        #endregion [Ctor's]


        #region [IMemberInfo members]
        /// <summary>
        /// Gets the type of a member
        /// </summary>
        /// <returns>The type of member</returns>
        public Type GetMemberType()
        {
            return this._fieldInfo.FieldType;
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
        public object GetValue(object valueAccessor)
        {
            //Guard.AgainstNullReference(valueAccessor, "valueAccessor");
            return this._fieldInfo.GetValue(valueAccessor);
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
        public void SetValue(object valueAccessor, object value)
        {
            //Guard.AgainstNullReference(valueAccessor, "valueAccessor");
            //Guard.AgainstNullReference(value, "value");
            this._fieldInfo.SetValue(valueAccessor, value);
        }

        /// <summary>
        /// Gets the information of type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type of information to get</typeparam>
        /// <returns>Resulting information</returns>
        public abstract T GetInfo<T>() where T : class;
        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        public abstract object GetInfo(Type infoType);
        #endregion [IMemberInfo members]
    }
}