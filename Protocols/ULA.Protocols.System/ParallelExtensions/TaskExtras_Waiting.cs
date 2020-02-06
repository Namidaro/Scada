using System;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions
{
    public static partial class TaskExtras
    {
        #region Waiting
        /// <summary>Waits for the task to complete execution, returning the task's final status.</summary>
        /// <param name="task">The task for which to wait.</param>
        /// <returns>The completion status of the task.</returns>
        /// <remarks>Unlike Wait, this method will not throw an exception if the task ends in the Faulted or Canceled state.</remarks>
        public static TaskStatus WaitForCompletionStatus(this Task task)
        {
            if (task == null) throw new ArgumentNullException("task");
            var status = task.Status;
            ((IAsyncResult)task).AsyncWaitHandle.WaitOne();
            return task.Status;
        }
        #endregion 
    }
}