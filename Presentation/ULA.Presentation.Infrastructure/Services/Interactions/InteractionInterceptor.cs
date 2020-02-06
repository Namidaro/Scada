using System;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Infrastructure.Services.Interactions
{
    /// <summary>
    ///     Represents default interaction interceptors registry functionality
    /// </summary>
    /// <typeparam name="TViewModel"><see cref="Type" /> that represents current interaction view model</typeparam>
    public class InteractionInterceptor<TViewModel> where TViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets an instance of <see cref="Action{T}" /> that represents an intercaption method that will be invoked
        ///     before interaction request initialization phase
        /// </summary>
        public Action<TViewModel> OnBeforeInitialization { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Action{T}" /> that represents an intercaption method that will be invoked
        ///     after interaction request initialization phase
        /// </summary>
        public Action<TViewModel> OnAfterInitialization { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Action{T}" /> that represents an intercaption method that will be invoked
        ///     before interaction request un-initialization phase
        /// </summary>
        public Action<TViewModel> OnBeforeUnInitialization { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Action{T}" /> that represents an intercaption method that will be invoked
        ///     after interaction request un-initialization phase
        /// </summary>
        public Action<TViewModel> OnAfterUnInitialization { get; set; }
    }
}