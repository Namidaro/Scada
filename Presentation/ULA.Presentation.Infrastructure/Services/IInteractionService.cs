using System;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Infrastructure.Services
{
    /// <summary>
    ///     Exposes an interaction service functionality
    /// </summary>
    public interface IInteractionService
    {
        /// <summary>
        ///     Sends an interaction request to a user
        /// </summary>
        /// <typeparam name="TViewModel"><see cref="Type" /> that represents the view model of the interaction request</typeparam>
        /// <param name="provider">
        ///     An instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current
        ///     interaction request provider
        /// </param>
        /// <param name="interactionInterceptor">An instance of <see cref="InteractionInterceptor{TViewModel}" /> to use</param>
        /// <returns>An instance of <see cref="IInteractionToken" /> that represents interaction request token</returns>
        IInteractionToken Interact<TViewModel>(IInteractionProvider<TViewModel> provider,
            InteractionInterceptor<TViewModel> interactionInterceptor)
            where TViewModel : IInteractionViewModel;

        /// <summary>
        ///     Sends an interaction request to a user
        /// </summary>
        /// <typeparam name="TViewModel"><see cref="Type" /> that represents the view model of the interaction request</typeparam>
        /// <param name="provider">
        ///     An instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current
        ///     interaction request provider
        /// </param>
        /// <param name="onInitialized">An instance of <see cref="Action{T}" /> that represents an initialization callback</param>
        /// <param name="onUninitialized">An instance of <see cref="Action{T}" /> that represents an uninitialization callback</param>
        /// <returns>An instance of <see cref="IInteractionToken" /> that represents interaction request token</returns>
        IInteractionToken Interact<TViewModel>(IInteractionProvider<TViewModel> provider,
            Action<TViewModel> onInitialized = null, Action<TViewModel> onUninitialized = null)
            where TViewModel : IInteractionViewModel;
    }
}