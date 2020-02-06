using System;
using System.Collections.Generic;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Represents an event aggregation functionality
    /// </summary>
    public class EventAggregator : IEventAggregator
    {
        #region [Private fields]
        private readonly Dictionary<Type, EventBase> _events = new Dictionary<Type, EventBase>(); 
        #endregion [Private fields]


        #region [IEventAggregator members]
        /// <summary>
        /// Gets an event of <see cref="TEventType"/>
        /// </summary>
        /// <typeparam name="TEventType">The type of an event to get</typeparam>
        /// <returns>An instance of got event</returns>
        public TEventType GetEvent<TEventType>() where TEventType : EventBase, new()
        {
            EventBase eventBase;
            if (this._events.TryGetValue(typeof(TEventType), out eventBase))
                return (TEventType)eventBase;
            var instance = Activator.CreateInstance<TEventType>();
            this._events[typeof(TEventType)] = instance;
            return instance;
        } 
        #endregion [IEventAggregator members]
    }
}