using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ULA.AddinsHost.Business;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Context;
using ULA.AddinsHost.Business.Device.DataTables;
using ULA.AddinsHost.Business.Device.SchemeTable;
using ULA.AddinsHost.Business.Driver;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.AddinsHost.Business.Exceptions;
using ULA.AddinsHost.Business.Strings;
using ULA.Business.AddressesContainer;
using ULA.Business.AddressesContainer.Entities;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Common.Formatters;
using ULA.Localization;
using ULA.Presentation.Infrastructure.DTOs;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.ConfigLogicalDevice
{
    /// <summary>
    ///     Represents basic configuration logical deviceViewModel functionality
    ///     Представляет базовый функционал устройства в режиме реального времени
    /// </summary>
    public class ConfigLogicalDevice : Disposable, IConfigLogicalDevice, INotifyPropertyChanged
    {
        #region [Private fields]

        private IDeviceContext _context;
        IPersistanceService _persistanceService;
        private string _deviceType;

        #endregion [Private fields]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistanceService"></param>
        public ConfigLogicalDevice(IPersistanceService persistanceService)
        {
            _persistanceService = persistanceService;
        }


        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="IDataContentContainer" /> that represents current deviceViewModel data container
        ///     Контейнер специфичных данных устройства
        /// </summary>
        protected IDeviceContext DeviceContext
        {
            get { return this._context; }
        }

    

        /// <summary>
        ///     Gets an instance of <see cref="IConfiguratedDeviceSchemeTable"/> that represents current deviceViewModel scheme table
        ///     Таблица схемы устройства
        /// </summary>
        protected IConfiguratedDeviceSchemeTable SchemeTable
        {
            get { return this._context.SchemeTable; }
        }

        #endregion [Properties]

        #region [IConfigLogicalDevice members]



        /// <summary>
        ///     Gets or sets an instance of <see cref="Guid" /> that represents deviceViewModel unique identifier
        ///     Id устройства
        /// </summary>
        Guid ILogicalDevice.DeviceId { get; set; }

        /// <summary>
        ///     Gets the name of the deviceViewModel
        ///     Имя устройства
        /// </summary>
        public string DeviceName
        {
            get { return this._context.DeviceName; }
        }

        /// <summary>
        ///     Gets or sets the desciption of the deviceViewModel
        ///     Описание устройства 
        /// </summary>
        public string DeviceDescription
        {
            get { return this._context.DeviceDescription; }
        }

        public IDeviceMomento DeviceMomento { get; set; }

        public int DeviceNumber
        {
            get { return _context.DeviceNumber; }
            set { _context.DeviceNumber = value; }
        }

        /// <summary>
        ///     Creates an instance of <see cref="IDeviceMomento" /> that reprents current deviceViewModel state momento
        ///     Вернет состояние устройства
        /// </summary>
        /// <returns>
        ///     An instance of <see cref="IDeviceMomento" /> that represents current deviceViewModel state momento
        /// </returns>
        public virtual IDeviceMomento CreateMomento()
        {
            return new DefaultConfigLogicalDeviceMomento(this._context);
        }

        /// <summary>
        ///     Sets an instance of <see cref="IDeviceMomento" /> that represents current deviceViewModel state momento asynchronously
        ///     Сохраняет состояние устройства
        /// </summary>
        /// <param name="momento">
        ///     An instance of <see cref="IDeviceMomento" /> to restore current deviceViewModel state from
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents cyrrent asynchronous operation</returns>
        public Task SetMomentoAsync(IDeviceMomento momento)
        {
            return Task.Factory.StartNew(() => this.SetMomento(momento));
        }

        /// <summary>
        ///     Пытается достать DeviceDataTable. Сокращает (CreateMomento().State.DataTable) 
        /// </summary>
        public ITagNamedObjectCollection<IDeviceDataTableRow> RestoreDataTableFromMemento
        {
            get { return this.CreateMomento().State.DataTable; }
        }


       

        /// <summary>
        ///    Даст тип счетчика устройства
        /// </summary>
        public string AnalogMeterType { get; set; }

        public IDriverMomento DriverMomento
        {
            get;
            set;
        }

        #endregion [IConfigLogicalDevice members]

        #region [Public members]

        /// <summary>
        ///     Initialize current deviceViewModel asynchronously
        ///     Асинхронно инициализирует устройство
        /// </summary>
        /// <param name="context">
        ///     An instance of <see cref="IDeviceContext" /> that represents current deviceViewModel context
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public async Task InitializeAsync(IDeviceContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _deviceType = context.DeviceType;
            AnalogMeterType = context.AnalogMeterType;
            this._context = context;
            this._context.SchemeTable = new DefaultSchemeTable();
            await SeedingAsync();
        }

        public string DeviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }

        #endregion [Public members]

        #region [Templated members]

        /// <summary>
        ///     Seeds current deviceViewModel with data asynchronously
        ///     Заполняет устройство тех данными
        /// </summary>
        protected async Task SeedingAsync()
        {



            var stringDriverId = this.DeviceContext.RelatedDriverId;
            if (string.IsNullOrEmpty(stringDriverId))
                throw new SetupDriverException(LocalizationResources.Instance.NoAssociatedDriverExceptionMessage);

            var driverPersistableContext = await _persistanceService.GetDriverPersistableContextAsync(
                Guid.Parse(stringDriverId));
            if (driverPersistableContext == null)
                throw new SetupDriverException(
                    LocalizationResources.Instance.NotAccessFromPersistableToDriver +
                    stringDriverId);
            var driverMomento = await driverPersistableContext.GetDriverMomentoAsync();
            if (driverMomento == null)
                throw new SetupDriverException(
                    LocalizationResources.Instance.NotRestoreStateOfDriver + stringDriverId);

          
            await driverPersistableContext.SaveDriverMomentoAsync(driverMomento);

        }




        /// <summary>
        ///     Добавляет с таблицу данных устройства новую строку.
        /// </summary>
        /// <param name="tag">Тэг строки данных</param>
        /// <param name="driverDataId">Ссылка на Id блока данных в таблице драйвера</param>
        /// <param name="formatter">Объект конвертирующий данные между байтовым(как хранится в устройстве) и исходным
        /// (представленным в программе) типом данным</param>
        protected void AddDataRow(string tag, Guid driverDataId, BinaryFormatterBase formatter)
        {
            var row = new DefaultDataRow
            {
                DriverDataId = driverDataId,
                Formatter = formatter,
                Tag = tag
            };
            _context.DataTable.AddObject(row);
        }


        #endregion [Templated members]

        #region [Help members]

        /// <summary>
        ///     Sets an instance of <see cref="IDeviceMomento" /> that represents current deviceViewModel state momento
        ///     Сохраняет состояние устройства
        /// </summary>
        /// <param name="momento">
        ///     An instance of <see cref="IDeviceMomento" /> to restore current deviceViewModel state from
        /// </param>
        private void SetMomento(IDeviceMomento momento)
        {
            if (momento == null) throw new ArgumentNullException("momento");
            var context = momento.State;
            if (context == null) throw new ArgumentException();
            this._context = context;
        }

        #endregion [Help members]

        #region [INotifyPropertyChanged members]

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// реализация INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion [INotifyPropertyChanged members]
    }
}