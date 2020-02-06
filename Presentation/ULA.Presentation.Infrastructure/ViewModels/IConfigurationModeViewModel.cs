using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///    Интерфейс Вью-модели для режима конфигурации руно
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Runo")]
    public interface IConfigurationModeViewModel
    {
        /// <summary>
        ///     Gets device name/ Имя устройства
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        ///     Представляет команду отправки конфигурационных данных на устройство
        /// </summary>
        ICommand SendConfiguration { get; }

        /// <summary>
        ///     Представляет команду считывания конфигурационных данных с устройства
        /// </summary>
        ICommand GetConfiguration { get; }

        /// <summary>
        ///     Команда навигации назад на схему устройства
        /// </summary>
        ICommand BackToSchemeCommand { get; }

        /// <summary>
        ///     Загружает конфигурацию из файла
        /// </summary>
        ICommand GetConfigurationFromFileCommand { get; }

        /// <summary>
        ///     Сохраняет конфигурацию в файл
        /// </summary>
        ICommand SaveConfigurationInFileCommand { get; }
    }
}
