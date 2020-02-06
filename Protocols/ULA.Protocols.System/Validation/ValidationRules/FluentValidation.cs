using YP.Toolkit.System.Validation.ValidationRules.Builders;
using YP.Toolkit.System.Validation.ValidationRules.Validators;

namespace YP.Toolkit.System.Validation.ValidationRules
{
    /// <summary>
    /// Represents a basic fluent validation interface
    /// </summary>
    public static class FluentValidation
    {
        /// <summary>
        /// Sets a not null validation rule
        /// </summary>
        /// <param name="ruleBuilder">The rule builder to use</param>
        /// <typeparam name="TInstance">The type of instance to connect the rule to</typeparam>
        /// <typeparam name="TProperty">The type of property to attach validation the rule to</typeparam>
        /// <returns>An instance of a validation rule builder</returns>
        public static IRuleBuilder<TInstance, TProperty> NotNull<TInstance, TProperty>(
            this IRuleBuilder<TInstance, TProperty> ruleBuilder) where TProperty : class
        {
            Guard.AgainstNullReference(ruleBuilder, "ruleBuilder");
            return ruleBuilder.Custom(new NotNullRule<TProperty>());
        }
        /// <summary>
        /// Sets a not null or not empty validation rule
        /// </summary>
        /// <param name="ruleBuilder">The rule builder to use</param>
        /// <typeparam name="TInstance">The type of instance to connect the rule to</typeparam>
        /// <returns>An instance of a validation rule builder</returns>
        public static IRuleBuilder<TInstance, string> NotNullOrNotEmpty<TInstance>(
            this IRuleBuilder<TInstance, string> ruleBuilder)
        {
            Guard.AgainstNullReference(ruleBuilder,"ruleBuilder");
            return ruleBuilder.Custom(new NotNullOrNotEmptyRule());
        }
        /// <summary>
        /// Sets a rule that depends on regex result
        /// </summary>
        /// <param name="ruleBuilder">The rule builder to use</param>
        /// <typeparam name="TInstance">The type of instance to connect the rule to</typeparam>
        /// <param name="pattern">The pattern to use in regex</param>
        /// <returns>An instance of a validation rule builder</returns>
        public static IRuleBuilder<TInstance, string> MatchRegex<TInstance>(
            this IRuleBuilder<TInstance, string> ruleBuilder, string pattern)
        {
            Guard.AgainstNullReference(ruleBuilder, "ruleBuilder");
            return ruleBuilder.Custom(new RegExRule(pattern));
        }
        /// <summary>
        /// Sets a rule taht checks for string maximum length value
        /// </summary>
        /// <typeparam name="TInstance">The type of instance to connect the rule to</typeparam>
        /// <param name="ruleBuilder">The rule builder to use</param>
        /// <param name="length">The maximum string length</param>
        /// <returns>An instance of a validation rule builder</returns>
        public static IRuleBuilder<TInstance, string> WithMaxLength<TInstance>(
            this IRuleBuilder<TInstance, string> ruleBuilder, int length)
        {
            Guard.AgainstNullReference(ruleBuilder, "ruleBuilder");
            return ruleBuilder.Custom(new StringMaxLengthRule(length));
        }
    }
}