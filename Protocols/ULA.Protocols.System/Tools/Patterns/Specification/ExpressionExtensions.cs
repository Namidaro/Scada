using System;
using System.Linq;
using System.Linq.Expressions;
using YP.Toolkit.System.Exceptions;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.Specification
{
    /// <summary>
    /// Represents class that comopsing lambda expressions
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Extension method for composing lambda expression to 'Or' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="expression1">Curernt expression to compose.</param>
        /// <param name="expression2">The expression to compose with.</param>
        /// <returns>Resulting expression</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            Guard.AgainstNullReference<SpecificationException>(expression1, "expression1");
            Guard.AgainstNullReference<SpecificationException>(expression2, "expression2");


            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression), expression1.Parameters);
        }
        /// <summary>
        /// Extension method for composing lambda expression to 'And' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="expression1">Curernt expression to compose.</param>
        /// <param name="expression2">The expression to compose with.</param>
        /// <returns>Resulting expression</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            Guard.AgainstNullReference<SpecificationException>(expression1, "expression1");
            Guard.AgainstNullReference<SpecificationException>(expression2, "expression2");

            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(expression1.Body, invokedExpression), expression1.Parameters);
        }
        /// <summary>
        /// Extension method for composing lambda expression to 'Not' condition.
        /// 
        /// 
        /// <exception cref="SpecificationException"></exception>
        /// 
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="expression">Curernt expression to compose.</param>
        /// <returns>Resulting expression</returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            Guard.AgainstNullReference<SpecificationException>(expression, "expression");

            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }
    }
}