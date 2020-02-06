using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using ULA.Business.ApplicationModeServices;
using ULA.Business.Infrastructure.ApplicationModes;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Business.PersistanceServices;
using ULA.CompositionRoot.IoC;
using ULA.Interceptors.Application;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using ULA.Presentation.Services;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.ViewModels;
using ULA.Presentation.ViewModels.Interactions;
using ULA.Presentation.Views;
using ULA.Presentation.Views.Interactions;
using ULA.Devices.Runo3.Presentation.ViewModels;
using ULA.Devices.Runo3.Presentation.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Unity;
using ULA.AddinsHost.Module;
using ULA.Business.Infrastructure.DTOs;
using ULA.Business.Module;
using ULA.Devices.PICON2.Business.Module;
using ULA.Devices.PICON2.Presentation.Module;
using ULA.Devices.PICONGS.Business.Module;
using ULA.Devices.PICONGS.Presentation.Module;
using ULA.Devices.Presentation.Module;
using ULA.Devices.Runo3.Business.Module;
using ULA.Devices.Runo3.Presentation.Module;
using ULA.Drivers.TcpFamily.Module;
using ULA.Presentation.DTOs;
using ULA.Presentation.Module;
using SchemeModeRuntimeView = ULA.Devices.Presentation.View.SchemeModeRuntimeView;

//using ULA.Devices.Runo3.Presentation.ViewModels;
//using RunoConfigurationModeView = ULA.Devices.Runo3.Presentation.Views.RunoConfigurationModeView;

