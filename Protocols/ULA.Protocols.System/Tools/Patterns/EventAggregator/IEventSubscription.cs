using System;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Defines a contract for an event subscription to be used by 
    /// <see cref="EventBase"/>
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        /// Gets or sets a <see cref="SubscriptionToken"/> that identifies this <see cref="IEventSubscription"/>
        /// </summary>
        /// <value>
        /// A token that identifies this <see cref="IEventSubscription"/>
        /// </value>
        SubscriptionToken SubscriptionToken { get; set; }

        /// <summary>
        /// Gets the execution strategy to publish this event.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Action`1"/> with the execution strategy, or <see langword="null"/>
        /// if the <see cref="IEventSubscription"/> is no longer valid
        /// </returns>
        Action<object[]> GetExecutionStrategy();
    }
}