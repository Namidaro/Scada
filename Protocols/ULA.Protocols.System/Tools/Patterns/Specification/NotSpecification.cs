using System;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Represents specification with 'Not' condition.
    /// </summary>
    /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
    public class NotSpecification<T> : SpecificationBase<T>
    {
        #region [Private fields]
        private readonly SpecificationBase<T> _specification;
        #endregion [Private fields]


        #region [Ctors]
        /// <summary>
        /// Initialise a new instance of the specification class that represents a 'Not' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <param name="specification">The specification to apply 'Not' condition.</param>
        /// <exception cref="SpecificationException">Thrown when current specification is null.</exception>
        public NotSpecification(SpecificationBase<T> specification)
        {
            Guard.AgainstNullReference<SpecificationException>(specification, "specification",
                                                               CompositeSpecification<T>.NULL_COMPOSITION_MESSAGE);
            this._specification = specification;
        }
        #endregion [Ctors]


        #region [SpecificationBase<T> members]
        /// <summary>
        /// Gets current specification expression.
        /// </summary>
        protected internal override Expression<Func<T, bool>> Predicate
        {
            get { return this._specification.Predicate.Not(); }
        }
        #endregion [SpecificationBase<T> members]
    }
}