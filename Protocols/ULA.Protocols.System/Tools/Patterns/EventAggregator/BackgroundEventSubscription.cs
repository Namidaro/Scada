using System;
using System.Threading;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Extends <see cref="EventSubscription{TPayload}"/> to invoke the <see cref="EventSubscription{TPayload}.Action"/> delegate in a background thread.
    /// </summary>
    /// <typeparam name="TPayload">The type to use for the generic <see cref="T:System.Action`1"/> 
    /// and <see cref="T:System.Predicate`1"/> types.</typeparam>
    public class BackgroundEventSubscription<TPayload> : EventSubscription<TPayload>
    {
        #region [Ctor's]
        /// <summary>
        /// Creates a new instance of <see cref="BackgroundEventSubscription{TPayload}"/>.
        /// </summary>
        /// <param name="actionReference">A reference to a delegate of type <see cref="T:System.Action`1"/>.</param>
        /// <param name="filterReference">A reference to a delegate of type <see cref="T:System.Predicate`1"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="actionReference"/> or <see paramref="filterReference"/> are <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException">When the target of <paramref name="actionReference"/> is not of type <see cref="T:System.Action`1"/>,
        /// or the target of <paramref name="filterReference"/> is not of type <see cref="T:System.Predicate`1"/>.
        /// </exception>
        public BackgroundEventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
            : base(actionReference, filterReference)
        { }
        /// <summary>
        /// Invokes the specified <see cref="T:System.Action`1"/> in an asynchronous thread by using a <see cref="T:System.Threading.ThreadPool"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="argument">The payload to pass <paramref name="action"/> while invoking it.</param>
        public override void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            ThreadPool.QueueUserWorkItem(o => action(argument));
        } 
        #endregion [Ctor's]
    }
}