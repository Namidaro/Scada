using System;
using System.Linq.Expressions;
using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.Validators
{
    /// <summary>
    /// Represents a not null or empty validation rule
    /// </summary>
    public class NotNullOrNotEmptyRule : SpecificationBase<string>
    {
        #region [Private fields]
        private readonly Expression<Func<string, bool>> _predicate; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="NotNullOrNotEmptyRule"/> validation rule
        /// </summary>
        public NotNullOrNotEmptyRule()
        {
            this._predicate = prop => !string.IsNullOrEmpty(prop);
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal override Expression<Func<string, bool>> Predicate
        {
            get { return this._predicate; }
        } 
        #endregion [Override members]
    }
}