using System.Threading.Tasks;
using System.Windows.Input;

namespace ULA.Presentation.SharedResourses.Common
{
    /// <summary>
    ///     Exposes asynchronous command functionality
    ///     Описывает функционал сущности Асинхронной команды
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        ///     Executes command asynchronously
        /// </summary>
        /// <param name="parameter">An instance of command parameter</param>
        /// <returns>
        ///     An instance of <see cref="Task" /> that represents asynchronous operation
        /// </returns>
        Task ExecuteAsync(object parameter = null);
    }
}