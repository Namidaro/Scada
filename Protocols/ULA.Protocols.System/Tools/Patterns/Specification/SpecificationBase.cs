using System;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Use specification to make complex (business) logic explicit.
    /// For additional information visit - http://en.wikipedia.org/wiki/Specification_pattern
    /// </summary>
    /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
    public abstract class SpecificationBase<T>
    {
        #region [Private fields]
        private Func<T, bool> _compiledFunc;
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal abstract Expression<Func<T, bool>> Predicate { get; }
        #endregion [Properties]


        #region [Internal members]
        /// <summary>
        /// Evaluates the predicate for a single entity.
        /// 
        /// 
        /// <exception cref="SpecificationException">Thrown when the <c>Predicate</c> is null.</exception>
        /// 
        /// </summary>
        /// <param name="item">The item to examine.</param>
        /// <returns>A value that indicates whether entity is satisfeing current predicate.</returns>
        protected internal bool IsSatisfiedBy(T item)
        {
            Guard.AgainstNullReference<SpecificationException>(this.Predicate, "The specification predicate is not set");
            this._compiledFunc = this._compiledFunc ?? this.Predicate.Compile();
            return this._compiledFunc.Invoke(item);
        }
        #endregion [Internal members]


        #region [Overrided operators]
        /// <summary>
        /// Overrides 'Or' operator.
        /// </summary>
        /// <param name="left">Left operator operand.</param>
        /// <param name="right">Right operator operand.</param>
        /// <returns>A specification that represents 'Or' operator result.</returns>
        public static SpecificationBase<T> operator |(SpecificationBase<T> left, SpecificationBase<T> right)
        {
            return new OrSpecification<T>(left, right);
        }
        /// <summary>
        /// Overrides 'And' operator.
        /// </summary>
        /// <param name="left">Left operator operand.</param>
        /// <param name="right">Right operator operand.</param>
        /// <returns>A specification that represents 'And' operator result.</returns>
        public static SpecificationBase<T> operator &(SpecificationBase<T> left, SpecificationBase<T> right)
        {
            return new AndSpecification<T>(left, right);
        }
        /// <summary>
        /// Overrides 'Not' operator.
        /// </summary>
        /// <param name="specification">The specification to apply 'Not' operation to.</param>
        /// <returns>A specification that represents 'Not' operator result.</returns>
        public static SpecificationBase<T> operator !(SpecificationBase<T> specification)
        {
            return new NotSpecification<T>(specification);
        }
        #endregion [Overrided operators]


        #region [Implicit members]
        /// <summary>
        /// Implicitly converts the specification instance to expression.
        /// </summary>
        /// <param name="specification">Current specification instance.</param>
        /// <returns>Resulting expression.</returns>
        public static implicit operator Expression<Func<T, bool>>(SpecificationBase<T> specification)
        {
            return specification.Predicate;
        }
        /// <summary>
        /// Implicitly converts the specification instance to delegate.
        /// </summary>
        /// <param name="specification">Current specification instance.</param>
        /// <returns>Resulting delegate.</returns>
        public static implicit operator Func<T, bool>(SpecificationBase<T> specification)
        {
            return specification.IsSatisfiedBy;
        }
        #endregion [Implicit members]
    }
}