using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.ServiceLocation;
using NLog;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Driver.DataTables;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Localization;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.ViewModels.UserControl;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Представляет вью-модель графика освещения
    /// </summary>
    public class LightingSheduleViewModel : ViewModelBase, ILightingSheduleViewModel, INavigationAware
    {
        #region [Const]

        private const string SEPTEMBER_NAME = "Сентябрь";
        private const string OCTOBER_NAME = "Октябрь";
        private const string NOVEMBER_NAME = "Ноябрь";
        private const string DECEMBER_NAME = "Декабрь";
        private const string JANUARY_NAME = "Январь";
        private const string FEBRUARY_NAME = "Февраль";
        private const string MARCH_NAME = "Март";
        private const string APRIL_NAME = "Апрель";
        private const string MAY_NAME = "Май";
        private const string JUNE_NAME = "Июнь";
        private const string JULY_NAME = "Июль";
        private const string AUGUST_NAME = "Август";
        private const ushort MAIN_PACKAGE_LENGHT_WORD = 0x64; // длина основных пакетов
        private const ushort LAST_PACKAGE_LENGHT = 0x46; // длина последнего пакета
        private const int COUNT_PACKAGE = 8; //количество пакетов

        private const int MONTH_LENGHT_INDEX = 32 * 4;
        //31 день + 4 индекса на (Экономия время по) . точнее Уточнять у конструктора

        private const int DAY_LENGHT_INDEX = 4; // Количесиво байт которые занимает один день в блоке данных
        private const int ECONOMY_ADDRESS_INDEX = MONTH_LENGHT_INDEX * 12 - 4; //Адрес начала данных режима экономии

        private const int MONTH_COUNT = 12;

        #endregion

        #region [Private Fields]

        private Dictionary<string, byte[]> _sheduleCache;

        private IInteractionService _interactionService;

        private string _deviceName;
        private IRuntimeDeviceViewModel _currentDeviceViewModel;
        private ICommand _sendLightingShedule;
        private ICommand _backToSchemeCommand;
        private ICommand _getLightingSheduleCommand;
        private ICommand _navigateToLightingShedule;
        private ICommand _navigateToBackLightShedule;
        private ICommand _navigateToStoregeEnergyShedule;
        private ICommand _getSheduleFromFileCommand;
        private ICommand _storeToFileCommand;

        private string _currentMonthName;
        private int _currentMonthIndex;
        private ObservableCollection<IDaySheduleViewModel> _currentMothDaysCollection;

        private readonly ObservableCollection<string> _mothNames = new ObservableCollection<string>
        {
            JANUARY_NAME,
            FEBRUARY_NAME,
            MARCH_NAME,
            APRIL_NAME,
            MAY_NAME,
            JUNE_NAME,
            JULY_NAME,
            AUGUST_NAME,
            SEPTEMBER_NAME,
            OCTOBER_NAME,
            NOVEMBER_NAME,
            DECEMBER_NAME
        };

        private readonly ObservableCollection<int> _rangeMoth = new ObservableCollection<int>
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12
        };

        private ObservableCollection<int> _rangeDaysInEconomyStopMonth;
        private ObservableCollection<int> _rangeDaysInEconomyStartMonth;
        private Dictionary<string, int> _monthsLenghtDictionary;
        private Dictionary<string, ObservableCollection<IDaySheduleViewModel>> _monthsCollection;

        private bool _isEconomyMode;
        private int _startEconomyMonth;
        private int _startEconomyDay;
        private int _startEconomyHour;
        private int _startEconomyMinute;
        private int _stopEconomyMonth;
        private int _stopEconomyDay;
        private int _stopEconomyHour;
        private int _stopEconomyMinute;

        private Dictionary<string, object> _navigationContext = new Dictionary<string, object>();
        private ushort _startAddress; // адрес начала блока данных на устройстве

        private Logger _logger;

        private string _title = String.Empty;

        #endregion

        #region [Ctor's]

        /// <summary>
        ///     Конструктор
        /// </summary>
        public LightingSheduleViewModel(IInteractionService interactionService)
        {
            Guard.AgainstNullReference(interactionService, "interactionService");
            this._interactionService = interactionService;
            _sheduleCache = new Dictionary<string, byte[]>();

            this._monthsLenghtDictionary = new Dictionary<string, int>();
            for (int i = 0; i < MONTH_COUNT; i++)
            {
                //2012 год, т.к. он высокосный а на вьюшке у февраля д.б. 29 дней
                this._monthsLenghtDictionary.Add(this._mothNames[i], DateTime.DaysInMonth(2012, i + 1));
            }

            this._monthsCollection = new Dictionary<string, ObservableCollection<IDaySheduleViewModel>>();
        }

        #endregion

        #region [Properties]

        /// <summary>
        ///     Заголовок вьюхи
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set
            {
                if (this._title != null && this._title.Equals(value)) return;
                this._title = value;
                OnPropertyChanged("Title");
            }
        }

        /// <summary>
        ///     Свойство для представления текущего месяца(выбранного на вьюшке)
        /// </summary>
        public string CurrentMonthName
        {
            get { return this._currentMonthName; }
            set
            {
                if (this._currentMonthName != null && this._currentMonthName.Equals(value)) return;
                this._currentMonthName = value;
                OnPropertyChanged("CurrentMonth");
                this.CurrentMonthDayCollection = this._monthsCollection[value];
                this.CurrentMothIndex = this._mothNames.IndexOf(value);
            }
        }

        /// <summary>
        ///     Индекс текущего месяца(выбранного в данный момент)
        /// </summary>
        public int CurrentMothIndex
        {
            get { return this._currentMonthIndex; }
            set
            {
                if (this._currentMonthIndex.Equals(value)) return;
                this._currentMonthIndex = value;
                OnPropertyChanged("CurrentMothIndex");
            }
        }

        /// <summary>
        ///     Коллекция с днями текцщего месяца
        /// </summary>
        public ObservableCollection<IDaySheduleViewModel> CurrentMonthDayCollection
        {
            get { return this._currentMothDaysCollection; }
            set
            {
                if (this._currentMothDaysCollection != null && this._currentMothDaysCollection.Equals(value)) return;
                this._currentMothDaysCollection = value;
                OnPropertyChanged("CurrentMonthDayCollection");
            }
        }

        /// <summary>
        ///     Коллекция с названиями месяцев
        /// </summary>
        public ObservableCollection<string> MonthCollection
        {
            get { return this._mothNames; }
        }

        /// <summary>
        ///     Свойство показывающее установлен ли режим экономии
        /// </summary>
        public bool IsEconomyMode
        {
            get { return this._isEconomyMode; }
            set
            {
                if (this._isEconomyMode.Equals(value)) return;
                this._isEconomyMode = value;
                OnPropertyChanged("IsEconomyMode");
            }
        }

        /// <summary>
        ///     Время(час) начала режима экономии
        /// </summary>
        public int StartEconomyHour
        {
            get { return this._startEconomyHour; }
            set
            {
                if (this._startEconomyHour.Equals(value)) return;
                this._startEconomyHour = value;
                OnPropertyChanged("StartEconomyHour");
            }
        }

        /// <summary>
        ///     Время(час) конца режима экономии
        /// </summary>
        public int StopEconomyHour
        {
            get { return this._stopEconomyHour; }
            set
            {
                if (this._stopEconomyHour.Equals(value)) return;
                this._stopEconomyHour = value;
                OnPropertyChanged("StopEconomyHour");
            }
        }

        /// <summary>
        ///     Дата(месяц) начала режима экономии
        /// </summary>
        public int StartEconomyMonth
        {
            get { return this._startEconomyMonth; }
            set
            {
                if (this._startEconomyMonth.Equals(value) || value > 12) return;
                this._startEconomyMonth = value;
                var currentStartDay = this.StartEconomyDay;
                this.RangeDaysEconomyStartMonth.Clear();
                for (int i = 0; i < this._monthsLenghtDictionary[this._mothNames[value - 1]]; i++)
                {
                    this.RangeDaysEconomyStartMonth.Add(i + 1);
                }
                if (currentStartDay > this._monthsLenghtDictionary[this._mothNames[value - 1]])
                {
                    this.StartEconomyDay = 1;
                }
                else
                {
                    this.StartEconomyDay = currentStartDay;
                }
                OnPropertyChanged("StartEconomyMonth");
            }
        }

        /// <summary>
        ///     Дата(месяц) конца режима экономии
        /// </summary>
        public int StopEconomyMonth
        {
            get { return this._stopEconomyMonth; }
            set
            {
                if (this._stopEconomyMonth.Equals(value) || value > 12) return;
                this._stopEconomyMonth = value;
                this.RangeDaysEconomyStopMonth.Clear();
                for (int i = 0; i < this._monthsLenghtDictionary[this._mothNames[value - 1]]; i++)
                {
                    this.RangeDaysEconomyStopMonth.Add(i + 1);
                }
                OnPropertyChanged("StopEconomyMonth");
            }
        }

        /// <summary>
        ///     тут +-1 из-за того что вяжется индекс, который с нуля, а числа с единицы
        ///     Дата(день) начала режима экономии
        /// </summary>
        public int StartEconomyDay
        {
            get { return this._startEconomyDay - 1; }
            set
            {
                if (this._startEconomyDay.Equals(value + 1)) return;
                this._startEconomyDay = value + 1;
                OnPropertyChanged("StartEconomyDay");
            }
        }

        /// <summary>
        /// тут +-1 из-за того что вяжется индекс, который с нуля, а числа с единицы
        /// Дата(день) конца режима экономии
        /// </summary>
        public int StopEconomyDay
        {
            get { return this._stopEconomyDay - 1; }
            set
            {
                if (this._stopEconomyDay.Equals(value + 1)) return;
                this._stopEconomyDay = value + 1;
                OnPropertyChanged("StopEconomyDay");
            }
        }

        /// <summary>
        ///     Время(минута) начала режима экономии
        /// </summary>
        public int StartEconomyMinute
        {
            get { return this._startEconomyMinute; }
            set
            {
                if (this._startEconomyMinute.Equals(value)) return;
                this._startEconomyMinute = value;
                OnPropertyChanged("StartEconomyMinute");
            }
        }

        /// <summary>
        ///     Время(минута) конца режима экономии
        /// </summary>
        public int StopEconomyMinute
        {
            get { return this._stopEconomyMinute; }
            set
            {
                if (this._stopEconomyMinute.Equals(value)) return;
                this._stopEconomyMinute = value;
                OnPropertyChanged("StopEconomyMinute");
            }
        }

        /// <summary>
        ///     Возвращает набор месяцев в численном виде [1-12]
        /// </summary>
        public ObservableCollection<int> RangeMonthInts
        {
            get { return this._rangeMoth; }
        }

        /// <summary>
        ///     ВОзвращает набор чисел месяца окончания режима экономии
        /// </summary>
        public ObservableCollection<int> RangeDaysEconomyStopMonth
        {
            get { return this._rangeDaysInEconomyStopMonth; }
            set
            {
                if (this._rangeDaysInEconomyStopMonth != null && this._rangeDaysInEconomyStopMonth.Equals(value))
                    return;
                this._rangeDaysInEconomyStopMonth = value;
                OnPropertyChanged("RangeDaysEconomyStopMonth");
            }
        }

        /// <summary>
        ///     ВОзвращает набор чисел месяца начала режима экономии
        /// </summary>
        public ObservableCollection<int> RangeDaysEconomyStartMonth
        {
            get { return this._rangeDaysInEconomyStartMonth; }
            set
            {
                if (this._rangeDaysInEconomyStartMonth != null && this._rangeDaysInEconomyStartMonth.Equals(value))
                    return;
                this._rangeDaysInEconomyStartMonth = value;
                OnPropertyChanged("RangeDaysEconomyStartMonth");
            }
        }

        #endregion

        #region [IlightingSheduleViewModel]

        /// <summary>
        ///     Свойство представляющие имя устройства, для которого производится конфигурация
        /// </summary>
        public string DeviceName
        {
            get { return this._deviceName; }
            set
            {
                if (this._deviceName != null && this._deviceName.Equals(value)) return;
                this._deviceName = value;
                OnPropertyChanged("DeviceName");
            }
        }

        /// <summary>
        ///     Команда рагрузки графика освещения из файла
        /// </summary>
        public ICommand GetSheduleFromFileCommand
        {
            get
            {
                return this._getSheduleFromFileCommand ??
                       (this._getSheduleFromFileCommand = new DelegateCommand(OnGetScheduleFromFileCommand));
            }
        }

        /// <summary>
        ///     Команда записи текущего графика освещения в файл
        /// </summary>
        public ICommand SaveToFileCommand
        {
            get
            {
                return this._storeToFileCommand ??
                       (this._storeToFileCommand = new DelegateCommand(OnSaveToFileCommand));
            }
        }

        /// <summary>
        ///      Представляет команду отправки конфигурационных данных на устройство
        /// </summary>
        public ICommand SendLightingShedule
        {
            get
            {
                return this._sendLightingShedule ??
                       (this._sendLightingShedule = new DelegateCommand(OnSendLightingShedule));
            }
        }

        /// <summary>
        ///     Команда навигации назад на схему устройства
        /// </summary>
        public ICommand BackToSchemeCommand
        {
            get
            {
                return this._backToSchemeCommand ?? (this._backToSchemeCommand = new DelegateCommand(OnBackToScheme));
            }
        }

        /// <summary>
        ///     Представляет команду считывания графика освещения с устройства
        /// </summary>
        public ICommand GetLightingShedule
        {
            get
            {
                return this._getLightingSheduleCommand ??
                       (this._getLightingSheduleCommand = new DelegateCommand<byte[]>(this.InitializeOnNavigateTo));
            }
        }

        #endregion

        #region [INavigationAware]

        /// <summary>
        ///     
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL, _currentDeviceViewModel);
            _navigationContext.ForEach((pair =>
            {
                navigationContext.Parameters.Add(pair.Key,pair.Value);
            } ));
            this._deviceName = null;
            this._currentDeviceViewModel = null;
            this._sendLightingShedule = null;

            this._startEconomyDay = -1;
            this._stopEconomyDay = -1;
            this._startEconomyMonth = -1;
            this._stopEconomyMonth = -1;
            this._startEconomyMinute = -1;
            this._stopEconomyMinute = -1;
            this._startEconomyHour = -1;
            this._stopEconomyHour = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters != null)
            {
                
                    this._currentDeviceViewModel = navigationContext.Parameters[ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL] as
                        IRuntimeDeviceViewModel;
                



                    this.Title = navigationContext.Parameters["title"].ToString();


                    this._startAddress = 0;


                    var dataTable = _currentDeviceViewModel.Model.DriverMomento.State.DataContainer.GetValue<IDriverDataTable>("DataTable");
                    if (Title.Contains("1"))
                    {
                        _startAddress = (ushort) dataTable
                            .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.SHEDULE1_DATA_ID_NAME)
                            .Properties["Address"].Value;
                    }
                    if (Title.Contains("2"))
                    {
                        _startAddress = (ushort)dataTable
                            .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.SHEDULE2_DATA_ID_NAME)
                            .Properties["Address"].Value;
                    }
                    if (Title.Contains("3"))
                    {
                        _startAddress = (ushort)dataTable
                            .GetRowByName(DeviceStringKeys.DeviceTableTagKeys.SHEDULE3_DATA_ID_NAME)
                            .Properties["Address"].Value;
                    }




                    this.DeviceName = this._currentDeviceViewModel.DeviceName;
                    this._logger = LogManager.GetLogger(this.DeviceName + " " + this.Title);
                    this.RangeDaysEconomyStartMonth = new ObservableCollection<int>();
                    this.RangeDaysEconomyStopMonth = new ObservableCollection<int>();

                    this._monthsCollection.Clear();
                    foreach (var mothName in this._mothNames)
                    {
                        this._monthsCollection.Add(mothName, new ObservableCollection<IDaySheduleViewModel>());
                    }
                    var monthLengthList = this._monthsLenghtDictionary.Values.ToArray();
                    for (int i = 0; i < MONTH_COUNT; i++)
                    {
                        for (int j = 0; j < monthLengthList[i]; j++)
                        {
                            this._monthsCollection[this._mothNames[i]].Add(new DaySheduleViewModel
                            {
                                Month = this._mothNames[i],
                                DayNumber = j + 1
                            });
                        }
                    }


                    this.CurrentMonthName = this._mothNames[DateTime.Now.Month - 1];
                    this.StartEconomyDay = 0;
                    this.StopEconomyDay = 0;
                    this.StartEconomyMonth = 1;
                    this.StopEconomyMonth = 1;
                    this.StartEconomyMinute = 0;
                    this.StopEconomyMinute = 0;
                    this.StartEconomyHour = 0;
                    this.StopEconomyHour = 0;

                    this._logger = LogManager.GetLogger(this.DeviceName + " " + this.Title);
                    this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) + " Переход в режим настройки " +
                                       this._title);

                    for (int i = 0; i < this.MonthCollection.Count; i++)
                    {
                        string monthName = this._mothNames[i];
                        for (int j = 0; j < this._monthsLenghtDictionary[monthName]; j++)
                        {
                            this._monthsCollection[monthName][j].StartHour = 0;
                            this._monthsCollection[monthName][j].StartMinute = 0;
                            this._monthsCollection[monthName][j].StopHour = 0;
                            this._monthsCollection[monthName][j].StopMinute = 0;
                        }
                    }

                    for (int i = 0; i < CurrentMonthDayCollection.Count; i++)
                    {
                        this.CurrentMonthDayCollection[i].StartHour = 0;
                        this.CurrentMonthDayCollection[i].StartMinute = 0;
                        this.CurrentMonthDayCollection[i].StopHour = 0;
                        this.CurrentMonthDayCollection[i].StopMinute = 0;
                    }

                    if (this._sheduleCache.ContainsKey(this.Title))
                    {
                        this.InitializeOnNavigateTo(this._sheduleCache[this.Title]);
                    }
                    else
                    {
                        this.InitializeOnNavigateTo();
                    }
                
            }
            _navigationContext.Clear();
        }

        #endregion

        #region [Help members]

        private void OnGetScheduleFromFileCommand()
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "Schedule files (*.schld)|*.schld",
                Title = "Выберите файл"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var doc = new XmlDocument();
                doc.Load(fileDialog.FileName);
                var tempMonthSheduleCollection = new Dictionary<string, ObservableCollection<IDaySheduleViewModel>>();
                foreach (var mothName in this._mothNames)
                {
                    tempMonthSheduleCollection.Add(mothName, new ObservableCollection<IDaySheduleViewModel>());
                }
                this.IsEconomyMode = Boolean.Parse(doc.DocumentElement["IsSavingTurnOn"].InnerText);
                foreach (XmlElement xmlElement in doc.DocumentElement)
                {
                    if (xmlElement.Name.Equals("Month"))
                    {
                        var days = new ObservableCollection<IDaySheduleViewModel>();
                        var mothName = String.Empty;
                        int mothNumber = -1;
                        foreach (XmlElement element in xmlElement)
                        {
                            if (element.Name.Equals("MonthName"))
                            {
                                mothName = element.Value;
                            }
                            if (element.Name.Equals("Number"))
                            {
                                mothNumber = Int32.Parse(element.Value ?? element.InnerText);
                            }
                            if (element.Name.Equals("Day"))
                            {
                                foreach (XmlElement xmlDay in element)
                                {
                                    if (xmlDay.Name.Equals("GraphicDay"))
                                    {
                                        if (xmlDay["isVisible"].InnerText.
                                            ToLower(CultureInfo.InvariantCulture).Equals("false"))
                                        {
                                            continue;
                                        }
                                        var day = new DaySheduleViewModel();
                                        foreach (XmlElement dayNodes in xmlDay)
                                        {
                                            if (dayNodes.Name.Equals("Number"))
                                            {
                                                var dayNumberString = dayNodes.Value ?? dayNodes.InnerText;
                                                day.DayNumber = Int32.Parse(dayNumberString.Split(' ')[0]);
                                            }
                                            if (dayNodes.Name.Equals("TurnOnTime"))
                                            {
                                                day.StopHour = Int32.Parse(dayNodes["Hour"].InnerText);
                                                day.StopMinute = Int32.Parse(dayNodes["Minute"].InnerText);
                                            }
                                            if (dayNodes.Name.Equals("TurnOffTime"))
                                            {
                                                day.StartHour = Int32.Parse(dayNodes["Hour"].InnerText);
                                                day.StartMinute = Int32.Parse(dayNodes["Minute"].InnerText);
                                            }
                                        }
                                        days.Add(day);
                                    }
                                    if (xmlDay.Name.Equals("MonthSaving"))
                                    {
                                        foreach (XmlElement dayNodes in xmlDay)
                                        {
                                            if (dayNodes.Name.Equals("TurnOnTime"))
                                            {
                                                //day.StartHour = Int32.Parse(dayNodes["Hour"].Value);
                                                //day.StartMinute = Int32.Parse(dayNodes["Minute"].Value);
                                            }
                                            if (dayNodes.Name.Equals("TurnOffTime"))
                                            {
                                                //day.StopHour = Int32.Parse(dayNodes["Hour"].Value);
                                                //day.StopMinute = Int32.Parse(dayNodes["Minute"].Value);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        tempMonthSheduleCollection[this._mothNames[mothNumber - 1]] = days;
                    }
                    if (this.IsEconomyMode && xmlElement.Name.Equals("YearSaving"))
                    {
                        this.StartEconomyMonth = Int32.Parse(xmlElement["TurnOnMonth"].InnerText);
                        this.StartEconomyDay = Int32.Parse(xmlElement["TurnOnDay"].InnerText) - 1;
                        this.StopEconomyMonth = Int32.Parse(xmlElement["TurnOffMonth"].InnerText);
                        this.StopEconomyDay = Int32.Parse(xmlElement["TurnOffDay"].InnerText) - 1;
                    }
                    if (this.IsEconomyMode && xmlElement.Name.Equals("MonthSaving"))
                    {
                        this.StartEconomyHour = Int32.Parse(xmlElement["TurnOnTime"]["Hour"].InnerText);
                        this.StartEconomyMinute = Int32.Parse(xmlElement["TurnOnTime"]["Minute"].InnerText);
                        this.StopEconomyHour = Int32.Parse(xmlElement["TurnOffTime"]["Hour"].InnerText);
                        this.StopEconomyMinute = Int32.Parse(xmlElement["TurnOffTime"]["Minute"].InnerText);
                        //                <TurnOnTime>
                        //  <Hour>0</Hour>
                        //  <Minute>0</Minute>
                        //</TurnOnTime>
                        //<TurnOffTime>
                        //  <Hour>0</Hour>
                        //  <Minute>0</Minute>
                        //</TurnOffTime>
                    }
                    //if (xmlElement.Name.Equals("IsSavingTurnOn"))
                    //{
                    //    // false
                    //}
                }
                this._monthsCollection = tempMonthSheduleCollection;
                this.CurrentMonthName = this._mothNames[DateTime.Now.Month - 1];
                var curMonthData = _monthsCollection[_currentMonthName];
                for (int i = 0; i < CurrentMonthDayCollection.Count; i++)
                {
                    this.CurrentMonthDayCollection[i].StartHour = curMonthData[i].StartHour;
                    this.CurrentMonthDayCollection[i].StartMinute = curMonthData[i].StartMinute;
                    this.CurrentMonthDayCollection[i].StopHour = curMonthData[i].StopHour;
                    this.CurrentMonthDayCollection[i].StopMinute = curMonthData[i].StopMinute;
                }
                if (!IsEconomyMode)
                {
                    this.StartEconomyDay = 0;
                    this.StopEconomyDay = 0;
                    this.StartEconomyMonth = 1;
                    this.StopEconomyMonth = 1;
                    this.StartEconomyMinute = 0;
                    this.StopEconomyMinute = 0;
                    this.StartEconomyHour = 0;
                    this.StopEconomyHour = 0;
                }
            }
            catch
            {
                this.InteractWithError(new Exception("Файл не может быть прочитан"));
            }
        }

        private const string DECLARATION_VERSION = "1.0";
        private const string DECLARATION_ENCODING = "utf-8";
        private void OnSaveToFileCommand()
        {
            var fileDialog = new SaveFileDialog()
            {
                Filter = "Schedule files (*.schld)|*.schld",
                Title = "Сохраните файл"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            var xMonthSaving = new XElement("MonthSaving");
            var xTurnOnTimeSav = new XElement("TurnOnTime");
            var xOnHourSav = new XElement("Hour");
            var xOnMinuteSav = new XElement("Minute");
            if (this.IsEconomyMode)
            {
                xOnHourSav.SetValue(this.StartEconomyHour);
                xOnMinuteSav.SetValue(this.StartEconomyMinute);
            }
            else
            {
                xOnHourSav.SetValue(0);
                xOnMinuteSav.SetValue(0);
            }

            xTurnOnTimeSav.Add(xOnHourSav);
            xTurnOnTimeSav.Add(xOnMinuteSav);
            xMonthSaving.Add(xTurnOnTimeSav);

            var xTurnOffTimeSav = new XElement("TurnOffTime");
            var xOffHourSav = new XElement("Hour");
            var xOffMinuteSav = new XElement("Minute");
            if (this.IsEconomyMode)
            {
                xOffHourSav.SetValue(this.StopEconomyHour);
                xOffMinuteSav.SetValue(this.StopEconomyMinute);
            }
            else
            {
                xOffHourSav.SetValue(0);
                xOffMinuteSav.SetValue(0);
            }

            xTurnOffTimeSav.Add(xOffHourSav);
            xTurnOffTimeSav.Add(xOffMinuteSav);
            xMonthSaving.Add(xTurnOffTimeSav);

            try
            {
                var xDoc = new XDocument(new XDeclaration(DECLARATION_VERSION, DECLARATION_ENCODING, ""));
                var root = new XElement("Graphic");
                for (int month = 1; month <= 12; month++)
                {
                    var xMonth = new XElement("Month");
                    var xMonthName = new XElement("MonthName");
                    xMonthName.SetValue(this._mothNames[month - 1]);
                    xMonth.Add(xMonthName);
                    var xMonthNumber = new XElement("Number");
                    xMonthNumber.SetValue(month);
                    xMonth.Add(xMonthNumber);
                    var xDays = new XElement("Day");
                    for (int day = 1; day <= 31; day++)
                    {
                        var xDay = new XElement("GraphicDay");
                        var xVisible = new XElement("isVisible");
                        var vis = false;
                        if (this._monthsLenghtDictionary[this._mothNames[month - 1]] >= day)
                        {
                            vis = true;
                        }
                        else
                        {
                            vis = false;
                        }
                        xVisible.SetValue(vis);
                        xDay.Add(xVisible);

                        var xDayNumber = new XElement("Number");
                        xDayNumber.SetValue(day.ToString(CultureInfo.InvariantCulture) + " Число");
                        xDay.Add(xDayNumber);

                        var xTurnOnTime = new XElement("TurnOnTime");
                        var xOnHour = new XElement("Hour");
                        var xOnMinute = new XElement("Minute");
                        if (vis)
                        {
                            xOnHour.SetValue(this._monthsCollection[this._mothNames[month - 1]][day - 1].StartHour);
                            xOnMinute.SetValue(this._monthsCollection[this._mothNames[month - 1]][day - 1].StartMinute);
                        }
                        else
                        {
                            xOnHour.SetValue(0);
                            xOnMinute.SetValue(0);
                        }

                        var xTurnOffTime = new XElement("TurnOffTime");
                        xTurnOffTime.Add(xOnHour);
                        xTurnOffTime.Add(xOnMinute);
                        xDay.Add(xTurnOnTime);


                        var xOffHour = new XElement("Hour");
                        var xOffMinute = new XElement("Minute");
                        if (vis)
                        {
                            xOffHour.SetValue(this._monthsCollection[this._mothNames[month - 1]][day - 1].StopHour);
                            xOffMinute.SetValue(this._monthsCollection[this._mothNames[month - 1]][day - 1].StopMinute);
                        }
                        else
                        {
                            xOffHour.SetValue(0);
                            xOffMinute.SetValue(0);
                        }

                        xTurnOnTime.Add(xOffHour);
                        xTurnOnTime.Add(xOffMinute);
                        xDay.Add(xTurnOffTime);

                        xDays.Add(xDay);
                    }
                    xMonth.Add(xDays);

                    xMonth.Add(xMonthSaving);
                    root.Add(xMonth);
                }

                var xYearSaving = new XElement("YearSaving");
                if (this.IsEconomyMode)
                {
                    xYearSaving.Add(new object[] { new XElement("TurnOnMonth", this.StartEconomyMonth), new XElement("TurnOnDay", this.StartEconomyDay+1),
                        new XElement("TurnOffMonth", this.StopEconomyMonth), new XElement("TurnOffDay", this.StopEconomyDay+1) });
                }
                else
                {
                    xYearSaving.Add(new object[] { new XElement("TurnOnMonth", 1), new XElement("TurnOnDay", 1),
                        new XElement("TurnOffMonth", 1), new XElement("TurnOffDay", 1) });
                }
                root.Add(xYearSaving);

                root.Add(xMonthSaving);
                root.Add(new XElement("IsSavingTurnOn", this.IsEconomyMode));
                xDoc.Add(root);
                xDoc.Save(fileDialog.FileName);
            }
            catch (Exception err)
            {
                this.InteractWithError(err);
            }
        }

        private void OnBackToScheme()
        {
            this._sheduleCache.Clear();
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.SCHEME_RUNTIME_VIEW_NAME, UriKind.Relative);
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        private async void OnSendLightingShedule()
        {
            if (CheckRepair())
            {
                this._interactionService.Interact(
                    ApplicationInteractionProviders.InformationMessageBoxInteractionProvider,
                    viewModel =>
                    {
                        viewModel.Title = LocalizationResources.Instance.RepairDefandTitle;
                        viewModel.Message = Localization.LocalizationResources.Instance.RepairDefand;
                    });
                return;
            }
            var busyToken = this.InteractWithBusy();
            try
            {
                int countMainWritePachage = 48;
                ushort lenghtMainPackage = 0x10;
                int addressLastPackage = _startAddress + countMainWritePachage * lenghtMainPackage;
                //ushort lenghtLastPackage = 0x32;
                var tasks = new Task[countMainWritePachage + 1];
                byte[] initializingData = this.GetDeviceDataFromView();
                for (int i = 0; i < countMainWritePachage; i++)
                {
                    int startIndexPackage = i * lenghtMainPackage * 2; //*2 - т.к. здесь байты, а там слова
                    await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.PostDataByAddressAsync((ushort)(_startAddress + lenghtMainPackage * i),

                        this.GetPackageFromAllDAta(startIndexPackage, lenghtMainPackage * 2, initializingData), "Send Lighting Shedule. Package " + i);

                }
                //последний пакет короче, поэтому он отдельно
                int startIndexLastPackage = (countMainWritePachage) * lenghtMainPackage * 2;
                await Task.Delay(500);
                //await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.PostDataByAddressAsync((ushort)(addressLastPackage),

                //this.GetPackageFromAllDAta(startIndexLastPackage, lenghtLastPackage * 2, initializingData), "SendLightingShedule. Last package" + countMainWritePachage);


                this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) + " Изменение графика освещения");
            }
            catch (Exception error)
            {
                this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) +
                                   " Не удалось изменить график освещения. Ошибка: " + error.Message);
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private byte[] GetPackageFromAllDAta(int start, int lenght, byte[] allData)
        {
            var result = new byte[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = allData[start + i];
            }
            return result;
        }

        private byte[] GetDeviceDataFromView()
        {
            byte[] result = new byte[1540];

            for (int i = 0; i < this.MonthCollection.Count; i++)
            {
                string monthName = this._mothNames[i];
                int monthStartIndex = MONTH_LENGHT_INDEX * i;
                for (int j = 0; j < this._monthsLenghtDictionary[monthName]; j++)
                {
                    int dayStartIndex = monthStartIndex + j * DAY_LENGHT_INDEX;
                    result[dayStartIndex] = (byte)this._monthsCollection[monthName][j].StartHour;
                    result[dayStartIndex + 1] = (byte)this._monthsCollection[monthName][j].StartMinute;
                    result[dayStartIndex + 2] = (byte)this._monthsCollection[monthName][j].StopHour;
                    result[dayStartIndex + 3] = (byte)this._monthsCollection[monthName][j].StopMinute;
                }

                if (this.IsEconomyMode)
                {
                    int economyTimeAddressForMonth = monthStartIndex + 31 * DAY_LENGHT_INDEX;
                    result[economyTimeAddressForMonth + 3] = (byte)this.StartEconomyMinute;
                    result[economyTimeAddressForMonth + 1] = (byte)this.StopEconomyMinute;
                    result[economyTimeAddressForMonth + 2] = (byte)this.StartEconomyHour;
                    result[economyTimeAddressForMonth] = (byte)this.StopEconomyHour;
                }
            }
            if (this.IsEconomyMode)
            {
                result[ECONOMY_ADDRESS_INDEX + 4] = (byte)this.StartEconomyMonth;
                result[ECONOMY_ADDRESS_INDEX + 6] = (byte)this.StopEconomyMonth;
                result[ECONOMY_ADDRESS_INDEX + 5] = (byte)this._startEconomyDay;
                result[ECONOMY_ADDRESS_INDEX + 7] = (byte)this._stopEconomyDay;
                result[ECONOMY_ADDRESS_INDEX + 3] = (byte)this.StartEconomyMinute;
                result[ECONOMY_ADDRESS_INDEX + 1] = (byte)this.StopEconomyMinute;
                result[ECONOMY_ADDRESS_INDEX + 2] = (byte)this.StartEconomyHour;
                result[ECONOMY_ADDRESS_INDEX] = (byte)this.StopEconomyHour;
            }
            else
            {
                result[ECONOMY_ADDRESS_INDEX + 4] = 0;
                result[ECONOMY_ADDRESS_INDEX + 6] = 0;
                result[ECONOMY_ADDRESS_INDEX + 5] = 0;
                result[ECONOMY_ADDRESS_INDEX + 7] = 0;
                result[ECONOMY_ADDRESS_INDEX + 3] = 0;
                result[ECONOMY_ADDRESS_INDEX + 1] = 0;
                result[ECONOMY_ADDRESS_INDEX + 2] = 0;
                result[ECONOMY_ADDRESS_INDEX] = 0;
            }

            return result;
        }

        private async Task<byte[]> GetLightingSheduleDataFromDeviceAsync()
        {
            List<byte[]> dataToRead = new List<byte[]>(COUNT_PACKAGE);
            //var readingDataTask = new Task<object>[8];


            for (int i = 0; i < COUNT_PACKAGE - 1; i++)
            {
                dataToRead.Add(
                    await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection
                        .GetDataByAddressAsync((ushort)(_startAddress + MAIN_PACKAGE_LENGHT_WORD * i),
                            MAIN_PACKAGE_LENGHT_WORD, "Get Lighting Shedule Data. Package " + i));
            }

            dataToRead.Add(
                await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection
                    .GetDataByAddressAsync((ushort)(_startAddress + 700), LAST_PACKAGE_LENGHT, "Get Lighting Shedule Data. Last package " + COUNT_PACKAGE));

            var result = new List<byte>();

            foreach (byte[] data in dataToRead)
            {
                if (data == null)
                {
                    throw new Exception("2.Чтение данных из устройства завершилось с ошибкой.");
                }
                result.AddRange(data);
            }

            //}
            var arrayResult = result.ToArray();
            if (this._sheduleCache.ContainsKey(this.Title))
            {
                this._sheduleCache[this.Title] = arrayResult;
            }
            else
            {
                this._sheduleCache.Add(this.Title, arrayResult);
            }
            return arrayResult;
          
        }

        private async void InitializeOnNavigateTo(byte[] data = null)
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                byte[] initializingData = null;
                if (data == null)
                {
                    initializingData = await this.GetLightingSheduleDataFromDeviceAsync();
                }
                else
                {
                    initializingData = data;
                }
                this.InitializeDaysFromDeviceData(initializingData);
                var curMonthData = _monthsCollection[_currentMonthName];
                for (int i = 0; i < CurrentMonthDayCollection.Count; i++)
                {
                    this.CurrentMonthDayCollection[i].StartHour = curMonthData[i].StartHour;
                    this.CurrentMonthDayCollection[i].StartMinute = curMonthData[i].StartMinute;
                    this.CurrentMonthDayCollection[i].StopHour = curMonthData[i].StopHour;
                    this.CurrentMonthDayCollection[i].StopMinute = curMonthData[i].StopMinute;
                }
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private void InitializeDaysFromDeviceData(byte[] deviceData)
        {
            if (deviceData.Length != 1540)
            {
                throw new Exception("3.Чтение данных из устройства завершилось с ошибкой.");
            }

            for (int i = 0; i < this.MonthCollection.Count; i++)
            {
                string monthName = this._mothNames[i];
                int monthStartIndex = MONTH_LENGHT_INDEX * i;
                for (int j = 0; j < this._monthsLenghtDictionary[monthName]; j++)
                {
                    int dayStartIndex = monthStartIndex + j * DAY_LENGHT_INDEX;
                    this._monthsCollection[monthName][j].StartHour = deviceData[dayStartIndex];
                    this._monthsCollection[monthName][j].StartMinute = deviceData[dayStartIndex + 1];
                    this._monthsCollection[monthName][j].StopHour = deviceData[dayStartIndex + 2];
                    this._monthsCollection[monthName][j].StopMinute = deviceData[dayStartIndex + 3];
                }
            }
            bool isSetEconomy = false;
            for (int i = 0; i < 8; i++)
            {
                if (deviceData[ECONOMY_ADDRESS_INDEX + i] != 0)
                {
                    isSetEconomy = true;
                    break;
                }
            }
            this.IsEconomyMode = isSetEconomy;
            if (isSetEconomy)
            {
                this.StartEconomyMonth = deviceData[ECONOMY_ADDRESS_INDEX + 4];
                this.StopEconomyMonth = deviceData[ECONOMY_ADDRESS_INDEX + 6];
                this.StartEconomyDay = deviceData[ECONOMY_ADDRESS_INDEX + 5] - 1;
                this.StopEconomyDay = deviceData[ECONOMY_ADDRESS_INDEX + 7] - 1;
                this.StartEconomyMinute = deviceData[ECONOMY_ADDRESS_INDEX + 3];
                this.StopEconomyMinute = deviceData[ECONOMY_ADDRESS_INDEX + 1];
                this.StartEconomyHour = deviceData[ECONOMY_ADDRESS_INDEX + 2];
                this.StopEconomyHour = deviceData[ECONOMY_ADDRESS_INDEX];
            }
            else
            {
                this.StartEconomyMonth = 1;
                this.StopEconomyMonth = 1;
                this.StartEconomyDay = 0;
                this.StopEconomyDay = 0;
                this.StartEconomyMinute = 0;
                this.StopEconomyMinute = 0;
                this.StartEconomyHour = 0;
                this.StopEconomyHour = 0;
            }
        }

        private IInteractionToken InteractWithBusy()
        {
            return this._interactionService
                .Interact(ApplicationInteractionProviders.BusyInteraction, dialog =>
                {
                    dialog.Title = LocalizationResources.Instance.BusyDialogTitle;
                    dialog.Message = LocalizationResources.Instance.WaitingBusyDialogMessage;
                });
        }

        private void InteractWithError(Exception error)
        {
            this._interactionService
                .Interact(ApplicationInteractionProviders.ErrorMessageBoxInteractionProvider,
                    dialog =>
                    {
                        dialog.ButtonType = MessageBoxButtonType.OK;
                        dialog.Title = LocalizationResources.Instance.ErrorHeader;
                        dialog.Message =
                            string.Format(LocalizationResources.Instance.UnexpectedErrorMessagePattern,
                                error.Message);
                    });
        }

        private bool CheckRepair()
        {
            return (_currentDeviceViewModel.Model as IRuntimeDevice).StartersOnDevice.Any((starter =>
            {
                return starter.IsInRepairState != null && starter.IsInRepairState.Value;
            }));
        }
        #endregion

        #region [Navigate]

        /// <summary>
        ///     Команда навигации на вьюху графика освещения
        /// </summary>
        public ICommand NavigateToLightingShedulerCommand
        {
            get
            {
                return this._navigateToLightingShedule ??
                       (this._navigateToLightingShedule = new DelegateCommand(OnNavigateToLightingShedule));
            }
        }

        /// <summary>
        ///     Команда навигации на вьюху графика подсветки
        /// </summary>
        public ICommand NavigateToBacklightShedulerCommand
        {
            get
            {
                return this._navigateToBackLightShedule ??
                       (this._navigateToBackLightShedule = new DelegateCommand(OnNavigateToBacklightShedule));
            }
        }

        /// <summary>
        ///     Команда навигации на вьюху графика энергосбережения
        /// </summary>
        public ICommand NavigateToStoregEnergyShedulerCommand
        {
            get
            {
                return this._navigateToStoregeEnergyShedule ??
                       (this._navigateToStoregeEnergyShedule = new DelegateCommand(OnNavigateToStoregEnergyShedule));
            }
        }


        private void OnNavigateToLightingShedule()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
            _navigationContext.Add("title", "График 1(освещение)");
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });

        }

        private void OnNavigateToBacklightShedule()
        {

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
            _navigationContext.Add("title", "График 2(подсветка)");

            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        private void OnNavigateToStoregEnergyShedule()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
            _navigationContext.Add("title", "График 3(иллюминация)");

            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        #endregion
    }
}
