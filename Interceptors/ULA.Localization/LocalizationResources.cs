

using System;


namespace ULA.Localization
{
	/// <summary>
    ///   Represents an application resources accessor functionality
    /// </summary>
    public class LocalizationResources
    {
		private static readonly Lazy<LocalizationResources> _factory =
            new Lazy<LocalizationResources>(() => new LocalizationResources());

        /// <summary>
        ///     Gets current instance of <see cref="LocalizationResources" />
        /// </summary>
        public static LocalizationResources Instance
        {
            get { return LocalizationResources._factory.Value; }
        }


		private static System.Resources.ResourceManager resourceMan;

		/// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
		internal static System.Resources.ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(resourceMan, null)) 
				{
                    var temp = new System.Resources.ResourceManager("ULA.Localization.Properties.Resources", typeof(LocalizationResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
			

	    /// <summary>
        /// Gets a resource by its name
        /// </summary>
        /// <param name="resource">The name of the resource to obtain</param>
        /// <returns>An instance of the obtained resource</returns>
		public string this[string resource]
		{
			get
			{
				return string.IsNullOrWhiteSpace(resource)
                          ? string.Empty
                          : ResourceManager.GetString(resource, null);
			}
		}



		/// <summary>
        ///   Looks up a localized string similar to 4444
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string PortExample 
		{
			get 
			{
				return ResourceManager.GetString("PortExample", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Функционал устройства не реализован для режиме конфигурации.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NoDeviceForConfigurationExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("NoDeviceForConfigurationExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Во время выполнения операции обнаружена ошибка: {0}
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string UnexpectedErrorMessagePattern 
		{
			get 
			{
				return ResourceManager.GetString("UnexpectedErrorMessagePattern", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to 127.0.0.1
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpAddressExample 
		{
			get 
			{
				return ResourceManager.GetString("IpAddressExample", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ручной режим
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ManualmodeCommand 
		{
			get 
			{
				return ResourceManager.GetString("ManualmodeCommand", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Обнаружена ошибка при работе с драйвером.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverExceptionSimpleMessage 
		{
			get 
			{
				return ResourceManager.GetString("DriverExceptionSimpleMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to IP адрес устройства не задан
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpAddressValidationNotSet 
		{
			get 
			{
				return ResourceManager.GetString("IpAddressValidationNotSet", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Хотите ли вы создать новое устройство?
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string WantToCreateNewDevice 
		{
			get 
			{
				return ResourceManager.GetString("WantToCreateNewDevice", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Синхронизация времени
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string SynchronizationTimeTitle 
		{
			get 
			{
				return ResourceManager.GetString("SynchronizationTimeTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Авторежим
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AutoMode 
		{
			get 
			{
				return ResourceManager.GetString("AutoMode", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Имя устройства не задано
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceNameValidationNotSet 
		{
			get 
			{
				return ResourceManager.GetString("DeviceNameValidationNotSet", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Драйвер таблицы данных
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverDataTable 
		{
			get 
			{
				return ResourceManager.GetString("DriverDataTable", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Дата
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string LogDate 
		{
			get 
			{
				return ResourceManager.GetString("LogDate", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Невозможно восстановить состояние драйвера с идентификатором:
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NotRestoreStateOfDriver 
		{
			get 
			{
				return ResourceManager.GetString("NotRestoreStateOfDriver", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Помощь
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string HelpHeader 
		{
			get 
			{
				return ResourceManager.GetString("HelpHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Вы хотите создать новое устройство?
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string WantCreateDevice 
		{
			get 
			{
				return ResourceManager.GetString("WantCreateDevice", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Действие
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string LogDescription 
		{
			get 
			{
				return ResourceManager.GetString("LogDescription", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Сохраненный слой не может получить доступ к драйверу с  ID:
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NotAccessFromPersistableToDriver 
		{
			get 
			{
				return ResourceManager.GetString("NotAccessFromPersistableToDriver", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Управление командами
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CitywideCommandsTitle 
		{
			get 
			{
				return ResourceManager.GetString("CitywideCommandsTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Название должно содержать символы или цифры и длина вводимого текста не должна превышать 20 символов
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceNameTooltip 
		{
			get 
			{
				return ResourceManager.GetString("DeviceNameTooltip", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Порт виртуального устройства. Применяется валидация свойственная TCP/IP порту.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TcpIpAddressTooltip 
		{
			get 
			{
				return ResourceManager.GetString("TcpIpAddressTooltip", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Нет
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NoButtonContent 
		{
			get 
			{
				return ResourceManager.GetString("NoButtonContent", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Имя уcтройства 1
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceNameExample 
		{
			get 
			{
				return ResourceManager.GetString("DeviceNameExample", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Защита
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string RepairDefandTitle 
		{
			get 
			{
				return ResourceManager.GetString("RepairDefandTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ошибка в устройстве: {0}.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceExceptionInfoPartMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("DeviceExceptionInfoPartMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Отключить все
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string StopAll 
		{
			get 
			{
				return ResourceManager.GetString("StopAll", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Полное
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Full 
		{
			get 
			{
				return ResourceManager.GetString("Full", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Строка в таблице данных драйвера с идентификатором {0} уже существует
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverDataTableDataRowExistsExceptionMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("DriverDataTableDataRowExistsExceptionMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Закрыть
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Close 
		{
			get 
			{
				return ResourceManager.GetString("Close", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Тип устройства {0} не найдет в системе.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string UnknownDeviceTypeExceptionMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("UnknownDeviceTypeExceptionMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to IP-адрес виртуального устройства. Применяется валидация свойственная для IP-адреса. Части адреса должны быть разделены точками ('.').
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TCPModbusDriverSettingsTooltip 
		{
			get 
			{
				return ResourceManager.GetString("TCPModbusDriverSettingsTooltip", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Редактировать
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string EditSelectedDeviceHeader 
		{
			get 
			{
				return ResourceManager.GetString("EditSelectedDeviceHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to +
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string UpSymbol 
		{
			get 
			{
				return ResourceManager.GetString("UpSymbol", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Режим реального времени
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string RuntimeMode 
		{
			get 
			{
				return ResourceManager.GetString("RuntimeMode", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Краткое описание виртуального устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceDescriptionTooltip 
		{
			get 
			{
				return ResourceManager.GetString("DeviceDescriptionTooltip", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Устройства не найдены
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NoDevicesDetected 
		{
			get 
			{
				return ResourceManager.GetString("NoDevicesDetected", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Новое
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NewDeviceHeader 
		{
			get 
			{
				return ResourceManager.GetString("NewDeviceHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Вечернее
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Evening 
		{
			get 
			{
				return ResourceManager.GetString("Evening", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Обнаружена ошибка при работе с устройством.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceExceptionSimpleMessage 
		{
			get 
			{
				return ResourceManager.GetString("DeviceExceptionSimpleMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to IP порт устройства задан
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpPortValidationNotSet 
		{
			get 
			{
				return ResourceManager.GetString("IpPortValidationNotSet", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Версия 5.0
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string VersionProgram 
		{
			get 
			{
				return ResourceManager.GetString("VersionProgram", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Путь к хранилищу информации об устройствах задан не верно
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DataStorageFilePathExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("DataStorageFilePathExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to От:
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string From 
		{
			get 
			{
				return ResourceManager.GetString("From", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to О программе
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AboutHeader 
		{
			get 
			{
				return ResourceManager.GetString("AboutHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Пускатели
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Starters 
		{
			get 
			{
				return ResourceManager.GetString("Starters", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Включить все
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string StartAll 
		{
			get 
			{
				return ResourceManager.GetString("StartAll", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Драйвер с такими параметрами уже существует. Хост {0}, Порт {1}
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverAlreadyExistsExceptionMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("DriverAlreadyExistsExceptionMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Имя свойства не может быть пустым
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AomPropertyTypePropertyNameExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("AomPropertyTypePropertyNameExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ассоциируемый драйвер с текущим устройством не найден
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NoAssociatedDriverExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("NoAssociatedDriverExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Описание устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceDescription 
		{
			get 
			{
				return ResourceManager.GetString("DeviceDescription", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Неверный формат IP адреса устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpAddressValidationFormat 
		{
			get 
			{
				return ResourceManager.GetString("IpAddressValidationFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to До:
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string To 
		{
			get 
			{
				return ResourceManager.GetString("To", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Тип драйвера {0} не найдет в системе.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string UnknownDriverTypeExceptionMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("UnknownDriverTypeExceptionMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Синхронизация времени
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TimeSynchronizationHeader 
		{
			get 
			{
				return ResourceManager.GetString("TimeSynchronizationHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Создать виртуальное устройство
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CreateNewDeviceDialogTitle 
		{
			get 
			{
				return ResourceManager.GetString("CreateNewDeviceDialogTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Имя устройства должно содержать от 2-х до 20-ти символов.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceNameValidationRange 
		{
			get 
			{
				return ResourceManager.GetString("DeviceNameValidationRange", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Создать
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string SubmitCreateButtonContent 
		{
			get 
			{
				return ResourceManager.GetString("SubmitCreateButtonContent", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Подсветка
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Backlight 
		{
			get 
			{
				return ResourceManager.GetString("Backlight", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Количество ошибок: {0}
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ValidationTemplate 
		{
			get 
			{
				return ResourceManager.GetString("ValidationTemplate", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Устройство
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string VirtualDeviceHeader 
		{
			get 
			{
				return ResourceManager.GetString("VirtualDeviceHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Идёт загрузка. Ожидайте....
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string WaitingBusyDialogMessage 
		{
			get 
			{
				return ResourceManager.GetString("WaitingBusyDialogMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Драйвер (TCP Modbus)
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TCPModbusDriverSettings 
		{
			get 
			{
				return ResourceManager.GetString("TCPModbusDriverSettings", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Устройство с именем {0} уже существует.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceAlreadyExistsExceptionMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("DeviceAlreadyExistsExceptionMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Энергосбережение
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string StoregEnergy 
		{
			get 
			{
				return ResourceManager.GetString("StoregEnergy", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Общее
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CommonDeviceSettings 
		{
			get 
			{
				return ResourceManager.GetString("CommonDeviceSettings", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Время
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Time 
		{
			get 
			{
				return ResourceManager.GetString("Time", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Управление Наружным Освещением
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ApplicationTitle 
		{
			get 
			{
				return ResourceManager.GetString("ApplicationTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ожидайте...
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string BusyDialogTitle 
		{
			get 
			{
				return ResourceManager.GetString("BusyDialogTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ошибка в драйвере: {0}.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverExceptionInfoPartMessageFormat 
		{
			get 
			{
				return ResourceManager.GetString("DriverExceptionInfoPartMessageFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Управление по шаблонам
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CommandOnTemplateTitle 
		{
			get 
			{
				return ResourceManager.GetString("CommandOnTemplateTitle", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Нет возможности работать с драйвером типа:
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NotWorkWithDriverType 
		{
			get 
			{
				return ResourceManager.GetString("NotWorkWithDriverType", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ошибка
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ErrorHeader 
		{
			get 
			{
				return ResourceManager.GetString("ErrorHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Устройство не найдено
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string NoDeviceDetected 
		{
			get 
			{
				return ResourceManager.GetString("NoDeviceDetected", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Удалить
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeleteSelectedDeviceHeader 
		{
			get 
			{
				return ResourceManager.GetString("DeleteSelectedDeviceHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to IP порт устройства должен быть в пределах от 0 до 9999
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpPortValidationRange 
		{
			get 
			{
				return ResourceManager.GetString("IpPortValidationRange", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Команда не может быть послана
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string StarterNoExistMessage 
		{
			get 
			{
				return ResourceManager.GetString("StarterNoExistMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to -
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DownSymbol 
		{
			get 
			{
				return ResourceManager.GetString("DownSymbol", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ok
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string OkButtonContent 
		{
			get 
			{
				return ResourceManager.GetString("OkButtonContent", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ip - адрес устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TcpIpAddress 
		{
			get 
			{
				return ResourceManager.GetString("TcpIpAddress", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Да
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string YesButtonContent 
		{
			get 
			{
				return ResourceManager.GetString("YesButtonContent", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Перейти на схему
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ViewSelectedDeviceSchemaHeader 
		{
			get 
			{
				return ResourceManager.GetString("ViewSelectedDeviceSchemaHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Устройство находится в режиме ремонта. Управление недоступно
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string RepairDefand 
		{
			get 
			{
				return ResourceManager.GetString("RepairDefand", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Тип сущности не может быть пустым
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AomEntityTypeNullTypeExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("AomEntityTypeNullTypeExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Формат файла хранилища данных устройства не верен
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DataStorageFileFormatExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("DataStorageFileFormatExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Ночное
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Nighlighting 
		{
			get 
			{
				return ResourceManager.GetString("Nighlighting", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Порт устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string TcpIpPort 
		{
			get 
			{
				return ResourceManager.GetString("TcpIpPort", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Журнал системы
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string JournalOfSystemsHeader 
		{
			get 
			{
				return ResourceManager.GetString("JournalOfSystemsHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Драйвер с идентификатором {0} не имеет таблицы данных для настройки
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DriverNotHaveDataTableSetup 
		{
			get 
			{
				return ResourceManager.GetString("DriverNotHaveDataTableSetup", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Синхронизировать
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Synchronize 
		{
			get 
			{
				return ResourceManager.GetString("Synchronize", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Отмена
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CancelButtonContent 
		{
			get 
			{
				return ResourceManager.GetString("CancelButtonContent", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Режим конфигурации
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string ConfigurationMode 
		{
			get 
			{
				return ResourceManager.GetString("ConfigurationMode", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Коллекция типов свойств в нотации AOM не задана
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AomPropertyNullCollectionExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("AomPropertyNullCollectionExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Иллюминация
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string Illumination 
		{
			get 
			{
				return ResourceManager.GetString("Illumination", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Тип свойства не может быть пустым
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AomPropertyTypeNullPropertyTypeExceptionMessage 
		{
			get 
			{
				return ResourceManager.GetString("AomPropertyTypeNullPropertyTypeExceptionMessage", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to IP порт устройства имеет неверный формат
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string IpPortValidationFormat 
		{
			get 
			{
				return ResourceManager.GetString("IpPortValidationFormat", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to История
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string HistoryHeader 
		{
			get 
			{
				return ResourceManager.GetString("HistoryHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Внимание! Данная программа защищается законом об авторских правах. Незаконное копирование или распространение этой программы или какой-либо её части может повлечь суровые гражданские или уголовные наказания и будет преследоваться по всей строгости закона.
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string AboutCopyright 
		{
			get 
			{
				return ResourceManager.GetString("AboutCopyright", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Общие команды
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string CitywideCommandHeader 
		{
			get 
			{
				return ResourceManager.GetString("CitywideCommandHeader", null);
			}
		}
		/// <summary>
        ///   Looks up a localized string similar to Имя устройства
        /// </summary>
       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
	    public string DeviceName 
		{
			get 
			{
				return ResourceManager.GetString("DeviceName", null);
			}
		}

       
    }
}
