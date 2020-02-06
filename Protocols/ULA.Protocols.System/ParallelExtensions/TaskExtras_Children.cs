using System;
using System.Threading;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions
{
    public static partial  class TaskExtras
    {
        #region Children
        /// <summary>
        /// Ensures that a parent task can't transition into a completed state
        /// until the specified task has also completed, even if it's not
        /// already a child task.
        /// </summary>
        /// <param name="task">The task to attach to the current task as a child.</param>
        public static void AttachToParent(this Task task)
        {
            if (task == null) throw new ArgumentNullException("task");
            task.ContinueWith(t => t.Wait(), CancellationToken.None,
                TaskContinuationOptions.AttachedToParent |
                TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
        }
        #endregion
    }
}