using System.Threading.Tasks;

namespace ULA.Common.AsyncServices
{
    /// <summary>
    ///     Exposes asynchronous service functionality
    ///     Описывает сервис асинхронной инициализации
    /// </summary>
    public interface IAsyncInitializationService
    {
        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        ///     Таска асинхронной инициализации
        /// </summary>
        Task Initialization { get; }
    }
}