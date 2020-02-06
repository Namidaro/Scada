using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Provides a thread-safe, concurrent collection for use with data binding.
    /// </summary>
    /// <typeparam name="T">Specifies the type of the elements in this collection.</typeparam>
    [DebuggerDisplay("Count={Count}")]
    [DebuggerTypeProxy(typeof(ProducerConsumerCollectionDebugView<>))]
    public class ObservableConcurrentCollection<T> : 
        ProducerConsumerCollectionBase<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        #region [Private fields]
        private readonly SynchronizationContext _context; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes an instance of the ObservableConcurrentCollection class with an underlying
        /// queue data structure.
        /// </summary>
        public ObservableConcurrentCollection() : this(new ConcurrentQueue<T>()) { }
        /// <summary>
        /// Initializes an instance of the ObservableConcurrentCollection class with the specified
        /// collection as the underlying data structure.
        /// </summary>
        public ObservableConcurrentCollection(IProducerConsumerCollection<T> collection)
            : base(collection)
        {
            this._context = AsyncOperationManager.SynchronizationContext;
        } 
        #endregion [Ctor's]


        #region [Event members]
        /// <summary>
        /// Event raised when the collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        /// <summary>
        /// Event raised when a property on the collection changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion [Event members]


        #region [Override members]
        /// <summary>Attempts to add the specified value to the end of the deque.</summary>
        /// <param name="item">The item to add.</param>
        /// <returns>true if the item could be added; otherwise, false.</returns>
        protected override bool TryAdd(T item)
        {
            // Try to add the item to the underlying collection.  If we were able to,
            // notify any listeners.
            var result = base.TryAdd(item);
            if (result) this.NotifyObserversOfChange();
            return result;
        }
        /// <summary>Attempts to remove and return an item from the collection.</summary>
        /// <param name="item">
        /// When this method returns, if the operation was successful, item contains the item removed. If
        /// no item was available to be removed, the value is unspecified.
        /// </param>
        /// <returns>
        /// true if an element was removed and returned from the collection; otherwise, false.
        /// </returns>
        protected override bool TryTake(out T item)
        {
            // Try to remove an item from the underlying collection.  If we were able to,
            // notify any listeners.
            var result = base.TryTake(out item);
            if (result) this.NotifyObserversOfChange();
            return result;
        } 
        #endregion [Override members]


        #region [Help members]
        /// <summary>
        /// Notifies observers of CollectionChanged or PropertyChanged of an update to the dictionary.
        /// </summary>
        private void NotifyObserversOfChange()
        {
            var collectionHandler = this.CollectionChanged;
            var propertyHandler = this.PropertyChanged;
            if (collectionHandler != null || propertyHandler != null)
            {
                this._context.Post(s =>
                {
                    if (collectionHandler != null)
                    {
                        collectionHandler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    if (propertyHandler != null)
                    {
                        propertyHandler(this, new PropertyChangedEventArgs("Count"));
                    }
                }, null);
            }
        } 
        #endregion [Help members]
    }
}