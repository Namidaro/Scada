using System;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        #region [Constants]
        private const string NEGATIVE_VALUE_MESSAGE = "The value is negative."; 
        #endregion [Constants]


        /// <summary>
        /// Invokes negative value validation rule and if the rule is not valid will throw <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="value">The value to check</param>
        public static void AgainstNegatives(int value)
        {
            if (value < 0) Guard.Throw(new ArgumentException(NEGATIVE_VALUE_MESSAGE));
        }
        /// <summary>
        /// Invokes negative value validation rule and if the rule is not valid will throw <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="parameterName">The name of a parameter to check</param>
        public static void AgainstNegatives(int value, [InvokerParameterName] string parameterName)
        {
            if (value < 0) Guard.Throw(new ArgumentException(NEGATIVE_VALUE_MESSAGE, parameterName));
        }
        /// <summary>
        /// Invokes negative value validation rule and if the rule is not valid will throw custom exception.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="exception">Custom exception to throw</param>
        public static void AgainstNegatives(int value, Exception exception)
        {
            if (value < 0) throw exception;
        }
        /// <summary>
        /// Invokes negative value validation rule on an <see cref="Int32"/>.
        /// If a value is negative an <see cref="T"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="value">The value to check</param>
        public static void AgainstNegatives<T>(int value) where T : Exception, new()
        {
            if (value < 0) Guard.Throw<T>();
        }
        /// <summary>
        /// Invokes negative value validation rule on an <see cref="Int32"/>.
        /// If a value is negative an <see cref="T"/> will be thrown.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="message">The exception message</param>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void AgainstNegatives<T>(int value, string message) where T : ExceptionBase, new()
        {
            if (value < 0) Guard.Throw<T>(message);
        }
        /// <summary>
        /// Invokes negative value validation rule on an <see cref="Int32"/>.
        /// If a value is negative an <see cref="T"/> will be thrown with <see cref="ArgumentNullException"/> as inner.
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">The exception message</param>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void AgainstNegatives<T>(int value, [InvokerParameterName] string parameterName, string message) where T : ExceptionBase, new()
        {
            if (value < 0) Guard.Throw<T>(message, new ArgumentException(NEGATIVE_VALUE_MESSAGE, parameterName));
        }
    }
}