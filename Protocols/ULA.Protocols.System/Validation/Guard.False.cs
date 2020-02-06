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
        /// Invokes false validation rule.
        /// If boolean value is false throwing a custom exception.
        /// </summary>
        /// <param name="value">The boolean value to validate</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsFalse(bool value, Exception exception)
        {
            if (value == false) throw exception;
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If boolean value is false throwing a ArgumentNullException exception.
        /// </summary>
        /// <param name="value">The boolean value to validate</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsFalse(bool value, [InvokerParameterName] string parameterName)
        {
            Guard.AgainstIsFalse(value, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If boolean value is false throwing ArgumentException exception.
        /// </summary>
        /// <param name="value">The boolean value to validate</param>
        public static void AgainstIsFalse(bool value)
        {
            Guard.AgainstIsFalse(value, new ArgumentException());
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If boolean value is false an <see cref="T"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The boolean value to validate</param>
        public static void AgainstIsFalse<T>(bool value) where T : Exception, new()
        {
            if (value == false) Guard.Throw<T>();
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If boolean value is false an <see cref="T"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The boolean value to validate</param>
        /// <param name="message">The exception message</param>
        public static void AgainstIsFalse<T>(bool value, string message) where T : ExceptionBase, new()
        {
            if (value == false) Guard.Throw<T>(message, new ArgumentException());
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If a value is false an <see cref="T"/> will be thrown with <see cref="ArgumentNullException"/> as inner.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The boolean value to validate</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">The exception message</param>
        public static void AgainstIsFalse<T>(bool value, [InvokerParameterName] string parameterName, string message)
            where T : ExceptionBase, new()
        {
            if (value == false) Guard.Throw<T>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes false validation rule.
        /// If value is false a <see cref="T"/> will be thrown with <see cref="Exception"/> as inner.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The boolean value to validate</param>
        /// <param name="message">The exception message</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsFalse<T>(bool value, string message, Exception exception)
            where T : ExceptionBase, new()
        {
            if (!value) Guard.Throw<T>(message, exception);
        }
    }
}
