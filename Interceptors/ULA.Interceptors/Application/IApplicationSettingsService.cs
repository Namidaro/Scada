namespace ULA.Interceptors.Application
{
    /// <summary>
    ///     Exposes application settings service
    ///     Описывает сервис настроек приложения
    /// </summary>
    public interface IApplicationSettingsService
    {
        /// <summary>
        ///     Gets a full path of the devices storage file
        ///     Возвращает путь к хранилищу устройств
        /// </summary>
        string DevicesStoragePath { get; }

        /// <summary>
        ///     Описывает номер экземпляра программы
        /// </summary>
        int ApplicationNumber { get; set; }

        /// <summary>
        ///     Возвращает путь к файлу с данными о найстройках
        /// </summary>
        string SettingsPath { get; }
        /// <summary>
        /// 
        /// </summary>
        string JournalFilePath { get; }
        /// <summary>
        /// автоквитировнаие
        /// </summary>
        bool AcknowledgeEnabled { get; set; }
        /// <summary>
        /// период обновления на схеме
        /// </summary>
        int FullTimeoutPeriod { get; set; }
        /// <summary>
        /// период обновления в списке
        /// </summary>
        int PartialTimeoutPeriod { get; set; }
        /// <summary>
        /// таймаут обменов
        /// </summary>
        int QueryTimeoutPeriod { get; set; }

        /// <summary>
        /// повторения комманд освещения
        /// </summary>
         int NumberOfLightingCommandRepeat { get; set; }

        /// <summary>
        /// интервал повторения комманд освещения
        /// </summary>
         int MillisecondRepeatInterval { get; set; }


        /// <summary>
        /// загрузить настройки
        /// </summary>
        void Load();
        /// <summary>
        /// сохранить настройки
        /// </summary>
        void Save();
    }
}