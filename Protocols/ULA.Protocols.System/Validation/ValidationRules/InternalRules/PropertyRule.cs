using System;
using System.Collections.Generic;
using System.Reflection;
using YP.Toolkit.System.Tools.Patterns.Specification;
using YP.Toolkit.System.Tools.StrongTypedReflection;

namespace YP.Toolkit.System.Validation.ValidationRules.InternalRules
{
    /// <summary>
    /// Exposes a validation rule that is connected to a property of an object
    /// </summary>
    /// <typeparam name="TInstance">The type of instance that property is connected to</typeparam>
    /// <typeparam name="TProperty">The type of property to validate</typeparam>
    internal class PropertyRule<TInstance, TProperty> :
        IValidationRule<TInstance>,
        IValidationInAdvanced<TProperty>
    {
        #region [Private fields]
        private readonly string _propertyName;
        private readonly Func<TInstance, TProperty> _propertyAccessor;
        private readonly LinkedList<ValidationBlock<TProperty>> _validationBlocks =
            new LinkedList<ValidationBlock<TProperty>>();
        private readonly Type _propertyType;
        private readonly Type _instanceType;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="PropertyRule{TEntity,TProperty}"/>
        /// </summary>
        /// <param name="property">The property information to use</param>
        public PropertyRule(PropertyInfo property)
        {
            this._propertyType = typeof(TProperty);
            this._instanceType = typeof(TInstance);
            this._propertyName = property.Name;
            this._propertyAccessor = TypeMembersHelper.CreatePropertyGetter<TInstance, TProperty>(property);
            this._validationBlocks.AddFirst(new ValidationBlock<TProperty>());
        }
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Begins to aggregate the next validation rule with 'And' operator
        /// </summary>
        internal void AggregateAnd()
        {
            this._validationBlocks.AddLast(new ValidationBlock<TProperty>());
        }
        /// <summary>
        /// Sets current validator
        /// </summary>
        /// <param name="validator"></param>
        internal void SetValidator(SpecificationBase<TProperty> validator)
        {
            this._validationBlocks.Last.Value.Validator = validator;
        }
        /// <summary>
        /// Sets a message to associate with current validation rule
        /// </summary>
        /// <param name="message">The message to associate with current validation rule</param>
        internal void SetMessage(string message)
        {
            this._validationBlocks.Last.Value.Message = message;
        }
        #endregion [Public members]


        #region [IValidationRule<TEntity> members]
        /// <summary>
        /// Gets a type of an instance that contains a property to validate
        /// </summary>
        Type IValidationRule.ForInstanceType
        {
            get { return this._instanceType; }
        }
        /// <summary>
        /// Gets the name of a property that should be validated
        /// </summary>
        string IValidationRule.ForInstancePropertyWithName
        {
            get { return this._propertyName; }
        }
        /// <summary>
        /// Gets the type of property value to validate
        /// </summary>
        Type IValidationRule.PropertyType
        {
            get { return this._propertyType; }
        }
        /// <summary>
        /// Validates a property value of an instance of <see cref="TInstance"/>
        /// </summary>
        /// <param name="value">The instance to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> IValidationRule<TInstance>.Validate(TInstance value)
        {
            return this.ValidateValueInternal(this._propertyAccessor.Invoke(value));
        }
        /// <summary>
        /// Validates a value of a property
        /// </summary>
        /// <param name="value">The value of a property to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> IValidationRule.Validate(object value)
        {
            return this.ValidateValueInternal(this._propertyAccessor.Invoke((TInstance)value));
        }
        #endregion [IValidationRule<TEntity> members]


        #region [IValidationInAdvanced<TProperty> members]
        /// <summary>
        /// Validates a value as if it is a value of a property
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> IValidationInAdvanced<TProperty>.ValidateInAdvance(TProperty value)
        {
            return this.ValidateValueInternal(value);
        }
        /// <summary>
        /// Validates a value as if it is a value of a property
        /// </summary>
        /// <param name="value">The value to validate</param>
        /// <returns>The result of validation</returns>
        IEnumerable<ValidationFailure> IValidationInAdvanced.ValidateInAdvance(object value)
        {
            return this.ValidateValueInternal((TProperty)value);
        }
        #endregion [IValidationInAdvanced<TProperty> members]


        #region [Help members]
        private IEnumerable<ValidationFailure> ValidateValueInternal(TProperty value)
        {
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (var validationBlock in this._validationBlocks)
            {
                if (validationBlock.Validator == null || validationBlock.Validator.IsSatisfiedBy(value)) continue;
                yield return new ValidationFailure(this._propertyName, validationBlock.Message);
            }
        }
        #endregion [Help members]
    }
}