using System;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        /* 
         * 
         TODO: [Yauheni Parshiniou] We SHOULD implement localizable exception throwing. It means that all messages in throwing exception must be localized
         *
         */

        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="exception">Exception to throw</param>
        public static void Throw<T>([NotNull] T exception) where T : Exception
        {
            throw exception;
        }

        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="message">The exception message</param>
        public static void Throw<T>(string message) where T : ExceptionBase, new()
        {
            var exception = new T();
            exception.SetMessage(message);
            throw exception;
        }

        /// <summary>
        /// Throws exception
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception of current exception</param>
        public static void Throw<T>(string message, Exception innerException) where T : ExceptionBase, new()
        {
            var exception = new T();
            exception.SetMessage(message);
            exception.SetInnerException(innerException);
            throw exception;
        }
        /// <summary>
        /// Throws custom excetion
        /// </summary>
        /// <typeparam name="T">The type of exception to throw</typeparam>
        public static void Throw<T>() where T : Exception, new()
        {
            throw new T();
        }
    }
}