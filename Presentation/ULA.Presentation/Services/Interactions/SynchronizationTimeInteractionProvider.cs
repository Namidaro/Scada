using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents default synchronization time interaction provider
    ///     Представляет Интерактивный провайдер окна "Синхронизация времени"
    /// (описание класса аналогично <see cref="AboutInteractionProvider"/>)
    /// </summary>
    public class SynchronizationTimeInteractionProvider : IInteractionProvider<ISynchronizationTimeInteractionViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly ISynchronizationTimeInteractionViewModel _viewModel;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="SynchronizationTimeInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public SynchronizationTimeInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.SYNCHRONIZATION_TIME_VIEW_NAME);
            this._viewModel = container.Resolve<ISynchronizationTimeInteractionViewModel>();
        }

        #endregion [Ctor's]

        #region [ IInteractionProvider<ISynchronizationTimeInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ISynchronizationTimeInteractionViewModel" /> that exposes current interaction view
        ///     model functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="ISynchronizationTimeInteractionViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </returns>
        public ISynchronizationTimeInteractionViewModel InteractionViewModel
        {
            get { return this._viewModel; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality</returns>
        IInteractionViewModel IInteractionProvider.InteractionViewModel
        {
            get { return InteractionViewModel; }
        }

        #endregion [ IInteractionProvider<ISynchronizationTimeInteractionViewModel> members]
    }
}
