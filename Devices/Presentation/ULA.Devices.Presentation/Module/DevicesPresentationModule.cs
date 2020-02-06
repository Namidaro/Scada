using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Devices.Presentation.Factories;
using ULA.Devices.Presentation.Interfaces;
using ULA.Devices.Presentation.Runtime;
using ULA.Devices.Presentation.Runtime.AnalogMeters;
using ULA.Devices.Presentation.Runtime.CustomItems;
using ULA.Devices.Presentation.Runtime.OutgoingLines;
using ULA.Devices.Presentation.View;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.ViewModels;
using ULA.Presentation.Views;

namespace ULA.Devices.Presentation.Module
{
    public class DevicesViewModelModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IResourcesService _resourceService;

        public DevicesViewModelModule(IUnityContainer container, IResourcesService resourceService)
        {
            _container = container;
            _resourceService = resourceService;
        }


        #region Implementation of IModule

        public void Initialize()
        {
            _container.RegisterType<IRuntimeDeviceViewModel, RuntimeDeviceViewModel>();
            _resourceService.AddResourceAsGlobal("Resources/DevicesResources.xaml", this.GetType().Assembly);
            //_resourceService.AddResourceAsGlobal("ResourceTemplates/TimeModeDataTemplate.xaml", this.GetType().Assembly);
            //_resourceService.AddResourceAsGlobal("ResourceTemplates/NoTimeModeDataTemplate.xaml", this.GetType().Assembly);
            _container.RegisterType<IConfigDeviceViewModelFactory, ConfigDeviceViewModelFactory>();
            _container.RegisterType<IConfigDeviceViewModel, ConfigDeviceViewModel>();
            _container.RegisterType<IStarterViewModelFactory, StarterViewModelFactory>();
            _container.RegisterType<IStarterViewModel, StarterViewModel>();
            _container.RegisterType<IDefectStateViewModel, DefectStateViewModel>();
            _container.RegisterType<ISchemeModeRuntimeViewModel, SchemeModeRuntimeViewModel>(
                new ContainerControlledLifetimeManager());

            _container.RegisterType<IDeviceCommandStateViewModel, DeviceCommandStateViewModel>();

            _container.RegisterType<IDeviceCommandQueueViewModel, DeviceCommandQueueViewModel>();
            _container.RegisterType<IDeviceCommandStateViewModelFactory, DeviceCommandStateViewModelFactory>();

            _container.RegisterType<IAnalogDataViewModel, AnalogDataViewModel>();
            _container.RegisterType<IOutgoingLinesViewModelFactory, OutgoingLinesViewModelFactory>();
            _container.RegisterType<ResistorOutgoingLinesViewModel>();
            _container.RegisterType<IResistorViewModelFactory, ResistorViewModelFactory>();
            _container.RegisterType<IResistorViewModel, ResistorViewModel>();

            _container.RegisterType<ICustomItemsViewModelFactory, CustomItemsViewModelFactory>();
            _container.RegisterType<ICustomItemsViewModel, CustomItemsViewModel>();
            _container.RegisterType<ICascadeViewModel, CascadeViewModel>();
            _container.RegisterType<IIndicatorViewModel, IndicatorViewModel>();
            _container.RegisterType<ISignalViewModel, SignalViewModel>();
            _container.RegisterType<object, LightingSheduleView>(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEW_NAME,
                new TransientLifetimeManager());

            _container.RegisterType<IAnalogMeterViewModelFactory, AnalogMeterViewModelFactory>();

            _container.RegisterType<IAnalogMeterViewModel, EnergomeraAnalogMeterViewModel>(DeviceStringKeys
                .DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE);
            _container.RegisterType<IAnalogMeterViewModel, MsaAnalogMeterViewModel>(DeviceStringKeys
                .DeviceAnalogMetersTagKeys.MSA_METER_TYPE);

            _container.RegisterType<FidersOutgoingLinesViewModel>();
            _container.RegisterType<IFiderViewModel, FiderViewModel>();

            _container.RegisterType<IQueryIndicatorViewModel, QueryIndicatorViewModel>();


            #endregion
        }
    }
}
