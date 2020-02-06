using System.Collections.Generic;

namespace ULA.AddinsHost.Business.Device.SchemeTable
{
    /// <summary>
    ///     Exposes a device scheme table functionality
    /// </summary>
    public interface IConfiguratedDeviceSchemeTable
    {
        /// <summary>
        ///     Add instance of <see cref="IDeviceStarterRow"/> to scheme table
        /// </summary>
        /// <param name="starterRow">Adding instance of <see cref="IDeviceStarterRow"/></param>
        void AddStarterRow(IDeviceStarterRow starterRow);

        /// <summary>
        ///     Add instance of <see cref="IDeviceResistorRow"/> to scheme table
        /// </summary>
        /// <param name="resistorRow">Adding instance of <see cref="IDeviceResistorRow"/></param>
        void AddResistorRow(IDeviceResistorRow resistorRow);




        /// <summary>
        ///     Get a instance of <see cref="IDeviceStarterRow"/> from device scheme table. Если Пускателя с таким id нет, то вернет null
        /// </summary>
        /// <param name="starterId">Id of requered starter</param>
        /// <returns>Requeres instance of <see cref="IDeviceStarterRow"/> or null if this id not found</returns>
        IDeviceStarterRow GetStarterRow(int starterId);

        /// <summary>
        ///     Get a instance of <see cref="IDeviceResistorRow"/> from device scheme table. Если Резистора с таким id нет, то вернет null
        /// </summary>
        /// <param name="resistorId">Id of requered resistor</param>
        /// <returns>Requeres instance of <see cref="IDeviceResistorRow"/> or null if this id not found</returns>
        IDeviceResistorRow GetResistorRow(int resistorId);

        /// <summary>
        ///     Get a IEnumerable for instancs IDeviceResistorRow  
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDeviceResistorRow> GetResistorEnumerable();




        /// <summary>
        ///      Get a instance of <see cref="IDeviceStarterRow"/> from device scheme table
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDeviceStarterRow> GetChannelsEnumerable();

        /// <summary>
        ///     Get starter with index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>starter</returns>
        IDeviceStarterRow GetStarterByIndex(int index);

        /// <summary>
        ///     Get resistor with index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>resistor</returns>
        IDeviceResistorRow GetResistorByIndex(int index);
    }
}
