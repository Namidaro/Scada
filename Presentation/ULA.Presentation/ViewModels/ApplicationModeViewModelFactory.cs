using System;
using ULA.Interceptors.IoC;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Represents an application mode view model factory functionality
    ///     Представляет фабрику вью-моделей для режимов приложения
    /// </summary>
    public class ApplicationModeViewModelFactory : IApplicationModeViewModelFactory
    {
        #region [Fields]

        private readonly IIoCContainer _container;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates aninstance of <see cref="ApplicationModeViewModelFactory" />
        /// </summary>
        /// <param name="container">An instance of <see cref="IIoCContainer" /> to use</param>
        public ApplicationModeViewModelFactory(IIoCContainer container)
        {
            this._container = container;
        }

        #endregion [Ctor's]

        #region [IApplicationModeViewModelFactory members]

        /// <summary>
        ///     Creates an instance of <see cref="IApplicationConfigurationModeViewModel" />
        ///     Создает вью-модель для режима конфигурации
        /// </summary>
        /// <returns>Created instance of <see cref="IApplicationConfigurationModeViewModel" /></returns>
        public IApplicationConfigurationModeViewModel CreateConfigurationModeViewModel()
        {
            return this._container
                .Resolve<IApplicationConfigurationModeViewModel>(
                    ApplicationGlobalNames.CONFIGURATION_MODE_VIEW_MODEL_NAME);
        }

        /// <summary>
        ///     Creates an instance of <see cref="IApplicationRuntimeModeViewModel" />
        ///     Создает вью-модель для режима реального времени
        /// </summary>
        /// <returns>Created instance of <see cref="IApplicationRuntimeModeViewModel" /></returns>
        public IApplicationRuntimeModeViewModel CreateRuntimeModeViewModel()
        {
            return this._container
                .Resolve<IApplicationRuntimeModeViewModel>(ApplicationGlobalNames.RUNTIME_MODE_VIEW_MODEL_NAME);
        }

        /// <summary>
        ///     Creates an instance of <see cref="IApplicationFailureModeViewModel" />
        ///     Создает вью-модель для режима ошибки
        /// </summary>
        /// <param name="failure">An instance of <see cref="Exception" /> that represents current mode failure</param>
        /// <returns>Created instance of <see cref="IApplicationFailureModeViewModel" /></returns>
        public IApplicationFailureModeViewModel CreateFailureModeViewModel(Exception failure)
        {
            var result = this._container
                .Resolve<IApplicationFailureModeViewModel>(ApplicationGlobalNames.FAILURE_MODE_VIEW_MODEL_NAME);

            if (result == null) return null;
            result.SetFailure(failure);
            return result;
        }

        #endregion [IApplicationModeViewModelFactory members]
    }
}