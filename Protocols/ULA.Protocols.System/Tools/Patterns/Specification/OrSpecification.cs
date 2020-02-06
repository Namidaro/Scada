using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Joins two and more specifications in an 'Or' condition.
    /// </summary>
    /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
    public class OrSpecification<T> : CompositeSpecification<T>
    {
        #region [Ctors]
        /// <summary>
        /// Initialise a new instance of the specification class that joins two specifications in an 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="left">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        public OrSpecification(SpecificationBase<T> left, SpecificationBase<T> right)
            : base(left, right) { }
        /// <summary>
        /// Initialise a new instance of the specification class that joins two an more specifications in an 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specifications to composite in 'Or' condition.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        public OrSpecification(params SpecificationBase<T>[] specifications)
            : base(specifications) { }
        /// <summary>
        /// Initialise a new instance of the specification class that joins two an more specifications in an 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specifications to composite in 'Or' condition.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        public OrSpecification(IEnumerable<SpecificationBase<T>> specifications)
            : base(specifications) { }
        #endregion [Ctors]


        #region [CompositeSpecification<T> members]
        /// <summary>
        /// Accumulates predicate over specifications sequence.
        /// </summary>
        /// <param name="predicate">Predicate for accumulation.</param>
        /// <param name="specification">Specification to accumulate with current <paramref name="predicate"/>predicate.</param>
        /// <returns>Resulting predicate.</returns>
        protected override Expression<Func<T, bool>> AccumulateExpression(Expression<Func<T, bool>> predicate, SpecificationBase<T> specification)
        {
            return predicate.Or(specification);
        }
        #endregion [CompositeSpecification<T> members]
    }
}