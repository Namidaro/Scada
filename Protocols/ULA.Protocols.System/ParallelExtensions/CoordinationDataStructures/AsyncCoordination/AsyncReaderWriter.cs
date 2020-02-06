using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures.AsyncCoordination
{
    /// <summary>
    /// Provides for asynchronous exclusive and concurrent execution support.
    /// </summary>
    [DebuggerDisplay("WaitingConcurrent={WaitingConcurrent}, WaitingExclusive={WaitingExclusive}, CurrentReaders={CurrentConcurrent}, Exclusive={CurrentlyExclusive}")]
    public sealed class AsyncReaderWriter
    {
        #region [Private fields]
        /// <summary>
        /// The lock that protects all shared state in this instance.
        /// </summary>
        private readonly object _lock = new object();
        /// <summary>
        /// The queue of concurrent readers waiting to execute.
        /// </summary>
        private readonly Queue<Task> _waitingConcurrent = new Queue<Task>();
        /// <summary>
        /// The queue of exclusive writers waiting to execute.
        /// </summary>
        private readonly Queue<Task> _waitingExclusive = new Queue<Task>();
        /// <summary>
        /// The number of concurrent readers currently executing.
        /// </summary>
        private int _currentConcurrent;
        /// <summary>
        /// The number of exclusive writers currently executing.
        /// </summary>
        private bool _currentlyExclusive;
        /// <summary>
        /// The non-generic factory to use for task creation.
        /// </summary>
        private readonly TaskFactory _factory; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the ReaderWriterAsync.
        /// </summary>
        public AsyncReaderWriter()
        {
            this._factory = Task.Factory;
        }
        /// <summary>
        /// Initializes the ReaderWriterAsync with the specified TaskFactory for us in creating all tasks.
        /// </summary>
        /// <param name="factory">The TaskFactory to use to create all tasks.</param>
        public AsyncReaderWriter(TaskFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            this._factory = factory;
        } 
        #endregion [Ctor's]


        #region [Properties]
        /// <summary>
        /// Gets the number of exclusive operations currently queued.
        /// </summary>
        public int WaitingExclusive
        {
            get
            {
                lock (this._lock)
                    return this._waitingExclusive.Count;
            }
        }
        /// <summary>
        /// Gets the number of concurrent operations currently queued.
        /// </summary>
        public int WaitingConcurrent
        {
            get
            {
                lock (this._lock)
                    return this._waitingConcurrent.Count;
            }
        }
        /// <summary>
        /// Gets the number of concurrent operations currently executing.
        /// </summary>
        public int CurrentConcurrent
        {
            get
            {
                lock (this._lock)
                    return this._currentConcurrent;
            }
        }
        /// <summary>
        /// Gets whether an exclusive operation is currently executing.
        /// </summary>
        public bool CurrentlyExclusive
        {
            get
            {
                lock (this._lock)
                    return this._currentlyExclusive;
            }
        } 
        #endregion [Properties]


        #region [Public members]
        /// <summary>
        /// Queues an exclusive writer action to the ReaderWriterAsync.
        /// </summary>
        /// <param name="action">The action to be executed exclusively.</param>
        /// <returns>A Task that represents the execution of the provided action.</returns>
        public Task QueueExclusiveWriter(Action action)
        {
            // Create the task.  This Task will be started by the coordination primitive
            // when it's safe to do so, e.g. when there are no other tasks associated
            // with this async primitive executing.
            var task = this._factory.Create(state =>
            {
                // Run the user-provided action
                try
                {
                    ((Action)state)();
                }
                // Ensure that we clean up when we're done
                finally
                {
                    this.FinishExclusiveWriter();
                }
            }, action);

            // Now that we've created the task, we need to do something with it, either queueing it or scheduling it immediately
            lock (this._lock)
            {
                // If there's already a task running, or if there are any other exclusive tasks that need to run,
                // queue it.  Otherwise, no one else is running or wants to run, so schedule it now.
                if (this._currentlyExclusive || this._currentConcurrent > 0 || this._waitingExclusive.Count > 0)
                    this._waitingExclusive.Enqueue(task);
                else this.RunExclusive_RequiresLock(task);
            }

            // Return the created task for the caller to track.
            return task;
        }
        /// <summary>
        /// Queues an exclusive writer function to the ReaderWriterAsync.
        /// </summary>
        /// <param name="function">The function to be executed exclusively.</param>
        /// <returns>A Task that represents the execution of the provided function.</returns>
        public Task<TResult> QueueExclusiveWriter<TResult>(Func<TResult> function)
        {
            // Create the task.  This Task will be started by the coordination primitive
            // when it's safe to do so, e.g. when there are no other tasks associated
            // with this async primitive executing.
            var task = this._factory.Create(state =>
            {
                // Run the user-provided function
                try
                {
                    return ((Func<TResult>)state)();
                }
                // Ensure that we clean up when we're done
                finally
                {
                    this.FinishExclusiveWriter();
                }
            }, function);

            // Now that we've created the task, we need to do something with it, either queueing it or scheduling it immediately
            lock (this._lock)
            {
                // If there's already a task running, or if there are any other exclusive tasks that need to run,
                // queue it.  Otherwise, no one else is running or wants to run, so schedule it now.
                if (this._currentlyExclusive || this._currentConcurrent > 0 || this._waitingExclusive.Count > 0)
                    this._waitingExclusive.Enqueue(task);
                else this.RunExclusive_RequiresLock(task);
            }

            // Return the created task for the caller to track.
            return task;
        }
        /// <summary>
        /// Queues a concurrent reader action to the ReaderWriterAsync.
        /// </summary>
        /// <param name="action">The action to be executed concurrently.</param>
        /// <returns>A Task that represents the execution of the provided action.</returns>
        public Task QueueConcurrentReader(Action action)
        {
            // Create the task.  This Task will be started by the coordination primitive
            // when it's safe to do so, e.g. when there are no exclusive tasks running
            // or waiting to run.
            var task = this._factory.Create(state =>
            {
                // Run the user-provided action
                try
                {
                    ((Action)state)();
                }
                // Ensure that we clean up when we're done
                finally
                {
                    this.FinishConcurrentReader();
                }
            }, action);

            // Now that we've created the task, we need to do something with it, either queueing it or scheduling it immediately
            lock (this._lock)
            {
                // If there are any exclusive tasks running or waiting, queue the concurrent task
                if (this._currentlyExclusive || this._waitingExclusive.Count > 0)
                    this._waitingConcurrent.Enqueue(task);
                // Otherwise schedule it immediately
                else this.RunConcurrent_RequiresLock(task);
            }

            // Return the task to the caller.
            return task;
        }
        /// <summary>
        /// Queues a concurrent reader function to the ReaderWriterAsync.
        /// </summary>
        /// <param name="function">The function to be executed concurrently.</param>
        /// <returns>A Task that represents the execution of the provided function.</returns>
        public Task<TResult> QueueConcurrentReader<TResult>(Func<TResult> function)
        {
            // Create the task.  This Task will be started by the coordination primitive
            // when it's safe to do so, e.g. when there are no exclusive tasks running
            // or waiting to run.
            var task = this._factory.Create(state =>
            {
                // Run the user-provided function
                try
                {
                    return ((Func<TResult>)state)();
                }
                // Ensure that we clean up when we're done
                finally
                {
                    this.FinishConcurrentReader();
                }
            }, function);

            // Now that we've created the task, we need to do something with it, either queueing it or scheduling it immediately
            lock (this._lock)
            {
                // If there are any exclusive tasks running or waiting, queue the concurrent task
                if (this._currentlyExclusive || this._waitingExclusive.Count > 0)
                    this._waitingConcurrent.Enqueue(task);
                // Otherwise schedule it immediately
                else this.RunConcurrent_RequiresLock(task);
            }

            // Return the task to the caller.
            return task;
        } 
        #endregion [Public members]



        #region [Private fields]
        /// <summary>
        /// Starts the specified exclusive task.
        /// </summary>
        /// <param name="exclusive">The exclusive task to be started.</param>
        /// <remarks>This must only be executed while holding the instance's lock.</remarks>
        private void RunExclusive_RequiresLock(Task exclusive)
        {
            this._currentlyExclusive = true;
            exclusive.Start(this._factory.GetTargetScheduler());
        }
        /// <summary>
        /// Starts the specified concurrent task.
        /// </summary>
        /// <param name="concurrent">The exclusive task to be started.</param>
        /// <remarks>This must only be executed while holding the instance's lock.</remarks>
        private void RunConcurrent_RequiresLock(Task concurrent)
        {
            this._currentConcurrent++;
            concurrent.Start(this._factory.GetTargetScheduler());
        }
        /// <summary>
        /// Starts all queued concurrent tasks.
        /// </summary>
        /// <remarks>This must only be executed while holding the instance's lock.</remarks>
        private void RunConcurrent_RequiresLock()
        {
            while (this._waitingConcurrent.Count > 0)
                this.RunConcurrent_RequiresLock(this._waitingConcurrent.Dequeue());
        }
        /// <summary>
        /// Completes the processing of a concurrent reader.
        /// </summary>
        private void FinishConcurrentReader()
        {
            lock (this._lock)
            {
                // Update the tracking count of the number of concurrently executing tasks
                this._currentConcurrent--;

                // If we've now hit zero tasks running concurrently and there are any waiting writers, run one of them
                if (this._currentConcurrent == 0 && this._waitingExclusive.Count > 0)
                    this.RunExclusive_RequiresLock(this._waitingExclusive.Dequeue());

                // Otherwise, if there are no waiting writers but there are waiting readers for some reason (they should
                // have started when they were added by the user), run all concurrent tasks waiting.
                else if (this._waitingExclusive.Count == 0 && this._waitingConcurrent.Count > 0)
                    this.RunConcurrent_RequiresLock();
            }
        }
        /// <summary>
        /// Completes the processing of an exclusive writer.
        /// </summary>
        private void FinishExclusiveWriter()
        {
            lock (this._lock)
            {
                // We're no longer executing exclusively, though this might get reversed shortly
                this._currentlyExclusive = false;

                // If there are any more waiting exclusive tasks, run the next one in line
                if (this._waitingExclusive.Count > 0)
                    this.RunExclusive_RequiresLock(this._waitingExclusive.Dequeue());

                // Otherwise, if there are any waiting concurrent tasks, run them all
                else if (this._waitingConcurrent.Count > 0)
                    this.RunConcurrent_RequiresLock();
            }
        } 
        #endregion [Private fields]
    }
}