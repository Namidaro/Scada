using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Devices.Runo3.Business;
using ULA.Devices.Runo3.Presentation.ViewModels;
using ULA.Devices.Runo3.Presentation.Views;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.Runo3.Presentation.Module
{
    /// <summary>
    /// 
    /// </summary>
    public class Runo3Module : IModule
    {
        private readonly IUnityContainer _container;
        private const string DEVICE_NAME = "RUNO";
        public Runo3Module(IUnityContainer container)
        {
            _container = container;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<ILogicalDeviceViewModelFactory, Runo3LogicalDeviceViewModelFactory>(DEVICE_NAME);
            _container.RegisterType<object, RunoConfigurationModeView>(DEVICE_NAME+
                ApplicationGlobalNames.CONFIGURATION_VIEW_NAME,
                new TransientLifetimeManager());
            _container.RegisterType<IConfigurationModeViewModel, RunoConfigurationModeViewModel>(
                ApplicationGlobalNames.RUNO_CONFIGURATION_VIEWMODEL_NAME,
                new TransientLifetimeManager());
        }

        #endregion
    }
}
