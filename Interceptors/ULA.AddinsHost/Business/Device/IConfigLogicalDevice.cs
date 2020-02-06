using System.Threading.Tasks;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.ViewModel.Device;

namespace ULA.AddinsHost.Business.Device
{
    /// <summary>
    ///     Exposes configuration logic device functionality
    ///     Аналогично <see cref="IRuntimeDeviceViewModel"/>
    /// </summary>
    public interface IConfigLogicalDevice : ILogicalDevice
    {
        /// <summary>
        ///     Creates an instance of <see cref="IDeviceMomento" /> that reprents current device state momento
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IDeviceMomento" /> that represents current device state momento
        /// </returns>
        IDeviceMomento CreateMomento();

        /// <summary>
        ///     Sets an instance of <see cref="IDeviceMomento" /> that represents current device state momento asynchronously
        /// </summary>
        /// <param name="momento">
        ///     An instance of <see cref="IDeviceMomento" /> to restore current device state from
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents cyrrent asynchronous operation</returns>
        Task SetMomentoAsync(IDeviceMomento momento);

        /// <summary>
        ///     Пытается достать DeviceDataTable. Сокращает (CreateMomento().State.DataTable) 
        /// </summary>
        ITagNamedObjectCollection<IDeviceDataTableRow> RestoreDataTableFromMemento { get; }

   
        /// <summary>
        ///     Initialize current device asynchronously
        ///     Асинхронно инициализирует устройство
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="IDeviceContext" /> that represents current device context
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        Task InitializeAsync(IDeviceContext context);


        /// <summary>
        ///    Даст тип устройства
        /// </summary>
         string DeviceType { get; set; }


        /// <summary>
        ///    Даст тип счетчика устройства
        /// </summary>
         string AnalogMeterType { get; set; }
    }
}