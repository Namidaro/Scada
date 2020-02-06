using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YP.Toolkit.System.Collections
{
    /// <summary>
    /// Provides a priority queue functionality
    /// </summary>
    /// <typeparam name="TValue">Specifies the type of elements in the queue.</typeparam>
    /// <typeparam name="TKey">Specifies the type of priority in the queue.</typeparam>
    public class PriorityQueue<TKey, TValue> : IPriorityQueue<TKey, TValue>
    {
        #region [Private]
        private Dictionary<TKey, Queue<TValue>> _queues;
        private readonly object _syncRoot = new object();
        #endregion [Private]


        #region [Properties]
        /// <summary>
        /// Gets the number of elements contained in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return this._queues.Sum(queue => queue.Value.Count);
            }
        }
        /// <summary>
        /// Gets whether the queue is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return this.Count == 0;
            }
        }
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="PriorityQueue{TKey,TValue}"/>
        /// </summary>
        public PriorityQueue()
        {
            this._queues = new Dictionary<TKey, Queue<TValue>>();
        }
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Adds the key/value pair to the priority queue.
        /// </summary>
        /// <param name="priority">The priority of the item to be added.</param>
        /// <param name="value">The item to be added.</param>
        public void Enqueue(TKey priority, TValue value)
        {
            if (!this._queues.ContainsKey(priority))
            {
                this._queues.Add(priority, new Queue<TValue>());
                this._queues = this._queues.OrderBy(x => x.Key).ToDictionary(x => x.Key, val => val.Value);
            }
            this._queues[priority].Enqueue(value);
        }
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
        public bool TryDequeue(out TValue value)
        {
            value = default(TValue);
            foreach (var queue in this._queues)
            {
                if (queue.Value.Count == 0) continue;
                value = queue.Value.Dequeue();
                return true;
            }
            return false;
        }
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
        public bool TryPeek(out TValue value)
        {
            value = default(TValue);
            foreach (var queue in this._queues)
            {
                if (queue.Value.Count == 0) continue;
                value = queue.Value.Peek();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <returns>
        /// true, if the source sequence contains an element with a specified value, otherwise - false.
        /// </returns>
        /// <param name="value">The value what have to found in the sequence.</param>
        public bool Contains(TValue value)
        {
            return this._queues.Any(queue => queue.Value.Contains(value));
        }
        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <param name="priority">The priority of the item to be searched.</param>
        /// <param name="value">The value what have to found in the sequence.</param>
        /// <returns>true, if the source sequence contains an element with a specified value, otherwise - false.</returns>
        public bool Contains(TKey priority, TValue value)
        {
            return this._queues.ContainsKey(priority) && this._queues[priority].Contains(value);
        }
        /// <summary>
        /// Determines whether the specified item in the sequence.
        /// </summary>
        /// <param name="priority">The priority of the item to be searched.</param>
        /// <param name="value">The value what have to found in the sequence.</param>
        /// <param name="comparer">Equality comparer is used to compare values​​.</param>
        /// <returns>true, if the source sequence contains an element with a specified value, otherwise - false.</returns>
        public bool Contains(TKey priority, TValue value, IComparer<TValue> comparer)
        {
            var comp = comparer ?? Comparer<TValue>.Default;
            return this._queues[priority].Any(x => comp.Compare(x, value) == 0);
        }
        /// <summary>
        /// Empties the queue
        /// </summary>
        public void Clear()
        {
            foreach (var priority in this._queues.Keys.ToList())
                this._queues[priority] = new Queue<TValue>();
        }
        #endregion  [Public members]


        #region [ICollection members]
        /// <summary>
        /// Gets an object that can be used to synchronize access to the collection.
        /// </summary>
        object ICollection.SyncRoot
        {
            get { return this._syncRoot; }
        }
        /// <summary>
        /// Gets a value indicating whether access to the ICollection is synchronized with the SyncRoot.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get { return false; }
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
        {
            return this._queues.GetEnumerator();
        }
        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied from the queue.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        public void CopyTo(Array array, int index)
        {
            var stronglyTypedArray = array as TValue[];
            if (stronglyTypedArray != null)
                for (int i = 0; i < this._queues.Keys.Count; i++)
                {
                    foreach (var value in (this._queues.Values.ToArray())[i])
                        stronglyTypedArray[index++] = value;
                }
        }
        #endregion [ICollection members]
    }
}