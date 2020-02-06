using System.Collections.ObjectModel;

namespace ULA.Presentation.Infrastructure.ViewModels.UserControl
{
    /// <summary>
    ///    Описывает вью-модель дня для графиуа освещения
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Shedule")]
    public interface IDaySheduleViewModel
    {
        /// <summary>
        /// относится ли этот график к экономии
        /// </summary>
        bool IsEconomy { get; set; }
        /// <summary>
        ///     месяц к которому относится день
        /// </summary>
        string Month { get; set; }

        /// <summary>
        ///     Календарное число месяца
        /// </summary>
        int DayNumber { get; set; }

        /// <summary>
        ///     Время включения в часах
        /// </summary>
        int StartHour { get; set; }

        /// <summary>
        ///     Время включения в минутах
        /// </summary>
        int StartMinute { get; set; }

        /// <summary>
        ///     Время отключения в часах
        /// </summary>
        int StopHour { get; set; }

        /// <summary>
        ///     Время отключения в минутах
        /// </summary>
        int StopMinute { get; set; }

        /// <summary>
        ///     Свойство с диапазоном часов [0-23]
        /// </summary>
        ObservableCollection<int> RangeHour { get; }

        /// <summary>
        ///     Свойство с диапазоном минут [0-59]
        /// </summary>
        ObservableCollection<int> RangeMinute { get; }
    }
}
