using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using ULA.AddinsHost.Business.Device;
using ULA.Business.BytesParsers;
using ULA.Business.DataServices;
using ULA.Business.DeviceDomain;
using ULA.Business.DeviceDomain.Commands;
using ULA.Business.DeviceDomain.Commands.Rules;
using ULA.Business.DeviceDomain.CustomItems;
using ULA.Business.Factories;
using ULA.Business.Infrastructure;
using ULA.Business.Infrastructure.BytesParsers;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Business.Infrastructure.LoggerServices;
using ULA.Business.Infrastructure.TimerInterrogation;
using ULA.Business.LoggerServices;
using ULA.Business.TimerInterrogation;

namespace ULA.Business.Module
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessLogicModule : IModule
    {
        private readonly IUnityContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public BusinessLogicModule(IUnityContainer container)
        {
            _container = container;
        }



        #region Implementation of IModule

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Initialize()
        {
            _container.RegisterType<IConfigLogicalDevice, ConfigLogicalDevice.ConfigLogicalDevice>();
            _container.RegisterType<ICommandSendingService, CommandSendingService>();
            _container.RegisterType<IDataLoadingService, DataLoadingService>();
            _container.RegisterType<IMetadataParserService, MetadataParserService>();
            _container.RegisterType<IDeviceTimerInterrogationService, TimerInterrigationService>();
            _container.RegisterType<IAnalogTimerInterrogation, AnalogTimerInterrogationService>();

            _container.RegisterType<IRawBytesToDeviceStateParserService, RawBytesToDeviceStateParserService>();
            _container.RegisterType<IRawBytesToDeviceStateParser, PartialUpdateParser>(DeviceStringKeys.DeviceBytesParsers.PARTIAL_MODE_PARSER);
            _container.RegisterType<IRawBytesToDeviceStateParser, DateTimeParser>(DeviceStringKeys.DeviceBytesParsers.DATETIME_PARSER);
            _container.RegisterType<IRawBytesToDeviceStateParser, SignalLevelParser>(DeviceStringKeys.DeviceBytesParsers.SIGNAL_LEVEL_PARSER);
            _container.RegisterType<IRawBytesToDeviceStateParser, DeviceSignatureParser>(DeviceStringKeys.DeviceBytesParsers.DEVICE_SIGNATURE_PARSER);
            _container.RegisterType<IRawBytesToDeviceStateParser, AnalogsParser>(DeviceStringKeys.DeviceBytesParsers.ANALOGS_PARSER);
            _container.RegisterType<IRawBytesToDeviceStateParser, AnalogDateTimeParser>(DeviceStringKeys.DeviceBytesParsers.ANALOG_DATETIME_PARSER);

            _container.RegisterType<IAnalogMeter, EnergomeraAnalogMeter>(DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE);
            _container.RegisterType<IAnalogMeter, Msa961AnalogMeter>(DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE);

            _container.RegisterType<IRawBytesToDeviceStateParserFactory, RawBytesToDeviceStateParserFactory>();

            _container.RegisterType<IStarter, Starter>();
            _container.RegisterType<IDefectState, DefectState>();
            _container.RegisterType<SyncTimeDeviceCommand>();
            _container.RegisterType<IDataWritingService, DataWritingService>();
            _container.RegisterType<IDeviceDataCache, DeviceDataCache>();
            _container.RegisterType<IDeviceCommandFactory, DeviceCommandFactory>();
            _container.RegisterType<IDeviceCommandService, DeviceCommandService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAnalogData, AnalogData>();
            _container.RegisterType<ICommandSuccessRule, LightingCommandSuccessRule>();
            _container.RegisterType<IGlobalDefectAcknowledgingService, GlobalDefectAcknowledgingService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDefectAcknowledgingService, DefectAcknowledgingService>();
            _container.RegisterType<IDeviceLogLoadingService, DeviceLogLoadingService>();
            _container.RegisterType<IResistor, Resistor>();
            _container.RegisterType<IResistorFactory,ResistorFactory>();

            _container.RegisterType<IDeviceCustomItems, DeviceCustomItems>();
            _container.RegisterType<IAnalogMeterFactory, AnalogMeterFactory>();

            _container.RegisterType<ICustomItemsFactory, CustomItemsFactory>();
            _container.RegisterType<ISignal, Signal>();
            _container.RegisterType<IIndicator, Indicator>();
            _container.RegisterType<ICascade, Cascade>();
            _container.RegisterType<DefectLoggerService>();
            _container.RegisterType<ConnectionLogger>();

            _container.RegisterType<StarterLoggerService>();
        }

        #endregion
    }
}
