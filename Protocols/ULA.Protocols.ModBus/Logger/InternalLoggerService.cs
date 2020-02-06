using System;
using System.Diagnostics;


namespace YP.Toolkit.Protocols.ModBus.Logger
{
    /// <summary>
    /// Represents an internal logger functionality. This logger writes entries to the output window
    /// </summary>
    internal class InternalLoggerService : ILogService
    {

        #region [Constants]
        private const string LOG_MESSAGE_PATTERN = "[{0}]({1})::{2}";
        private const string REPORTED_EXCEPTION_PATTERN = "{0}\r\nReported exception: {1}\r\n";
        #endregion [Constants]


        #region [ILogService members]
        /// <summary>
        /// Write a new log entry as information
        /// </summary>
        /// <param name="message">Message body to log</param>
        public void Log(string message)
        {
            Debug.WriteLine(String.Format(LOG_MESSAGE_PATTERN, Category.INFO, Priority.NONE, message));
        }
        /// <summary>
        /// Write a new log entry with the specified category and priority
        /// </summary>
        /// <param name="message">Message body to log</param>
        /// <param name="category">Category of the entry</param>
        /// <param name="priority">The priority of the entry</param>
        public void Log(string message, Category category, Priority priority)
        {
            Debug.WriteLine(String.Format(LOG_MESSAGE_PATTERN, category, priority, message));
        }
        /// <summary>
        /// Write a new log entry with the specified category and priority
        /// </summary>
        /// <param name="message">Message body to log</param>
        /// <param name="exception">Exception to log</param>
        /// <param name="category">Category of the entry</param>
        /// <param name="priority">The priority of the entry</param>
        public void Log(string message, Exception exception, Category category, Priority priority)
        {
            Debug.WriteLine(String.Format(LOG_MESSAGE_PATTERN, category, priority, 
                String.Format(REPORTED_EXCEPTION_PATTERN, message, exception)));
        } 
        #endregion [ILogService members]
    }
}