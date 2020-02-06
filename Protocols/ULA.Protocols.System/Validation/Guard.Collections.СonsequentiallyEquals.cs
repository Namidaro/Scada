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
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection,
            IEnumerable<TValue> secondCollection)
        {
            Guard.AgainstIsConsequentiallyEquals<TValue>(firstCollection, secondCollection, new ArgumentException());
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing ArgumentException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="comparer">A comparer to use</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection,  IEqualityComparer<TValue> comparer)
        {
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            Guard.AgainstIsConsequentiallyEquals<TValue>(firstCollection, secondCollection, comparer,  
                new ArgumentException());
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, Exception exception)
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection);
            if (result) throw exception;
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a custom exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="comparer">A comparer to use</param>
        /// <param name="exception">A custom exception to throw</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, IEqualityComparer<TValue> comparer, Exception exception)
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection, comparer);
            if (result) throw exception;
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing an ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, [InvokerParameterName] string parameterName)
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection);
            if (result) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing an ArgumentNullException.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="comparer">A comparer to use</param>
        /// <param name="parameterName">A method parameter name</param>
        public static void AgainstIsConsequentiallyEquals<TValue>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, IEqualityComparer<TValue> comparer, 
            [InvokerParameterName] string parameterName)
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection, comparer);
            if (result) throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/>.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection)
            where TException : Exception, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection);
            if (result) Guard.Throw<TException>();
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/>.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="comparer">A comparer to use</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, IEqualityComparer<TValue> comparer)
            where TException : Exception, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection, comparer);
            if (result) Guard.Throw<TException>();
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/> and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, string message)
            where TException : ExceptionBase, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection);
            if (result) Guard.Throw<TException>(message);
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/> and exception message.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="comparer">A comparer to use</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection,
            IEnumerable<TValue> secondCollection, IEqualityComparer<TValue> comparer, string message)
            where TException : ExceptionBase, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection, comparer);
            if (result) Guard.Throw<TException>(message);
        }

        /// <summary>
        /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/>, exception message and inner exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to check</param>
        /// <param name="secondCollection">Collection to check</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection, 
            IEnumerable<TValue> secondCollection, [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection);
            if (result) Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }

        /// <summary>
        /// /// Invokes collection consequentially equality validation rule.
        /// If collections are consequentially equals to each other throwing a <see cref="TException"/>, exception message and inner exception.
        /// </summary>
        /// <typeparam name="TValue">Type of object</typeparam>
        /// <typeparam name="TException">Type of exception</typeparam>
        /// <param name="firstCollection">Collection to chec</param>
        /// <param name="secondCollection">Collection to chec</param>
        /// <param name="comparer">A comparer to use</param>
        /// <param name="parameterName">A method parameter name</param>
        /// <param name="message">An exception message</param>
        public static void AgainstIsConsequentiallyEquals<TValue, TException>(IEnumerable<TValue> firstCollection,
            IEnumerable<TValue> secondCollection, IEqualityComparer<TValue> comparer, 
            [InvokerParameterName] string parameterName, string message)
            where TException : ExceptionBase, new()
        {
            Guard.AgainstNullReference(firstCollection);
            Guard.AgainstNullReference(secondCollection);
            comparer = comparer ?? EqualityComparer<TValue>.Default;
            bool result = firstCollection.SequenceEqual<TValue>(secondCollection, comparer);
            if (result) Guard.Throw<TException>(message, new ArgumentNullException(parameterName));
        }
    }
}
