using System.ComponentModel;
using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes a root view model functionality
    /// </summary>
    public interface IRootViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Gets an istance of <see cref="ICommand" /> that represents an action of changing current application mode to
        ///     configuration mode
        /// </summary>
        ICommand ChangeModeToConfigurationCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represent an action of changing current application mode top
        ///     runtime mode
        /// </summary>
        ICommand ChangeModeToRuntimeCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="ICommand" /> that represents an initialization action
        /// </summary>
        ICommand InitializeCommand { get; }
        /// <summary>
        /// положение картинки поверх или под остальным
        /// </summary>
        int ImageZIndex { get; set; }
    }
}