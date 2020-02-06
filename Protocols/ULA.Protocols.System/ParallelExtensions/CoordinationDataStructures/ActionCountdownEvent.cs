using System;
using System.Threading;

namespace YP.Toolkit.System.ParallelExtensions.CoordinationDataStructures
{
    /// <summary>
    /// Runs an action when the CountdownEvent reaches zero.
    /// </summary>
    public class ActionCountdownEvent : IDisposable
    {
        #region [Private fields]
        private readonly CountdownEvent _event;
        private readonly Action _action;
        private readonly ExecutionContext _context; 
        #endregion [Private fields]


        #region [Ctor's]
        /// <summary>
        /// Initializes the ActionCountdownEvent.
        /// </summary>
        /// <param name="initialCount">The number of signals required to set the CountdownEvent.</param>
        /// <param name="action">The delegate to be invoked when the count reaches zero.</param>
        public ActionCountdownEvent(int initialCount, Action action)
        {
            // Validate arguments
            if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
            if (action == null) throw new ArgumentNullException("action");

            // Store the action and create the event from the initial count. If the initial count forces the
            // event to be set, run the action immediately. Otherwise, capture the current execution context
            // so we can run the action in the right context later on.
            this._action = action;
            this._event = new CountdownEvent(initialCount);
            if (initialCount == 0)
                action();
            else
                this._context = ExecutionContext.Capture();
        } 
        #endregion [Ctor's]


        #region [Public members]
        /// <summary>
        /// Increments the current count by one.
        /// </summary>
        public void AddCount()
        {
            // Just delegate to the underlying event
            this._event.AddCount();
        }
        /// <summary>
        /// Registers a signal with the event, decrementing its count.
        /// </summary>
        public void Signal()
        {
            // If signaling the event causes it to become set
            if (!this._event.Signal()) return;
            // Execute the action.  If we were able to capture a context
            // at instantiation time, use that context to execute the action.
            // Otherwise, just run the action.
            if (this._context != null)
            {
                ExecutionContext.Run(this._context, _ => this._action(), null);
            }
            else this._action();
        } 
        #endregion [Public members]


        #region [IDisposable members]
        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }
        /// <summary>
        /// Releases all resources used by the current instance.
        /// </summary>
        /// <param name="disposing">
        /// true if called because the object is being disposed; otherwise, false.
        /// </param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
                this._event.Dispose();
        } 
        #endregion [IDisposable members]
    }
}