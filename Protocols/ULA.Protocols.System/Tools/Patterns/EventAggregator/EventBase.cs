using System;
using System.Collections.Generic;
using System.Linq;
using YP.Toolkit.System.Validation;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Defines a base class to publish and subscribe to events
    /// </summary>
    public abstract class EventBase
    {
        #region [Private fields]
        private readonly List<IEventSubscription> _subscriptions = new List<IEventSubscription>();
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets the list of current subscriptions
        /// </summary>
        /// <value>
        /// The current subscribers
        /// </value>
        protected ICollection<IEventSubscription> Subscriptions
        {
            get
            {
                return this._subscriptions;
            }
        }
        #endregion [Properties]


        #region [Public members]
        /// <summary>
        /// Removes the subscriber matching the <seealso cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> returned by <see cref="EventBase"/> 
        /// while subscribing to the event</param>
        public virtual void Unsubscribe(SubscriptionToken token)
        {
            lock (this.Subscriptions)
            {
                var subsciption = this.Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken.Equals(token));
                if (subsciption == null) return;
                this.Subscriptions.Remove(subsciption);
            }
        }
        /// <summary>
        /// Returns <see langword="true"/> if there is a subscriber matching <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> returned by <see cref="EventBase"/> 
        /// while subscribing to the event</param>
        /// <returns>
        /// <see langword="true"/> if there is a <see cref="SubscriptionToken"/> that matches;
        /// otherwise <see langword="false"/>
        /// </returns>
        public virtual bool Contains(SubscriptionToken token)
        {
            lock (this.Subscriptions)
                return this.Subscriptions.FirstOrDefault(evt => evt.SubscriptionToken.Equals(token)) != null;
        }
        #endregion [Public members]



        #region [Templated members]
        /// <summary>
        /// Adds the specified <see cref="IEventSubscription"/> to the subscribers' collection
        /// </summary>
        /// <param name="eventSubscription">The subscriber</param>
        /// <returns>
        /// The <see cref="SubscriptionToken"/> that uniquely identifies every subscriber
        /// </returns>
        /// <remarks>
        /// Adds the subscription to the internal list and assigns it a new <see cref="SubscriptionToken"/>
        /// </remarks>
        protected virtual SubscriptionToken InternalSubscribe(IEventSubscription eventSubscription)
        {
            Guard.AgainstNullReference(eventSubscription, "eventSubscription");
            eventSubscription.SubscriptionToken = new SubscriptionToken(this.Unsubscribe);
            lock (this.Subscriptions)
                this.Subscriptions.Add(eventSubscription);
            return eventSubscription.SubscriptionToken;
        }
        /// <summary>
        /// Calls all the execution strategies exposed by the list of 
        /// <see cref="IEventSubscription"/>.
        /// </summary>
        /// <param name="arguments">The arguments that will be passed to the listeners.</param>
        /// <remarks>
        /// Before executing the strategies, this class will prune all the subscribers from the
        /// list that return a <see langword="null"/><see cref="T:System.Action`1"/> when calling the
        /// <see cref="IEventSubscription.GetExecutionStrategy"/> method.
        /// </remarks>
        protected virtual void InternalPublish(params object[] arguments)
        {
            foreach (var action in this.PruneAndReturnStrategies())
                action(arguments);
        }
        #endregion [Templated members]


        #region [Help members]
        private IEnumerable<Action<object[]>> PruneAndReturnStrategies()
        {
            var list = new List<Action<object[]>>();
            lock (this.Subscriptions)
            {
                for (var i = this.Subscriptions.Count - 1; i >= 0; --i)
                {
                    var strategy = this._subscriptions[i].GetExecutionStrategy();
                    if (strategy == null) this._subscriptions.RemoveAt(i);
                    else list.Add(strategy);
                }
            }
            return list;
        }
        #endregion [Help members]
    }
}