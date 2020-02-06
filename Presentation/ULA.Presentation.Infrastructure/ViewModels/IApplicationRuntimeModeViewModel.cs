using System.Collections.Generic;
using System.Windows.Input;
using ULA.AddinsHost.ViewModel.Device;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes an application runtime mode view model functionality
    /// </summary>
    public interface IApplicationRuntimeModeViewModel : IApplicationModeViewModel
    {
        /// <summary>
        ///     Command represents action of changing mode to SchemeMode
        /// </summary>
        ICommand ChangeModeToScheme { get; }

        /// <summary>
        ///     Command represents action of changing mode to ListWidgetModeMode
        /// </summary>
        ICommand ChangeModeToListWidget { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents navigate to journal of system view command
        /// </summary>
        ICommand NavigateToJournalOfSystemCommand { get; }
        List<IRuntimeDeviceViewModel> SelectedDeviceViewModels { get; set; }
    }
}