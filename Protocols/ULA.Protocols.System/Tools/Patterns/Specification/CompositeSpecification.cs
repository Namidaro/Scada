using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Represents abstract specification that joins two and more specifications in composition.
    /// </summary>
    /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
    public abstract class CompositeSpecification<T> : SpecificationBase<T>
    {
        protected internal const string NULL_COMPOSITION_MESSAGE = "Not specififcations is provided for composition";
        protected internal const string COMPOSITION_COLLECTION_MESSAGE = "The collection of composed specification should contain at least one element and shopuld not contains null items";


        #region [Private fields]
        private readonly List<SpecificationBase<T>> _specifications; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initialise a new instance of the composite specification class.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specification to composite.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        protected CompositeSpecification(params SpecificationBase<T>[] specifications)
            : this((IEnumerable<SpecificationBase<T>>) specifications.AsEnumerable<SpecificationBase<T>>())
        { }
        /// <summary>
        /// Initialise a new instance of the composite specification class.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specifications">The collection of specification to composite.</param>
        /// <exception cref="SpecificationException">Thrown when the collection of specifications is null.</exception>
        protected CompositeSpecification(IEnumerable<SpecificationBase<T>> specifications)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Guard.AgainstNullReference<SpecificationException>(specifications, "specifications", NULL_COMPOSITION_MESSAGE);
            Guard.AgainstEmptyOrNullItemCollection<SpecificationBase<T>, SpecificationException>(specifications, "specifications", COMPOSITION_COLLECTION_MESSAGE);

            this._specifications = new List<SpecificationBase<T>>(specifications);
            // ReSharper restore PossibleMultipleEnumeration
        } 
        #endregion [Ctor's]


        #region [Abstract members]
        /// <summary>
        /// Accumulates predicate over specifications sequence.
        /// </summary>
        /// <param name="predicate">Predicate for accumulation.</param>
        /// <param name="specification">Specification to accumulate with current <paramref name="predicate"/>predicate.</param>
        /// <returns>Resulting predicate.</returns>
        protected abstract Expression<Func<T, bool>> AccumulateExpression(Expression<Func<T, bool>> predicate, SpecificationBase<T> specification);
        #endregion [Abstract members]


        #region [SpecificationBase<T> members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal sealed override Expression<Func<T, bool>> Predicate
        {
            get
            {
                Expression<Func<T, bool>> first = this._specifications.First();
                return this._specifications.Skip(1).Aggregate(first, this.AccumulateExpression);
            }
        }
        #endregion [SpecificationBase<T> members]
    }
}