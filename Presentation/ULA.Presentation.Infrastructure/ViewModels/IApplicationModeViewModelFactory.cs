using System;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes application mode view model factory functionality
    /// </summary>
    public interface IApplicationModeViewModelFactory
    {
        /// <summary>
        ///     Creates an instance of <see cref="IApplicationConfigurationModeViewModel" />
        /// </summary>
        /// <returns>Created instance of <see cref="IApplicationConfigurationModeViewModel" /></returns>
        IApplicationConfigurationModeViewModel CreateConfigurationModeViewModel();

        /// <summary>
        ///     Creates an instance of <see cref="IApplicationRuntimeModeViewModel" />
        /// </summary>
        /// <returns>Created instance of <see cref="IApplicationRuntimeModeViewModel" /></returns>
        IApplicationRuntimeModeViewModel CreateRuntimeModeViewModel();

        /// <summary>
        ///     Creates an instance of <see cref="IApplicationFailureModeViewModel" />
        /// </summary>
        /// <param name="failure">An instance of <see cref="Exception" /> that represents current mode failure</param>
        /// <returns>Created instance of <see cref="IApplicationFailureModeViewModel" /></returns>
        IApplicationFailureModeViewModel CreateFailureModeViewModel(Exception failure);
    }
}