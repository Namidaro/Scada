using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
using ULA.Devices.PICON2.Business;
using ULA.Devices.PICON2.Presentation.Interfaces;
using ULA.Devices.PICON2.Presentation.ViewModels;
using ULA.Devices.PICON2.Presentation.Views;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.PICON2.Presentation.Module
{
    public class Picon2Module : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IResourcesService _resourceService;
        private const string DEVICE_NAME = "PICON2";
        public Picon2Module(IUnityContainer container, IResourcesService resourceService)
        {
            _container = container;
            _resourceService = resourceService;
        }

        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<ILogicalDeviceViewModelFactory, Picon2LogicalDeviceViewModelFactory>(DEVICE_NAME);
            _container.RegisterType<object, PICON2ConfigurationModeView>(DEVICE_NAME +
                                                                          ApplicationGlobalNames.CONFIGURATION_VIEW_NAME,
                new TransientLifetimeManager());
    

            _container.RegisterType<object, Picon2LightingSheduleView>(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME,
                new TransientLifetimeManager());


            _container.RegisterType<ILightingSheduleViewModel,Picon2LightingSheduleViewModel>(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEWMODEL_NAME);
            _container.RegisterType<IConfigurationModeViewModel, PICON2ConfigurationModeViewModel>(
                ApplicationGlobalNames.PICON2_CONFIGURATION_VIEWMODEL_NAME,
                new TransientLifetimeManager());

            _container.RegisterType<IPicon2ModuleInfoViewModel, Picon2ModuleInfoViewModel>();
            _container.RegisterType<object,Picon2ModuleInfoView>(ApplicationGlobalNames.PICON2_MODULE_INFO_VIEW_NAME);
            _resourceService.AddResourceAsGlobal("Resources/Picon2Resources.xaml", this.GetType().Assembly);

        }

        #endregion
    }
}
