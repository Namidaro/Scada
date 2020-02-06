using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ULA.Presentation.SharedResourses.Common
{
    /// <summary>
    ///     Represents base async command implementation
    ///     Представляет реализацию базового класса для асинхронной команды
    /// </summary>
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        #region [IAsyncCommand members]

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Executes command asynchronously
        /// </summary>
        /// <param name="parameter">An instance of command parameter</param>
        /// <returns>
        ///     An instance of <see cref="Task" /> that represents asynchronous operation
        /// </returns>
        public abstract Task ExecuteAsync(object parameter = null);

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
        public abstract bool CanExecute(object parameter);

        #endregion [IAsyncCommand members]

        #region [Help members]

        /// <summary>
        ///     Raises <see cref="CanExecuteChanged" /> event
        /// </summary>
        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion [Help members]
    }
}