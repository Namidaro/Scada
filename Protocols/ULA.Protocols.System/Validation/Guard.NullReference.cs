using System;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        /// <summary>
        /// Invokes null reference validation rule and if the rule is not valid will throw custom exception.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="exception">Custom exception to throw</param>
        public static void AgainstNullReference(object value, Exception exception)
        {
            if (value == null) throw exception;
        }
        /// <summary>
        /// Invokes null reference validation rule on a method parameter.
        /// If a parameter value is not valid a ArgumentNullException will be thrown.
        /// </summary>
        /// <param name="value">A method parameter value to check</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstNullReference(object value, [InvokerParameterName] string parameterName)
        {
            if (value == null) Guard.Throw(new ArgumentNullException(parameterName));
        }
        /// <summary>
        /// Invokes null reference validation rule on an object.
        /// If a value is null an NullReferenceException will be thrown.
        /// </summary>
        /// <param name="value">The value to check</param>
        public static void AgainstNullReference(object value)
        {
            if (value == null) Guard.Throw(new NullReferenceException());
        }
        /// <summary>
        /// Invokes null reference validation rule on an object.
        /// If a value is null an <see cref="T"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The value to check</param>
        public static void AgainstNullReference<T>(object value) where T : Exception, new()
        {
            if (value == null)
                Guard.Throw<T>();
        }
        /// <summary>
        /// Invokes null reference validation rule on an object.
        /// If a value is null an <see cref="T"/> will be thrown.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="message">The exception message</param>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void AgainstNullReference<T>(object value, string message) where T : ExceptionBase, new()
        {
            if (value == null)
                Guard.Throw<T>(message);
        }
        /// <summary>
        /// Invokes null reference validation rule on an object.
        /// If a value is null an <see cref="T"/> will be thrown with <see cref="ArgumentNullException"/> as inner.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">The exception message</param>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void AgainstNullReference<T>(object value, [InvokerParameterName] string parameterName, string message) where T : ExceptionBase, new()
        {
            if (value == null)
                Guard.Throw<T>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes null reference validation rule on an object.
        /// If a value is null an <see cref="T"/> will be thrown with <see cref="Exception"/> as inner.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The value to check</param>
        /// <param name="message">The exception message</param>
        /// <param name="exception">The type of exception to throw</param>
        public static void AgainstNullReference<T>(object value, string message, Exception exception)
            where T : ExceptionBase, new()
        {
            if (value == null)
                Guard.Throw<T>(message, exception);
        }
    }
}