using System.Collections.Generic;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.Presentation.Infrastructure.DTOs
{
    /// <summary>
    ///     Represetns default device data transfer object model
    ///     Представляет модель данных для устройства
    /// </summary>
    public class DeviceModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DeviceModel()
        {
            CustomFidersOnDevice=new List<ICustomFider>();
            CustomIndicatorsOnDevice=new List<ICustomIndicator>();
            CustomSignalsOnDevice=new List<ICustomSignal>();
            CascadeIndicatorsOnDevice=new List<ICascadeIndicator>();
        }

        #region [IDeviceModel members]




        /// <summary>
        ///     Gets or sets the name of the device
        ///     Имя устройства
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description of the device
        ///     Описание устройства
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the tcp-ip address of the remote device
        ///     TCP/IP адрес устройства
        /// </summary>
        public string TcpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the tcp-ip port of the remote device
        ///     TCP порт устройства
        /// </summary>
        public int TcpPort { get; set; }

        /// <summary>
        ///     Gets or sets the type name of the device
        ///     Имя типа устройства
        /// </summary>
        public string DeviceFactoryTypeName { get; set; }


        /// <summary>
        ///     Gets or sets the type name of the meter of the device
        ///     Имя типа счетчика устройства
        /// </summary>
        public string AnalogMeterTypeName { get; set; }


        /// <summary>
        ///     Gets or sets the type name of the meter of the device
        ///     Имя типа счетчика устройства
        /// </summary>
        public int TransformKoefA { get; set; }


        /// <summary>
        ///     Gets or sets the type name of the meter of the device
        ///     Имя типа счетчика устройства
        /// </summary>
        public int TransformKoefB { get; set; }
        /// <summary>
        ///     Gets or sets the type name of the meter of the device
        ///     Имя типа счетчика устройства
        /// </summary>
        public int TransformKoefC { get; set; }
        /// <summary>
        ///     Gets or sets the type name of the driver that current device is going to consume
        ///     Имя типа драйвера
        /// </summary>
        public string DriverFactoryTypeName { get; set; }

        /// <summary>
        /// Описание стартеров
        /// </summary>
        public List<string> StarterDescriptions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ICustomFider> CustomFidersOnDevice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ICustomIndicator> CustomIndicatorsOnDevice { get; set; }     
        /// <summary>
        /// 
        /// </summary>
        public List<ICustomSignal> CustomSignalsOnDevice { get; set; }

        public List<ICascadeIndicator> CascadeIndicatorsOnDevice { get; set; }



        /// <summary>
        ///   номер канала для пускателя 1
        /// </summary>
        public string ChannelNumberOfStarter1 { get; set; }


        /// <summary>
        ///   номер канала для пускателя 2
        /// </summary>
        public string ChannelNumberOfStarter2 { get; set; }


        /// <summary>
        ///   номер канала для пускателя 3
        /// </summary>
        public string ChannelNumberOfStarter3 { get; set; }


        #endregion [IDeviceModel members]


    }
}