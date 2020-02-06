using System;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
		/// <summary>
		/// Invokes string is null or empty validation rule.
		/// If a value is empty string or contains null reference a custom exception will be thrown.
		/// </summary>
		/// <param name="value">The string value to validate</param>
		/// <param name="exception">A custom exception to throw</param>
        public static void AgainstEmptyStringOrNull(string value, Exception exception)
		{
		    if (string.IsNullOrEmpty(value)) throw exception;
		}
		/// <summary>
		/// Invokes string is null or empty validation rule on a method parameter.
        /// If a value is empty or contains null reference a ArgumentNullException will be thrown.
		/// </summary>
        /// <param name="value">A method parameter value to validate</param>
		/// <param name="parameterName">A method parameter name</param>
        public static void AgainstEmptyStringOrNull(string value, [InvokerParameterName] string parameterName)
        {
            Guard.AgainstEmptyStringOrNull(value, new ArgumentNullException(parameterName));
        }
		/// <summary>
		/// Invokes a string is null or empty validation rule.
		/// If a value is empty or contains null reference a NullReferenceException will be thrown.
		/// </summary>
        /// <param name="value">The string value to validat</param>
        public static void AgainstEmptyStringOrNull(string value)
        {
            Guard.AgainstEmptyStringOrNull(value, new NullReferenceException());
        }
    }
}