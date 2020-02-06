using System;
using System.Linq;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Defines a class that manages publication and subscription to events.
    /// </summary>
    /// <typeparam name="TPayload">The type of message that will be passed to the subscribers.</typeparam>
    public class CompositeEvent<TPayload> : EventBase
    {
        #region [Public members]
        /// <summary>
        /// Subscribes a delegate to an event that will be published on the <see cref="ThreadOption.PUBLISHER_THREAD"/>
        /// <see cref="CompositeEvent{TPayload}"/> will maintain a 
        /// <seealso cref="T:System.WeakReference"/> to the target of the supplied <paramref name="action"/> delegate
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published</param>
        /// <returns>
        /// A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription
        /// </returns>
        /// <remarks>
        /// The CompositePresentationEvent collection is thread-safe
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action)
        {
            return this.Subscribe(action, ThreadOption.PUBLISHER_THREAD);
        }
        /// <summary>
        /// Subscribes a delegate to an event
        /// CompositePresentationEvent will maintain a <seealso cref="T:System.WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is raised</param>
        /// <param name="threadOption">Specifies on which thread to receive the delegate callback</param>
        /// <returns>
        /// A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.
        /// </returns>
        /// <remarks>
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption)
        {
            return this.Subscribe(action, threadOption, false);
        }
        /// <summary>
        /// Subscribes a delegate to an event that will be published on the 
        /// <see cref="ThreadOption.PUBLISHER_THREAD"/>.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="CompositeEvent{TPayload}"/>
        /// keeps a reference to the subscriber so it does not get garbage collected.</param>
        /// <returns>
        /// A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.
        /// </returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false"/>, <see cref="CompositeEvent{TPayload}"/> 
        /// will maintain a <seealso cref="T:System.WeakReference"/> to the Target of the supplied 
        /// <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true"/>),
        /// the user must explicitly call Unsubscribe for the event when disposing the subscriber
        /// in order to avoid memory leaks or unexepcted behavior.
        /// <para/>
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, bool keepSubscriberReferenceAlive)
        {
            return this.Subscribe(action, ThreadOption.PUBLISHER_THREAD, keepSubscriberReferenceAlive);
        }
        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="threadOption">Specifies on which thread to receive the delegate callback.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="CompositeEvent{TPayload}"/>
        /// keeps a reference to the subscriber so it does not get garbage collected.
        /// </param>
        /// <returns>
        /// A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.
        /// </returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false"/>, <see cref="CompositeEvent{TPayload}"/> 
        /// will maintain a <seealso cref="T:System.WeakReference"/> to the Target of the supplied <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true"/>),
        /// the user must explicitly call Unsubscribe for the event when disposing
        /// the subscriber in order to avoid memory leaks or unexepcted behavior.
        /// <para/>
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive)
        {
            return this.Subscribe(action, threadOption, keepSubscriberReferenceAlive, null);
        }
        /// <summary>
        /// Subscribes a delegate to an event.
        /// </summary>
        /// <param name="action">The delegate that gets executed when the event is published.</param>
        /// <param name="threadOption">Specifies on which thread to receive the delegate callback.</param>
        /// <param name="keepSubscriberReferenceAlive">When <see langword="true"/>, the <seealso cref="CompositeEvent{TPayload}"/> 
        /// keeps a reference to the subscriber so it does not get garbage collected.
        /// </param>
        /// <param name="filter">Filter to evaluate if the subscriber should receive the event.</param>
        /// <returns>
        /// A <see cref="SubscriptionToken"/> that uniquely identifies the added subscription.
        /// </returns>
        /// <remarks>
        /// If <paramref name="keepSubscriberReferenceAlive"/> is set to <see langword="false"/>, <see cref="CompositeEvent{TPayload}"/> 
        /// will maintain a <seealso cref="T:System.WeakReference"/> to the Target of the supplied 
        /// <paramref name="action"/> delegate.
        /// If not using a WeakReference (<paramref name="keepSubscriberReferenceAlive"/> is <see langword="true"/>),
        /// the user must explicitly call Unsubscribe for the event when disposing the subscriber 
        /// in order to avoid memory leaks or unexepcted behavior.
        /// The CompositePresentationEvent collection is thread-safe.
        /// </remarks>
        public virtual SubscriptionToken Subscribe(Action<TPayload> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TPayload> filter)
        {
            var actionReference = (IDelegateReference)new DelegateReference(action, keepSubscriberReferenceAlive);
            var filterReference = filter == null ?
                new DelegateReference(new Predicate<TPayload>(b => true), true) :
                (IDelegateReference)new DelegateReference(filter, keepSubscriberReferenceAlive);
            EventSubscription<TPayload> eventSubscription;
            switch (threadOption)
            {
                case ThreadOption.PUBLISHER_THREAD:
                    eventSubscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
                case ThreadOption.BACKGROUND_THREAD:
                    eventSubscription = new BackgroundEventSubscription<TPayload>(actionReference, filterReference);
                    break;
                default:
                    eventSubscription = new EventSubscription<TPayload>(actionReference, filterReference);
                    break;
            }
            return this.InternalSubscribe(eventSubscription);
        }
        /// <summary>
        /// Publishes the <see cref="CompositeEvent{TPayload}"/>.
        /// </summary>
        /// <param name="payload">Message to pass to the subscribers.</param>
        public virtual void Publish(TPayload payload)
        {
            this.InternalPublish(new[] { (object)payload });
        }
        /// <summary>
        /// Removes the first subscriber matching <seealso cref="T:System.Action`1"/> from the subscribers' list.
        /// </summary>
        /// <param name="subscriber">The <see cref="T:System.Action`1"/> used when subscribing to the event.</param>
        public virtual void Unsubscribe(Action<TPayload> subscriber)
        {
            lock (this.Subscriptions)
            {
                IEventSubscription subscription = this.Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
                if (subscription == null) return;
                this.Subscriptions.Remove(subscription);
            }
        }
        /// <summary>
        /// Returns <see langword="true"/> if there is a subscriber matching <seealso cref="T:System.Action`1"/>.
        /// </summary>
        /// <param name="subscriber">The <see cref="T:System.Action`1"/> used when subscribing to the event.</param>
        /// <returns>
        /// <see langword="true"/> if there is an <seealso cref="T:System.Action`1"/> that matches; otherwise <see langword="false"/>.
        /// </returns>
        public virtual bool Contains(Action<TPayload> subscriber)
        {
            IEventSubscription eventSubscription;
            lock (this.Subscriptions)
                eventSubscription = this.Subscriptions.Cast<EventSubscription<TPayload>>().FirstOrDefault(evt => evt.Action == subscriber);
            return eventSubscription != null;
        } 
        #endregion [Public members]
    }
}