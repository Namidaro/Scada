namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Represents current application state enumeration
    ///     Представляет перечисление состояний предложения
    /// </summary>
    public enum ApplicationState
    {
        /// <summary>
        ///     Describes current application state as runtime state
        ///     Режим реального времени
        /// </summary>
        RUNTIME,

        /// <summary>
        ///     Describes current application state as configuration state
        ///     Режим конфигурации
        /// </summary>
        CONFIGURATION,

        /// <summary>
        ///     Describes current application state as bootstrapping state
        ///     Описывает состояние бутстрапинга
        /// </summary>
        BOOTSTRAPPING,

        /// <summary>
        ///     Describes current application state as fatal error state
        ///     Описывает состояние фатальной ошибки
        /// </summary>
        FATAL_ERROR,

        /// <summary>
        ///     Describes current application state as unknown state
        ///     Описывает неизвестное состояние
        /// </summary>
        UNKNOWN
    }
}