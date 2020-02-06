using YP.Toolkit.System.Tools.Patterns.Specification;
using YP.Toolkit.System.Validation.ValidationRules.InternalRules;

namespace YP.Toolkit.System.Validation.ValidationRules.Builders
{
    /// <summary>
    /// Represents a simple validation rule builder
    /// </summary>
    /// <typeparam name="TEntity">The type of entity which property is going to be validated</typeparam>
    /// <typeparam name="TProperty">The property of <see cref="TEntity"/> to validate</typeparam>
    internal class RuleBuilder<TEntity, TProperty> : 
        IRuleBuilder<TEntity, TProperty>
    {
        #region [Private fields]
        private readonly PropertyRule<TEntity, TProperty> _propertyRule;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="RuleBuilder{TEntity,TProperty}"/>
        /// </summary>
        /// <param name="propertyRule">A property validation rule to build</param>
        public RuleBuilder(PropertyRule<TEntity, TProperty> propertyRule)
        {
            this._propertyRule = propertyRule;
        } 
        #endregion [Ctor's]


        #region [IEndRuleBuilder<TEntity, TProperty> members]
        /// <summary>
        /// Aggregates a validation rule into a sequence of rules that are joined with 'And' operation
        /// </summary>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that begins validation rule builder functionality</returns>
        public IRuleBuilder<TEntity, TProperty> And()
        {
            this._propertyRule.AggregateAnd();
            return this;
        }
        /// <summary>
        /// Sets a message that will be associated with a faild validation 
        /// </summary>
        /// <param name="message">The message to associate with failed validation rule result</param>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that begins validation rule builder functionality</returns>
        public IRuleBuilder<TEntity, TProperty> WithMessage(string message)
        {
            this._propertyRule.SetMessage(message);
            return this;
        }
        /// <summary>
        /// Sets a custom validation specification
        /// </summary>
        /// <param name="validationRule">A specification to use as validation rule</param>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that is used to mark the validation rule as closing</returns>
        public IRuleBuilder<TEntity, TProperty> Custom(SpecificationBase<TProperty> validationRule)
        {
            this._propertyRule.SetValidator(validationRule);
            return this;
        } 
        #endregion [IEndRuleBuilder<TEntity, TProperty> members]
    }
}
