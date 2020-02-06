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
        /// Invokes value is not in range validation rule.
        /// If value is not in range thrwowing an ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue>(value, leftBorder, rightBorder, comparer);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing an ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="comparer">Comparer for objects</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) Guard.Throw<ArgumentException>();
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder,
            Exception exception)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue>(value, leftBorder, rightBorder, comparer, exception);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Right border of range</param>
        /// <param name="rightBorder">Left border of range</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer, Exception exception)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) throw exception;
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing an ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Right border of range</param>
        /// <param name="rightBorder">Left border of range</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder,
                                                    [InvokerParameterName] string parameterName)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue>(value, leftBorder, rightBorder, comparer, parameterName);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing an ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsNotInRange<TValue>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer, [InvokerParameterName] string parameterName)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to validate</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder)
            where TException : Exception, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue, TException>(value, leftBorder, rightBorder, comparer);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to validate</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="comparer">Cmparer for objects</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer)
            where TException : Exception, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) Guard.Throw<TException>();
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right vorder of range</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder,
            string message)
            where TException : ExceptionBase, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue, TException>(value, leftBorder, rightBorder, comparer, message);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer, string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) Guard.Throw<TException>(message);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception, with inner exception ArgumentNullException and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder,
            [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsNotInRange<TValue, TException>(value, leftBorder, rightBorder, comparer, parameterName, message);
        }

        /// <summary>
        /// Invokes value is not in range validation rule.
        /// If value is not in range throwing a <see cref="TException"/> exception, with inner exception ArgumentNullException and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="value">Value of object to check</param>
        /// <param name="leftBorder">Left border of range</param>
        /// <param name="rightBorder">Right border of range</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A metjod parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsNotInRange<TValue, TException>(TValue value, TValue leftBorder, TValue rightBorder,
            IComparer<TValue> comparer, [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            bool result = comparer.Compare(value, leftBorder) <= 0 || comparer.Compare(value, rightBorder) >= 0;
            if (result) Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }
    }
}
