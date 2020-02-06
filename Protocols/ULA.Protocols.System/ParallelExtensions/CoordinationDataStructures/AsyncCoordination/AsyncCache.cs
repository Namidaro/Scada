using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination
{
    /// <summary>
    /// Debugger type proxy for AsyncCache.
    /// </summary>
    /// <typeparam name="TKey">Specifies the type of the cache's keys.</typeparam>
    /// <typeparam name="TValue">Specifies the type of the cache's values.</typeparam>
    internal class AsyncCacheDebugView<TKey, TValue>
    {
        #region [Private fields]
        private readonly AsyncCache<TKey, TValue> _asyncCache; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Creates an instance of <see cref="AsyncCacheDebugView{TKey,TValue}"/>
        /// </summary>
        /// <param name="asyncCache">An instance of asynchronous cache</param>
        internal AsyncCacheDebugView(AsyncCache<TKey, TValue> asyncCache)
        {
            this._asyncCache = asyncCache;
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets a collection of values
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        internal KeyValuePair<TKey, Task<TValue>>[] Values
        {
            get { return _asyncCache.ToArray(); }
        } 
        #endregion [Properties]
    }

    /// <summary>
    /// Caches asynchronously retrieved data.
    /// </summary>
    /// <typeparam name="TKey">Specifies the type of the cache's keys.</typeparam>
    /// <typeparam name="TValue">Specifies the type of the cache's values.</typeparam>
    [DebuggerTypeProxy(typeof(AsyncCacheDebugView<,>))]
    [DebuggerDisplay("Count={Count}")]
    public class AsyncCache<TKey, TValue> : ICollection<KeyValuePair<TKey, Task<TValue>>>
    {
        #region [Private fields]
        /// <summary>
        /// The factory to use to create tasks.
        /// </summary>
        private readonly Func<TKey, Task<TValue>> _valueFactory;
        /// <summary>
        /// The dictionary to store all of the tasks.
        /// </summary>
        private readonly ConcurrentDictionary<TKey, Lazy<Task<TValue>>> _map; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the cache.
        /// </summary>
        /// <param name="valueFactory">A factory for producing the cache's values.</param>
        public AsyncCache(Func<TKey, Task<TValue>> valueFactory)
        {
            if (valueFactory == null) throw new ArgumentNullException("valueFactory");

            this._valueFactory = valueFactory;
            this._map = new ConcurrentDictionary<TKey, Lazy<Task<TValue>>>();
        } 
        #endregion [Ctor's]


        #region [ICollection<KeyValuePair<TKey, Task<TValue>>> members]
        /// <summary>
        /// Gets a Task to retrieve the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value should be retrieved.</param>
        /// <returns>A Task for the value of the specified key.</returns>
        public Task<TValue> GetValue(TKey key)
        {
            var value = new Lazy<Task<TValue>>(() => _valueFactory(key));
            return this._map.GetOrAdd(key, value).Value;
        }
        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value should be set.</param>
        /// <param name="value">The value to which the key should be set.</param>
        public void SetValue(TKey key, TValue value)
        {
            this.SetValue(key, Task.Factory.FromResult(value));
        }
        /// <summary>
        /// Sets the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value should be set.</param>
        /// <param name="value">The value to which the key should be set.</param>
        public void SetValue(TKey key, Task<TValue> value)
        {
            this._map[key] = LazyExtensions.Create(value);
        }
        /// <summary>
        /// Gets a Task to retrieve the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value should be retrieved.</param>
        /// <returns>A Task for the value of the specified key.</returns>
        public Task<TValue> this[TKey key]
        {
            get { return this.GetValue(key); }
            set { this.SetValue(key, value); }
        }
        /// <summary>
        /// Empties the cache.
        /// </summary>
        public void Clear() { this._map.Clear(); }
        /// <summary>
        /// Gets the number of items in the cache.
        /// </summary>
        public int Count { get { return this._map.Count; } }
        /// <summary>
        /// Gets an enumerator for the contents of the cache.
        /// </summary>
        /// <returns>An enumerator for the contents of the cache.</returns>
        public IEnumerator<KeyValuePair<TKey, Task<TValue>>> GetEnumerator()
        {
            return this._map.Select(p => new KeyValuePair<TKey, Task<TValue>>(p.Key, p.Value.Value)).GetEnumerator();
        }
        /// <summary>
        /// Gets an enumerator for the contents of the cache.
        /// </summary>
        /// <returns>An enumerator for the contents of the cache.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return this.GetEnumerator(); }
        /// <summary>
        /// Adds or overwrites the specified entry in the cache.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        void ICollection<KeyValuePair<TKey, Task<TValue>>>.Add(KeyValuePair<TKey, Task<TValue>> item)
        {
            this[item.Key] = item.Value;
        }
        /// <summary>
        /// Determines whether the cache contains the specified key.
        /// </summary>
        /// <param name="item">The item contained the key to be searched for.</param>
        /// <returns>True if the cache contains the key; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.Contains(KeyValuePair<TKey, Task<TValue>> item)
        {
            return this._map.ContainsKey(item.Key);
        }
        /// <summary>
        /// Copies the elements of the System.Collections.Generic.ICollection{T} to an
        /// System.Array, starting at a particular System.Array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional System.Array that is the destination of the elements
        /// copied from System.Collections.Generic.ICollection{T}. The System.Array must
        /// have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        void ICollection<KeyValuePair<TKey, Task<TValue>>>.CopyTo(KeyValuePair<TKey, Task<TValue>>[] array, int arrayIndex)
        {
            // ReSharper disable SuspiciousTypeConversion.Global
            ((ICollection<KeyValuePair<TKey, Task<TValue>>>)this._map).CopyTo(array, arrayIndex);
            // ReSharper restore SuspiciousTypeConversion.Global
        }
        /// <summary>
        /// Gets whether the cache is read-only.
        /// </summary>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.IsReadOnly { get { return false; } }
        /// <summary>
        /// Removes the specified key from the cache.
        /// </summary>
        /// <param name="item">The item containing the key to be removed.</param>
        /// <returns>True if the item could be removed; otherwise, false.</returns>
        bool ICollection<KeyValuePair<TKey, Task<TValue>>>.Remove(KeyValuePair<TKey, Task<TValue>> item)
        {
            Lazy<Task<TValue>> value;
            return this._map.TryRemove(item.Key, out value);
        } 
        #endregion [ICollection<KeyValuePair<TKey, Task<TValue>>> members]
    }
}