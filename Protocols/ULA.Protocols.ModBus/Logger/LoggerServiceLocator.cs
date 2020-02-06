using System;

namespace YP.Toolkit.Protocols.ModBus.Logger
{
    /// <summary>
    /// Represents a logging service
    /// </summary>
    public static class LoggerServiceLocator
    {
        #region [Private fields]
        private static Lazy<ILogService> _factory = new Lazy<ILogService>(() => new InternalLoggerService());
        #endregion [Private fields]


        #region [Public members]
        /// <summary>
        /// Gets an instance of current logging service
        /// </summary>
        public static ILogService Current
        {
            get { return _factory.Value; }
        }
        /// <summary>
        /// Registers a logging service
        /// </summary>
        /// <param name="logger">An instance of delegate for obtaining a service</param>
        public static void RegisterLogger(Func<ILogService> logger)
        {
            _factory = new Lazy<ILogService>(logger);
        } 
        #endregion [Public members]
    }
}