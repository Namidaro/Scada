using System;
using System.Collections.Generic;

namespace YP.Toolkit.System.Validation.ValidationRules.InternalRules
{
    /// <summary>
    /// Exposes validation rule functionality
    /// </summary>
    internal interface IValidationRule
    {
        /// <summary>
        /// Gets a type of an instance that contains a property to validate
        /// </summary>
        Type ForInstanceType { get; }
        /// <summary>
        /// Gets the name of a property that should be validated
        /// </summary>
        string ForInstancePropertyWithName { get; }
        /// <summary>
        /// Gets the type of property value to validate
        /// </summary>
        Type PropertyType { get; }
        /// <summary>
        /// Validates a value of a property
        /// </summary>
        /// <param name="value">The value of a property to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> Validate(object value);
    }

    /// <summary>
    /// Exposes validation rule functionality
    /// </summary>
    /// <typeparam name="TInstance">The type of an instance that cantains the property to validate</typeparam>
    internal interface IValidationRule<in TInstance> : IValidationRule
    {
        /// <summary>
        /// Validates a property value of an instance of <see cref="TInstance"/>
        /// </summary>
        /// <param name="value">The instance to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> Validate(TInstance value);
    }
}