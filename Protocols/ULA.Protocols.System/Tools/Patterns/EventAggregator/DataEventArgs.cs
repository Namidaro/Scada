using System;

namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Generic arguments class to pass to event handlers that need to receive data.
    /// </summary>
    /// <typeparam name="TData">The type of data to pass.</typeparam>
    public class DataEventArgs<TData> : EventArgs
    {
        #region [Private fields]
        private readonly TData _value; 
        #endregion [Private fields]


        #region [Properties]
        /// <summary>
        /// Gets the information related to the event.
        /// </summary>
        /// <value>
        /// Information related to the event.
        /// </value>
        public TData Value
        {
            get
            {
                return this._value;
            }
        } 
        #endregion [Properties]


        #region [Ctor's]
        /// <summary>
        /// Initializes the DataEventArgs class.
        /// </summary>
        /// <param name="value">Information related to the event.</param>
        public DataEventArgs(TData value)
        {
            this._value = value;
        } 
        #endregion [Ctor's]
    }
}