namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Result of CitywideInteraction
    /// </summary>
    public enum CitywideCommandResult
    {
        /// <summary>
        ///     CancelCommand
        /// </summary>
        Cancel,
        /// <summary>
        /// Ночное вкл
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nighlight")]
        OnNighlight,
        /// <summary>
        /// Ночное откл
        /// </summary>
        OffNighlight,
         /// <summary>
         /// Вечернее вкл
         /// </summary>
         OnEvening,
         /// <summary>
         /// Вечернее откл
         /// </summary>
         OffEvening,
         /// <summary>
         /// Run полное
         /// </summary>
         OnFull,
         /// <summary>
         /// Srop полное
         /// </summary>
         OffFull,
         /// <summary>
         /// Run подсветка
         /// </summary>
        OnBacklight,
        /// <summary>
        /// Srop подсветка
        /// </summary>
        OffBacklight,
        /// <summary>
        /// Иллюминация вкл
        /// </summary>
        OnIllumination,
        /// <summary>
        /// Иллюминация откл
        /// </summary>
        OffIllumination,
        /// <summary>
        /// Энергосережение вкл
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Storeg")]
        OnStoregEnergy,
        /// <summary>
        /// Энергосбережение откл
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Storeg")]
        OffStoregEnergy,
        /// <summary>
        ///     Start automode all devices
        /// </summary>
        AutoAll,
        /// <summary>
        /// Ручной режим, off automode all devices
        /// </summary>
        ManualModeAll
    }
}
