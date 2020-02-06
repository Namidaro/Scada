using System;
using System.Collections.Generic;
using System.Linq;
using YP.Toolkit.System.Exceptions;

namespace YP.Toolkit.System.Validation
{
    /// <summary>
    /// Represents guardian for common used validation rules
    /// </summary>
    public static partial class Guard
    {
        /// <summary>
        /// Invokes empty collection validation rule and if the rule is not valid <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">The collection to check</param>
        public static void AgainstEmptyCollection<T>(IEnumerable<T> collection)
        {
            if (!collection.Any()) throw new ArgumentException("The collection could not be empty");
        }
        /// <summary>
        /// Invokes empty collection validation rule and if the rule is not valid a custom exception will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="exception">Custom exception to throw</param>
        public static void AgainstEmptyCollection<T>(IEnumerable<T> collection, Exception exception)
        {
            if (!collection.Any()) throw exception;
        }
        /// <summary>
        /// Invokes empty collection validation rule on a method parameter.
        /// If a parameter value is not valid a <see cref="ArgumentNullException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">A method parameter value to check</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstEmptyCollection<T>(IEnumerable<T> collection, [InvokerParameterName] string parameterName)
        {
            if (!collection.Any()) throw new ArgumentException("The collection could not be empty", parameterName);
        }
        /// <summary>
        /// Invokes empty collection validation rule on an collection.
        /// If the collection is empty an <see cref="TException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        public static void AgainstEmptyCollection<TItem, TException>(IEnumerable<TItem> collection) where TException : Exception, new()
        {
            if (!collection.Any()) Guard.Throw<TException>();
        }
        /// <summary>
        /// Invokes empty collection validation rule on an collection.
        /// If the collection is empty an <see cref="TException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="message">The exception message</param>
        public static void AgainstEmptyCollection<TItem, TException>(IEnumerable<TItem> collection, string message) where TException : ExceptionBase, new()
        {
            if (!collection.Any()) Guard.Throw<TException>(message);
        }
        /// <summary>
        /// Invokes empty collection validation rule on an collection.
        /// If the collection is empty an <see cref="TException"/> will be thrown with <see cref="ArgumentNullException"/> as inner.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">The exception message</param>
        public static void AgainstEmptyCollection<TItem, TException>(IEnumerable<TItem> collection, [InvokerParameterName] string parameterName, string message) where TException : ExceptionBase, new()
        {
            if (!collection.Any())
                Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// Invokes empty collection validation rule and if the rule is not valid <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">The collection to check</param>
        public static void AgainstEmptyOrNullItemCollection<T>(IEnumerable<T> collection) where T : class 
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a=> a == null)) throw new ArgumentException("The collection is empty or contains null items");
        }
        /// <summary>
        /// Invokes empty collection or collection with null item validation rule and if the rule is not valid a custom exception will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="exception">Custom exception to throw</param>
        public static void AgainstEmptyOrNullItemCollection<T>(IEnumerable<T> collection, Exception exception) where T : class 
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a => a == null)) throw exception;
        }
        /// <summary>
        /// Invokes empty collection or collection with null item validation rule on a method parameter.
        /// If a parameter value is not valid a <see cref="ArgumentNullException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of the collection items</typeparam>
        /// <param name="collection">A method parameter value to check</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstEmptyOrNullItemCollection<T>(IEnumerable<T> collection, [InvokerParameterName] string parameterName) where T : class 
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a => a == null)) throw new ArgumentException("The collection is empty or contains null items", parameterName);
        }
        /// <summary>
        /// Invokes empty collection or collection with null item validation rule on an collection.
        /// If the collection is empty or contains null item an <see cref="TException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        public static void AgainstEmptyOrNullItemCollection<TItem, TException>(IEnumerable<TItem> collection) 
            where TException : Exception, new()
            where TItem : class 
        {
            var enumerable = collection as TItem[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a => a == null)) Guard.Throw<TException>();
        }
        /// <summary>
        /// Invokes empty collection or collection with null item validation rule on an collection.
        /// If the collection is empty or contains null item an <see cref="TException"/> will be thrown.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="message">The exception message</param>
        public static void AgainstEmptyOrNullItemCollection<TItem, TException>(IEnumerable<TItem> collection, string message) 
            where TException : ExceptionBase, new()
            where TItem : class 
        {
            var enumerable = collection as TItem[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a => a == null)) Guard.Throw<TException>(message);
        }
        /// <summary>
        /// Invokes empty collection or collection with null item validation rule on an collection.
        /// If the collection is empty or contains null item an <see cref="TException"/> will be thrown with <see cref="ArgumentException"/> as inner.
        /// </summary>
        /// <typeparam name="TItem">The type of collection item</typeparam>
        /// <typeparam name="TException">The type of excetpion to throw</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">The exception message</param>
        public static void AgainstEmptyOrNullItemCollection<TItem, TException>(IEnumerable<TItem> collection, [InvokerParameterName] string parameterName, string message) 
            where TException : ExceptionBase, new()
            where TItem : class 
        {
            var enumerable = collection as TItem[] ?? collection.ToArray();
            if (!enumerable.Any() | enumerable.Any(a => a == null)) Guard.Throw<TException>(message, new ArgumentException("The collection is empty or contains null items", parameterName));
        }
    }
}