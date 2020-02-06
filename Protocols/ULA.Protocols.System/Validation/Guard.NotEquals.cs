using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an ArgumentException exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right)
        {
            bool resultOfCompare = EqualityComparer<TValue>.Default.Equals(left, right);
            if (resultOfCompare == false) Guard.Throw<ArgumentException>();
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an ArgumentException exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="right">Value of object to validate</param>
        /// <param name="left">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right,
            IEqualityComparer<TValue> comparer)
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) Guard.Throw<ArgumentException>();
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right, Exception exception)
        {
            var comparer = EqualityComparer<TValue>.Default;
            Guard.AgainstInequalityOfValues(left, right, comparer, exception);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="right">Value of object to validate</param>
        /// <param name="left">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right,
            IEqualityComparer<TValue> comparer, Exception exception)
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) throw exception;
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an ArgumentNullException exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right,
            [InvokerParameterName] string parameterName)
        {
            var comparer = EqualityComparer<TValue>.Default;
            Guard.AgainstInequalityOfValues(left, right, comparer, parameterName);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an ArgumentNullException exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstInequalityOfValues<TValue>(TValue left, TValue right,
            IEqualityComparer<TValue> comparer, [InvokerParameterName] string parameterName)
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing a <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right)
            where TException : Exception, new()
        {
            var comparer = EqualityComparer<TValue>.Default;
            Guard.AgainstInequalityOfValues<TValue, TException>(left, right, comparer);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="right">Value of object to validate</param>
        /// <param name="left">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right,
            IEqualityComparer<TValue> comparer) where TException : Exception, new()
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) Guard.Throw<TException>();
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="message">An exception message</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right, string message)
            where TException : ExceptionBase, new()
        {
            var comparer = EqualityComparer<TValue>.Default;
            Guard.AgainstInequalityOfValues<TValue, TException>(left, right, comparer, message);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="message">An exception message</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right, 
            IEqualityComparer<TValue> comparer, string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) Guard.Throw<TException>(message);
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an <see cref="TException"/> exception and with inner exception ArgumentNullException and with exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right,
            IEqualityComparer<TValue> comparer,
            [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool resultOfCompare = comparer.Equals(left, right);
            if (resultOfCompare == false) Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes inequality validation rule.
        /// If objects are not equals throwing an <see cref="TException"/> exception and with inner exception ArgumentNullException and with exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="left">Value of object to validate</param>
        /// <param name="right">Value of object to validate</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstInequalityOfValues<TValue, TException>(TValue left, TValue right, 
            [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            var comparer = EqualityComparer<TValue>.Default;
            Guard.AgainstInequalityOfValues<TValue, TException>(left, right, comparer, parameterName, message);
        }
    }
}
