using System;
using System.Threading.Tasks;

namespace ULA.Presentation.SharedResourses.Common
{
    /// <summary>
    ///     Represents a default asynchronous comamnd implementation
    ///     Представляет параметризованную асинхронную команду
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncCommand<T> : AsyncCommandBase
    {
        #region [Private fields]

        private readonly Func<T, bool> _canExecute;
        private readonly Func<T, Task> _execution;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AsyncCommand{T}" />
        /// </summary>
        /// <param name="execution">A delegate that represents asynchronous command operation</param>
        /// <param name="canExecute">A delegate that determines whether the command can execute in its current state.</param>
        public AsyncCommand(Func<T, Task> execution, Func<T, bool> canExecute = null)
        {
            this._execution = execution;
            this._canExecute = canExecute ?? (param => true);
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Executes command asynchronously
        /// </summary>
        /// <param name="parameter">An instance of command parameter</param>
        /// <returns>
        ///     An instance of <see cref="Task" /> that represents asynchronous operation
        /// </returns>
        public override Task ExecuteAsync(object parameter = null)
        {
            return parameter == null ? this._execution(default(T)) : this._execution((T)parameter);
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public override bool CanExecute(object parameter)
        {
            return parameter == null ? this._canExecute(default(T)) : this._canExecute((T)parameter);
        }

        #endregion [Override members]
    }
}