namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Exposes an application state manager functionality
    ///     Описывает менеджер состояний приложения
    /// </summary>
    public interface IApplicationStateManager
    {
        /// <summary>
        ///     Gets an instance of <see cref="IApplicationState" /> that represents current state
        ///     Описывает текущее состояние приложение
        /// </summary>
        IApplicationState CurrentState { get; }

        /// <summary>
        ///     Goes to a new application state
        ///     Переход на новое состояние
        /// </summary>
        /// <param name="state">
        ///     An instance of <see cref="ApplicationState" /> that represent new application state enumeration
        /// </param>
        void GotToNewState(ApplicationState state);
    }
}