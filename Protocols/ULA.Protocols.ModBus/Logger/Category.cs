namespace YP.Toolkit.Protocols.ModBus.Logger
{
    /// <summary>
    /// Defines values for the categories used by <see cref="ILogService"/>
    /// </summary>
    public enum Category
    {
        /// <summary>
        /// Debug category
        /// </summary>
        DEBUG = 0,
        /// <summary>
        /// Exception category
        /// </summary>
        ERROR = 1,
        /// <summary>
        /// Informational category
        /// </summary>
        INFO = 2,
        /// <summary>
        /// Warning category
        /// </summary>
        WARN = 3
    }
}
