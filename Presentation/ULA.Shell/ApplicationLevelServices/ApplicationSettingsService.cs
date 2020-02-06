using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ULA.Interceptors.Application;
using ULA.Shell.Properties;

namespace ULA.Shell.ApplicationLevelServices
{
    /// <summary>
    ///     Represents an application settings facade service functionality
    ///     Представляет фасад для сервиса настроек
    /// </summary>
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        private static int _applicationNumber;
        private bool _isLoaded=false;
        #region [IApplicationSettingsService members]

        /// <summary>
        ///     Gets a full path of the devices storage file
        ///     Получает путь к хранилищу устройств
        /// </summary>
        public string DevicesStoragePath => String.Format(CultureInfo.CurrentCulture, Settings.Default.DeviceStoragePath, this.ApplicationNumber);

        /// <summary>
        ///     Описывает номер экземпляра программы
        /// </summary>
        public int ApplicationNumber
        {
            get { return ApplicationSettingsService._applicationNumber; }
            set { ApplicationSettingsService._applicationNumber = value; }
        }

        /// <summary>
        ///     Возвращает путь к файлу с данными о найстройках
        /// </summary>
        public string SettingsPath => String.Format(CultureInfo.CurrentCulture, Settings.Default.SettingsPath, this.ApplicationNumber);

      
        /// <summary>
        ///     Возвращает путь к файлу журнала
        /// </summary>
        public string JournalFilePath => String.Format(CultureInfo.CurrentCulture, Settings.Default.JournalPath, DateTime.Now.Month.ToString("D2", CultureInfo.CurrentCulture), 
            DateTime.Now.Year.ToString("D4", CultureInfo.CurrentCulture), this.ApplicationNumber);
        /// <summary>
        /// автоквитировнаие
        /// </summary>
        public bool AcknowledgeEnabled { get; set; }
        /// <summary>
        /// период обновления на схеме
        /// </summary>
        public int FullTimeoutPeriod { get; set; }
        /// <summary>
        /// период обновления в списке
        /// </summary>
        public int PartialTimeoutPeriod { get; set; }
        /// <summary>
        /// таймаут обменов
        /// </summary>
        public int QueryTimeoutPeriod { get; set; }
        /// <summary>
        /// повторения комманд освещения
        /// </summary>
        public int NumberOfLightingCommandRepeat { get; set; }

        /// <summary>
        /// интервал повторения комманд освещения
        /// </summary>
        public int MillisecondRepeatInterval { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void Load()
        {
            if(_isLoaded)return;
            ExtraSettingsEntity settings;

            if (!File.Exists(SettingsPath))
            {
                AcknowledgeEnabled = false;
                FullTimeoutPeriod = 5;
                PartialTimeoutPeriod = 20;
                QueryTimeoutPeriod = 10;
                MillisecondRepeatInterval = 60000;
                NumberOfLightingCommandRepeat = 0;
                Save();
                return;
            }

            using (FileStream fs = File.OpenRead(SettingsPath))
            {
                XmlSerializer xs = new XmlSerializer(typeof (ExtraSettingsEntity));
                settings = (ExtraSettingsEntity) xs.Deserialize(fs);
            }
            if (settings == null) return;
            AcknowledgeEnabled = settings.AcknowledgeEnabled;
            FullTimeoutPeriod = settings.FullTimeoutPeriod;
            PartialTimeoutPeriod = settings.PartialTimeoutPeriod;
            QueryTimeoutPeriod = settings.QueryTimeoutPeriod;
            MillisecondRepeatInterval =settings.MillisecondRepeatInterval;
            NumberOfLightingCommandRepeat = settings.NumberOfLightingCommandRepeat;
            if (QueryTimeoutPeriod < 10) QueryTimeoutPeriod = 10;
            if (MillisecondRepeatInterval < 60000) MillisecondRepeatInterval = 60000;
            _isLoaded = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            ExtraSettingsEntity settings=new ExtraSettingsEntity();
             settings.AcknowledgeEnabled=AcknowledgeEnabled;
            settings.FullTimeoutPeriod = FullTimeoutPeriod;
            settings.PartialTimeoutPeriod = PartialTimeoutPeriod;
            settings.QueryTimeoutPeriod = QueryTimeoutPeriod;
            settings.MillisecondRepeatInterval = MillisecondRepeatInterval;
            settings.NumberOfLightingCommandRepeat = NumberOfLightingCommandRepeat;
            using (FileStream fs = File.Create(SettingsPath))
            {
                XmlSerializer xs = new XmlSerializer(typeof(ExtraSettingsEntity));
                xs.Serialize(fs, settings);
            }
          
        }

        #endregion [IApplicationSettingsService members]
    }
}