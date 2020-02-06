using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination
{
    /// <summary>
    /// Asynchronously invokes a handler for every posted item.
    /// </summary>
    /// <typeparam name="T">Specifies the type of data processed by the instance.</typeparam>
    public sealed class AsyncCall<T> : MarshalByRefObject
    {
        #region [Private fields]
        /// <summary>
        /// A queue that stores the posted data.  Also serves as the syncObj for protected instance state.
        /// A ConcurrentQueue is used to enable lock-free dequeues while running with a single consumer task.
        /// </summary>
        private readonly ConcurrentQueue<T> _queue;
        /// <summary>
        /// The delegate to invoke for every element.
        /// </summary>
        private readonly Delegate _handler;
        /// <summary>
        /// The maximum number of items that should be processed by an individual task.
        /// </summary>
        private readonly int _maxItemsPerTask;
        /// <summary>
        /// The TaskFactory to use to launch new tasks.
        /// </summary>
        private readonly TaskFactory _tf;
        /// <summary>
        /// The options to use for parallel processing of data.
        /// </summary>
        private readonly ParallelOptions _parallelOptions;
        /// <summary>
        /// Whether a processing task has been scheduled.
        /// </summary>
        private int _processingCount;
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the AsyncCall with an action to execute for each element.
        /// </summary>
        /// <param name="actionHandler">The action to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="scheduler">The scheduler to use.  If null, the default scheduler is used.</param>
        /// <param name="maxItemsPerTask">The maximum number of items to be processed per task.  If not specified, Int32.MaxValue is used.</param>
        public AsyncCall(Action<T> actionHandler, int maxDegreeOfParallelism = 1, int maxItemsPerTask = Int32.MaxValue, TaskScheduler scheduler = null) :
            this(maxDegreeOfParallelism, maxItemsPerTask, scheduler)
        {
            if (actionHandler == null) throw new ArgumentNullException("actionHandler");
            this._handler = actionHandler;
        }
        /// <summary>
        /// Initializes the AsyncCall with a function to execute for each element.  The function returns an Task 
        /// that represents the asynchronous completion of that element's processing.
        /// </summary>
        /// <param name="functionHandler">The function to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="scheduler">The scheduler to use.  If null, the default scheduler is used.</param>
        public AsyncCall(Func<T, Task> functionHandler, int maxDegreeOfParallelism = 1, TaskScheduler scheduler = null) :
            this(maxDegreeOfParallelism, 1, scheduler)
        {
            if (functionHandler == null) throw new ArgumentNullException("functionHandler");
            this._handler = functionHandler;
        }
        /// <summary>
        /// General initialization of the AsyncCall.  Another constructor must initialize the delegate.
        /// </summary>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="maxItemsPerTask">The maximum number of items to be processed per task.  If not specified, Int32.MaxValue is used.</param>
        /// <param name="scheduler">The scheduler to use.  If null, the default scheduler is used.</param>
        private AsyncCall(int maxDegreeOfParallelism = 1, int maxItemsPerTask = Int32.MaxValue, TaskScheduler scheduler = null)
        {
            // Validate arguments
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            if (maxItemsPerTask < 1) throw new ArgumentOutOfRangeException("maxItemsPerTask");
            if (scheduler == null) scheduler = TaskScheduler.Default;

            // Configure the instance
            this._queue = new ConcurrentQueue<T>();
            this._maxItemsPerTask = maxItemsPerTask;
            this._tf = new TaskFactory(scheduler);
            if (maxDegreeOfParallelism != 1)
            {
                this._parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism, TaskScheduler = scheduler };
            }
        }
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Post an item for processing.
        /// </summary>
        /// <param name="item">The item to be processed.</param>
        public void Post(T item)
        {
            lock (this._queue)
            {
                // Add the item to the internal queue
                this._queue.Enqueue(item);

                // Check to see whether the right number of tasks have been scheduled.
                // If they haven't, schedule one for this new piece of data.
                if (this._handler is Action<T>)
                {
                    if (this._processingCount == 0)
                    {
                        this._processingCount = 1;
                        this._tf.StartNew(ProcessItemsActionTaskBody);
                    }
                }
                else if (this._handler is Func<T, Task>)
                {
                    if (this._processingCount == 0 ||
                        (this._parallelOptions != null && this._processingCount < this._parallelOptions.MaxDegreeOfParallelism && // are enough workers currently processing?
                        !this._queue.IsEmpty)) // and, as an optimization, double check to make sure the item hasn't already been picked up by another worker
                    {
                        this._processingCount++;
                        this._tf.StartNew(ProcessItemFunctionTaskBody, null);
                    }
                }
                else Debug.Fail("_handler is an invalid delegate type");
            }
        } 
        #endregion [Public members]


        #region [Help members]
        /// <summary>
        /// Gets an enumerable that yields the items to be processed at this time.
        /// </summary>
        /// <returns>An enumerable of items.</returns>
        private IEnumerable<T> GetItemsToProcess()
        {
            // Yield the next elements to be processed until either there are no more elements
            // or we've reached the maximum number of elements that an individual task should process.
            var processedCount = 0;
            T nextItem;
            while (processedCount < this._maxItemsPerTask && this._queue.TryDequeue(out nextItem))
            {
                yield return nextItem;
                processedCount++;
            }
        }
        /// <summary>
        /// Used as the body of an action task to process items in the queue.
        /// </summary>
        private void ProcessItemsActionTaskBody()
        {
            try
            {
                // Get the handler
                var handler = (Action<T>)_handler;

                // Process up to _maxItemsPerTask items, either serially or in parallel
                // based on the provided maxDegreeOfParallelism (which determines
                // whether a ParallelOptions is instantiated).
                if (this._parallelOptions == null)
                    foreach (var item in GetItemsToProcess())
                        handler(item);
                else
                    Parallel.ForEach(GetItemsToProcess(), _parallelOptions, handler);
            }
            finally
            {
                lock (this._queue)
                {
                    // If there are still items in the queue, schedule another task to continue processing.
                    // Otherwise, note that we're no longer processing.
                    if (!this._queue.IsEmpty)
                        this._tf.StartNew(ProcessItemsActionTaskBody, TaskCreationOptions.PreferFairness);
                    else this._processingCount = 0;
                }
            }
        }
        /// <summary>
        /// Used as the body of a function task to process items in the queue.
        /// </summary>
        private void ProcessItemFunctionTaskBody(object ignored)
        {
            var anotherTaskQueued = false;
            try
            {
                // Get the handler
                var handler = (Func<T, Task>)_handler;

                // Get the next item from the queue to process
                T nextItem;
                if (!this._queue.TryDequeue(out nextItem)) return;
                // Run the handler and get the follow-on task.
                // If we got a follow-on task, run this process again when the task completes.
                // If we didn't, just start another task to keep going now.
                var task = handler(nextItem);
                if (task != null) task.ContinueWith(this.ProcessItemFunctionTaskBody, this._tf.Scheduler);
                else this._tf.StartNew(this.ProcessItemFunctionTaskBody, null);

                // We've queued a task to continue processing, which means that logically
                // we're still maintaining the same level of parallelism.
                anotherTaskQueued = true;
            }
            finally
            {
                // If we didn't queue up another task to continue processing (either
                // because an exception occurred, or we failed to grab an item from the queue)
                if (!anotherTaskQueued)
                {
                    lock (this._queue)
                    {
                        // Verify that there's still nothing in the queue, now under the same
                        // lock that the queuer needs to take in order to increment the processing count
                        // and launch a new processor.
                        if (!this._queue.IsEmpty)
                            this._tf.StartNew(this.ProcessItemFunctionTaskBody, null);
                        else _processingCount--;
                    }
                }
            }
        } 
        #endregion [Help members]
    }

    /// <summary>
    /// Provides static factory methods for creating AsyncCall(Of T) instances.
    /// </summary>
    public static class AsyncCall
    {
        /// <summary>
        /// Initializes the AsyncCall with an action to execute for each element.
        /// </summary>
        /// <param name="actionHandler">The action to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="scheduler">The scheduler to use.  If null, the default scheduler is used.</param>
        /// <param name="maxItemsPerTask">The maximum number of items to be processed per task.  If not specified, Int32.MaxValue is used.</param>
        public static AsyncCall<T> Create<T>(Action<T> actionHandler, int maxDegreeOfParallelism = 1, int maxItemsPerTask = Int32.MaxValue, TaskScheduler scheduler = null)
        {
            return new AsyncCall<T>(actionHandler, maxDegreeOfParallelism, maxItemsPerTask, scheduler);
        }
        /// <summary>
        /// Initializes the AsyncCall with a function to execute for each element.  The function returns an Task 
        /// that represents the asynchronous completion of that element's processing.
        /// </summary>
        /// <param name="functionHandler">The function to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="scheduler">The scheduler to use.  If null, the default scheduler is used.</param>
        public static AsyncCall<T> Create<T>(Func<T, Task> functionHandler, int maxDegreeOfParallelism = 1, TaskScheduler scheduler = null)
        {
            return new AsyncCall<T>(functionHandler, maxDegreeOfParallelism, scheduler);
        }
        /// <summary>
        /// Initializes the AsyncCall in the specified AppDomain with an action to execute for each element.
        /// </summary>
        /// <param name="actionHandler">The action to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        /// <param name="maxItemsPerTask">The maximum number of items to be processed per task.  If not specified, Int32.MaxValue is used.</param>
        public static AsyncCall<T> CreateInTargetAppDomain<T>(AppDomain targetDomain, Action<T> actionHandler, int maxDegreeOfParallelism = 1, int maxItemsPerTask = Int32.MaxValue)
        {
            return (AsyncCall<T>)targetDomain.CreateInstanceAndUnwrap(
                typeof(AsyncCall<T>).Assembly.FullName, typeof(AsyncCall<T>).FullName,
                false, global::System.Reflection.BindingFlags.CreateInstance, null,
                new object[] { actionHandler, maxDegreeOfParallelism, maxItemsPerTask, null },
                null, null);
        }
        /// <summary>
        /// Initializes the AsyncCall in the specified AppDomain with a function to execute for each element.  
        /// The function returns an Task that represents the asynchronous completion of that element's processing.
        /// </summary>
        /// <param name="targetDomain">An instance of current app domain</param>
        /// <param name="functionHandler">The action to run for every posted item.</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism to use.  If not specified, 1 is used for serial execution.</param>
        public static AsyncCall<T> CreateInTargetAppDomain<T>(AppDomain targetDomain, Func<T, Task> functionHandler, int maxDegreeOfParallelism = 1)
        {
            return (AsyncCall<T>)targetDomain.CreateInstanceAndUnwrap(
                typeof(AsyncCall<T>).Assembly.FullName, typeof(AsyncCall<T>).FullName,
                false, BindingFlags.CreateInstance, null,
                new object[] { functionHandler, maxDegreeOfParallelism, null },
                null, null);
        }
    }
}