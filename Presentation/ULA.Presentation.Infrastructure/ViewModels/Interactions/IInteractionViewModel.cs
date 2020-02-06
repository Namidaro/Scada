using System;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes interaction view model functionality
    /// </summary>
    public interface IInteractionViewModel
    {
        /// <summary>
        ///     Gets an ivent that will be fired if user canceled interaction request
        /// </summary>
        event EventHandler OnInteractionComplete;
    }
}