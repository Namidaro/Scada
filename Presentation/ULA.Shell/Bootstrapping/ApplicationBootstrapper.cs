using System.Windows;
using Microsoft.Practices.Unity;
using ULA.CompositionRoot.Bootstrapping;
using ULA.Interceptors.Application;
using ULA.Presentation.Infrastructure.Services;
using ULA.Shell.ApplicationLevelServices;

namespace ULA.Shell.Bootstrapping
{
    /// <summary>
    ///     Represents the application bootstrapper
    ///     Представляет бутстрапер приложения
    /// </summary>
    public class ApplicationBootstrapper : Bootstrapper
    {
        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="ApplicationBootstrapper" />
        /// </summary>
        /// <param name="currentApplicationStateManager">
        ///     An instance of <see cref="IApplicationStateManager" /> that represents application management functionality
        /// </param>
        public ApplicationBootstrapper(IApplicationStateManager currentApplicationStateManager)
            : base(currentApplicationStateManager)
        {
        }

        #endregion [Ctor's]

        #region [Properties]

        /// <summary>
        ///     Gets an instanhce of <see cref="DependencyObject" /> that represents current shell
        /// </summary>
        public DependencyObject CurrentShell
        {
            get { return Shell; }
        }

        #endregion [Properties]

        #region [Override members]

        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        ///     The shell of the application.
        /// </returns>
        /// <remarks>
        ///     If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        ///     <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default
        ///     <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        ///     the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" />
        ///     attached property
        ///     in order to be able to add regions by using the
        ///     <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        ///     attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<Shell>();
        }

        /// <summary>
        ///     Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />. May be overwritten in a derived class to
        ///     add specific
        ///     type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            this.Container.RegisterType<IResourcesService, DefaultResourceService>(
                new ContainerControlledLifetimeManager());

            base.ConfigureContainer();

            this.Container.RegisterType<IApplicationSettingsService, ApplicationSettingsService>(
                new ContainerControlledLifetimeManager());
        }

        #endregion [Override members]
    }
}