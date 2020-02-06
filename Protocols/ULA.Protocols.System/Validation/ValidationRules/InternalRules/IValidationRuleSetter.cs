using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.InternalRules
{
    /// <summary>
    /// Exposes a validation rule setter
    /// </summary>
    /// <typeparam name="TProperty">The type of property to validate</typeparam>
    internal interface IValidationRuleSetter<TProperty>
    {
        /// <summary>
        /// Sets a validator for the current validation rule
        /// </summary>
        /// <param name="validator">The validation to use as a rule</param>
        void SetValidator(SpecificationBase<TProperty> validator);
        /// <summary>
        /// Sets a message that will be associated with failed validation rule result information
        /// </summary>
        /// <param name="message">The message that will associated with failed validation rule result information</param>
        void SetMessage(string message);
    }
}