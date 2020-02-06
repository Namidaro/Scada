using System.Collections.Generic;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Represents class for implementing specification fluent API
    /// </summary>
    public static class SpecificationFluentApi
    {
        /// <summary>
        /// Joins specifications in an 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        /// <returns>Resulting specification.</returns>
        public static SpecificationBase<T> And<T>(this SpecificationBase<T> current, SpecificationBase<T> right)
        {
            return new AndSpecification<T>(current, right);
        }

        /// <summary>
        /// Joins specifications in an 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Left operand specification instance.</param>
        /// <param name="right">The collection of specification to join in an 'And' condition.</param>
        /// <returns>Resulting specification.</returns> 
        public static SpecificationBase<T> And<T>(this SpecificationBase<T> current, params SpecificationBase<T>[] right)
        {
            var specifications = new List<SpecificationBase<T>>(right.Length + 1) { current };
            specifications.AddRange(right);
            return new AndSpecification<T>(specifications);
        }

        /// <summary>
        /// Joins specifications in an 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="left">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        /// <returns>Resulting specification.</returns>
        public static SpecificationBase<T> Or<T>(this SpecificationBase<T> left, SpecificationBase<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        /// <summary>
        /// Joins specifications in an 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Left operand specification instance.</param>
        /// <param name="right">The collection of specification to join in an 'Or' condition.</param>
        /// <returns>Resulting specification.</returns> 
        public static SpecificationBase<T> Or<T>(this SpecificationBase<T> current, params SpecificationBase<T>[] right)
        {
            var specifications = new List<SpecificationBase<T>>(right.Length + 1) { current };
            specifications.AddRange(right);
            return new OrSpecification<T>(specifications);
        }

        /// <summary>
        /// Creates 'Not' condition specification from <paramref name="current"/>
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Current specification</param>
        /// <returns>Resulting specification</returns>
        public static SpecificationBase<T> Not<T>(this SpecificationBase<T> current)
        {
            return new NotSpecification<T>(current);
        }

        /// <summary>
        /// Joins specifications in an 'AndNot' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        /// <returns>Resulting specification.</returns>
        public static SpecificationBase<T> AndNot<T>(this SpecificationBase<T> current, SpecificationBase<T> right)
        {
            return current.And(right.Not());
        }

        /// <summary>
        /// Joins specifications in an 'OrNot' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of entity that is the subject of the specification.</typeparam>
        /// <param name="current">Left operand specification instance.</param>
        /// <param name="right">Right operand specification instance.</param>
        /// <returns>Resulting specification.</returns>
        public static SpecificationBase<T> OrNot<T>(this SpecificationBase<T> current, SpecificationBase<T> right)
        {
            return current.Or(right.Not());
        }
    }
}