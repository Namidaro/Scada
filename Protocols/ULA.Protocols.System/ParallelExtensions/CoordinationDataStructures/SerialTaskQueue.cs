using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Represents a queue of tasks to be started and executed serially.
    /// </summary>
    public class SerialTaskQueue
    {
        #region [Private fields]
        /// <summary>
        /// The ordered queue of tasks to be executed. Also serves as a lock protecting all shared state.
        /// </summary>
        private readonly Queue<object> _tasks = new Queue<object>();
        /// <summary>
        /// The task currently executing, or null if there is none.
        /// </summary>
        private Task _taskInFlight;
        #endregion [Private fields]


        #region [Public members]
        /// <summary>
        /// Enqueues the task to be processed serially and in order.
        /// </summary>
        /// <param name="taskGenerator">The function that generates a non-started task.</param>
        public void Enqueue(Func<Task> taskGenerator)
        {
            this.EnqueueInternal(taskGenerator);
        }
        /// <summary>
        /// Enqueues the non-started task to be processed serially and in order.
        /// </summary>
        /// <param name="task">The task.</param>
        public Task Enqueue(Task task)
        {
            this.EnqueueInternal(task);
            return task;
        }
        /// <summary>
        /// Gets a Task that represents the completion of all previously queued tasks.
        /// </summary>
        public Task Completed()
        {
            return this.Enqueue(new Task(() => { }));
        } 
        #endregion [Public members]


        #region [Help members]
        /// <summary>
        /// Enqueues the task to be processed serially and in order.
        /// </summary>
        /// <param name="taskOrFunction">The task or functino that generates a task.</param>
        /// <remarks>The task must not be started and must only be started by this instance.</remarks>
        private void EnqueueInternal(object taskOrFunction)
        {
            // Validate the task
            if (taskOrFunction == null) throw new ArgumentNullException("taskOrFunction");
            lock (this._tasks)
            {
                // If there is currently no task in flight, we'll start this one
                if (this._taskInFlight == null)
                    this.StartTask_CallUnderLock(taskOrFunction);
                // Otherwise, just queue the task to be started later
                else
                    this._tasks.Enqueue(taskOrFunction);
            }
        }
        /// <summary>
        /// Called when a Task completes to potentially start the next in the queue.
        /// </summary>
        /// <param name="ignored">The task that completed.</param>
        private void OnTaskCompletion(Task ignored)
        {
            lock (this._tasks)
            {
                // The task completed, so nothing is currently in flight.
                // If there are any tasks in the queue, start the next one.
                this._taskInFlight = null;
                if (this._tasks.Count > 0)
                    this.StartTask_CallUnderLock(this._tasks.Dequeue());
            }
        }
        /// <summary>
        /// Starts the provided task (or function that returns a task).
        /// </summary>
        /// <param name="nextItem">The next task or function that returns a task.</param>
        private void StartTask_CallUnderLock(object nextItem)
        {
            var next = nextItem as Task ?? ((Func<Task>)nextItem)();

            if (next.Status == TaskStatus.Created) next.Start();
            this._taskInFlight = next;
            next.ContinueWith(this.OnTaskCompletion);
        } 
        #endregion [Help members]
    }
}