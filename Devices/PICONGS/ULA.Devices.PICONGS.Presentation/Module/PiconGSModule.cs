using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Presentation;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business;
using ULA.Business.ConfigLogicalDevice;
using ULA.Devices.PICONGS.Business;
using ULA.Devices.PICONGS.Presentation.ViewModels;
using ULA.Devices.PICONGS.Presentation.Views;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.PICONGS.Presentation.Module
{
    /// <summary>
    /// 
    /// </summary>
    public class PiconGSModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IResourcesService _resourceService;
        private const string DEVICE_NAME = "PICONGS";
        public PiconGSModule(IUnityContainer container, IResourcesService resourceService)
        {
            _container = container;
            _resourceService = resourceService;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<ILogicalDeviceViewModelFactory, PicongsLogicalDeviceViewModelFactory>(DEVICE_NAME);
            _container.RegisterType<object, PICONGSConfigurationModeView>(DEVICE_NAME +
                                                                       ApplicationGlobalNames.CONFIGURATION_VIEW_NAME,
                new TransientLifetimeManager());


            _container.RegisterType<IConfigurationModeViewModel, PICONGSConfigurationModeViewModel>(
                ApplicationGlobalNames.PICONGS_CONFIGURATION_VIEWMODEL_NAME,
                new TransientLifetimeManager());
        }

        #endregion
    }
}
