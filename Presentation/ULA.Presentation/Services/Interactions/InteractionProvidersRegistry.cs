using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents a default implementation of an interaction providers registry
    ///     Представляет реализацию провайдера интерактивных окон
    /// </summary>
    public class InteractionProvidersRegistry : IInteractionProvidersRegistry
    {
        #region [Fields]

        private readonly IIoCContainer _container;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="InteractionProvidersRegistry" />
        /// </summary>
        /// <param name="container">An instance of <see cref="IIoCContainer" /> to use as system IoC container</param>
        public InteractionProvidersRegistry(IIoCContainer container)
        {
            this._container = container;
        }

        #endregion [Ctor's]


        //СООТВЕТСТВУЕТ МЕТОДАМ ИЗ КЛАССА ApplicationInteractionProviders
        #region [IInteractionProvidersRegistry members]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the citywide
        ///     commands messagebox interaction provider
        /// </summary>
        public IInteractionProvider<ICitywideCommandsInteractionViewModel> CitywideCommandsInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<ICitywideCommandsInteractionViewModel>>(
                    ApplicationGlobalNames.CITYWIDE_COMMANDS_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the commands on templates
        ///     messagebox interaction provider
        /// </summary>
        public IInteractionProvider<ICommandOnTemplateInteractionViewModel> CommandOnTemplatesInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<ICommandOnTemplateInteractionViewModel>>(
                    ApplicationGlobalNames.COMMAND_ON_TEMPLATES_INTERACTION_PROVIDER_NAME);
            }
        }


        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the about program
        ///     interaction provider
        /// </summary>
        public IInteractionProvider<IAboutInteractionViewModel> AboutInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IAboutInteractionViewModel>>(
                    ApplicationGlobalNames.ABOUT_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined busy
        ///     interaction provider
        /// </summary>
        public IInteractionProvider<IBusyInteractionViewModel> BusyInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IBusyInteractionViewModel>>(
                    ApplicationGlobalNames.BUSY_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined error
        ///     messagebox interaction provider
        /// </summary>
        public IInteractionProvider<IErrorMessageBoxInteractionViewModel> ErrorMessageBoxInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IErrorMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.ERROR_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     information messagebox interaction provider
        /// </summary>
        public IInteractionProvider<IInformationMessageBoxInteractionViewModel> InformationMessageBoxInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IInformationMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.INFORMATION_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     question messagebox interaction provider
        /// </summary>
        public IInteractionProvider<IQuestionMessageBoxInteractionViewModel> QuestionMessageBoxInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IQuestionMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.QUESTION_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     warning messagebox interaction provider
        /// </summary>
        public IInteractionProvider<IWarningMessageBoxInteractionViewModel> WarningMessageBoxInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IWarningMessageBoxInteractionViewModel>>(
                    ApplicationGlobalNames.WARNING_MESSAGEBOX_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the log
        ///      interaction provider
        /// </summary>
        public IInteractionProvider<ILogInteractionViewModel> LogInteractionProvider
        {
            get
            {
                return
                    this._container.Resolve<IInteractionProvider<ILogInteractionViewModel>>(
                        ApplicationGlobalNames.LOG_INTERACTION_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///      Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the Synchronization Time
        ///      interaction provider
        /// </summary>
        public IInteractionProvider<ISynchronizationTimeInteractionViewModel> SynchronizationTimeInteractionProvider
        {
            get
            {
                return
                    this._container.Resolve<IInteractionProvider<ISynchronizationTimeInteractionViewModel>>(
                        ApplicationGlobalNames.SYNCHRONIZATION_TIME_PROVIDER_NAME);
            }
        }


        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the update 
        ///     timeout interaction provider
        /// </summary>
        public IInteractionProvider<IUpdateTimeoutInteractionViewModel> UpdateTimeoutInteractionProvider
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IUpdateTimeoutInteractionViewModel>>(
                    ApplicationGlobalNames.UPDATE_TIMEOUT_PROVIDER_NAME);
            }
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the 
        ///     passord interaction provider
        /// </summary>
        public IInteractionProvider<IPasswordInteractionViewModel> PasswordInteractionViewModel
        {
            get
            {
                return this._container.Resolve<IInteractionProvider<IPasswordInteractionViewModel>>(
                    ApplicationGlobalNames.PASSWORD_INTERACTION_PROVIDER_NAME);
            }
        }

        #endregion [IInteractionProvidersRegistry members]
    }
}