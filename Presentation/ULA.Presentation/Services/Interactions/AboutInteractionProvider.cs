using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents about program interaction provider
    ///     Представляет Интерактивный провайдер окна "О Программе"
    /// </summary>
    public class AboutInteractionProvider : IInteractionProvider<IAboutInteractionViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly IAboutInteractionViewModel _viewModel;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="AboutInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public AboutInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.ABOUT_VIEW_NAME);
            this._viewModel = container.Resolve<IAboutInteractionViewModel>();
        }

        #endregion [Ctor's]

        #region [ IInteractionProvider<IAboutInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        ///     Вернет интерактивную вьюху
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IAboutInteractionViewModel" /> that exposes current interaction view
        ///     model functionality
        ///     Вернет интерактивную вью-модель (окна о программе)
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IAboutInteractionViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </returns>
        public IAboutInteractionViewModel InteractionViewModel
        {
            get { return this._viewModel; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality
        ///     Вернет интерактивную вью-модель
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality</returns>
        IInteractionViewModel IInteractionProvider.InteractionViewModel
        {
            get { return InteractionViewModel; }
        }

        #endregion [ IInteractionProvider<IAboutInteractionViewModel> members]
    }
}