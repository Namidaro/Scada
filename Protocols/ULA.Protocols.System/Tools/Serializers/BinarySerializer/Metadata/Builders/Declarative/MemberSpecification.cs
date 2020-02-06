using System;
using System.Linq.Expressions;
using System.Reflection;
using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Tools.Serializers.BinarySerializer.Metadata.Builders.Declarative
{
    /// <summary>
    /// Represents a specification for checking whether the member could be binary serializable
    /// </summary>
    internal class MemberSpecification : SpecificationBase<MemberInfo>
    {
        #region [Private fields]
        private readonly Expression<Func<MemberInfo, bool>> _predicate; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="MemberSpecification"/>
        /// </summary>
        public MemberSpecification()
        {
            this._predicate = item =>
                Attribute.IsDefined(item, typeof (BinarySerializableMemberAttribute)) ||
                Attribute.IsDefined(item, typeof (BinarySkipMemberAttribute));
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal override Expression<Func<MemberInfo, bool>> Predicate
        {
            get { return this._predicate; }
        } 
        #endregion [Override members]
    }
}