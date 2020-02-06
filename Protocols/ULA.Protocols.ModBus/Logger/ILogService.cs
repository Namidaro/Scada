using System;

namespace YP.Toolkit.Protocols.ModBus.Logger
{
    /// <summary>
    /// Exposes a logger functionality
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Write a new log entry as information
        /// </summary>
        /// <param name="message">Message body to log</param>
        void Log(string message);
        /// <summary>
        /// Write a new log entry with the specified category and priority
        /// </summary>
        /// <param name="message">Message body to log</param>
        /// <param name="category">Category of the entry</param>
        /// <param name="priority">The priority of the entry</param>
        void Log(string message, Category category, Priority priority);
        /// <summary>
        /// Write a new log entry with the specified category and priority
        /// </summary>
        /// <param name="message">Message body to log</param>
        /// <param name="exception">Exception to log</param>
        /// <param name="category">Category of the entry</param>
        /// <param name="priority">The priority of the entry</param>
        void Log(string message, Exception exception, Category category, Priority priority);
    }
}