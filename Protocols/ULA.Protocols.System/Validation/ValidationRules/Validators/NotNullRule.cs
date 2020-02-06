using System;
using System.Linq.Expressions;
using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.Validators
{
    /// <summary>
    /// Represents a not null validation rule
    /// </summary>
    /// <typeparam name="TProperty">The type of property to validate</typeparam>
    public class NotNullRule<TProperty> : SpecificationBase<TProperty> where TProperty : class
    {
        #region [Private fields]
        private readonly Expression<Func<TProperty, bool>> _predicate; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="NotNullRule{TProperty}"/> validation rule
        /// </summary>
        public NotNullRule()
        {
            this._predicate = prop => prop != null;
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal override Expression<Func<TProperty, bool>> Predicate
        {
            get { return this._predicate; }
        } 
        #endregion [Override members]
    }
}