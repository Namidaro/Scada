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
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing an ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue>(valueForCompare, value, comparer);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing an ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfCompare = comparer.Compare(valueForCompare, value);
            if (resultOfCompare >= 0) Guard.Throw<ArgumentException>();
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value, Exception exception)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue>(valueForCompare, value, comparer, exception);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer, Exception exception)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfCompare = comparer.Compare(valueForCompare, value);
            if (resultOfCompare >= 0) throw exception;
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value,
            [InvokerParameterName] string parameterName)
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue>(valueForCompare, value, comparer, parameterName);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer, [InvokerParameterName] string parameterName)
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfCompare = comparer.Compare(valueForCompare, value);
            if (resultOfCompare >= 0) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value)
            where TException : Exception, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue, TException>(valueForCompare, value, comparer);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing <see cref="TException"/> exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer)
            where TException : Exception, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfCompare = comparer.Compare(valueForCompare, value);
            if (resultOfCompare >= 0) Guard.Throw<Exception>();
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value,
            string message)
            where TException : ExceptionBase, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue, TException>(valueForCompare, value, comparer, message);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer, string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfComare = comparer.Compare(valueForCompare, value);
            if (resultOfComare >= 0) Guard.Throw<TException>(message);
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="comparer">Comparer for objects</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value,
            IComparer<TValue> comparer, [InvokerParameterName] string parameterName,
            string message)
            where TException : ExceptionBase, new()
        {
            comparer = comparer ?? Comparer<TValue>.Default;
            int resultOfCompare = comparer.Compare(valueForCompare, value);
            if (resultOfCompare >= 0) Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes value is greater or equals than another validation rule.
        /// If first parameter is greater of equals than second parameter throwing a <see cref="TException"/> exception and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="valueForCompare">Value of object to compare</param>
        /// <param name="value">Value of object to compare</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsGreaterOrEqualsThan<TValue, TException>(TValue valueForCompare, TValue value, 
            [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            var comparer = Comparer<TValue>.Default;
            Guard.AgainstIsGreaterOrEqualsThan<TValue, TException>(valueForCompare, value, comparer, 
                parameterName, message);
        }
    }
}
