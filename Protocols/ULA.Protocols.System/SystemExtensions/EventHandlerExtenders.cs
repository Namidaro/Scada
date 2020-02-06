using System;
using System.ComponentModel;

namespace YP.Toolkit.System.SystemExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventHandlerExtenders
    {
        /// <summary>
        /// Raises an event of any type that implements the standard event signature 
        /// (object sender, :EventArgs e) on the current thread.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the EventHandler used to raise the event.</typeparam>
        /// <param name="eventHandler">The EventHandler instance use to raise the event.</param>
        /// <param name="sender">The sender object instance to pass to subscribers.</param>
        /// <param name="e">The EventArgs (or derivative) to pass to subscribers.</param>
        public static void RaiseEvent<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender,
                                                  TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler != null)
                eventHandler(sender, e);
        }

        /// <summary>
        /// Raises an event of any type that implements the standard event signature 
        /// (object sender, :EventArgs e) on the event subscribers UI thread if possible.
        /// </summary>
        /// <typeparam name="TEventArgs">The type of the EventHandler used to raise the event.</typeparam>
        /// <param name="eventHandler">The EventHandler instance use to raise the event.</param>
        /// <param name="sender">The sender object instance to pass to subscribers.</param>
        /// <param name="e">The EventArgs (or derivative) to pass to subscribers.</param>
        public static void RaiseEventOnUiThread<TEventArgs>(
            this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null) return;
            foreach (EventHandler<TEventArgs> singleCast in eventHandler.GetInvocationList())
            {
                var syncInvoke = singleCast.Target as ISynchronizeInvoke;
                if (syncInvoke != null && syncInvoke.InvokeRequired)
                {
                    // Invoke the event on the event subscribers thread
                    syncInvoke.Invoke(eventHandler, new[] { sender, e });
                }
                else
                {
                    // Raise the event on this thread
                    singleCast(sender, e);
                }
            }
        }

        /// <summary>
        /// Raises an event.
        /// </summary>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender of the event.</param>
        public static void Raise(this EventHandler handler, object sender)
        {
            if (handler == null)
                return;
            handler(sender, EventArgs.Empty);
        }
        /// <summary>
        /// Raises an event.
        /// </summary>
        /// <typeparam name="T">The type of event arguments.</typeparam>
        /// <param name="handler">The event handler.</param>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">The instance of the event arguments.</param>
        public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
        {
            if (handler == null)
                return;
            handler(sender, e);
        }
    }
}
