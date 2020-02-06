using System.Collections.Generic;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.Business.Infrastructure.DTOs
{
    /// <summary>
    ///     Represents logical deviceViewModel information structure
    /// </summary>
    public class LogicalDeviceInformation
    {
        /// <summary>
        ///     Gets or sets the name of the deviceViewModel
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        ///     Gets or sets the desciption of the deviceViewModel
        /// </summary>
        public string DeviceDescription { get; set; }

        /// <summary>
        ///     Gets or sets the name of deviceViewModel type
        /// </summary>
        public string DeviceTypeName { get; set; }
   /// <summary>
        ///     Gets or sets the name of deviceViewModel meter type
        /// </summary>
        public string AnalogMeterTypeName { get; set; }


        /// <summary>
        ///     Gets or sets the name of the driver with what current deviceViewModel is associated
        /// </summary>
        public string DriverTypeName { get; set; }



        /// <summary>
        ///     Gets or sets the tcp port number of a driver
        /// </summary>
        public int DriverTcpPort { get; set; }

        /// <summary>
        ///     Gets or sets the tcp address of a driver
        /// </summary>
        public string DriverTcpAddress { get; set; }

   
        /// <summary>
        /// 
        /// </summary>
        public List<ICustomFider> CustomFidersOnDevice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ICustomSignal> CustomSignalsOnDevice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ICustomIndicator> CustomIndicatorsOnDevice { get; set; }
        public List<ICascadeIndicator> CascadeIndicatorsOnDevice { get; set; }

        /// <summary>
        ///     Описание пускателей
        /// </summary>
        public List<string> StarterDescriptions { get; set; }


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
    }
}