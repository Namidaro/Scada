namespace ULA.AddinsHost.Business.Driver
{
    /// <summary>
    ///     Exposes logical driver factory functionality
    ///     Описывает фунциональность фабрики логических драйверов
    /// </summary>
    public interface ILogicalDriverFactory
    {
        /// <summary>
        ///     Creates an instance of <see cref="ConfigLogicalDriverBase" /> that represents basic configuration logical driver functionality
        ///     Создает логический дайвер для режима конфигурации
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="ConfigLogicalDriverBase" /> that represents basic configuration logical driver functionality
        /// </returns>
        ConfigLogicalDriverBase CreateConfigLogicalDriver();

        /// <summary>
        ///     Creates an instance of <see cref="RuntimeLogicalDriverBase" /> that represents basic runtime logical driver functionality
        ///     Создает логический дайвер для режима реального времени
        /// </summary>
        /// <returns>
        ///     Created instance of <see cref="RuntimeLogicalDriverBase" /> that represents basic runtime logical driver functionality
        /// </returns>
        RuntimeLogicalDriverBase CreateRuntimeLogicalDriver();
    }
}