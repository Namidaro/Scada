using System;
using System.Threading;
using System.Threading.Tasks;

namespace YP.Toolkit.System.ParallelExtensions
{
    public static partial class TaskExtras
    {
        #region Observables
        /// <summary>
        /// Creates an IObservable that represents the completion of a Task.
        /// </summary>
        /// <typeparam name="TResult">Specifies the type of data returned by the Task.</typeparam>
        /// <param name="task">The Task to be represented as an IObservable.</param>
        /// <returns>An IObservable that represents the completion of the Task.</returns>
        public static IObservable<TResult> ToObservable<TResult>(this Task<TResult> task)
        {
            if (task == null) throw new ArgumentNullException("task");
            return new TaskObservable<TResult> { _task = task };
        }

        /// <summary>
        /// An implementation of IObservable that wraps a Task.
        /// </summary>
        /// <typeparam name="TResult">The type of data returned by the task.</typeparam>
        private class TaskObservable<TResult> : IObservable<TResult>
        {
            internal Task<TResult> _task;

            public IDisposable Subscribe(IObserver<TResult> observer)
            {
                // Validate arguments
                if (observer == null) throw new ArgumentNullException("observer");

                // Support cancelling the continuation if the observer is unsubscribed
                var cts = new CancellationTokenSource();

                // Create a continuation to pass data along to the observer
                _task.ContinueWith(t =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            observer.OnNext(_task.Result);
                            observer.OnCompleted();
                            break;

                        case TaskStatus.Faulted:
                            if (_task != null && _task.Exception != null)
                                observer.OnError(_task.Exception);
                            break;

                        case TaskStatus.Canceled:
                            observer.OnError(new TaskCanceledException(t));
                            break;
                    }
                }, cts.Token);

                // Support unsubscribe simply by canceling the continuation if it hasn't yet run
                return new CancelOnDispose { _source = cts };
            }
        }

        /// <summary>
        /// Translate a call to IDisposable.Dispose to a CancellationTokenSource.Cancel.
        /// </summary>
        private class CancelOnDispose : IDisposable
        {
            internal CancellationTokenSource _source;
            void IDisposable.Dispose() { _source.Cancel(); }
        }
        #endregion
    }
}