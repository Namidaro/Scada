using System;
using System.Threading.Tasks;

namespace ULA.Presentation.SharedResourses.Common
{
    /// <summary>
    ///     Represents a default asynchronous command implementation
    ///     Представляет реализацию асинхронной команды
    /// </summary>
    public class AsyncCommand : AsyncCommandBase
    {
        #region [Private fields]

        private readonly Func<bool> _canExecute;
        private readonly Func<Task> _execution;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AsyncCommand" />
        /// </summary>
        /// <param name="execution">A delegate that represents asynchronous command operation</param>
        /// <param name="canExecute">A delegate that determines whether the command can execute in its current state.</param>
        public AsyncCommand(Func<Task> execution, Func<bool> canExecute = null)
        {
           this._execution = execution;
            this._canExecute = canExecute ?? (() => true);
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Executes command asynchronously
        ///     Выполнить команду асинхронно
        /// </summary>
        /// <param name="parameter">An instance of command parameter</param>
        /// <returns>
        ///     An instance of <see cref="Task" /> that represents asynchronous operation
        /// </returns>
        public override Task ExecuteAsync(object parameter = null)
        {
            return this._execution();
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        ///     Описывает метод проверяющий возмолжно ли выполнить команду
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
            return this._canExecute();
        }

        #endregion [Override members]
    }
}