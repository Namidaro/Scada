namespace ULA.Interceptors
{
    /// <summary>
    ///     Описывает результат отправки команды управления освещением
    /// </summary>
    public enum LightingCommandResult
    {
        /// <summary>
        ///     Команда отправлена
        /// </summary>
        OK,
        /// <summary>
        ///     Пускатель с таким режимом не существует на данном устройстве
        /// </summary>
        NO_EXIST,
        /// <summary>
        ///     Пускатель с таким режимом освещения находится в режиме ремонта
        /// </summary>
        REPAIR,
        /// <summary>
        ///     Значение показывает неопределенный результат (сбой в логике)
        /// </summary>
        UNDEFINED
    }
}
