namespace YP.Toolkit.Protocols.ModBus.Logger
{
    /// <summary>
    /// Defines values for the priorities used by <see cref="ILogService"/>
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// No priority specified
        /// </summary>
        NONE = 0,
        /// <summary>
        /// High priority entry
        /// </summary>
        HIGH = 1,
        /// <summary>
        /// Medium priority entry
        /// </summary>
        MEDIUM = 2,
        /// <summary>
        /// Low priority entry
        /// </summary>
        LOW = 3
    }
}