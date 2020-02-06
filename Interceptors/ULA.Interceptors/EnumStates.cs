namespace ULA.Interceptors
{
    /// <summary>
    ///     Represents a states of states of resistor, starter, and etc. command blocks
    ///     Представляет состояния предохранителей, пускателей и т.п.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
    public enum CommandTypesEnum
    {
        /// <summary>
        ///     Represent On state(Work)
        ///     Включенное состояние
        /// </summary>
        ON, 

        /// <summary>
        ///     Represents Off state(Stop)
        ///     отключенное состояние
        /// </summary>
        OFF,
        
        /// <summary>
        ///     Represent config state(not link)
        ///     Состояние конфигурации(Нет связи)
        /// </summary>
        CONFIG,

        /// <summary>
        ///     Автоматическое состояние
        /// </summary>
        AUTO,

        /// <summary>
        ///     Режим ремонта
        /// </summary>
        Repair,

        /// <summary>
        ///     Режим неремонта
        /// </summary>
        NoRepair,

        /// <summary>
        /// Ручной режим
        /// </summary>
        MANUAL
    }
}
