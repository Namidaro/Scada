using System;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Infrastructure.Services.Interactions
{
    /// <summary>
    ///     Represents generic version of <see cref="IInteractionProvider" />
    /// </summary>
    /// <typeparam name="TViewModel"><see cref="Type" /> that repersents interaction view model functionality</typeparam>
    public interface IInteractionProvider<out TViewModel> : IInteractionProvider
        where TViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets an instance of view model that exposes current interaction view model functionality
        /// </summary>
        /// <returns>An instance of view model that exposes current interaction view model functionality</returns>
        new TViewModel InteractionViewModel { get; }
    }
}