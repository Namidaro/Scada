using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Описывает вью-модель графика освещения
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Shedule")]
    public interface ILightingSheduleViewModel
    {
        /// <summary>
        ///     Свойство представляющие имя устройства, для которого производится конфигурация
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        ///      Представляет команду отправки конфигурационных данных на устройство
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Shedule")]
        ICommand SendLightingShedule { get; }

        /// <summary>
        ///     Представляет команду считывания графика освещения с устройства
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Shedule")]
        ICommand GetLightingShedule { get; }

        /// <summary>
        ///     Команда навигации назад на схему устройства
        /// </summary>
        ICommand BackToSchemeCommand { get; }
    }
}
