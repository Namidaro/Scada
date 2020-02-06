namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Exposes the event aggregator functionality that is responsible for locating or
    /// building events and for keeping a collection of the events in the system
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Gets an event of <see cref="TEventType"/>
        /// </summary>
        /// <typeparam name="TEventType">The type of an event to get</typeparam>
        /// <returns>An instance of got event</returns>
        TEventType GetEvent<TEventType>() where TEventType : EventBase, new();
    }
}