using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Microsoft.Practices.ObjectBuilder2;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.Business.DeviceDomain.Commands;
using ULA.Business.Infrastructure.Commands;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Factories;
using ULA.Business.Infrastructure.Metadata;
using ULA.Interceptors;
using ULA.Interceptors.IoC;

namespace ULA.Business.Factories
{
    public class DeviceCommandFactory : IDeviceCommandFactory
    {
        private readonly IIoCContainer _container;

        public DeviceCommandFactory(IIoCContainer container)
        {
            _container = container;
        }


        #region Implementation of IDeviceCommandFactory

        public IDeviceCommand CreateSyncTimeCommand(DateTime dateTime, IRuntimeDevice runtimeDevice)
        {
            SyncTimeDeviceCommand syncTimeDeviceCommand = _container.Resolve<SyncTimeDeviceCommand>();
            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.DATETIME_ID_NAME);

            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            syncTimeDeviceCommand.EntityMetadata = new EntityMetadata() { NumberOfPoints = (ushort)countAddresses, StartAddress = (ushort)address };
            syncTimeDeviceCommand.SetValue(dateTime);
            return syncTimeDeviceCommand;
        }

        public IDeviceCommand CreateRunNightlightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach(starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            });

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.ON, starters);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        public IDeviceCommand CreateStopNightlightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        public IDeviceCommand CreateRunFullLightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.NIGHTLIGHTING||starter.StarterLightingMode==LightingModeEnum.ECONOMY_NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            }));

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.ON, starters);
        }

        public IDeviceCommand CreateStopFullLightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.NIGHTLIGHTING || starter.StarterLightingMode == LightingModeEnum.ECONOMY_NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        public IDeviceCommand CreateRunBackLightLightingCommand(IRuntimeDevice runtimeDevice)
        { var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.BACKLIGHT || starter.StarterLightingMode == LightingModeEnum.ECONOMY_BACKLIGHT));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        public IDeviceCommand CreateStopBackLightLightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.BACKLIGHT|| starter.StarterLightingMode == LightingModeEnum.ECONOMY_BACKLIGHT));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        public IDeviceCommand CreateRunIlluminationCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ILLUMINATION || starter.StarterLightingMode == LightingModeEnum.ECONOMY_ILLUMINATION));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.ON, starters);
        }

        public IDeviceCommand CreateStopIlluminationCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ILLUMINATION || starter.StarterLightingMode == LightingModeEnum.ECONOMY_ILLUMINATION));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        public IDeviceCommand CreateRunAutoModeCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice;
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                if (starter.IsStarterOn.HasValue)
                {
                    tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                    boolValues.Add(false);
                    tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                    boolValues.Add(starter.IsStarterOn.Value);
                }
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.AUTO, starters);
        }
        public IDeviceCommand CreateStopAutoModeCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice;
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                if (starter.IsStarterOn.HasValue)
                {
                    tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                    boolValues.Add(true);
                    tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                    boolValues.Add(starter.IsStarterOn.Value);
                }
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.MANUAL, starters);
        }

        public IDeviceCommand CreateStartRepairCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice;
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_REPAIR_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            }));

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.Repair, starters);
        }
        public IDeviceCommand CreateStopRepairCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice;
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_REPAIR_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));
            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                CommandTypesEnum.NoRepair, starters);
        }

        public IDeviceCommand CreateStartEnergySavingCommand(IRuntimeDevice modelParentRuntimeDevice)
        {
            var starters = modelParentRuntimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ENERGY_SAVING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach(starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            });

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(modelParentRuntimeDevice),
                CommandTypesEnum.ON, starters);
        }

        public IDeviceCommand CreateStopEnergySavingCommand(IRuntimeDevice modelParentRuntimeDevice)
        {
            var starters = modelParentRuntimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ENERGY_SAVING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(modelParentRuntimeDevice),
                CommandTypesEnum.OFF, starters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDevice"></param>
        /// <returns></returns>
        public IDeviceCommand CreateRunEveninglightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ECONOMY_NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
            }));

            return CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),
                 CommandTypesEnum.ON,starters);
        }

        public IDeviceCommand CreateStopEveninglightingCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice.Where((starter =>
                starter.StarterLightingMode == LightingModeEnum.ECONOMY_NIGHTLIGHTING));
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_MANUALMODE_STARTER + starter.ChannelNumber);
                boolValues.Add(true);
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));

            IDeviceCommand command= CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice),CommandTypesEnum.OFF,starters);
           
            return command;
        }

        public IDeviceCommand CreateStopAllStartersCommand(IRuntimeDevice runtimeDevice)
        {
            var starters = runtimeDevice.StartersOnDevice;
            List<bool> boolValues = new List<bool>();
            List<string> tags = new List<string>();
            starters.ForEach((starter =>
            {
                tags.Add(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_SWITCHING_STARTER + starter.ChannelNumber);
                boolValues.Add(false);
            }));
            IDeviceCommand command= CreateLightingCommandByTagsAndValues(tags, boolValues, GetLightingEntityMetadata(runtimeDevice), CommandTypesEnum.OFF, starters);
            return command;
        }

        #endregion


        private IDeviceCommand CreateLightingCommandByTagsAndValues(List<string> tags, List<bool> boolValues,EntityMetadata entityMetadata ,CommandTypesEnum commandType,IEnumerable<IStarter> starters)
        {
            if (starters.Any((starter => { return starter.IsInRepairState != null && starter.IsInRepairState.Value; })))
            {
                if (!tags.Any(s => s.Contains(DeviceStringKeys.DeviceCommandsTagKeys.COMMAND_REPAIR_STARTER)))
                {
                    throw new ArgumentException("Пускатель в режиме ремонта");
                }
            }
            StartersCommand startersCommand = _container.Resolve<StartersCommand>();
            startersCommand.EntityMetadata = entityMetadata;
            startersCommand.SetTag(tags.ToArray());
            startersCommand.SetValue(boolValues.ToArray());
            startersCommand.CommandType = commandType;
            startersCommand.Starters.Clear();
            startersCommand.Starters.AddRange(starters);

            ICommandSuccessRule commandSuccessRule = _container.Resolve<ICommandSuccessRule>();
            commandSuccessRule.SetRuleData(starters.Select((starter => starter.StarterNumber)).ToList(),commandType);
            startersCommand.SetRule(commandSuccessRule);

            return startersCommand;
        }




        private EntityMetadata GetLightingEntityMetadata(IRuntimeDevice runtimeDevice)
        {
            var driverDataTable = runtimeDevice.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable")
                .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.COMMAND_MANAGENT_ID_NAME);

            var countAddresses = Convert.ToInt16(driverDataTable.Properties["Length"].Value);
            var address = driverDataTable.Properties["Address"].Value;
            return new EntityMetadata() { NumberOfPoints = (ushort)countAddresses, StartAddress = (ushort)address };
        }


    }
}
