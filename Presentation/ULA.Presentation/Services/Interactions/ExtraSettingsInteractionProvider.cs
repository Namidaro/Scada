using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    /// 
    /// </summary>
   public class ExtraSettingsInteractionProvider : IInteractionProvider<IExtraSettingsViewModel>
    {
        #region [Fields]

        private readonly IInteractionView _interactionView;
        private readonly IExtraSettingsViewModel _viewModel;

        #endregion [Fields]

        /// <summary>
        ///     Creates an instance of <see cref="BusyInteractionProvider" />
        /// </summary>
        /// <param name="container">
        ///     An instance of <see cref="IIoCContainer" /> that represents the system specific IoC container
        /// </param>
        public ExtraSettingsInteractionProvider(IIoCContainer container)
        {
            Guard.AgainstNullReference(container, "container");

            this._interactionView =
                container.Resolve<IInteractionView>(ApplicationGlobalNames.EXTRASETTINGS_INTERACTION_VIEW_NAME);
            this._viewModel = container.Resolve<IExtraSettingsViewModel>();
        }

        #region [ IInteractionProvider<IBusyInteractionViewModel> members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that exposes current interaction view functionality
        /// Вернет вьюху
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionView" /> that exposes current interaction view functionality</returns>
        public IInteractionView InteractionView
        {
            get { return this._interactionView; }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IExtraSettingsViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IExtraSettingsViewModel" /> that exposes current interaction view model
        ///     functionality
        /// </returns>
        public IExtraSettingsViewModel InteractionViewModel
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

        #endregion [ IInteractionProvider<IBusyInteractionViewModel> members]
    }
}