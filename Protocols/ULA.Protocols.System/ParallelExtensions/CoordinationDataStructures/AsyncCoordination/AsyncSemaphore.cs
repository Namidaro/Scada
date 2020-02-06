using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination
{
    /// <summary>
    /// Provides an asynchronous semaphore.
    /// </summary>
    [DebuggerDisplay("CurrentCount={CurrentCount}, MaximumCount={MaximumCount}, WaitingCount={WaitingCount}")]
    public sealed class AsyncSemaphore : IDisposable
    {
        #region [Private fields]
        /// <summary>
        /// The current count.
        /// </summary>
        private int _currentCount;
        /// <summary>
        /// The maximum count. If _maxCount isn't positive, the instance has been disposed.
        /// </summary>
        private int _maxCount;
        /// <summary>
        /// Tasks waiting to be completed when the semaphore has count available.
        /// </summary>
        private readonly Queue<TaskCompletionSource<object>> _waitingTasks; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the SemaphoreAsync with a count of zero and a maximum count of Int32.MaxValue.
        /// </summary>
        public AsyncSemaphore() : this(0) { }
        /// <summary>
        /// Initializes the SemaphoreAsync with the specified count and a maximum count of Int32.MaxValue.
        /// </summary>
        /// <param name="initialCount">The initial count to use as the current count.</param>
        public AsyncSemaphore(int initialCount) : this(initialCount, Int32.MaxValue) { }
        /// <summary>
        /// Initializes the SemaphoreAsync with the specified counts.
        /// </summary>
        /// <param name="initialCount">The initial count to use as the current count.</param>
        /// <param name="maxCount">The maximum count allowed.</param>
        public AsyncSemaphore(int initialCount, int maxCount)
        {
            if (maxCount <= 0) throw new ArgumentOutOfRangeException("maxCount");
            if (initialCount > maxCount || initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
            this._currentCount = initialCount;
            this._maxCount = maxCount;
            this._waitingTasks = new Queue<TaskCompletionSource<object>>();
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets the current count.
        /// </summary>
        public int CurrentCount
        {
            get
            {
                return _currentCount;
            }
        }
        /// <summary>
        /// Gets the maximum count.
        /// </summary>
        public int MaximumCount
        {
            get
            {
                return _maxCount;
            }
        }
        /// <summary>
        /// Gets the number of operations currently waiting on the semaphore.
        /// </summary>
        public int WaitingCount
        {
            get
            {
                lock (this._waitingTasks)
                    return this._waitingTasks.Count;
            }
        } 
        #endregion [Properties]


        #region [Public members]
        /// <summary>
        /// Waits for a unit to be available in the semaphore.
        /// </summary>
        /// <returns>A Task that will be completed when a unit is available and this Wait operation succeeds.</returns>
        public Task WaitAsync()
        {
            this.ThrowIfDisposed();
            lock (this._waitingTasks)
            {
                // If there's room, decrement the count and return a completed task
                if (this._currentCount > 0)
                {
                    Interlocked.Decrement(ref _currentCount);
                    return CompletedTask.Default;
                }
                // Otherwise, cache a new task and return it
                var tcs = new TaskCompletionSource<object>();
                this._waitingTasks.Enqueue(tcs);
                return tcs.Task;
            }
        }
        /// <summary>
        /// Queues an action that will be executed when space is available
        /// in the semaphore.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        /// <returns>
        /// A Task that represents the execution of the action.
        /// </returns>
        /// <remarks>
        /// Release does not need to be called for this action, as it will be handled implicitly
        /// by the Queue method.
        /// </remarks>
        public Task Queue(Action action)
        {
            return this.WaitAsync().ContinueWith(_ =>
            {
                try
                {
                    action();
                }
                finally
                {
                    this.Release();
                }
            });
        }
        /// <summary>
        /// Queues a function that will be executed when space is available
        /// in the semaphore.
        /// </summary>
        /// <param name="function">The function to be executed.</param>
        /// <returns>
        /// A Task that represents the execution of the function.
        /// </returns>
        /// <remarks>
        /// Release does not need to be called for this function, as it will be handled implicitly
        /// by the Queue method.
        /// </remarks>
        public Task<TResult> Queue<TResult>(Func<TResult> function)
        {
            return this.WaitAsync().ContinueWith(_ =>
            {
                try
                {
                    return function();
                }
                finally
                {
                    this.Release();
                }
            });
        }
        /// <summary>
        /// Releases a unit of work to the semaphore.
        /// </summary>
        public void Release()
        {
            this.ThrowIfDisposed();
            lock (this._waitingTasks)
            {
                // Validate that there's room
                if (this._currentCount == this._maxCount)
                    throw new SemaphoreFullException();

                // If there are any tasks waiting, allow one of them access
                if (this._waitingTasks.Count > 0)
                {
                    var tcs = this._waitingTasks.Dequeue();
                    tcs.SetResult(null);
                }
                // Otherwise, increment the available count
                Interlocked.Increment(ref this._currentCount);
            }
        } 
        #endregion [Public members]


        #region [IDisposable members]
        private void ThrowIfDisposed()
        {
            if (this._maxCount <= 0)
                throw new ObjectDisposedException(this.GetType().Name);
        }
        /// <summary>
        /// Releases the resources used by the semaphore.
        /// </summary>
        public void Dispose()
        {
            if (_maxCount <= 0) return;
            this._maxCount = 0;
            lock (this._waitingTasks)
            {
                while (this._waitingTasks.Count > 0)
                {
                    this._waitingTasks.Dequeue().SetCanceled();
                }
            }
        } 
        #endregion [IDisposable members]
    }
}