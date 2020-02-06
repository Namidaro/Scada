using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents default system error messagebox interaction provider
    ///     Представляет Интерактивный провайдер окна "Ошибкм"
    /// (описание класса аналогично <see cref="AboutInteractionProvider"/>)
    /// </summary>
    public class ErrorMessageBoxInteractionProvider : IInteractionProvider<IErrorMessageBoxInteractionViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly IErrorMessageBoxInteractionViewModel _viewModel;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ErrorMessageBoxInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public ErrorMessageBoxInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.ERROR_MESSAGEBOX_VIEW_NAME);
            this._viewModel = container.Resolve<IErrorMessageBoxInteractionViewModel>();
        }

        #endregion [Ctor's]

        #region [ IInteractionProvider<IErrorMessageBoxInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IErrorMessageBoxInteractionViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IErrorMessageBoxInteractionViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </returns>
        public IErrorMessageBoxInteractionViewModel InteractionViewModel
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

        #endregion [ IInteractionProvider<IErrorMessageBoxInteractionViewModel> members]
    }
}