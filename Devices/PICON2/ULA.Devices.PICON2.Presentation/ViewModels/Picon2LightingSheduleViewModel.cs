using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using NLog;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Localization;
using ULA.Presentation.Infrastructure;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;
using ULA.Presentation.Services.Interactions;
using ULA.Presentation.ViewModels.UserControl;
using YP.Toolkit.System.Validation;

namespace ULA.Devices.PICON2.Presentation.ViewModels
{
    /// <summary>
    ///     Представляет вью-модель графика освещения
    /// </summary>
    public class Picon2LightingSheduleViewModel : ViewModelBase, ILightingSheduleViewModel, INavigationAware
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
        private const ushort LAST_PACKAGE_LENGHT = 0x44; // длина последнего пакета
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


        private Dictionary<string, object> _navigationContext = new Dictionary<string, object>();
        private ushort _startAddress; // адрес начала блока данных на устройстве

        private Logger _logger;

        private string _title = String.Empty;

        #endregion

        #region [Ctor's]

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Picon2LightingSheduleViewModel(IInteractionService interactionService)
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
            _navigationContext.Add("CurrentdDeviceViewModel", _currentDeviceViewModel);
            _navigationContext.ForEach((pair =>
            {
                navigationContext.Parameters.Add(pair.Key, pair.Value);
            }));
            this._deviceName = null;
            this._currentDeviceViewModel = null;
            this._sendLightingShedule = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters != null)
            {
                this._currentDeviceViewModel = navigationContext.Parameters["CurrentdDeviceViewModel"] as
                    IRuntimeDeviceViewModel;
               // this._startAddress = ushort.Parse(navigationContext.Parameters["address"].ToString());
                this.Title = navigationContext.Parameters["title"].ToString();

                if (Title.Contains("1"))
                {
                    _startAddress = 0x3280;
                }
                if (Title.Contains("2"))
                {
                    _startAddress = 0x3580;
                }
                if (Title.Contains("3"))
                {
                    _startAddress = 0x3880;
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
                    this._monthsCollection[this._mothNames[i]].Add(new DaySheduleViewModel
                    {
                        Month = this._mothNames[i],
                        DayNumber = monthLengthList[i] + 1,
                        IsEconomy = true

                    });
                }


                this.CurrentMonthName = this._mothNames[DateTime.Now.Month - 1];

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
                                            if (dayNodes.Name.Equals("IsEconomy"))
                                            {
                                                day.IsEconomy = bool.Parse(dayNodes.InnerText);
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
            //if (this.IsEconomyMode)
            //{
            //    xOnHourSav.SetValue(this.StartEconomyHour);
            //    xOnMinuteSav.SetValue(this.StartEconomyMinute);
            //}
            //else
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
                for (int monthIndex = 1; monthIndex <= 12; monthIndex++)
                {
                    var xMonth = new XElement("Month");
                    var xMonthName = new XElement("MonthName");
                    xMonthName.SetValue(this._mothNames[monthIndex - 1]);
                    xMonth.Add(xMonthName);
                    var xMonthNumber = new XElement("Number");
                    xMonthNumber.SetValue(monthIndex);
                    xMonth.Add(xMonthNumber);
                    var xDays = new XElement("Day");
                    for (int dayIndex = 1; dayIndex <= 32; dayIndex++)
                    {
                        var xDay = new XElement("GraphicDay");
                        var xVisible = new XElement("isVisible");
                        var xIsEconomy = new XElement("IsEconomy");
                        var vis = false;
                        if (this._monthsLenghtDictionary[this._mothNames[monthIndex - 1]] + 1 >= dayIndex)
                        {
                            vis = true;
                        }
                        else
                        {
                            vis = false;
                        }
                        xVisible.SetValue(vis);
                        xDay.Add(xVisible);

                        bool isEconomy = _monthsLenghtDictionary[this._mothNames[monthIndex - 1]] + 1 == dayIndex;
                        xIsEconomy.SetValue(isEconomy);
                        xDay.Add(xIsEconomy);


                        var xDayNumber = new XElement("Number");
                        xDayNumber.SetValue(dayIndex.ToString(CultureInfo.InvariantCulture) + " Число");
                        xDay.Add(xDayNumber);

                        var xTurnOnTime = new XElement("TurnOnTime");
                        var xOnHour = new XElement("Hour");
                        var xOnMinute = new XElement("Minute");
                        if (vis)
                        {
                            xOnHour.SetValue(this._monthsCollection[this._mothNames[monthIndex - 1]][dayIndex - 1].StartHour);
                            xOnMinute.SetValue(this._monthsCollection[this._mothNames[monthIndex - 1]][dayIndex - 1].StartMinute);
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
                            xOffHour.SetValue(this._monthsCollection[this._mothNames[monthIndex - 1]][dayIndex - 1].StopHour);
                            xOffMinute.SetValue(this._monthsCollection[this._mothNames[monthIndex - 1]][dayIndex - 1].StopMinute);
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

                {
                    xYearSaving.Add(new object[] { new XElement("TurnOnMonth", 1), new XElement("TurnOnDay", 1),
                        new XElement("TurnOffMonth", 1), new XElement("TurnOffDay", 1) });
                }
                root.Add(xYearSaving);

                root.Add(xMonthSaving);
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
                int countMainWritePachage = 12;
                ushort lenghtMainPackage = 0x40;
                var tasks = new Task[countMainWritePachage + 1];
                byte[] initializingData = this.GetDeviceDataFromView();
                for (int i = 0; i < countMainWritePachage; i++)
                {
                    int startIndexPackage = i * lenghtMainPackage * 2; //*2 - т.к. здесь байты, а там слова
                    await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.PostDataByAddressAsync((ushort)(_startAddress + lenghtMainPackage * i),
                    
                    this.GetPackageFromAllDAta(startIndexPackage, lenghtMainPackage * 2, initializingData), "Send Lighting Shedule. Package " + i);
                }

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
            byte[] result = new byte[1536];

            for (int i = 0; i < this.MonthCollection.Count; i++)
            {
                string monthName = this._mothNames[i];
                int monthStartIndex = MONTH_LENGHT_INDEX * i;

                IDaySheduleViewModel economyDaySheduleViewModel = this._monthsCollection[monthName].Last();


                result[monthStartIndex + 1] = (byte)economyDaySheduleViewModel.StartHour;
                result[monthStartIndex] = (byte)economyDaySheduleViewModel.StartMinute;
                result[monthStartIndex + 3] = (byte)economyDaySheduleViewModel.StopHour;
                result[monthStartIndex + 2] = (byte)economyDaySheduleViewModel.StopMinute;

                for (int j = 0; j < this._monthsLenghtDictionary[monthName]; j++)
                {
                    int dayStartIndex = monthStartIndex + j * DAY_LENGHT_INDEX;
                    result[dayStartIndex + 5] = (byte)this._monthsCollection[monthName][j].StartHour;
                    result[dayStartIndex + 4] = (byte)this._monthsCollection[monthName][j].StartMinute;
                    result[dayStartIndex + 7] = (byte)this._monthsCollection[monthName][j].StopHour;
                    result[dayStartIndex + 6] = (byte)this._monthsCollection[monthName][j].StopMinute;
                }
            }
            List<byte> resAsHex = ConvertFromIntAsDecToIntAsHex(result);
            return resAsHex.ToArray();
        }

        private async Task<byte[]> GetLightingSheduleDataFromDeviceAsync()
        {
            List<byte[]> dataToRead = new List<byte[]>(COUNT_PACKAGE);


            for (int i = 0; i < COUNT_PACKAGE - 1; i++)
            {
                dataToRead.Add(await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.GetDataByAddressAsync((ushort)(_startAddress + MAIN_PACKAGE_LENGHT_WORD * i), MAIN_PACKAGE_LENGHT_WORD, "Get Lighting Shedule Data. Package " + i));
            }

            dataToRead.Add(await (_currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection.GetDataByAddressAsync((ushort)(_startAddress + 700), LAST_PACKAGE_LENGHT, "Get Lighting Shedule Data. Last package " + COUNT_PACKAGE));

            var result = new List<byte>();



            foreach (byte[] data in dataToRead)
            {
                if (data == null)
                {
                    throw new Exception("2.Чтение данных из устройства завершилось с ошибкой.");
                }
                result.AddRange(data);
            }
            var arrayResult = ConvertFromIntAsHexToIntAsDec(result).ToArray();
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
            if (deviceData.Length != 1536)
            {
                throw new Exception("3.Чтение данных из устройства завершилось с ошибкой.");
            }

            for (int i = 0; i < this.MonthCollection.Count; i++)
            {
                string monthName = this._mothNames[i];
                int monthStartIndex = MONTH_LENGHT_INDEX * i;
                IDaySheduleViewModel economyDaySheduleViewModel = this._monthsCollection[monthName].Last();

                economyDaySheduleViewModel.StartHour = deviceData[monthStartIndex + 1];
                economyDaySheduleViewModel.StartMinute = deviceData[monthStartIndex];
                economyDaySheduleViewModel.StopHour = deviceData[monthStartIndex + 3];
                economyDaySheduleViewModel.StopMinute = deviceData[monthStartIndex + 2];


                for (int j = 0; j < this._monthsLenghtDictionary[monthName]; j++)
                {
                    int dayStartIndex = monthStartIndex + j * DAY_LENGHT_INDEX;
                    this._monthsCollection[monthName][j].StartHour = deviceData[dayStartIndex + 5];
                    this._monthsCollection[monthName][j].StartMinute = deviceData[dayStartIndex + 4];
                    this._monthsCollection[monthName][j].StopHour = deviceData[dayStartIndex + 7];
                    this._monthsCollection[monthName][j].StopMinute = deviceData[dayStartIndex + 6];

                }
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
        public ICommand NavigateToSheduler1Command
        {
            get
            {
                return this._navigateToLightingShedule ??
                       (this._navigateToLightingShedule = new DelegateCommand(OnNavigateToShedule1));
            }
        }

        /// <summary>
        ///     Команда навигации на вьюху графика подсветки
        /// </summary>
        public ICommand NavigateToSheduler2Command
        {
            get
            {
                return this._navigateToBackLightShedule ??
                       (this._navigateToBackLightShedule = new DelegateCommand(OnNavigateToShedule2));
            }
        }

        /// <summary>
        ///     Команда навигации на вьюху графика энергосбережения
        /// </summary>
        public ICommand NavigateToSheduler3Command
        {
            get
            {
                return this._navigateToStoregeEnergyShedule ??
                       (this._navigateToStoregeEnergyShedule = new DelegateCommand(OnNavigateToShedule3));
            }
        }


        private void OnNavigateToShedule1()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
            _navigationContext.Add("title", "График 1 (освещение)");
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });

        }

        private void OnNavigateToShedule2()
        {

            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
        
            _navigationContext.Add("title", "График 2 (иллюминация)");
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }

        private void OnNavigateToShedule3()
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            var runtimeRegion = regionManager.Regions[ApplicationGlobalNames.RUNTIME_REGION_NAME];
            var uri = new Uri(ApplicationGlobalNames.PICON2LIGHTING_SHEDULER_VIEW_NAME, UriKind.Relative);
            _navigationContext.Add("title", "График 3 (подсветка)");
            runtimeRegion.RequestNavigate(uri, result =>
            {
                if (result.Result == false)
                {
                    throw new Exception(result.Error.Message);
                }
            });
        }
        // интересная история: в пиконе2 значения времени хранятся в хексе, 
        // но если читать это хексовое значение и предполагать, что на самом деле ты видишь десятичное, то это и будет действительное значение времени в десятичном варианте
        // то есть видишь 22 в хексе значит это 22 в дэке, но с устройства получаем уже преобразованные из хекса в дэк байты, поэтому так:
        private List<byte> ConvertFromIntAsHexToIntAsDec(List<byte> intsAsHex)
        {
            List<byte> intsAsDec = new List<byte>();
            foreach (var intAsHex in intsAsHex)
            {
                string hexValueStr = intAsHex.ToString("X");
                byte decValue = Convert.ToByte(hexValueStr);
                intsAsDec.Add(decValue);
            }

            return intsAsDec;
        }
        // наоборот
        private List<byte> ConvertFromIntAsDecToIntAsHex(byte[] intsAsDec)
        {
            List<byte> intsAsHex = new List<byte>();
            foreach (var intAsHex in intsAsDec)
            {
                string decValueStr = intAsHex.ToString("D");
                byte hexValue = Convert.ToByte(decValueStr, 16);
                intsAsHex.Add(hexValue);
            }

            return intsAsHex;
        }
        #endregion
    }
}
