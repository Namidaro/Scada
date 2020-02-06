using System;
using System.Linq;
using System.Reflection;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents a declarative property information obtainer
    /// </summary>
    /// <typeparam name="TAccessor">The type of the instance that provides the access to a property</typeparam>
    /// <typeparam name="TValue">The type of a property</typeparam>
    internal class DeclarativePropertyInfoMember<TAccessor, TValue> : DelegatePropertyInfoMember<TAccessor, TValue>
    {
        #region [Private fields]

        private readonly PropertyInfo _propertyInfo;

        #endregion [Private fields]


        #region [Ctor's]

        /// <summary>
        /// Creates an instance of <see cref="DeclarativePropertyInfoMember{TAccessor,TValue}"/>
        /// 
        /// 
        /// <exception cref="MetadataBuilderException"></exception>
        /// 
        /// </summary>
        /// <param name="propertyInfo">The property information to use</param>
        public DeclarativePropertyInfoMember(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            this._propertyInfo = propertyInfo;
        }

        #endregion [Ctor's]


        #region [PropertyInfoMember members]

        /// <summary>
        /// Gets the information of member
        /// </summary>
        /// <param name="infoType">The type of information to get</param>
        /// <returns>Resulting information</returns>
        public override object GetInfo(Type infoType)
        {
            if (infoType == null) return null;
            var attribute = this._propertyInfo.GetCustomAttributes(infoType, false).FirstOrDefault();
            return attribute;
        }

        #endregion [PropertyInfoMember members]
    }
}
