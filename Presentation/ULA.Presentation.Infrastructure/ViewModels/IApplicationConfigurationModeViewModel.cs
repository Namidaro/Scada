using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes an application configuration mode view model functionality
    /// </summary>
    public interface IApplicationConfigurationModeViewModel : IApplicationModeViewModel
    {
        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents create new virtual device command
        /// </summary>
        ICommand CreateNewVirtualDeviceCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents edit current virtual device command
        /// </summary>
        ICommand EditCurrentDeviceCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents delete virtual device command
        /// </summary>
        ICommand DeleteCurrentDeviceCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to about view command
        /// </summary>
        ICommand NavigateToAboutCommand { get; }
        /// <summary>
        ///     Команда навигации на вьюшку изменения пароля привелигированного доступа
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Naigate")]
        ICommand NaigateToEditPassword { get; }

        /// <summary>
        /// коммандан на добавление картинки
        /// </summary>
        ICommand AddImageCommand { get; }
    }
}