using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Debug view for the IProducerConsumerCollection.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the data being aggregated.</typeparam>
    internal sealed class ProducerConsumerCollectionDebugView<T>
    {
        #region [Private fields]
        private readonly IProducerConsumerCollection<T> _collection; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ProducerConsumerCollectionDebugView{T}"/>
        /// </summary>
        /// <param name="collection">An instance of <see cref="IProducerConsumerCollection{T}"/> to use</param>
        public ProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
        {
            this._collection = collection;
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets a collection of values
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Values
        {
            get
            {
                return this._collection.ToArray();
            }
        } 
        #endregion [Properties]
    }

    /// <summary>
    /// Provides a base implementation for producer-consumer collections that wrap other
    /// producer-consumer collections.
    /// </summary>
    /// <typeparam name="T">Specifies the type of elements in the collection.</typeparam>
    [Serializable]
    public abstract class ProducerConsumerCollectionBase<T> : IProducerConsumerCollection<T>
    {
        #region [Private fields]
        private readonly IProducerConsumerCollection<T> _contained; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the ProducerConsumerCollectionBase instance.
        /// </summary>
        /// <param name="contained">The collection to be wrapped by this instance.</param>
        protected ProducerConsumerCollectionBase(IProducerConsumerCollection<T> contained)
        {
            if (contained == null) throw new ArgumentNullException("contained");
            this._contained = contained;
        } 
        #endregion [Ctor's]


        #region [IProducerConsumerCollection<T> members]
        /// <summary>
        /// Attempts to add the specified value to the end of the deque.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the item could be added; otherwise, false.</returns>
        bool IProducerConsumerCollection<T>.TryAdd(T item)
        {
            return this.TryAdd(item);
        }
        /// <summary>
        /// Attempts to remove and return an item from the collection.
        /// </summary>
        /// <param name="item">
        /// When this method returns, if the operation was successful, item contains the item removed. If
        /// no item was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the collection; otherwise, false.
        /// </returns>
        bool IProducerConsumerCollection<T>.TryTake(out T item)
        {
            return this.TryTake(out item);
        }
        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
        public int Count
        {
            get
            {
                return this._contained.Count;
            }
        }
        /// <summary>
        /// Creates an array containing the contents of the collection.
        /// </summary>
        /// <returns>The array.</returns>
        public T[] ToArray()
        {
            return this._contained.ToArray();
        }
        /// <summary>
        /// Copies the contents of the collection to an array.
        /// </summary>
        /// <param name="array">The array to which the data should be copied.</param>
        /// <param name="index">The starting index at which data should be copied.</param>
        public void CopyTo(T[] array, int index)
        {
            this._contained.CopyTo(array, index);
        }
        /// <summary>
        /// Copies the contents of the collection to an array.
        /// </summary>
        /// <param name="array">The array to which the data should be copied.</param>
        /// <param name="index">The starting index at which data should be copied.</param>
        void ICollection.CopyTo(Array array, int index)
        {
            this._contained.CopyTo(array, index);
        }
        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._contained.GetEnumerator();
        }
        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>An enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <summary>
        /// Gets whether the collection is synchronized.
        /// </summary>
        bool ICollection.IsSynchronized
        {
            get
            {
                return this._contained.IsSynchronized;
            }
        }
        /// <summary>
        /// Gets the synchronization root object for the collection.
        /// </summary>
        object ICollection.SyncRoot
        {
            get
            {
                return this._contained.SyncRoot;
            }
        } 
        #endregion [IProducerConsumerCollection<T> members]


        #region [Help members]
        /// <summary>
        /// Gets the contained collection.
        /// </summary>
        protected IProducerConsumerCollection<T> ContainedCollection
        {
            get
            {
                return this._contained;
            }
        }
        /// <summary>
        /// Attempts to add the specified value to the end of the deque.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the item could be added; otherwise, false.</returns>
        protected virtual bool TryAdd(T item)
        {
            return this._contained.TryAdd(item);
        }
        /// <summary>
        /// Attempts to remove and return an item from the collection.
        /// </summary>
        /// <param name="item">
        /// When this method returns, if the operation was successful, item contains the item removed. If
        /// no item was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the collection; otherwise, false.
        /// </returns>
        protected virtual bool TryTake(out T item)
        {
            return this._contained.TryTake(out item);
        } 
        #endregion [Help members]
    }
}