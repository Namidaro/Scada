using System;
using System.Linq;
using System.Reflection;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents a declarative field information obtainer
    /// </summary>
    /// <typeparam name="TAccessor">The type of the instance that provides the access to a field</typeparam>
    /// <typeparam name="TValue">The type of a field</typeparam>
    internal class DeclarativeFieldInfoMember<TAccessor, TValue> : DelegateFieldInfoMember<TAccessor, TValue>
    {
        #region [Private fields]
        private readonly FieldInfo _fieldInfo; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="DeclarativeFieldInfoMember{TAccessor,TValue}"/>
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="fieldInfo">The field information to use</param>
        public DeclarativeFieldInfoMember(FieldInfo fieldInfo)
            : base(fieldInfo)
        {
            this._fieldInfo = fieldInfo;
        }
        #endregion [Ctor's]


        #region [FieldInfoMember members]
        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        public override object GetInfo(Type infoType)
        {
            if (infoType == null) return null;
            var attribute = this._fieldInfo.GetCustomAttributes(infoType, false).FirstOrDefault();
            return attribute;
        }
        #endregion [FieldInfoMember members]
    }
}
