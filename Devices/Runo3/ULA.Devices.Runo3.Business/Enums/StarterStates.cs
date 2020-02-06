namespace ULA.Devices.Runo3.Business.Enums
{
    /// <summary>
    ///     Represents a states of resistor, starter, and etc. command blocks
    ///     Представляет набор состояний для предохранителей, пускателей, и т.п.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames")]
    public enum StarterStates
    {
        /// <summary>
        ///     Represent On state(Work)
        /// </summary>
        ON,

        /// <summary>
        ///     Represents Off state(Stop)
        /// </summary>
        OFF,

        /// <summary>
        ///     Represent no connection state(not link)
        /// </summary>
        NO_CONNECT
    }
}
