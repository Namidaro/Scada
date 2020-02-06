using System;
using System.ComponentModel;
using ULA.Common.AOM;
using ULA.Common.Formatters;

namespace ULA.AddinsHost.Business.Device.DataTables
{
    /// <summary>
    ///     Exposees device data table row functionality
    /// </summary>
    public interface IDeviceDataTableRow :ITagNamedObject
    {
      

        /// <summary>
        ///     Gets the unique identifier of the driver's data that this value is associated with
        /// </summary>
        Guid DriverDataId { get; }

        /// <summary>
        ///     Gets an instance of <see cref="IValueFormatter" /> that represetns the strategy of formatting current value
        /// </summary>
        IValueFormatter Formatter { get; set; }

        /// <summary>
        ///     Get a value 
        /// </summary>
        object Value { get; set; }
     
    
    }
}