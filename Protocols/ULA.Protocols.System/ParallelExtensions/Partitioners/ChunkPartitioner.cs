﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace YP.Toolkit.System.ParallelExtensions.Partitioners
{
    /// <summary>
    /// Partitions an enumerable into chunks based on user-supplied criteria.
    /// </summary>
    public static class ChunkPartitioner
    {
        /// <summary>
        /// Creates a partitioner that chooses the next chunk size based on a user-supplied function.
        /// </summary>
        /// <typeparam name="TSource">The type of the data being partitioned.</typeparam>
        /// <param name="source">The data being partitioned.</param>
        /// <param name="nextChunkSizeFunc">A function that determines the next chunk size based on the
        /// previous chunk size.</param>
        /// <returns>A partitioner.</returns>
        public static OrderablePartitioner<TSource> Create<TSource>(
            IEnumerable<TSource> source, Func<int, int> nextChunkSizeFunc)
        {
            return new ChunkPartitioner<TSource>(source, nextChunkSizeFunc);
        }
        /// <summary>
        /// Creates a partitioner that always uses a user-specified chunk size.
        /// </summary>
        /// <typeparam name="TSource">The type of the data being partitioned.</typeparam>
        /// <param name="source">The data being partitioned.</param>
        /// <param name="chunkSize">The chunk size to be used.</param>
        /// <returns>A partitioner.</returns>
        public static OrderablePartitioner<TSource> Create<TSource>(
            IEnumerable<TSource> source, int chunkSize)
        {
            return new ChunkPartitioner<TSource>(source, chunkSize);
        }
        /// <summary>
        /// Creates a partitioner that chooses chunk sizes between the user-specified min and max.
        /// </summary>
        /// <typeparam name="TSource">The type of the data being partitioned.</typeparam>
        /// <param name="source">The data being partitioned.</param>
        /// <param name="minChunkSize">The minimum chunk size to use.</param>
        /// <param name="maxChunkSize">The maximum chunk size to use.</param>
        /// <returns>A partitioner.</returns>
        public static OrderablePartitioner<TSource> Create<TSource>(
            IEnumerable<TSource> source, int minChunkSize, int maxChunkSize)
        {
            return new ChunkPartitioner<TSource>(source, minChunkSize, maxChunkSize);
        }
    }

    /// <summary>
    /// Partitions an enumerable into chunks based on user-supplied criteria.
    /// </summary>
    internal sealed class ChunkPartitioner<T> : OrderablePartitioner<T>
    {
        #region [Nested types]
        // The object used to dynamically create partitions
        private class EnumerableOfEnumerators : IEnumerable<KeyValuePair<long, T>>, IDisposable
        {
            #region [Nested types]
            private class Enumerator : IEnumerator<KeyValuePair<long, T>>
            {
                #region [Private fields]
                private readonly EnumerableOfEnumerators _parentEnumerable;
                private readonly List<KeyValuePair<long, T>> _currentChunk = new List<KeyValuePair<long, T>>();
                private int _currentChunkCurrentIndex;
                private int _lastRequestedChunkSize;
                private bool _disposed;
                #endregion [Private fields]


                #region [Ctor's]
                public Enumerator(EnumerableOfEnumerators parentEnumerable)
                {
                    if (parentEnumerable == null) throw new ArgumentNullException("parentEnumerable");
                    this._parentEnumerable = parentEnumerable;
                }
                #endregion [Ctor's]

                /// <summary>
                /// Advances the enumerator to the next element of the collection.
                /// </summary>
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
                public bool MoveNext()
                {
                    if (this._disposed)
                        throw new ObjectDisposedException(this.GetType().Name);

                    // Move to the next cached element. If we already retrieved a chunk and if there's still
                    // data left in it, just use the next item from it.
                    ++this._currentChunkCurrentIndex;
                    if (this._currentChunkCurrentIndex >= 0 &&
                        this._currentChunkCurrentIndex < this._currentChunk.Count) return true;

                    // First, figure out how much new data we want. The previous requested chunk size is used
                    // as input to figure out how much data the user now wants.  The initial chunk size
                    // supplied is 0 so that the user delegate is made aware that this is the initial request
                    // such that it can select the initial chunk size on first request.
                    var nextChunkSize = this._parentEnumerable._parentPartitioner._nextChunkSizeFunc(this._lastRequestedChunkSize);
                    if (nextChunkSize <= 0) throw new InvalidOperationException(
                        "Invalid chunk size requested: chunk sizes must be positive.");
                    this._lastRequestedChunkSize = nextChunkSize;

                    // Reset the list
                    this._currentChunk.Clear();
                    this._currentChunkCurrentIndex = 0;
                    if (nextChunkSize > this._currentChunk.Capacity)
                        this._currentChunk.Capacity = nextChunkSize;

                    // Try to grab the next chunk of data
                    lock (this._parentEnumerable._sharedEnumerator)
                    {
                        // If we've already discovered that no more elements exist (and we've gotten this
                        // far, which means we don't have any elements cached), we're done.
                        if (this._parentEnumerable._noMoreElements) return false;

                        // Get another chunk
                        for (var i = 0; i < nextChunkSize; i++)
                        {
                            // If there are no more elements to be retrieved from the shared enumerator, mark
                            // that so that other partitions don't have to check again. Return whether we
                            // were able to retrieve any data at all.
                            if (!this._parentEnumerable._sharedEnumerator.MoveNext())
                            {
                                this._parentEnumerable._noMoreElements = true;
                                return this._currentChunk.Count > 0;
                            }

                            ++this._parentEnumerable._nextSharedIndex;
                            this._currentChunk.Add(new KeyValuePair<long, T>(
                                this._parentEnumerable._nextSharedIndex,
                                this._parentEnumerable._sharedEnumerator.Current));
                        }
                    }

                    // We got at least some data
                    return true;
                }
                /// <summary>
                /// Gets the element in the collection at the current position of the enumerator.
                /// </summary>
                /// <returns>
                /// The element in the collection at the current position of the enumerator.
                /// </returns>
                public KeyValuePair<long, T> Current
                {
                    get
                    {
                        if (this._currentChunkCurrentIndex >= this._currentChunk.Count)
                            throw new InvalidOperationException("There is no current item.");
                        return this._currentChunk[this._currentChunkCurrentIndex];
                    }
                }
                /// <summary>
                /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
                /// </summary>
                /// <filterpriority>2</filterpriority>
                public void Dispose()
                {
                    if (this._disposed) return;
                    this._parentEnumerable.DisposeEnumerator();
                    this._disposed = true;
                }
                /// <summary>
                /// Gets the current element in the collection.
                /// </summary>
                /// <returns>
                /// The current element in the collection.
                /// </returns>
                /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception><filterpriority>2</filterpriority>
                object IEnumerator.Current
                {
                    get
                    {
                        return this.Current;
                    }
                }
                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element in the collection.
                /// </summary>
                /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
                public void Reset()
                {
                    throw new NotSupportedException();
                }
            }
            #endregion [Nested types]


            #region [Private fields]
            private readonly ChunkPartitioner<T> _parentPartitioner;
            private readonly IEnumerator<T> _sharedEnumerator;
            private long _nextSharedIndex;
            private int _activeEnumerators;
            private bool _noMoreElements;
            private bool _disposed;
            private readonly bool _referenceCountForDisposal;
            #endregion [Private fields]

            #region [Ctor's]
            /// <summary>
            /// Creates an instance of <see cref="EnumerableOfEnumerators"/>
            /// </summary>
            /// <param name="parentPartitioner">An instance of parent partioner</param>
            /// <param name="referenceCountForDisposal"></param>
            public EnumerableOfEnumerators(ChunkPartitioner<T> parentPartitioner, bool referenceCountForDisposal)
            {
                // Validate parameters
                if (parentPartitioner == null)
                    throw new ArgumentNullException("parentPartitioner");

                // Store the data, including creating an enumerator from the underlying data source
                this._parentPartitioner = parentPartitioner;
                this._sharedEnumerator = parentPartitioner._source.GetEnumerator();
                this._nextSharedIndex = -1;
                this._referenceCountForDisposal = referenceCountForDisposal;
            }
            #endregion [Ctor's]


            #region [IEnumerable<KeyValuePair<long, T>> members]
            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
            /// </returns>
            /// <filterpriority>1</filterpriority>
            public IEnumerator<KeyValuePair<long, T>> GetEnumerator()
            {
                if (this._referenceCountForDisposal)
                {
                    Interlocked.Increment(ref this._activeEnumerators);
                }
                return new Enumerator(this);
            }
            #endregion [IEnumerable<KeyValuePair<long, T>> members]


            #region [Private fields]
            private void DisposeEnumerator()
            {
                if (!this._referenceCountForDisposal) return;
                if (Interlocked.Decrement(ref _activeEnumerators) == 0)
                    this._sharedEnumerator.Dispose();
            }
            #endregion [Private fields]


            #region [IDisposable members]
            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                if (this._disposed) return;
                if (!this._referenceCountForDisposal)
                    this._sharedEnumerator.Dispose();
                this._disposed = true;
            }
            #endregion [IDisposable members]
        } 
        #endregion [Nested types]


        #region [Private fields]
        private readonly IEnumerable<T> _source;
        private readonly Func<int, int> _nextChunkSizeFunc; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="ChunkPartitioner{T}"/>
        /// </summary>
        /// <param name="source">A source collection to make chunks from</param>
        /// <param name="nextChunkSizeFunc">An instance of delegate to obtain the next chink of data</param>
        public ChunkPartitioner(IEnumerable<T> source, Func<int, int> nextChunkSizeFunc)
            // The keys will be ordered across both individual partitions and across partitions,
            // and they will be normalized.
            : base(true, true, true)
        {
            // Validate and store the enumerable and function (used to determine how big
            // to make the next chunk given the current chunk size)
            if (source == null) throw new ArgumentNullException("source");
            if (nextChunkSizeFunc == null) throw new ArgumentNullException("nextChunkSizeFunc");
            this._source = source;
            this._nextChunkSizeFunc = nextChunkSizeFunc;
        }
        /// <summary>
        /// Creates an instance of <see cref="ChunkPartitioner{T}"/>
        /// </summary>
        /// <param name="source">A source collection to make chunks from</param>
        /// <param name="chunkSize">A sizze of a chunk</param>
        public ChunkPartitioner(IEnumerable<T> source, int chunkSize)
            : this(source, prev => chunkSize) // uses a function that always returns the specified chunk size
        {
            if (chunkSize <= 0)
                throw new ArgumentOutOfRangeException("chunkSize");
        }
        /// <summary>
        /// Creates an instance of <see cref="ChunkPartitioner{T}"/>
        /// </summary>
        /// <param name="source">A source collection to make chunks from</param>
        /// <param name="minChunkSize">A minimum chunk size</param>
        /// <param name="maxChunkSize">A maximum chunk size</param>
        public ChunkPartitioner(IEnumerable<T> source, int minChunkSize, int maxChunkSize) :
            this(source, CreateFuncFromMinAndMax(minChunkSize, maxChunkSize)) // uses a function that grows from min to max
        {
            if (minChunkSize <= 0 || minChunkSize > maxChunkSize)
                throw new ArgumentOutOfRangeException("minChunkSize");
        } 
        #endregion [Ctor's]


        #region [Override members]
        /// <summary>
        /// Partitions the underlying collection into the specified number of orderable partitions.
        /// </summary>
        /// <param name="partitionCount">The number of partitions to create.</param>
        /// <returns>An object that can create partitions over the underlying data source.</returns>
        public override IList<IEnumerator<KeyValuePair<long, T>>> GetOrderablePartitions(int partitionCount)
        {
            // Validate parameters
            if (partitionCount <= 0) throw new ArgumentOutOfRangeException("partitionCount");

            // Create an array of dynamic partitions and return them
            var partitions = new IEnumerator<KeyValuePair<long, T>>[partitionCount];
            var dynamicPartitions = GetOrderableDynamicPartitions(true);
            for (var i = 0; i < partitionCount; i++)
            {
                partitions[i] = dynamicPartitions.GetEnumerator(); // Create and store the next partition
            }
            return partitions;
        }
        /// <summary>
        /// Gets whether additional partitions can be created dynamically.
        /// </summary>
        public override bool SupportsDynamicPartitions
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// Creates an object that can partition the underlying collection into a variable number of
        /// partitions.
        /// </summary>
        /// <returns>
        /// An object that can create partitions over the underlying data source.
        /// </returns>
        public override IEnumerable<KeyValuePair<long, T>> GetOrderableDynamicPartitions()
        {
            return new EnumerableOfEnumerators(this, false);
        } 
        #endregion [Override members]


        #region [Help members]
        private static Func<int, int> CreateFuncFromMinAndMax(int minChunkSize, int maxChunkSize)
        {
            // Create a function that returns exponentially growing chunk sizes between minChunkSize and maxChunkSize
            return delegate(int prev)
            {
                if (prev < minChunkSize) return minChunkSize;
                if (prev >= maxChunkSize) return maxChunkSize;
                var next = prev * 2;
                if (next >= maxChunkSize || next < 0) return maxChunkSize;
                return next;
            };
        }
        private IEnumerable<KeyValuePair<long, T>> GetOrderableDynamicPartitions(bool referenceCountForDisposal)
        {
            return new EnumerableOfEnumerators(this, referenceCountForDisposal);
        } 
        #endregion [Help members]
    }
}