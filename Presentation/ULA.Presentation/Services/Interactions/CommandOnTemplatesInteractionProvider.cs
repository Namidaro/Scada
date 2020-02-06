using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{ 
    /// <summary>
    ///     Represents commands on template interaction provider
    /// Представляет Интерактивный провайдер окна "Команды по шаблону"
    /// (описание класса аналогично <see cref="AboutInteractionProvider"/>)
    /// </summary>
    public class CommandOnTemplatesInteractionProvider : IInteractionProvider<ICommandOnTemplateInteractionViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly ICommandOnTemplateInteractionViewModel _viewModel;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="CommandOnTemplatesInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public CommandOnTemplatesInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.COMMAND_ON_TEMPLATES_VIEW_NAME);
            this._viewModel = container.Resolve<ICommandOnTemplateInteractionViewModel>();
        }

        #endregion [Ctor's]

        #region [ IInteractionProvider<ICitywideCommandsInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="ICommandOnTemplateInteractionViewModel" /> that exposes current interaction view
        ///     model
        ///     functionality
        /// </summary>
        public ICommandOnTemplateInteractionViewModel InteractionViewModel
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

        #endregion [ IInteractionProvider<ICitywideInteractionViewModel> members]
    }
}
