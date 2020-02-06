using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.Validators
{
    /// <summary>
    /// Represents a validation rule that is based on regex
    /// </summary>
    public class RegExRule : SpecificationBase<string>
    {
        #region [Private fields]
        private readonly Expression<Func<string, bool>> _predicate;
        private readonly Regex _regEx; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="RegExRule"/>
        /// </summary>
        /// <param name="pattern">The pattern to use in regex functionality</param>
        public RegExRule(string pattern)
        {
            this._regEx = new Regex(pattern, RegexOptions.Compiled);
            this._predicate = prop => this._regEx.Match(prop).Success;
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