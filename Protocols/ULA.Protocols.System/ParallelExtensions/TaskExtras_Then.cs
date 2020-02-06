using System;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions
{
    public static partial class TaskExtras
    {
        #region Then
        /// <summary>Creates a task that represents the completion of a follow-up action when a task completes.</summary>
        /// <param name="task">The task.</param>
        /// <param name="next">The action to run when the task completes.</param>
        /// <returns>The task that represents the completion of both the task and the action.</returns>
        public static Task Then(this Task task, Action next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next();
                        tcs.TrySetResult(null);
                    }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a follow-up function when a task completes.</summary>
        /// <param name="task">The task.</param>
        /// <param name="next">The function to run when the task completes.</param>
        /// <returns>The task that represents the completion of both the task and the function.</returns>
        public static Task<TResult> Then<TResult>(this Task task, Func<TResult> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TResult>();
            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        var result = next();
                        tcs.TrySetResult(result);
                    }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a follow-up action when a task completes.</summary>
        /// <param name="task">The task.</param>
        /// <param name="next">The action to run when the task completes.</param>
        /// <returns>The task that represents the completion of both the task and the action.</returns>
        public static Task Then<TResult>(this Task<TResult> task, Action<TResult> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        next(task.Result);
                        tcs.TrySetResult(null);
                    }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a follow-up function when a task completes.</summary>
        /// <param name="task">The task.</param>
        /// <param name="next">The function to run when the task completes.</param>
        /// <returns>The task that represents the completion of both the task and the function.</returns>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, TNewResult> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNewResult>();
            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    try
                    {
                        var result = next(task.Result);
                        tcs.TrySetResult(result);
                    }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
        /// <param name="task">The first task.</param>
        /// <param name="next">The function that produces the second task.</param>
        /// <returns>The task that represents the completion of both the first and second task.</returns>
        public static Task Then(this Task task, Func<Task> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate
            {
                // When the first task completes, if it faulted or was canceled, bail
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    // Otherwise, get the next task.  If it's null, bail.  If not,
                    // when it's done we'll have our result.
                    try { next().ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default); }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
        /// <param name="task">The first task.</param>
        /// <param name="next">The function that produces the second task based on the result of the first task.</param>
        /// <returns>The task that represents the completion of both the first and second task.</returns>
        public static Task Then<T>(this Task<T> task, Func<T, Task> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(delegate
            {
                // When the first task completes, if it faulted or was canceled, bail
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    // Otherwise, get the next task.  If it's null, bail.  If not,
                    // when it's done we'll have our result.
                    try { next(task.Result).ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default); }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
        /// <param name="task">The first task.</param>
        /// <param name="next">The function that produces the second task.</param>
        /// <returns>The task that represents the completion of both the first and second task.</returns>
        public static Task<TResult> Then<TResult>(this Task task, Func<Task<TResult>> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TResult>();
            task.ContinueWith(delegate
            {
                // When the first task completes, if it faulted or was canceled, bail
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    // Otherwise, get the next task.  If it's null, bail.  If not,
                    // when it's done we'll have our result.
                    try { next().ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default); }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }

        /// <summary>Creates a task that represents the completion of a second task when a first task completes.</summary>
        /// <param name="task">The first task.</param>
        /// <param name="next">The function that produces the second task based on the result of the first.</param>
        /// <returns>The task that represents the completion of both the first and second task.</returns>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, Task<TNewResult>> next)
        {
            if (task == null) throw new ArgumentNullException("task");
            if (next == null) throw new ArgumentNullException("next");

            var tcs = new TaskCompletionSource<TNewResult>();
            task.ContinueWith(delegate
            {
                // When the first task completes, if it faulted or was canceled, bail
                if (task.IsFaulted)
                {
                    if (task.Exception != null) 
                        tcs.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled) tcs.TrySetCanceled();
                else
                {
                    // Otherwise, get the next task.  If it's null, bail.  If not,
                    // when it's done we'll have our result.
                    try { next(task.Result).ContinueWith(t => tcs.TrySetFromTask(t), TaskScheduler.Default); }
                    catch (Exception exc) { tcs.TrySetException(exc); }
                }
            }, TaskScheduler.Default);
            return tcs.Task;
        }
        #endregion 
    }
}