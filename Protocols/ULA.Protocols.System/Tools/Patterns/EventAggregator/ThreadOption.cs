namespace YP.Toolkit.System.Tools.Patterns.EventAggregator
{
    /// <summary>
    /// Specifies on which thread a <see cref="CompositeEvent{TPayload}"/> subscriber will be called.
    /// </summary>
    public enum ThreadOption
    {
        /// <summary>
        /// Indicates that event will be launched in publisher thread 
        /// </summary>
        PUBLISHER_THREAD,
        /// <summary>
        /// Indicates that event will be launched in his own background thread 
        /// </summary>
        BACKGROUND_THREAD

    }
}