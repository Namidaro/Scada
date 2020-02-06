using System.Collections.Generic;

namespace YP.Toolkit.System.Validation.ValidationRules.InternalRules
{
    /// <summary>
    /// Exposes a validation in advanced functionality
    /// </summary>
    internal interface IValidationInAdvanced
    {
        /// <summary>
        /// Validates a value as if it is a value of a property
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> ValidateInAdvance(object value);
    }

    /// <summary>
    /// Exposes a validation in advanced functionality
    /// </summary>
    /// <typeparam name="TValue">The type of a property's value </typeparam>
    internal interface IValidationInAdvanced<in TValue> : IValidationInAdvanced
    {
        /// <summary>
        /// Validates a value as if it is a value of a property
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> ValidateInAdvance(TValue value);
    }
}