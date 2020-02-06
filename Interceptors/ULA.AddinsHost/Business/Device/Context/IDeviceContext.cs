using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.SchemeTable;

namespace ULA.AddinsHost.Business.Device.Context
{
    /// <summary>
    ///     Описывает контекст устройства
    /// </summary>
    public interface IDeviceContext
    {


        /// <summary>
        ///     Gets or sets the name of current device
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        ///     Gets or sets the desciption of current device
        /// </summary>
        string DeviceDescription { get; set; }

        /// <summary>
        ///     Gets or sets the type of current device
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        ///     Gets or sets the type of meter current device
        /// </summary>
        string AnalogMeterType { get; set; }
     /// <summary>
     /// 
     /// </summary>
        string RelatedDriverId { get; set; }


        string Starter1Description { get; set; }
        string Starter2Description { get; set; }
        string Starter3Description { get; set; }

        string ChannelNumberOfStarter1 { get; set; }
        string ChannelNumberOfStarter2 { get; set; }
        string ChannelNumberOfStarter3 { get; set; }

        int TransformKoefA { get; set; }
        int TransformKoefB { get; set; }
        int TransformKoefC { get; set; }



        /// <summary>
        /// 
        /// </summary>
        int DeviceNumber { get; set; }


        /// <summary>
        ///    
        /// </summary>
        ITagNamedObjectCollection<IDeviceDataTableRow> DataTable { get;  }

        /// <summary>
        ///     Gets or sets an instance of <see cref="IConfiguratedDeviceSchemeTable" /> that represents current device scheme table
        /// </summary>
        IConfiguratedDeviceSchemeTable SchemeTable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ICustomDeviceSchemeTable CustomDeviceSchemeTable { get; set; }
        
    }
}