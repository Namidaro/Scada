using YP.Toolkit.System.Tools.Patterns.Specification;

namespace YP.Toolkit.System.Validation.ValidationRules.InternalRules
{
    /// <summary>
    /// Represents a validation block information 
    /// that contains an associated message and a validator to use
    /// </summary>
    /// <typeparam name="TProperty">The type of property that is used in validation rule</typeparam>
    internal class ValidationBlock<TProperty>
    {
        /// <summary>
        /// Gets or sets a message that is associated with current validation rule
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets a validator to use
        /// </summary>
        public SpecificationBase<TProperty> Validator { get; set; }
    }
}