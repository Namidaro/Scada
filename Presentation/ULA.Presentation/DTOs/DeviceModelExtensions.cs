using System.Collections.Generic;
using System.Linq;
using ULA.AddinsHost.Business.Device;
using ULA.Business.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.DTOs;

namespace ULA.Presentation.DTOs
{
    /// <summary>
    ///     Represents extension methods registry for <see cref="DeviceModel" />
    ///     Расширения для клаасов представляющих устройство
    /// </summary>
    public static class DeviceModelExtensions
    {
        /// <summary>
        ///     Maps the <see cref="IConfigLogicalDevice" /> to <see cref="DeviceModel" />
        ///     Конвертирует <see cref="IConfigLogicalDevice" /> в <see cref="DeviceModel" />
        /// </summary>
        /// <param name="device">
        ///     An instance of <see cref="IConfigLogicalDevice" /> to map to <see cref="DeviceModel" />
        /// </param>
        /// <returns>
        ///     Resulted instance of <see cref="DeviceModel" />
        /// </returns>
        public static DeviceModel ToDeviceModel(this IConfigLogicalDevice device)
        {
            DeviceModel deviceModel=new DeviceModel();

            
            deviceModel.DeviceFactoryTypeName = device.CreateMomento().State.DeviceType;
            deviceModel.Name = device.DeviceName;
            deviceModel.Description = device.DeviceDescription;
            deviceModel.AnalogMeterTypeName = device.CreateMomento().State.AnalogMeterType;
            deviceModel.DriverFactoryTypeName = "TCP_MB";
            deviceModel.StarterDescriptions =
                new List<string>(3)
                {
                  device.CreateMomento().State.Starter1Description,
                    device.CreateMomento().State.Starter2Description,
                     device.CreateMomento().State.Starter3Description
                };
            deviceModel.TransformKoefA = device.CreateMomento().State.TransformKoefA;
            deviceModel.TransformKoefB = device.CreateMomento().State.TransformKoefB;
            deviceModel.TransformKoefC = device.CreateMomento().State.TransformKoefC;
            deviceModel.ChannelNumberOfStarter1 = device.CreateMomento().State.ChannelNumberOfStarter1;
            deviceModel.ChannelNumberOfStarter2 = device.CreateMomento().State.ChannelNumberOfStarter2;
            deviceModel.ChannelNumberOfStarter3 = device.CreateMomento().State.ChannelNumberOfStarter3;
            var fiders = device.CreateMomento().State.CustomDeviceSchemeTable?.FidersTable
                .GetEnumeratorObjects();
            if (fiders != null) deviceModel.CustomFidersOnDevice.AddRange(fiders);
            var signals = device.CreateMomento().State.CustomDeviceSchemeTable?.SignalsTable.GetEnumeratorObjects();
            if (signals != null) deviceModel.CustomSignalsOnDevice.AddRange(signals);
            var inds = device.CreateMomento().State.CustomDeviceSchemeTable?.IndicatorsTable.GetEnumeratorObjects();
            if (inds != null) deviceModel.CustomIndicatorsOnDevice.AddRange(inds);
            var cascades= device.CreateMomento().State.CustomDeviceSchemeTable?.CascadeIndicatorsTable.GetEnumeratorObjects();
            if (cascades != null) deviceModel.CascadeIndicatorsOnDevice.AddRange(cascades);
            return deviceModel;
        }

        /// <summary>
        ///     Maps the <see cref="DeviceModel" /> to the <see cref="LogicalDeviceInformation" />
        ///     Конвертирует <see cref="DeviceModel" /> в <see cref="LogicalDeviceInformation" />
        /// </summary>
        /// <param name="model">
        ///     An instance of <see cref="DeviceModel" /> to map to <see cref="LogicalDeviceInformation" />
        /// </param>
        /// <returns>
        ///     Resulted instance of <see cref="LogicalDeviceInformation" />
        /// </returns>
        public static LogicalDeviceInformation ToLogicalDeviceInfo(this DeviceModel model)
        {
            LogicalDeviceInformation logicalDeviceInformation= new LogicalDeviceInformation
                {
                    DeviceName = model.Name,
                    DeviceDescription = model.Description,
                    DeviceTypeName = model.DeviceFactoryTypeName,
                    DriverTypeName = model.DriverFactoryTypeName,
                    DriverTcpAddress = model.TcpAddress,
                    DriverTcpPort = model.TcpPort,
                   AnalogMeterTypeName = model.AnalogMeterTypeName,
                   CustomFidersOnDevice=model.CustomFidersOnDevice,
                CustomIndicatorsOnDevice = model.CustomIndicatorsOnDevice,
                    CustomSignalsOnDevice = model.CustomSignalsOnDevice,
                    CascadeIndicatorsOnDevice = model.CascadeIndicatorsOnDevice,
                    TransformKoefA = model.TransformKoefA,
                    TransformKoefB = model.TransformKoefB,
                    TransformKoefC = model.TransformKoefC,
                    ChannelNumberOfStarter1 = model.ChannelNumberOfStarter1,
                    ChannelNumberOfStarter2 = model.ChannelNumberOfStarter2,
                    ChannelNumberOfStarter3 = model.ChannelNumberOfStarter3,
                StarterDescriptions = model.StarterDescriptions,

                };
            return logicalDeviceInformation;
        }
    }
}