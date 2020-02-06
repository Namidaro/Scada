using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents default system warning messagebox interaction provider
    ///     Представляет Интерактивный провайдер окна "MessageBox предупреждения"
    /// (описание класса аналогично <see cref="AboutInteractionProvider"/>)
    /// </summary>
    public class WarningMessageBoxInteractionProvider : IInteractionProvider<IWarningMessageBoxInteractionViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly IWarningMessageBoxInteractionViewModel _viewModel;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="WarningMessageBoxInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public WarningMessageBoxInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.WARNING_MESSAGEBOX_VIEW_NAME);
            this._viewModel = container.Resolve<IWarningMessageBoxInteractionViewModel>();
        }

        #endregion [Ctor's]

        #region [IQuestionProvider<IInformationrMessageBoxInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IWarningMessageBoxInteractionViewModel" /> that exposes current interaction view
        ///     model
        ///     functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IWarningMessageBoxInteractionViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </returns>
        public IWarningMessageBoxInteractionViewModel InteractionViewModel
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

        #endregion [ IInteractionProvider<IQuestionMessageBoxInteractionViewModel> members]
    }
}