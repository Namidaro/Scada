namespace ULA.Interceptors
{
    /// <summary>
    ///     Описывает режимы освещения
    /// </summary>
    public enum LightingModeEnum
    {
        /// <summary>
        ///     Неопределенное значение графика освещения
        /// </summary>
        UNDEFINED,
        /// <summary>
        ///     Ночное освещение
        /// </summary>
        NIGHTLIGHTING,
        /// <summary>
        ///     подсветка
        /// </summary>
        BACKLIGHT,
        /// <summary>
        ///     Иллюминация
        /// </summary>
        ILLUMINATION,
        /// <summary>
        ///     Энергосбережение
        /// </summary>
        ENERGY_SAVING,
        /// <summary>
        ///     Обогрев
        /// </summary>
        HEATING,
        /// <summary>
        ///     Экономия+ночное - Вечернее
        /// </summary>
        ECONOMY_NIGHTLIGHTING,
        /// <summary>
        ///     Экономия+подсветка
        /// </summary>
        ECONOMY_BACKLIGHT,
        /// <summary>
        ///    Экономия+иллюминация
        /// </summary>
        ECONOMY_ILLUMINATION,
        /// <summary>
        ///     Полное ночное - это логический режим освещения включающий в себя режимы ночного и вечернего освещения(NIGHTLIGHTING + ECONOMY_NIGHTLIGHTING)
        /// </summary>
        FULL_NIGHTLIGHT,
        /// <summary>
        ///     Полное подсветка - это логический режим освещения включающий в себя режимы подсветки и подсветки+экономия
        /// </summary>
        FULL_BACKLIGHT,
        /// <summary>
        ///     Полное иллюминацию - это логический режим освещения включающий в себя режимы иллюминации и иллюминации+осверщения
        /// </summary>
        FULL_ILLUMINATION
    }
}
