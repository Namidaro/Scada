using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents a system default create new deviceViewModel interaction request provider
    ///     Представляет Интерактивный провайдер окна "Создание нового устройства"
    /// (описание класса аналогично <see cref="AboutInteractionProvider"/>)
    /// </summary>
    public class CreateNewDeviceProvider : IInteractionProvider<IModifyDeviceViewModel>
    {
        #region [Private fields]

        private readonly IInteractionView _interactionView;
        private readonly IModifyDeviceViewModel _viewModel;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="CreateNewDeviceProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> to use
        /// </param>
        public CreateNewDeviceProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.CREATE_NEW_DEVICE_VIEW_NAME);
            this._viewModel = container.Resolve<IModifyDeviceViewModel>();
        }

        #endregion [Ctor's]

        #region [IInteractionProvider<ICreateNewDeviceViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// </returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IModifyDeviceViewModel" /> that exposes current interaction view model functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IModifyDeviceViewModel" /> that exposes current interaction view model functionality
        /// </returns>
        public IModifyDeviceViewModel InteractionViewModel
        {
            get { return this._viewModel; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IInteractionViewModel" /> that exposes current interaction view model functionality
        /// </returns>
        IInteractionViewModel IInteractionProvider.InteractionViewModel
        {
            get { return InteractionViewModel; }
        }

        #endregion [IInteractionProvider<ICreateNewDeviceViewModel> members]
    }
}