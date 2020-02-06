using System;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Provides a way to retrieve a <see cref="T:System.Delegate"/> to execute an action depending
    /// on the value of a second filter predicate that returns true if the action should execute.
    /// </summary>
    /// <typeparam name="TPayload">The type to use for the generic <see cref="T:System.Action`1"/> and
    /// <see cref="T:System.Predicate`1"/> types.</typeparam>
    public class EventSubscription<TPayload> : IEventSubscription
    {
        #region [Private fields]
        private readonly IDelegateReference _actionReference;
        private readonly IDelegateReference _filterReference; 
        #endregion [Private fields]

        #region [Properties]
        /// <summary>
        /// Gets the target <see cref="T:System.Action`1"/> that is referenced by the
        /// <see cref="IDelegateReference"/>.
        /// </summary>
        /// <value>
        /// An <see cref="T:System.Action`1"/> or <see langword="null"/> 
        /// if the referenced target is not alive.
        /// </value>
        public Action<TPayload> Action
        {
            get
            {
                return (Action<TPayload>)this._actionReference.Target;
            }
        }
        /// <summary>
        /// Gets the target <see cref="T:System.Predicate`1"/> that is referenced by the 
        /// <see cref="IDelegateReference"/>.
        /// </summary>
        /// <value>
        /// An <see cref="T:System.Predicate`1"/> or <see langword="null"/> if the referenced target 
        /// is not alive.
        /// </value>
        public Predicate<TPayload> Filter
        {
            get
            {
                return (Predicate<TPayload>)this._filterReference.Target;
            }
        }
        /// <summary>
        /// Gets or sets a <see cref="SubscriptionToken"/> that identifies this 
        /// <see cref="IEventSubscription"/>.
        /// </summary>
        /// <value>
        /// A token that identifies this <see cref="IEventSubscription"/>.
        /// </value>
        public SubscriptionToken SubscriptionToken { get; set; } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Creates a new instance of <see cref="EventSubscription{TPayload}"/>.
        /// </summary>
        /// <param name="actionReference">A reference to a delegate of type <see cref="T:System.Action`1"/>.</param>
        /// <param name="filterReference">A reference to a delegate of type <see cref="T:System.Predicate`1"/>.</param>
        /// <exception cref="T:System.ArgumentNullException">When <paramref name="actionReference"/> or <see paramref="filterReference"/> 
        /// are <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException">When the target of <paramref name="actionReference"/> is not of type <see cref="T:System.Action`1"/>,
        /// or the target of <paramref name="filterReference"/> is not of type <see cref="T:System.Predicate`1"/>.
        /// </exception>
        public EventSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
        {
            Guard.AgainstNullReference(actionReference, "actionReference");
            Guard.AgainstNotOfType<Action<TPayload>>(actionReference.Target);
            Guard.AgainstNullReference(filterReference, "filterReference");
            Guard.AgainstNotOfType<Predicate<TPayload>>(filterReference.Target);

            this._actionReference = actionReference;
            this._filterReference = filterReference;
        } 
        #endregion [Ctor's]

        #region [Public members]
        /// <summary>
        /// Gets the execution strategy to publish this event.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Action`1"/> with the execution strategy, or <see langword="null"/> if the <see cref="IEventSubscription"/> is no longer valid.
        /// </returns>
        /// <remarks>
        /// If <see cref="EventSubscription{TPayload}.Action"/> or <see cref="EventSubscription{TPayload}.Filter"/> are no longer valid because they were
        /// garbage collected, this method will return <see langword="null"/>.
        /// Otherwise it will return a delegate that evaluates the <see cref="EventSubscription{TPayload}.Filter"/> and if it
        /// returns <see langword="true"/> will then call <see cref="EventSubscription{TPayload}.InvokeAction"/>.
        /// The returned delegate holds hard references to the <see cref="EventSubscription{TPayload}.Action"/> and <see cref="EventSubscription{TPayload}.Filter"/> target
        /// <see cref="T:System.Delegate">delegates</see>. As long as the returned delegate is not garbage collected,
        /// the <see cref="EventSubscription{TPayload}.Action"/> and <see cref="EventSubscription{TPayload}.Filter"/> references delegates won't get collected either.
        /// </remarks>
        public virtual Action<object[]> GetExecutionStrategy()
        {
            var action = this.Action;
            var filter = this.Filter;
            if (action == null || filter == null) return null;
            return arguments =>
                {
                    var payLoad = default(TPayload);
                    if (arguments != null && arguments.Length > 0 && arguments[0] != null)
                        payLoad = (TPayload)arguments[0];
                    if (!filter(payLoad))
                        return;
                    this.InvokeAction(action, payLoad);
                };
        }
        /// <summary>
        /// Invokes the specified <see cref="T:System.Action`1"/> synchronously when not overriden.
        /// </summary>
        /// <param name="action">The action to execute.</param><param name="argument">The payload to pass <paramref name="action"/> while invoking it.</param><exception cref="T:System.ArgumentNullException">An <see cref="T:System.ArgumentNullException"/> is thrown if <paramref name="action"/> is null.</exception>
        public virtual void InvokeAction(Action<TPayload> action, TPayload argument)
        {
            Guard.AgainstNullReference(action, "action");
            action(argument);
        } 
        #endregion [Public members]
    }
}