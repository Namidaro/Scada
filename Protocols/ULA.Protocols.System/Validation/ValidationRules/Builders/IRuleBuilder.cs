using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.Builders
{
    /// <summary>
    /// Exposes begin validation rule builder functionality.
    /// It is used to provide an ability to set custom validation specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity which property is going to be validated</typeparam>
    /// <typeparam name="TProperty">The property of <see cref="TEntity"/> to validate</typeparam>
    public interface IRuleBuilder<TEntity, TProperty>
    {
        /// <summary>
        /// Aggregates a validation rule into a sequence of rules that are joined with 'And' operation
        /// </summary>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that begins validation rule builder functionality</returns>
        IRuleBuilder<TEntity, TProperty> And();
        /// <summary>
        /// Sets a custom validation specification
        /// </summary>
        /// <param name="validationRule">A specification to use as validation rule</param>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that is used to mark the validation rule as closing</returns>
        IRuleBuilder<TEntity, TProperty> Custom(SpecificationBase<TProperty> validationRule);
        /// <summary>
        /// Sets a message that will be associated with a faild validation 
        /// </summary>
        /// <param name="message">The message to associate with failed validation rule result</param>
        /// <returns>An instance of <see cref="IRuleBuilder{TEntity,TProperty}"/> that begins validation rule builder functionality</returns>
        IRuleBuilder<TEntity, TProperty> WithMessage(string message);
    }
}