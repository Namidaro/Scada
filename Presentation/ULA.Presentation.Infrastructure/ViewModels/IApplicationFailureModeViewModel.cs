using System;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes an application failure mode view model functionality
    /// </summary>
    public interface IApplicationFailureModeViewModel : IApplicationModeViewModel
    {
        /// <summary>
        ///     Sets an instance of <see cref="Exception" /> that represents application mode changing failure
        /// </summary>
        /// <param name="failure">An instance of <see cref="Exception" /> that represents application mode changing failure</param>
        void SetFailure(Exception failure);
    }
}