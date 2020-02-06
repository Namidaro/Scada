using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YP.Toolkit.System.Tools.StrongTypedReflection;
using YP.Toolkit.System.Validation.ValidationRules.Builders;
using YP.Toolkit.System.Validation.ValidationRules.InternalRules;

namespace YP.Toolkit.System.Validation.ValidationRules
{
    /// <summary>
    /// Represents a basic validator for <see cref="TInstance"/>
    /// </summary>
    public abstract class ValidationBase<TInstance>
    {
        #region [Private fields]
        private readonly IList<IValidationRule<TInstance>> _rules; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of <see cref="ValidationBase{TInstance}"/>
        /// </summary>
        protected ValidationBase()
        {
            this._rules = new List<IValidationRule<TInstance>>();
        } 
        #endregion [Ctor's]


        #region [Configuration members]
        /// <summary>
        /// Configures a validation rule for a property of an instance
        /// </summary>
        /// <param name="propertyAccessor">The property to validate</param>
        /// <typeparam name="TInstance">The type of instance which property should be validated</typeparam>
        /// <typeparam name="TProperty">The type of property to validate</typeparam>
        /// <returns>An instance of a property validation rule builder</returns>
        public IRuleBuilder<TInstance, TProperty> RuleFor<TProperty>(Expression<Func<TInstance, TProperty>> propertyAccessor)
        {
            var property = StrongTypeReflection.InstanceOf<TInstance>.Property(propertyAccessor);
            var propertyValidationRule = new PropertyRule<TInstance, TProperty>(property);
            this._rules.Add(propertyValidationRule);
            return new RuleBuilder<TInstance, TProperty>(propertyValidationRule);
        } 
        #endregion [Configuration members]


        #region [Validation members]
        /// <summary>
        /// Validates an instance
        /// </summary>
        /// <param name="instance">Te instance to validate</param>
        /// <returns>The result of validation</returns>
        public IEnumerable<ValidationFailure> Validate(TInstance instance)
        {
            return this._rules.Where(s => s.ForInstanceType.Equals((typeof(TInstance))))
                       .SelectMany(sm => sm.Validate(instance));
        }
        /// <summary>
        /// Validates an instance
        /// </summary>
        /// <param name="instance">The instance to validate</param>
        /// <returns>The result of validation</returns>
        public IEnumerable<ValidationFailure> Validate(object instance)
        {
            return this._rules.Where(s => s.ForInstanceType.Equals(instance.GetType()))
                     .SelectMany(sm => sm.Validate(instance));
        }
        /// <summary>
        /// Validates an instance
        /// </summary>
        /// <param name="instance">The instance to validate</param>
        /// <param name="propertyName">The name of a property to validate</param>
        /// <returns>The result of validation</returns>
        public IEnumerable<ValidationFailure> Validate(TInstance instance, string propertyName)
        {
            return this._rules.Where(s => s.ForInstancePropertyWithName.Equals(propertyName))
                       .SelectMany(sm => sm.Validate(instance));
        }
        /// <summary>
        /// Validates an instance
        /// </summary>
        /// <param name="instance">The instance to validate</param>
        /// <param name="propertyName">The name of a property to validate</param>
        /// <returns>The result of validation</returns>
        public IEnumerable<ValidationFailure> Validate(object instance, string propertyName)
        {
            return this._rules.Where(s => s.ForInstancePropertyWithName.Equals(propertyName))
                     .SelectMany(sm => sm.Validate(instance));
        } 
        #endregion [Validation members]


        #region [Validation in advance members]
        /// <summary>
        /// Validates an instance in advanced. It means that actual property value is not tested.
        /// The value is tested as if it is set to a property of <see cref="propertyName"/> 
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <param name="propertyName">The name of property</param>
        /// <returns>The result of validation</returns>
        public IEnumerable<ValidationFailure> ValidateInAdvance(object value, string propertyName)
        {
            return this._rules.Where(s => s.ForInstancePropertyWithName.Equals(propertyName))
                       .OfType<IValidationInAdvanced>()
                       .SelectMany(sm => sm.ValidateInAdvance(value));
        } 
        #endregion [Validation in advance members]


        #region [Protected members]
        /// <summary>
        /// Gets the type of a property that was previously registered in the instance validation functionality
        /// </summary>
        /// <param name="propertyName">The name of property to get type of</param>
        /// <returns>The type of a property to validate</returns>
        protected Type GetPropertyTypeByName(string propertyName)
        {
            var rule = this._rules.FirstOrDefault(f => f.ForInstancePropertyWithName.Equals(propertyName));
            return rule == null ? null : rule.PropertyType;
        } 
        #endregion [Protected members]
    }
}