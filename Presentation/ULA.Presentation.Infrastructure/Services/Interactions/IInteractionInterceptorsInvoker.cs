using System;

namespace ULA.Presentation.Infrastructure.Services.Interactions
{
    /// <summary>
    ///     Exposes an interaction request invokers registry functionality
    /// </summary>
    public interface IInteractionInterceptorsInvoker : IDisposable
    {
        /// <summary>
        ///     Invokes an action that represents before initialization request phase
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        void InvokeBeforeInitialization(object viewModel);

        /// <summary>
        ///     Invokes an action that represents after initialization request phase
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        void InvokeAfterInitialization(object viewModel);

        /// <summary>
        ///     Invokes an action that represents before un-initialization request phase
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        void InvokeBeforeUnInitialization(object viewModel);

        /// <summary>
        ///     Invokes an action that represents after un-initialization request phase
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        void InvokeAfterUnInitialization(object viewModel);
    }
}