namespace ULA.CompositionRoot.Bootstrapping
{
    /// <summary>
    ///     Represents composition root bootstrapper that is based on <see cref="UnityBootstrapper" />
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling"),
     System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
         "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public abstract class Bootstrapper : UnityBootstrapper
    {
        #region [Private fields]

        private readonly IApplicationStateManager _currentApplicationStateManager;
        private IApplicationContentContainer _contentContainer;

        #endregion [Private fields]

        /// <summary>
        ///     Gets an instance of <see cref="IApplicationContentContainer" /> that represents current appplication content
        /// </summary>
        public IApplicationContentContainer ContentContainer
        {
            get { return this._contentContainer; }
        }

        #region [Ctor's]

        /// <summary>
        ///     Initializes an instance of <see cref="Bootstrapper" />
        /// </summary>
        /// <param name="currentApplicationStateManager">
        ///     An instance of <see cref="IApplicationStateManager" /> that represents application management functionality
        /// </param>
        protected Bootstrapper(IApplicationStateManager currentApplicationStateManager)
        {
            this._currentApplicationStateManager = currentApplicationStateManager;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Run the bootstrapper process.
        /// </summary>
        /// <param name="runWithDefaultConfiguration">
        ///     If <see langword="true" />, registers default Composite Application Library services in the container. This is the default behavior.
        /// </param>
        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
            this._contentContainer = new ApplicationContentContainer(this.Container);
        }

        /// <summary>
        ///     Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />. May be overwritten in a derived class to
        ///     add specific
        ///     type mappings required by the application.
        /// </summary>
        [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        protected override void ConfigureContainer()
        {
            /*TODO: Moove this to config file during the next refactoring stage*/

            base.ConfigureContainer();

            this.Container.RegisterType<IIoCContainer, UnityIoCWrapper>();


            Container.RegisterInstance(typeof(ISoundNotificationsService), new SoundNotificationsService());

            Container.RegisterType<ApplicationConnectionService.ApplicationConnectionService>();
            var applicationConnectionService =
                Container.Resolve<ApplicationConnectionService.ApplicationConnectionService>();
            Container.RegisterInstance(applicationConnectionService);



            this.Container.RegisterInstance(this._currentApplicationStateManager,
                new ContainerControlledLifetimeManager());
            this.Container.RegisterInstance(this._currentApplicationStateManager.CurrentState,
                new ContainerControlledLifetimeManager());



            this.Container.RegisterType<IConfigurationModeDevicesService, DefaultConfigurationModeDevicesService>(
                new ApplicationStateLifetimeManager(this._currentApplicationStateManager.CurrentState));
            this.Container.RegisterType<IConfigurationModeDriversService, DefaultConfigurationModeDriversService>(
                new ApplicationStateLifetimeManager(this._currentApplicationStateManager.CurrentState));
            this.Container.RegisterType<IRuntimeModeDevicesServices, DefaultRuntimeModeDevicesService>(
                new ApplicationStateLifetimeManager(this._currentApplicationStateManager.CurrentState));
            this.Container.RegisterType<IRuntimeModeDriversService, DefaultRuntimeModeDriversService>(
                new ApplicationStateLifetimeManager(this._currentApplicationStateManager.CurrentState));


            this.Container.RegisterType<IPersistanceService, XmlPersistanceService>(
                new ApplicationStateLifetimeManager(this._currentApplicationStateManager.CurrentState));


            this.Container.RegisterType<UserControl, RootView>(ApplicationGlobalNames.ROOT_VIEW_NAME);
            this.Container.RegisterType<IRootViewModel, RootViewModel>();
            this.Container.RegisterType<IApplicationModeViewModelFactory, ApplicationModeViewModelFactory>();
            this.Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IInteractionService, InteractionService>(
                new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IInteractionProvidersRegistry, InteractionProvidersRegistry>(
                new ContainerControlledLifetimeManager());


            this.Container.RegisterType<IInteractionProvider<IBusyInteractionViewModel>, BusyInteractionProvider>(
                ApplicationGlobalNames.BUSY_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, BusyInteractionView>(
                ApplicationGlobalNames.BUSY_INTERACTION_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IBusyInteractionViewModel, BusyInteractionViewModel>(
                new TransientLifetimeManager());


            Container.RegisterType<IInteractionProvider<IExtraSettingsViewModel>, ExtraSettingsInteractionProvider>(
        ApplicationGlobalNames.EXTRASETTINGS_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            Container.RegisterType<IInteractionView, ExtraSettingsView>(
                ApplicationGlobalNames.EXTRASETTINGS_INTERACTION_VIEW_NAME, new TransientLifetimeManager());
            Container.RegisterType<IExtraSettingsViewModel, ExtraSettingsViewModel>(
                new TransientLifetimeManager());



            this.Container.RegisterType
                <IInteractionProvider<IErrorMessageBoxInteractionViewModel>, ErrorMessageBoxInteractionProvider>(
                    ApplicationGlobalNames.ERROR_MESSAGEBOX_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, ErrorMessageBoxInteractionView>(
                ApplicationGlobalNames.ERROR_MESSAGEBOX_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IErrorMessageBoxInteractionViewModel, ErrorMessageBoxInteractionViewModel>(
                new TransientLifetimeManager());

            this.Container.RegisterType<IInteractionProvider<IInformationMessageBoxInteractionViewModel>,
                InformationMessageBoxInteractionProvider>(
                    ApplicationGlobalNames.INFORMATION_MESSAGEBOX_INTERACTION_PROVIDER_NAME,
                    new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, InformationMessageBoxInteractionView>(
                ApplicationGlobalNames.INFORMATION_MESSAGEBOX_VIEW_NAME, new TransientLifetimeManager());
            this.Container
                .RegisterType<IInformationMessageBoxInteractionViewModel, InformationMessageBoxInteractionViewModel>(
                    new TransientLifetimeManager());

            this.Container.RegisterType
                <IInteractionProvider<IQuestionMessageBoxInteractionViewModel>, QuestionMessageBoxInteractionProvider>(
                    ApplicationGlobalNames.QUESTION_MESSAGEBOX_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, QuestionMessageBoxInteractionView>(
                ApplicationGlobalNames.QUESTION_MESSAGEBOX_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IQuestionMessageBoxInteractionViewModel, QuestionMessageBoxInteractionViewModel>
                (new TransientLifetimeManager());

            this.Container.RegisterType
                <IInteractionProvider<IWarningMessageBoxInteractionViewModel>, WarningMessageBoxInteractionProvider>(
                    ApplicationGlobalNames.WARNING_MESSAGEBOX_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, WarningMessageBoxInteractionView>(
                ApplicationGlobalNames.WARNING_MESSAGEBOX_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IWarningMessageBoxInteractionViewModel, WarningMessageBoxInteractionViewModel>(
                new TransientLifetimeManager());

            this.Container.RegisterType
                <IInteractionProvider<ICitywideCommandsInteractionViewModel>, CitywideCommandsInteractionProvider>(
                    ApplicationGlobalNames.CITYWIDE_COMMANDS_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<ICitywideCommandsInteractionViewModel, CitywideCommandsInteractionViewModel>(
                new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, CitywideCommandsInteractionView>(
                ApplicationGlobalNames.CITYWIDE_COMMANDS_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<ICitywideCommandsInteractionViewModel, CitywideCommandsInteractionViewModel>(
                new TransientLifetimeManager());


            this.Container.RegisterType
                <IInteractionProvider<ICommandOnTemplateInteractionViewModel>, CommandOnTemplatesInteractionProvider>(
                    ApplicationGlobalNames.COMMAND_ON_TEMPLATES_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<ICommandOnTemplateInteractionViewModel, CommandOnTemplateInteractionViewModel>(
                new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, CommandOnTemplateInteractionView>(
                ApplicationGlobalNames.COMMAND_ON_TEMPLATES_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<ICommandOnTemplateInteractionViewModel, CommandOnTemplateInteractionViewModel>(
                new TransientLifetimeManager());


            








            this.Container.RegisterType<IInteractionProvider<IAboutInteractionViewModel>, AboutInteractionProvider>(
                ApplicationGlobalNames.ABOUT_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, AboutInteractionView>(ApplicationGlobalNames.ABOUT_VIEW_NAME,
                new TransientLifetimeManager());
            this.Container.RegisterType<IAboutInteractionViewModel, AboutInteractionViewModel>(
                new TransientLifetimeManager());

            this.Container.RegisterType<IInteractionProvider<ILogInteractionViewModel>, LogInteractionProvider>(
                ApplicationGlobalNames.LOG_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, LogInteractionView>(ApplicationGlobalNames.LOG_VIEW_NAME,
                new TransientLifetimeManager());


            this.Container
                .RegisterType
                <IInteractionProvider<IUpdateTimeoutInteractionViewModel>, UpdateTimeoutInteractionProvider>(
                    ApplicationGlobalNames.UPDATE_TIMEOUT_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, UpdateTimeoutInteractionView>(
                ApplicationGlobalNames.UPDATE_TIMEOUT_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IUpdateTimeoutInteractionViewModel, UpdateTimeoutInteractionViewModel>(
                new TransientLifetimeManager());

            this.Container
                .RegisterType<IInteractionProvider<IPasswordInteractionViewModel>, PasswordInteractionProvider>(
                    ApplicationGlobalNames.PASSWORD_INTERACTION_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, PasswordInteractionView>(
                ApplicationGlobalNames.PASSWORD_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IPasswordInteractionViewModel, PasswordInteractionViewModel>(
                new TransientLifetimeManager());

            this.Container
                .RegisterType
                <IInteractionProvider<ISynchronizationTimeInteractionViewModel>, SynchronizationTimeInteractionProvider>
                (
                    ApplicationGlobalNames.SYNCHRONIZATION_TIME_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, SynchronizationTimeInteractionView>(
                ApplicationGlobalNames.SYNCHRONIZATION_TIME_VIEW_NAME, new TransientLifetimeManager());
            this.Container
                .RegisterType<ISynchronizationTimeInteractionViewModel, SynchronizationTimeInteractionViewModel>(
                    new TransientLifetimeManager());

            this.Container.RegisterType<IInteractionProvider<IModifyDeviceViewModel>, CreateNewDeviceProvider>(
                ApplicationGlobalNames.CREATE_NEW_DEVICE_PROVIDER_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IInteractionView, ModifyDeviceInteractionView>(
                ApplicationGlobalNames.CREATE_NEW_DEVICE_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<IModifyDeviceViewModel, ModifyDeviceInteractionViewModel>(
                new TransientLifetimeManager());


            this.Container.RegisterType<IApplicationConfigurationModeViewModel, ApplicationConfigurationModeViewModel>(
                ApplicationGlobalNames.CONFIGURATION_MODE_VIEW_MODEL_NAME);
            this.Container.RegisterType<IApplicationRuntimeModeViewModel, ApplicationRuntimeModeViewModel>(
                ApplicationGlobalNames.RUNTIME_MODE_VIEW_MODEL_NAME);
            this.Container.RegisterType<IApplicationFailureModeViewModel, ApplicationFailureModeViewModel>(
                ApplicationGlobalNames.FAILURE_MODE_VIEW_MODEL_NAME);

            this.Container.RegisterType<object, SchemeModeRuntimeView>(ApplicationGlobalNames.SCHEME_RUNTIME_VIEW_NAME,
                new TransientLifetimeManager());
            this.Container.RegisterType<object, ListWidgetModeRuntimeView>(
                ApplicationGlobalNames.LIST_WIDGET_RUNTIME_VIEW_NAME, new TransientLifetimeManager());
            this.Container.RegisterType<object, ListWidgetModeConfigurationView>(
                ApplicationGlobalNames.LIST_WIDGET_CONFIGURATION_VIEW_NAME, new TransientLifetimeManager());



            //вьюхи  конфигурации  разных типов устройств

            //this.Container.RegisterType<object, PICONGSConfigurationModeView>(
            //    ApplicationGlobalNames.PICONGS_CONFIGURATION_VIEW_NAME,
            //    new TransientLifetimeManager());
            //this.Container.RegisterType<object, PICON2ConfigurationModeView>(
            //    ApplicationGlobalNames.PICON2_CONFIGURATION_VIEW_NAME,
            //    new TransientLifetimeManager());





            //this.Container.RegisterType<object, Picon2LightingSheduleView>(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME,
            //    new TransientLifetimeManager());


            this.Container.RegisterType<ILogInformation, LogInformation>();




            this.Container.RegisterType<ILogInteractionViewModel, LogInteractionViewModel>();

            this.Container.RegisterType<ILightingSheduleViewModel, LightingSheduleViewModel>(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEWMODEL_NAME,
                new TransientLifetimeManager());


            //this.Container.RegisterType<ILightingSheduleViewModel, Picon2LightingSheduleViewModel>(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEWMODEL_NAME,
            //    new TransientLifetimeManager());


            //this.Container.RegisterType<IPresentationDataAccessLayerBinder, PresentationDataAccessLayerBinder>(
            //    new TransientLifetimeManager());

            /*TODO: skipping discovery part here*/



            InitializeModules();
        }

        #region Overrides of Bootstrapper




        /// <summary>
        /// настройка каталога модулей
        /// </summary>
        /// <returns></returns>
        protected override void ConfigureModuleCatalog()
        {

            var catalog = (ModuleCatalog)ModuleCatalog;
            catalog.AddModule(typeof(BusinessLogicModule));
            //catalog.AddModule(typeof(PiconGSModule));
            // catalog.AddModule(typeof(Picon2Module));
            catalog.AddModule(typeof(Runo3Module));
            catalog.AddModule(typeof(TcpModbusDriverModule));
            catalog.AddModule(typeof(DevicesViewModelModule));
            catalog.AddModule(typeof(Runo3BusinessModule));
            catalog.AddModule(typeof(AddinsModule));
            catalog.AddModule(typeof(PresentationModule));
            catalog.AddModule(typeof(PiconGsBusinessModule));
            catalog.AddModule(typeof(PiconGSModule));
            catalog.AddModule(typeof(Picon2BusinessModule));
            catalog.AddModule(typeof(Picon2Module));
            base.ConfigureModuleCatalog();

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void InitializeModules()
        {

            base.InitializeModules();

        }
        #endregion

        #endregion [Override members]
    }
}