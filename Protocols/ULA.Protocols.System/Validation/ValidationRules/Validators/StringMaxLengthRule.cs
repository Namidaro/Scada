using System;
using System.Linq.Expressions;
using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.Validators
{
    /// <summary>
    /// Represents a string maximum length validation rule
    /// </summary>
    public class StringMaxLengthRule : SpecificationBase<string>
    {
        #region [Private fields]
        private readonly Expression<Func<string, bool>> _predicate; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="StringMaxLengthRule"/>
        /// </summary>
        /// <param name="maxLength">The maximum length of a string to validate</param>
        public StringMaxLengthRule(int maxLength)
        {
            this._predicate = prop => !(prop == null || prop.Length > maxLength);
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal override Expression<Func<string, bool>> Predicate
        {
            get
            {
                return this._predicate;
            }
        } 
        #endregion [Override members]
    }
}