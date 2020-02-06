using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a citywide commands interaction view model functionality
    /// </summary>
    public interface ICitywideCommandsInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets the title of current interaction request
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the message of current interaction request
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Get a result a citywide interaction
        /// </summary>
        CitywideCommandResult Result { get; }

        /// <summary>
        ///     Включить ночное освещение
        /// </summary>
        ICommand RunNightLightCommand { get; }

        /// <summary>
        ///     Отключить ночное освещение
        /// </summary>
        ICommand StopNightLightCommand { get; }

        /// <summary>
        ///     Включить вечернее освещение
        /// </summary>
        ICommand RunEveningCommand { get; }

        /// <summary>
        ///     Отключить вечернее освещение
        /// </summary>
        ICommand StopEveningCommand { get; }

        /// <summary>
        ///     Включить полное освещение
        /// </summary>
        ICommand RunFullCommand { get; }

        /// <summary>
        ///     Отключить полное освещение
        /// </summary>
        ICommand StopFullCommand { get; }

        /// <summary>
        ///     Включить подсветку
        /// </summary>
        ICommand RunBacklightCommand { get; }

        /// <summary>
        ///     Отключить подсветку
        /// </summary>
        ICommand StopBacklightCommand { get; }

        /// <summary>
        ///     Включить иллюминацию
        /// </summary>
        ICommand RunIlluminationCommand { get; }

        /// <summary>
        ///     Отключить иллюминацию
        /// </summary>
        ICommand StopIlluminationCommand { get; }

        /// <summary>
        ///     Включить энергосбережение
        /// </summary>
        ICommand RunStoregEnergyCommand { get; }

        /// <summary>
        ///     Отключить энергосбережение
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Storeg")]
        ICommand StopStoregEnergyCommand { get; }

        /// <summary>
        ///     Represent run auto mode all devices action
        ///     Авторежим
        /// </summary>
        ICommand AutoAllCommand { get; }

        /// <summary>
        /// Run manual mode on all devices (Ручной режим)
        /// </summary>
        ICommand ManualModeAllCommand { get; }
    }
}