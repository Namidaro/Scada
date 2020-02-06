using NLog.LayoutRenderers.Wrappers;

namespace ULA.AddinsHost.Business.Driver.Context
{
    /// <summary>
    ///     Exposes driver context functionality
    ///     Описывает функциональность контекста драйвера
    /// </summary>
    public interface IDriverContext : IDriverCommonContext
    {
        /// <summary>
        ///     Get a tcp address
        ///     Tcp Ip-адрес
        /// </summary>
        /// <returns></returns>
        string GetTcpAddress();

        /// <summary>
        ///     Get a tcp port
        ///     Tcp порт
        /// </summary>
        /// <returns></returns>
        int GetTcpPort();  
        
        /// <summary>
        ///     Gets or sets the type of current driver
        ///     Тип драйвера
        /// </summary>
        string DriverType { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="IDataContentContainer" /> that represents current driver content
        ///     Контент драйвера(его специфические данные)
        /// </summary>
        IDataContentContainer DataContainer { get; }

        
    }
}