using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Provides a thread-safe priority queue data structure.
    /// </summary>
    /// <typeparam name="TKey">Specifies the type of keys used to prioritize values.</typeparam>
    /// <typeparam name="TValue">Specifies the type of elements in the queue.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    public class ConcurrentHeapPriorityQueue<TKey, TValue> :
        IProducerConsumerCollection<KeyValuePair<TKey,TValue>> 
        where TKey : IComparable<TKey>
    {
        #region [Nested types]
        /// <summary>
        /// Implements a binary heap that prioritizes smaller values.
        /// </summary>
        private sealed class MinBinaryHeap
        {
            #region [Private fields]
            private readonly List<KeyValuePair<TKey, TValue>> _items;
            #endregion [Private fields]


            #region [Ctor's]
            /// <summary>
            /// Initializes an empty heap.
            /// </summary>
            public MinBinaryHeap()
            {
                this._items = new List<KeyValuePair<TKey, TValue>>();
            }
            /// <summary>
            /// Initializes a heap as a copy of another heap instance.
            /// </summary>
            /// <param name="heapToCopy">The heap to copy.</param>
            /// <remarks>Key/Value values are not deep cloned.</remarks>
            public MinBinaryHeap(MinBinaryHeap heapToCopy)
            {
                this._items = new List<KeyValuePair<TKey, TValue>>(heapToCopy.Items);
            }
            #endregion [Ctor's]


            #region [Public members]
            /// <summary>
            /// Empties the heap.
            /// </summary>
            public void Clear()
            {
                this._items.Clear();
            }
            /// <summary>
            /// Adds an item to the heap.
            /// </summary>
            public void Insert(KeyValuePair<TKey, TValue> entry)
            {
                // Add the item to the list, making sure to keep track of where it was added.
                this._items.Add(entry);
                var pos = _items.Count - 1;

                // If the new item is the only item, we're done.
                if (pos == 0) return;

                // Otherwise, perform log(n) operations, walking up the tree, swapping
                // where necessary based on key values
                while (pos > 0)
                {
                    // Get the next position to check
                    var nextPos = (pos - 1) / 2;

                    // Extract the entry at the next position
                    var toCheck = _items[nextPos];

                    // Compare that entry to our new one.  If our entry has a smaller key, move it up.
                    // Otherwise, we're done.
                    if (entry.Key.CompareTo(toCheck.Key) >= 0)
                        break;
                    this._items[pos] = toCheck;
                    pos = nextPos;
                }

                // Make sure we put this entry back in, just in case
                this._items[pos] = entry;
            }
            /// <summary>
            /// Returns the entry at the top of the heap.
            /// </summary>
            public KeyValuePair<TKey, TValue> Peek()
            {
                // Returns the first item
                if (this._items.Count == 0)
                    throw new InvalidOperationException("The heap is empty.");
                return this._items[0];
            }
            /// <summary>
            /// Removes the entry at the top of the heap.
            /// </summary>
            public KeyValuePair<TKey, TValue> Remove()
            {
                // Get the first item and save it for later (this is what will be returned).
                if (this._items.Count == 0)
                    throw new InvalidOperationException("The heap is empty.");
                var toReturn = this._items[0];

                // Remove the first item if there will only be 0 or 1 items left after doing so.  
                if (this._items.Count <= 2)
                    this._items.RemoveAt(0);
                // A reheapify will be required for the removal
                else
                {
                    // Remove the first item and move the last item to the front.
                    this._items[0] = this._items[this._items.Count - 1];
                    this._items.RemoveAt(this._items.Count - 1);

                    // Start reheapify
                    int current = 0, possibleSwap = 0;

                    // Keep going until the tree is a heap
                    while (true)
                    {
                        // Get the positions of the node's children
                        var leftChildPos = 2 * current + 1;
                        var rightChildPos = leftChildPos + 1;

                        // Should we swap with the left child?
                        if (leftChildPos < _items.Count)
                        {
                            // Get the two entries to compare (node and its left child)
                            var entry1 = this._items[current];
                            var entry2 = this._items[leftChildPos];

                            // If the child has a lower key than the parent, set that as a possible swap
                            if (entry2.Key.CompareTo(entry1.Key) < 0)
                                possibleSwap = leftChildPos;
                        }
                        else break; // if can't swap this, we're done

                        // Should we swap with the right child?  Note that now we check with the possible swap
                        // position (which might be current and might be left child).
                        if (rightChildPos < _items.Count)
                        {
                            // Get the two entries to compare (node and its left child)
                            var entry1 = this._items[possibleSwap];
                            var entry2 = this._items[rightChildPos];

                            // If the child has a lower key than the parent, set that as a possible swap
                            if (entry2.Key.CompareTo(entry1.Key) < 0)
                                possibleSwap = rightChildPos;
                        }

                        // Now swap current and possible swap if necessary
                        if (current != possibleSwap)
                        {
                            var temp = this._items[current];
                            this._items[current] = this._items[possibleSwap];
                            this._items[possibleSwap] = temp;
                        }
                        else break; // if nothing to swap, we're done

                        // Update current to the location of the swap
                        current = possibleSwap;
                    }
                }

                // Return the item from the heap
                return toReturn;
            }
            /// <summary>
            /// Gets the number of objects stored in the heap.
            /// </summary>
            public int Count
            {
                get
                {
                    return this._items.Count;
                }
            }
            #endregion [Public members]


            #region [Internal members]
            /// <summary>
            /// Gets a collection of items
            /// </summary>
            internal List<KeyValuePair<TKey, TValue>> Items
            {
                get
                {
                    return this._items;
                }
            }
            #endregion [Internal members]
        } 
        #endregion [Nested types]


        #region [Private fields]
        private readonly object _syncLock = new object();
        private readonly MinBinaryHeap _minHeap = new MinBinaryHeap(); 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes a new instance of the ConcurrentPriorityQueue class.
        /// </summary>
        public ConcurrentHeapPriorityQueue() { }
        /// <summary>
        /// Initializes a new instance of the ConcurrentPriorityQueue class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new ConcurrentPriorityQueue.</param>
        public ConcurrentHeapPriorityQueue(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (var item in collection)
                this._minHeap.Insert(item);
        } 
        #endregion [Ctor's]


        #region [IProducerConsumerCollection<KeyValuePair<TKey,TValue>>  members]
        /// <summary>
        /// Adds the key/value pair to the priority queue.
        /// </summary>
        /// <param name="priority">The priority of the item to be added.</param>
        /// <param name="value">The item to be added.</param>
        public void Enqueue(TKey priority, TValue value)
        {
            this.Enqueue(new KeyValuePair<TKey, TValue>(priority, value));
        }
        /// <summary>
        /// Adds the key/value pair to the priority queue.
        /// </summary>
        /// <param name="item">The key/value pair to be added to the queue.</param>
        public void Enqueue(KeyValuePair<TKey, TValue> item)
        {
            lock (this._syncLock)
                this._minHeap.Insert(item);
        }
        /// <summary>
        /// Attempts to remove and return the next prioritized item in the queue.
        /// </summary>
        /// <param name="result">
        /// When this method returns, if the operation was successful, result contains the object removed. If
        /// no object was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the queue succesfully; otherwise, false.
        /// </returns>
        public bool TryDequeue(out KeyValuePair<TKey, TValue> result)
        {
            result = default(KeyValuePair<TKey, TValue>);
            lock (this._syncLock)
            {
                if (this._minHeap.Count > 0)
                {
                    result = this._minHeap.Remove();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Attempts to return the next prioritized item in the queue.
        /// </summary>
        /// <param name="result">
        /// When this method returns, if the operation was successful, result contains the object.
        /// The queue was not modified by the operation.
        /// </param>
        /// <returns>
        /// true if an element was returned from the queue succesfully; otherwise, false.
        /// </returns>
        public bool TryPeek(out KeyValuePair<TKey, TValue> result)
        {
            result = default(KeyValuePair<TKey, TValue>);
            lock (this._syncLock)
            {
                if (this._minHeap.Count > 0)
                {
                    result = this._minHeap.Peek();
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Empties the queue.
        /// </summary>
        public void Clear()
        {
            lock (this._syncLock)
                this._minHeap.Clear();
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
        /// <summary>
        /// Gets the number of elements contained in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                lock (this._syncLock)
                    return this._minHeap.Count;
            }
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
        /// <remarks>The elements will not be copied to the array in any guaranteed order.</remarks>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            lock (this._syncLock)
                this._minHeap.Items.CopyTo(array, index);
        }
        /// <summary>
        /// Copies the elements stored in the queue to a new array.
        /// </summary>
        /// <returns>A new array containing a snapshot of elements copied from the queue.</returns>
        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            lock (this._syncLock)
            {
                var clonedHeap = new MinBinaryHeap(this._minHeap);
                var result = new KeyValuePair<TKey, TValue>[this._minHeap.Count];
                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = clonedHeap.Remove();
                }
                return result;
            }
        }
        /// <summary>
        /// Attempts to add an item in the queue.
        /// </summary>
        /// <param name="item">The key/value pair to be added.</param>
        /// <returns>
        /// true if the pair was added; otherwise, false.
        /// </returns>
        bool IProducerConsumerCollection<KeyValuePair<TKey, TValue>>.TryAdd(KeyValuePair<TKey, TValue> item)
        {
            this.Enqueue(item);
            return true;
        }
        /// <summary>
        /// Attempts to remove and return the next prioritized item in the queue.
        /// </summary>
        /// <param name="item">
        /// When this method returns, if the operation was successful, result contains the object removed. If
        /// no object was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the queue succesfully; otherwise, false.
        /// </returns>
        bool IProducerConsumerCollection<KeyValuePair<TKey, TValue>>.TryTake(out KeyValuePair<TKey, TValue> item)
        {
            return this.TryDequeue(out item);
        }
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator for the contents of the queue.</returns>
        /// <remarks>
        /// The enumeration represents a moment-in-time snapshot of the contents of the queue. It does not
        /// reflect any updates to the collection after GetEnumerator was called. The enumerator is safe to
        /// use concurrently with reads from and writes to the queue.
        /// </remarks>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var arr = this.ToArray();
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)arr).GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
        void ICollection.CopyTo(Array array, int index)
        {
            lock (this._syncLock)
                ((ICollection)this._minHeap.Items).CopyTo(array, index);
        }
        /// <summary>
        /// Gets a value indicating whether access to the ICollection is synchronized with the SyncRoot.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// Gets an object that can be used to synchronize access to the collection.
        /// </summary>
        object ICollection.SyncRoot
        {
            get
            {
                return this._syncLock;
            }
        } 
        #endregion [IProducerConsumerCollection<KeyValuePair<TKey,TValue>>  members]
    }
}