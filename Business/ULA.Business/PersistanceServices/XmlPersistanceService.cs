using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ULA.Business.Exceptions;
using ULA.Business.Infrastructure.PersistanceServices;
using ULA.Interceptors.Application;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Business.PersistanceServices
{
    /// <summary>
    ///     Represents default logical devices persistance service functionality
    ///     Сервис работы с xml
    /// </summary>
    public class XmlPersistanceService : Disposable, IPersistanceService
    {
        #region [Const]

        private const string DECLARATION_VERSION = "1.0";
        private const string DECLARATION_ENCODING = "utf-8";

        private const string DEVICE_ELEMENT_NAME = "deviceViewModel";
        private const string ID_ELEMENT_NAME = "id";
        private const string DRIVER_ELEMENT_NAME = "driver";

        private const string DEVICES_ROOT_NAME = "devices";
        private const string DRIVERS_ROOT_NAME = "drivers";

        private const string CONTENT_ROOT_NAME = "Content";
        private const string APPLICATION_STORAGE_SPACE_NAME = "Application storage space";
        private const string DEVICES_TABLES_COMMENT_VALUE = "Devices tables";
        private const string DRIVERS_TABLES_COMMENT_VALUE = "Drivers tables";

        #endregion

        #region [Private fields]

        private IDictionary<Guid, LogicalDeviceXmlPersistableContext> _devicePersistableContentCache;
        private XElement _devicesRoot;
        private IDictionary<Guid, LogicalDriverXmlPersistableContext> _driverPersistableContentCache;
        private XElement _driversRoot;
        private XDocument _root;
        private IApplicationSettingsService _settingsService;
        private object _lockObj;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="XmlPersistanceService" />
        /// </summary>
        public XmlPersistanceService(IApplicationSettingsService settingsService)
        {
            this._settingsService = settingsService;
            this._devicePersistableContentCache = new Dictionary<Guid, LogicalDeviceXmlPersistableContext>();
            this.Initialization = this.OnInitializing();
            _lockObj = new object();
        }

        #endregion [Ctor's]

        #region [ILogicalDevicesPersistanceService members]

        /// <summary>
        ///     Gets an instance of System.Threading.Tasks.Task that represents asynchronous initialization strategy
        /// Таска инициализации сервиса
        /// </summary>
        public Task Initialization { get; private set; }

        /// <summary>
        ///     Gets a collection of all deviceViewModel persistable contexts
        /// Вернет весь сохраненный в xml контекст(дынные) устройств
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public Task<IEnumerable<KeyValuePair<Guid, IDevicePersistableContext>>> GetDevicePersistableContextsAsync()
        {
            Task<IEnumerable<KeyValuePair<Guid, IDevicePersistableContext>>> result =
                Task.Factory.StartNew(
                    () =>
                        this._devicePersistableContentCache.Select(
                            s => new KeyValuePair<Guid, IDevicePersistableContext>(s.Key, s.Value))
                            .ToArray()
                            .AsEnumerable());
            return result;
        }

        /// <summary>
        ///     Gets an instance of <see cref="IDevicePersistableContext" /> that represents persisting context for a deviceViewModel asynchronously
        /// Вернет контекст устройства по id-ку из xml
        /// </summary>
        /// <param name="deviceId">
        ///     An instance of <see cref="Guid" /> that represents current deviceViewModel unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public async Task<IDevicePersistableContext> GetDevicePersistableContextAsync(Guid deviceId)
        {
            this.ThrowIfDisposed();
            LogicalDeviceXmlPersistableContext result;
            if (this._devicePersistableContentCache.TryGetValue(deviceId, out result)) return result;

            var xElement = new XElement(DEVICE_ELEMENT_NAME, new XAttribute(ID_ELEMENT_NAME, deviceId));
            this._devicesRoot.Add(xElement);
            await this.SaveRootAsync();

            result = this.CreateDevicePersistableContextAsync(xElement);
            this._devicePersistableContentCache.Add(deviceId, result);
            return result;
        }

        /// <summary>
        ///     Gets an instance of <see cref="IDriverPersistableContext" /> that represents persisting context for a driver asynchronously
        /// Вернет контекст драйвера по id-ку из xml
        /// </summary>
        /// <param name="driverId">
        ///     An instance of <see cref="Guid" /> that represents current driver unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public async Task<IDriverPersistableContext> GetDriverPersistableContextAsync(Guid driverId)
        {
            this.ThrowIfDisposed();
            LogicalDriverXmlPersistableContext result;
            if (this._driverPersistableContentCache.TryGetValue(driverId, out result)) return result;

            var xElement = new XElement(DRIVER_ELEMENT_NAME, new XAttribute(ID_ELEMENT_NAME, driverId));
            this._driversRoot.Add(xElement);
            await this.SaveRootAsync();

            result = this.CreateDriverPersistableContextAsync(xElement);
            this._driverPersistableContentCache.Add(driverId, result);
            return result;
        }

        /// <summary>
        ///     Gets a collection of all drivers persistable contexts
        /// Вернет контекс для драйверов из xml
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public Task<IEnumerable<KeyValuePair<Guid, IDriverPersistableContext>>> GetDriversPersistableContextAsynk()
        {
            return
                Task.Factory.StartNew(
                    () =>
                        this._driverPersistableContentCache.Select(
                            s => new KeyValuePair<Guid, IDriverPersistableContext>(s.Key, s.Value))
                            .ToArray()
                            .AsEnumerable());
        }

        /// <summary>
        ///     Remove deviceViewModel from XML persistable context asynchronously
        /// Удалит  из xml-ны контекст устройства с id, переданным в параметрах
        /// </summary>
        /// <param name="deviceId">
        ///     An instance of <see cref="Guid" /> that represents current deviceViewModel unique identifier
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public async Task RemoveDevicePersistanbleContextAsync(Guid deviceId)
        {
            this.ThrowIfDisposed();

            LogicalDeviceXmlPersistableContext result;
            if (this._devicePersistableContentCache.TryGetValue(deviceId, out result))
            {
                this._devicePersistableContentCache.Remove(deviceId);
            }

            var xElement = new XElement(DEVICE_ELEMENT_NAME, new XAttribute(ID_ELEMENT_NAME, deviceId));
            var deletedDevices = this._devicesRoot.Descendants()
                .Where(
                    e =>
                        e.Name == DEVICE_ELEMENT_NAME && e.HasAttributes &&
                        e.Attribute(ID_ELEMENT_NAME).Value == deviceId.ToString())
                .ToArray();

            for (int i = 0; i < deletedDevices.Length; i++)
            {
                deletedDevices[i].Remove();
            }
            await this.SaveRootAsync();
        }

        /// <summary>
        ///     Remove driver from persistable context asynchronously
        /// Удалит  из xml-ны контекст драйвера с id, переданным в параметрах
        /// </summary>
        /// <param name="driverId">A instance of <see cref="Guid"/> that represents current deviceViewModel unique identifier</param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current synchronous operation</returns>
        public async Task RemoveDriverPersistableContextAsynk(Guid driverId)
        {
            this.ThrowIfDisposed();

            LogicalDriverXmlPersistableContext result;
            if (this._driverPersistableContentCache.TryGetValue(driverId, out result))
            {
                this._driverPersistableContentCache.Remove(driverId);
            }

            var deletedDriver = this._driversRoot.Descendants()
                .Where(
                    e =>
                        e.Name == DRIVER_ELEMENT_NAME && e.HasAttributes &&
                        e.Attribute(ID_ELEMENT_NAME).Value == driverId.ToString())
                .ToArray();

            for (int index = 0; index < deletedDriver.Length; index++)
            {
                deletedDriver[index].Remove();
            }
            await this.SaveRootAsync();
        }

        #endregion [ILogicalDevicesPersistanceService members]

        #region [Help members]

        //Инициализирует это класс
        private async Task OnInitializing()
        {
            var storageFileFullName = new FileInfo(this._settingsService.DevicesStoragePath);
            if (storageFileFullName.Directory == null) throw new PersistanceServiceFileNotFoundException();
            if (!Directory.Exists(storageFileFullName.Directory.FullName))
            {
                Directory.CreateDirectory(storageFileFullName.Directory.FullName);
                this.SeedStorageFile(storageFileFullName);
                this._devicePersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDeviceXmlPersistableContext>();
                this._driverPersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDriverXmlPersistableContext>();
                return;
            }
            else if (!File.Exists(this._settingsService.DevicesStoragePath))
            {
                this.SeedStorageFile(storageFileFullName);
                this._devicePersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDeviceXmlPersistableContext>();
                this._driverPersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDriverXmlPersistableContext>();
                return;
            }
            try
            {
                var text = await this.ReadTextAsync(storageFileFullName.FullName);
                var xDoc = XDocument.Parse(text);
                await this.ParseStorageAsync(xDoc);
            }
            catch (Exception exception)
            {
                throw new PersistanceServiceFileFormatException(exception);
            }
        }

        //Парсит эту xml-ну
        private Task ParseStorageAsync(XDocument xDoc)
        {
            return Task.Factory.ContinueWhenAll(this.GetParsingSteps(xDoc).ToArray(),
                tasks => tasks, TaskContinuationOptions.ExecuteSynchronously);
        }

        private IEnumerable<Task> GetParsingSteps(XDocument xDoc)
        {
            yield return Task.Factory.StartNew(() =>
            {
                this._devicePersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDeviceXmlPersistableContext>();
                var devicesRoot = this.GetRootElement(xDoc, DEVICES_ROOT_NAME);
                foreach (var xElement in devicesRoot.Elements(DEVICE_ELEMENT_NAME))
                {
                    var guid = Guid.Parse(xElement.Attribute(ID_ELEMENT_NAME).Value);
                    this._devicePersistableContentCache.Add(guid, this.CreateDevicePersistableContextAsync(xElement));
                }
                this._devicesRoot = devicesRoot;
            });
            yield return Task.Factory.StartNew(() =>
            {
                this._driverPersistableContentCache =
                    new ConcurrentDictionary<Guid, LogicalDriverXmlPersistableContext>();
                var driversRoot = this.GetRootElement(xDoc, DRIVERS_ROOT_NAME);
                foreach (var xElement in driversRoot.Elements(DRIVER_ELEMENT_NAME))
                {
                    var guid = Guid.Parse(xElement.Attribute(ID_ELEMENT_NAME).Value);
                    this._driverPersistableContentCache.Add(guid, this.CreateDriverPersistableContextAsync(xElement));
                }
                this._driversRoot = driversRoot;
            });
            this._root = xDoc;
        }

        //вернет из xml документа элемент с именем rootName
        private XElement GetRootElement(XDocument xDoc, string rootName)
        {
            var root = xDoc.Root;
            if (root == null) throw new PersistanceServiceFileFormatException();
            var result = root.Element(rootName);
            if (result == null) throw new PersistanceServiceFileFormatException();
            return result;
        }

        //Создет и зполняет xml документ данными
        private void SeedStorageFile(FileSystemInfo storageFileFullName)
        {
            var xDoc = new XDocument(new XDeclaration(DECLARATION_VERSION, DECLARATION_ENCODING, ""));
            var root = new XElement(CONTENT_ROOT_NAME);
            root.Add(new XComment(APPLICATION_STORAGE_SPACE_NAME));
            var devicesRoot = new XElement(DEVICES_ROOT_NAME);
            devicesRoot.Add(new XComment(DEVICES_TABLES_COMMENT_VALUE));
            var driversRoot = new XElement(DRIVERS_ROOT_NAME);
            driversRoot.Add(new XComment(DRIVERS_TABLES_COMMENT_VALUE));
            root.Add(devicesRoot);
            root.Add(driversRoot);
            xDoc.Add(root);
            xDoc.Save(storageFileFullName.FullName);

            this._root = xDoc;
            this._devicesRoot = devicesRoot;
            this._driversRoot = driversRoot;
        }

        //Асинхронно считывает текст из файла
        private async Task<string> ReadTextAsync(string filePath)
        {
            using (TextReader sourceStream = File.OpenText(filePath))
            {
                try
                {
                    var sb = new StringBuilder();
                    string line;
                    while ((line = await sourceStream.ReadLineAsync()) != null)
                        sb.Append(line);
                    return sb.ToString();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

            }
            return null;
        }

        //создаст контекст для устройств
        private LogicalDeviceXmlPersistableContext CreateDevicePersistableContextAsync(XElement root)
        {
            return new LogicalDeviceXmlPersistableContext(root, this.SaveRootAsync);
        }

        //создаст контекст для драйверов
        private LogicalDriverXmlPersistableContext CreateDriverPersistableContextAsync(XElement root)
        {
            return new LogicalDriverXmlPersistableContext(root, this.SaveRootAsync);
        }

        //сохранит xml-ну
        private Task SaveRootAsync()
        {
            var path = this._settingsService.DevicesStoragePath;
            return Task.Factory.StartNew(() =>
            {
                lock (_lockObj)
                {
                    this._root.Save(path);
                }
            });
        }

        #endregion [Help members]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            this.Initialization.ContinueWith(task =>
            {
                if (this._devicePersistableContentCache != null)
                {
                    foreach (var context in this._devicePersistableContentCache)
                        context.Value.Dispose();
                    this._devicePersistableContentCache.Clear();
                    this._devicePersistableContentCache = null;
                }
                if (this._driverPersistableContentCache != null)
                {
                    foreach (var context in this._driverPersistableContentCache)
                        context.Value.Dispose();
                    this._driverPersistableContentCache.Clear();
                    this._driverPersistableContentCache = null;
                }
                this._devicesRoot = null;
                this._driversRoot = null;
                this._root = null;
                this._settingsService = null;
                base.OnDisposing();
            });
        }

        #endregion [Override members]
    }
}