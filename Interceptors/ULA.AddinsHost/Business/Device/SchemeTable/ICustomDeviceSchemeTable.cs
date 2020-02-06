using System.Collections.Generic;
using System.Runtime.InteropServices;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.SchemeTable.CustomTableItems;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomDeviceSchemeTable
    {
        /// <summary>
        /// 
        /// </summary>
         ITagNamedObjectCollection<ICustomFider> FidersTable { get; set; }



        /// <summary>
        /// 
        /// </summary>
        ITagNamedObjectCollection<ICustomSignal> SignalsTable { get; set; }


        /// <summary>
        /// 
        /// </summary>
        ITagNamedObjectCollection<ICustomIndicator> IndicatorsTable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITagNamedObjectCollection<ICascadeIndicator> CascadeIndicatorsTable { get; set; }
    }
}