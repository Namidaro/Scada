using System.Collections;
using System.Collections.Generic;

namespace YP.Toolkit.System.Collections
{
    /// <summary>
    /// Exposes a priority queue data structure.
    /// </summary>
    /// <typeparam name="TValue">Specifies the type of elements in the queue.</typeparam>
    /// <typeparam name="TKey">Specifies the type of priority in the queue.</typeparam>
    public interface IPriorityQueue<in TKey, TValue> : ICollection
    {
        /// <summary>
        /// Gets the number of elements contained in the queue.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets whether the queue is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Adds the key/value pair to the priority queue.
        /// </summary>
        /// <param name="priority">The priority of the item to be added.</param>
        /// <param name="value">The item to be added.</param>
        void Enqueue(TKey priority, TValue value);

        /// <summary>,
        /// Attempts to remove and return the next prioritized item in the queue.
        /// </summary>
        /// <param name="value">
        /// When this method returns, if the operation was successful, result contains the object removed. If
        /// no object was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the queue succesfully; otherwise, false.
        /// </returns>
        bool TryDequeue(out TValue value);

        /// <summary>
        /// Attempts to return the next prioritized item in the queue.
        /// </summary>
        /// <param name="value">
        /// When this method returns, if the operation was successful, result contains the object.
        /// The queue was not modified by the operation.
        /// </param>
        /// <returns>
        /// true if an element was returned from the queue succesfully; otherwise, false.
        /// </returns>
        bool TryPeek(out TValue value);

        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <returns>
        /// true, if the source sequence contains an element with a specified value, otherwise - false.
        /// </returns>
        /// <param name="value">The value what have to found in the sequence.</param>
        bool Contains(TValue value);

        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <param name="priority">The priority of the item to be searched.</param>
        /// <param name="value">The value what have to found in the sequence.</param>
        /// <returns>true, if the source sequence contains an element with a specified value, otherwise - false.</returns>
        bool Contains(TKey priority, TValue value);

        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <param name="priority">The priority of the item to be searched.</param>
        /// <param name="value">The value what have to found in the sequence.</param>
        /// <param name="comparer">Equality comparer is used to compare values​​.</param>
        /// <returns>true, if the source sequence contains an element with a specified value, otherwise - false.</returns>
        bool Contains(TKey priority, TValue value, IComparer<TValue> comparer);

        /// <summary>
        /// Empties the queue
        /// </summary>
        void Clear();
    }
}