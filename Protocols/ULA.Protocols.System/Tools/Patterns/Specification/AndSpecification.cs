using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Joins two and more specifications in an 'And' condition.
    /// </summary>
    /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        #region [Ctor]
        /// <summary>
        /// Initialise a new instance of the specification class that joins two specifications in an 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="left">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        public AndSpecification(SpecificationBase<T> left, SpecificationBase<T> right)
            : base(left, right) { }
        /// <summary>
        /// Initialise a new instance of the specification class that joins two an more specifications in an 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specifications to composite in 'And' condition.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        public AndSpecification(params SpecificationBase<T>[] specifications)
            : base(specifications) { }
        /// <summary>
        /// Initialise a new instance of the specification class that joins two an more specifications in an 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specifications to composite in 'And' condition.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        public AndSpecification(IEnumerable<SpecificationBase<T>> specifications)
            : base(specifications) { }
        #endregion [Ctor]

        #region [CompositeSpecification<T> members]
        /// <summary>
        /// Accumulates predicate over specifications sequence.
        /// </summary>
        /// <param name="predicate">Predicate for accumulation.</param>
        /// <param name="specification">Specification to accumulate with current <paramref name="predicate"/>predicate.</param>
        /// <returns>Resulting predicate.</returns>
        protected override Expression<Func<T, bool>> AccumulateExpression(Expression<Func<T, bool>> predicate, SpecificationBase<T> specification)
        {
            return predicate.And(specification);
        }
        #endregion [CompositeSpecification<T> members]
    }
}