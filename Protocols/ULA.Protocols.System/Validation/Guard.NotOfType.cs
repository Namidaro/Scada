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
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
		/// <typeparam name="T">The type to check to be assignable from</typeparam>
		/// <param name="valueToCheck">An instance to check</param>
        public static void AgainstNotOfType<T>(object valueToCheck)
        {
			if(!(valueToCheck is T)) throw new ArgumentException();
        }
		/// <summary>
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
        /// <typeparam name="T">The type to check to be assignable from</typeparam>
        /// <param name="valueToCheck">An instance to check</param>
		/// <param name="parameterName">The name of parmeter to validate</param>
        public static void AgainstNotOfType<T>(object valueToCheck, string parameterName)
        {
            if (!(valueToCheck is T)) throw new ArgumentException(string.Empty, parameterName);
        }
		/// <summary>
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
        /// <typeparam name="T">The type to check to be assignable from</typeparam>
        /// <param name="valueToCheck">An instance to check</param>
		/// <param name="message">A message to inject to exception</param>
		/// <param name="parameterName">The name of parameter to validate</param>
        public static void AgainstNotOfType<T>(object valueToCheck, string message, string parameterName)
        {
            if (!(valueToCheck is T)) throw new ArgumentException(message, parameterName);
        }
		/// <summary>
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
        /// <typeparam name="T">The type to check to be assignable from</typeparam>
		/// <typeparam name="TException">The type of exception to throw</typeparam>
        /// <param name="valueToCheck">An instance to check</param>
        public static void AgainstNotOfType<T, TException>(object valueToCheck) where TException : Exception, new()
        {
            if (!(valueToCheck is T)) Guard.Throw<TException>();
        }
		/// <summary>
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
        /// <typeparam name="T">The type to check to be assignable from</typeparam>
        /// <typeparam name="TException">The type of exception to throw</typeparam>
        /// <param name="valueToCheck">An instance to check</param>
        /// <param name="message">A message to inject to exception</param>
        public static void AgainstNotOfType<T, TException>(object valueToCheck, string message) where TException : ExceptionBase, new()
        {
            if (!(valueToCheck is T)) Guard.Throw<TException>(message);
        }
		/// <summary>
        /// Invokes value is not of type validation rule.
        /// If value is not in range thrwowing an <see cref="ArgumentException"/>.
		/// </summary>
        /// <typeparam name="T">The type to check to be assignable from</typeparam>
        /// <typeparam name="TException">The type of exception to throw</typeparam>
        /// <param name="valueToCheck">An instance to check</param>
        /// <param name="message">A message to inject to exception</param>
		/// <param name="innerException">An instance of inner exception</param>
        public static void AgainstNotOfType<T, TException>(object valueToCheck, string message, Exception  innerException) where TException : ExceptionBase, new()
        {
            if (!(valueToCheck is T)) Guard.Throw<TException>(message, innerException);
        }
    }
}