using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using Microsoft.Practices.ServiceLocation;
using NLog;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Strings;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Devices.Runo3.Presentation.ViewModels.UserControls;
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

namespace ULA.Devices.Runo3.Presentation.ViewModels
{
    /// <summary>
    ///     Вью-модель для режима конфигурации руно
    /// </summary>
    public class RunoConfigurationModeViewModel : ViewModelBase, IConfigurationModeViewModel, INavigationAware
    {
        #region [Const]

        private const ushort ADDRESS = 0x8200;
        private const ushort LENGTH_WORD = 0x3D;
        private const ushort RUNO_VERSION_ADDRESS = 0x400;
        private const int RUNO_VERSION_LENGHT = 10;

        private const string NO = "Нет";

        private const string DATA_KU_LIGHTING = "График освещения";
        private const string DATA_KU_BACKLIGHT = "График подсветки";
        private const string DATA_KU_ILLUMINATION = "График иллюминации";
        private const string DATA_KU_ENERGY_SAVING = "График энергосбережения";
        private const string DATA_KU_HEATING = "График обогрева";
        private const string DATA_KU_ECONOMY_LIGHTING = "График освещения+экономия";
        private const string DATA_KU_ECONOMY_BACKLIGHT = "График подсветки+экономия";
        private const string DATA_KU_ECONOMY_ILLUMINATION = "График иллюминации+экономия";

        private const string OUTPUTS_KU_RELAY_1 = "Реле №1";
        private const string OUTPUTS_KU_RELAY_2 = "Реле №2";
        private const string OUTPUTS_KU_RELAY_3 = "Реле №3";

        private const string MANAGEMENT_KU_DISCRETE_1 = "Дискрет №1";
        private const string MANAGEMENT_KU_DISCRETE_2 = "Дискрет №2";
        private const string MANAGEMENT_KU_DISCRETE_3 = "Дискрет №3";
        private const string MANAGEMENT_KU_DISCRETE_4 = "Дискрет №4";
        private const string MANAGEMENT_KU_DISCRETE_5 = "Дискрет №5";
        private const string MANAGEMENT_KU_DISCRETE_6 = "Дискрет №6";
        private const string MANAGEMENT_KU_DISCRETE_7 = "Дискрет №7";
        private const string MANAGEMENT_KU_DISCRETE_8 = "Дискрет №8";
        private const string MANAGEMENT_KU_DISCRETE_9 = "Дискрет №9";
        private const string MANAGEMENT_KU_DISCRETE_10 = "Дискрет №10";
        private const string MANAGEMENT_KU_DISCRETE_11 = "Дискрет №11";

        #endregion

        #region [Private Fields]

        private IRuntimeDeviceViewModel _currentDeviceViewModel;
        private string _deviceName;
        private string _runoVersionName = String.Empty;
        private ICommand _sendConfiguration;
        private ICommand _getConfiguration;
        private ICommand _getConfigurationFromFileCommand;
        private ICommand _saveConfigurationInFileCommand;
        private IInteractionService _interactionService;
        private ICommand _backToSchemeCommand;
        private ObservableCollection<IConfigCheckBoxControlViewModel> _faultCanals;
        private IConfigCheckBoxControlViewModel _faultPower;
        private IConfigCheckBoxControlViewModel _faultManagemet;
        private IConfigCheckBoxControlViewModel _faultSecurity;
        private IConfigCheckBoxControlViewModel _faultMask;
        private int _timeToStart;
        private ObservableCollection<string> _dataKuSelected;
        private ObservableCollection<string> _outputsKuSelected;
        private ObservableCollection<string> _managementKuSelected;
        private ObservableCollection<string> _dataKuCollection;
        private ObservableCollection<string> _outputsKuCollection;
        private ObservableCollection<string> _managementKuCollection;
        private ObservableCollection<string> _tempManagementKuCollection;
        private ObservableCollection<string> _tempOutputKuSelected; 
        private Logger _logger;
        private bool[] _diskretArr;
        private bool _isReadAndInitializeNow = false;

        // Словарь где имени графика соотносится его hex значение в устройстве
        private readonly Dictionary<string, ushort> _dataNameValueDictionary = new Dictionary<string, ushort>
        {
            {NO, 0x0000}, {DATA_KU_LIGHTING, 0x0001}, {DATA_KU_BACKLIGHT, 0x0002}, {DATA_KU_ILLUMINATION, 0x0003}, {DATA_KU_ENERGY_SAVING, 0x0004},
            {DATA_KU_HEATING, 0x0005}, {DATA_KU_ECONOMY_LIGHTING, 0x0006}, {DATA_KU_ECONOMY_BACKLIGHT, 0x0007}, {DATA_KU_ECONOMY_ILLUMINATION, 0x0008}
        };

        // Словарь где имени реле соотносится его hex значение в устройстве
        private readonly Dictionary<string, ushort> _outputsNameValueDictionary = new Dictionary<string, ushort>
        {
            {OUTPUTS_KU_RELAY_1, 0x01}, {OUTPUTS_KU_RELAY_2, 0x02}, {OUTPUTS_KU_RELAY_3, 0x03}, {NO, 0x0000}
        };

        // Словарь где имени дискрета управления соотносится его hex значение в устройстве
        private readonly Dictionary<string, ushort> _managementNameValueDictionary = new Dictionary<string, ushort>
        {
            {MANAGEMENT_KU_DISCRETE_1, 0x0001}, {MANAGEMENT_KU_DISCRETE_2, 0x0002}, {MANAGEMENT_KU_DISCRETE_3, 0x0003}, {MANAGEMENT_KU_DISCRETE_4, 0x0004}, 
            {MANAGEMENT_KU_DISCRETE_5, 0x0005}, {MANAGEMENT_KU_DISCRETE_6, 0x0006}, {MANAGEMENT_KU_DISCRETE_7, 0x0007}, {MANAGEMENT_KU_DISCRETE_8, 0x0008}, 
            {MANAGEMENT_KU_DISCRETE_9, 0x0009},  {MANAGEMENT_KU_DISCRETE_10, 0x000A}, {MANAGEMENT_KU_DISCRETE_11, 0x000B}, {NO, 0x0000}
        };

        // Словарь где имени данного соотносится его hex значение в устройстве
        private readonly Dictionary<string, ushort> _nameValueDictionar = new Dictionary<string, ushort>
        {
            {NO, 0x0000},
            {DATA_KU_LIGHTING, 0x0001}, {DATA_KU_BACKLIGHT, 0x0002}, {DATA_KU_ILLUMINATION, 0x0003}, {DATA_KU_ENERGY_SAVING, 0x0004},
            {DATA_KU_HEATING, 0x0005}, {DATA_KU_ECONOMY_LIGHTING, 0x0006}, {DATA_KU_ECONOMY_BACKLIGHT, 0x0007}, {DATA_KU_ECONOMY_ILLUMINATION, 0x0008},
            {OUTPUTS_KU_RELAY_1, 0x01}, {OUTPUTS_KU_RELAY_2, 0x02}, {OUTPUTS_KU_RELAY_3, 0x03}, 
            {MANAGEMENT_KU_DISCRETE_1, 0x01}, {MANAGEMENT_KU_DISCRETE_2, 0x02}, {MANAGEMENT_KU_DISCRETE_3, 0x03}, {MANAGEMENT_KU_DISCRETE_4, 0x04}, 
            {MANAGEMENT_KU_DISCRETE_5, 0x05}, {MANAGEMENT_KU_DISCRETE_6, 0x06}, {MANAGEMENT_KU_DISCRETE_7, 0x07}, {MANAGEMENT_KU_DISCRETE_8, 0x08}, 
            {MANAGEMENT_KU_DISCRETE_9, 0x09},  {MANAGEMENT_KU_DISCRETE_10, 0xA}, {MANAGEMENT_KU_DISCRETE_11, 0xB}
        };

        // Маска реле неисправности канала
        private readonly ushort[] _maskRelayFaultCanal = new ushort[11]
        {
            0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080, 0x0100, 0x0200, 0x0400
        };



        #endregion [Private Fields]

        #region [Ctor's]

        /// <summary>
        ///     Конструктор класса <see cref="RunoConfigurationModeViewModel"/>
        /// </summary>
        /// <param name="interactionService">Сущность представляющая сервис модальных окон. 
        ///         Реализует интерфейс <see cref="IInteractionService"/></param>
        public RunoConfigurationModeViewModel(IInteractionService interactionService)
        {
            Guard.AgainstNullReference(interactionService, "interactionService");
            this._interactionService = interactionService;
            Refresh();
        }


        private void Refresh()
        {
            var faultList = new List<IConfigCheckBoxControlViewModel>();
            for (int i = 0; i < 8; i++)
            {
                faultList.Add(new RunoConfigCheckBoxControlViewModel());
            }
            this.FaultCanals = new ObservableCollection<IConfigCheckBoxControlViewModel>(faultList);
            this.FaultMask = new RunoConfigCheckBoxControlViewModel();
            this.FaultManagement = new RunoConfigCheckBoxControlViewModel();
            this.FaultPower = new RunoConfigCheckBoxControlViewModel();
            this.FaultSecurity = new RunoConfigCheckBoxControlViewModel();
            this.DataKuSelected = new ObservableCollection<string>
            {
                NO, NO, NO, NO, NO, NO, NO, NO
            };
            this.OutputsKuSelected = new ObservableCollection<string>
            {
                NO, NO, NO, NO, NO, NO, NO, NO
            };
            this.ManagementKuSelected = new ObservableCollection<string>
            {
                NO, NO, NO, NO, NO, NO, NO, NO
            };
            _tempManagementKuCollection = new ObservableCollection<string>()
            {
                NO,
                NO,
                NO,
                NO,
                NO,
                NO,
                NO,
                NO
            };

            this._tempOutputKuSelected = new ObservableCollection<string>
            {
                NO, NO, NO, NO, NO, NO, NO, NO
            };
            this.TimeToStart = 100;
            this._diskretArr = new bool[11];
        }



        #endregion

        #region [Properties]

        /// <summary>
        ///     Описывает версию устройства
        /// </summary>
        public string RunoVersionDescription
        {
            get { return this._runoVersionName; }
            set
            {
                if (this._runoVersionName != null && this._runoVersionName.Equals(value)) return;
                this._runoVersionName = value;
                OnPropertyChanged("RunoVersionDescription");
            }
        }

        /// <summary>
        /// Флаг, true - если происходит считывание
        /// и передача конфигурации на вьюшку в данный момент 
        /// </summary>
        public Boolean IsInitializeNow
        {
            get { return this._isReadAndInitializeNow; }
            set { this._isReadAndInitializeNow = value; }

        }

        /// <summary>
        ///  Представляет временную коллекцию для комбобокс (Контроль управления)
        /// </summary>
        public ObservableCollection<string> TempManagmentCollection
        {
            get { return this._tempManagementKuCollection; }
            set { this._tempManagementKuCollection = value; }
        }

        /// <summary>
        ///     Представляет временную коллекцию для комбобокс (Выходы КУ)
        /// </summary>
        public ObservableCollection<string> TempOutputKuSelected
        {
            get { return this._tempOutputKuSelected; }
            set { this._tempOutputKuSelected = value; }
        }

        /// <summary>
        ///     Представляет коллекцию для неисправностей каналов(Чекбоксы)
        /// index + 1 == номер канала
        /// </summary>
        public ObservableCollection<IConfigCheckBoxControlViewModel> FaultCanals
        {
            get { return this._faultCanals; }
            set
            {
                if (this._faultCanals != null && this._faultCanals.Equals(value)) return;
                this._faultCanals = value;
                OnPropertyChanged("FaultCanals");
            }
        }

        /// <summary>
        ///     Представляет вью-модель для представления Маски неисправностей
        /// </summary>
        public IConfigCheckBoxControlViewModel FaultMask
        {
            get { return this._faultMask; }
            set
            {
                if (this._faultMask != null && this._faultMask.Equals(value)) return;
                this._faultMask = value;
                OnPropertyChanged("FaultMask");
            }
        }

        /// <summary>
        ///     Представляет вью-модель для представления неисправностей Режима управления
        /// </summary>
        public IConfigCheckBoxControlViewModel FaultManagement
        {
            get { return this._faultManagemet; }
            set
            {
                if (this._faultManagemet != null && this._faultManagemet.Equals(value)) return;
                this._faultManagemet = value;
                OnPropertyChanged("FaultManagement");
            }
        }

        /// <summary>
        ///     Представляет вью-модель для представления неисправностей Питания
        /// </summary>
        public IConfigCheckBoxControlViewModel FaultPower
        {
            get { return this._faultPower; }
            set
            {
                if (this._faultPower != null && this._faultPower.Equals(value)) return;
                this._faultPower = value;
                OnPropertyChanged("FaultPower");
            }
        }

        /// <summary>
        ///     Представляет вью-модель для представления неисправностей Охраны
        /// </summary>
        public IConfigCheckBoxControlViewModel FaultSecurity
        {
            get { return this._faultSecurity; }
            set
            {
                if (this._faultSecurity != null && this._faultSecurity.Equals(value)) return;
                this._faultSecurity = value;
                OnPropertyChanged("FaultSecurity");
            }
        }

        /// <summary>
        ///     Представляет результат выбранного в Combobox режима Контроля управления
        ///     для каждого канала
        ///     номер канала == index + 1
        /// </summary>
        public ObservableCollection<string> ManagementKuSelected
        {
            get { return this._managementKuSelected; }
            set
            {
                if (this._managementKuSelected != null && this._managementKuSelected.Equals(value)) return;
                this._managementKuSelected = value;
                OnPropertyChanged("ManagementKuSelected");
            }
        }

        /// <summary>
        ///     Представляет результат выбранного в Combobox режима Данные КУ
        ///     для каждого канала
        ///     номер канала == index + 1
        /// </summary>
        public ObservableCollection<string> DataKuSelected
        {
            get { return this._dataKuSelected; }
            set
            {
                if (this._dataKuSelected != null && this._dataKuSelected.Equals(value)) return;
                this._dataKuSelected = value;
                OnPropertyChanged("DataKuSelected");
            }
        }

        /// <summary>
        ///     Представляет результат выбранного в Combobox режима Выходы КУ
        ///     для каждого канала
        ///     номер канала == index + 1
        /// </summary>
        public ObservableCollection<string> OutputsKuSelected
        {
            get { return this._outputsKuSelected; }
            set
            {
                if (this._outputsKuSelected != null && this._outputsKuSelected.Equals(value)) return;
                this._outputsKuSelected = value;
                OnPropertyChanged("OutputsKuSelected");
            }
        }

        /// <summary>
        ///     Вернет коллекцию со списком значений для режима Контроль управления
        /// </summary>
        public ObservableCollection<string> ManagementKuCollection
        {
            get
            {
                return this._managementKuCollection ??
                       (this._managementKuCollection = new ObservableCollection<string>
                       {
                           NO, MANAGEMENT_KU_DISCRETE_1, MANAGEMENT_KU_DISCRETE_2, MANAGEMENT_KU_DISCRETE_3,
                           MANAGEMENT_KU_DISCRETE_4, MANAGEMENT_KU_DISCRETE_5, MANAGEMENT_KU_DISCRETE_6, 
                           MANAGEMENT_KU_DISCRETE_7, MANAGEMENT_KU_DISCRETE_8, MANAGEMENT_KU_DISCRETE_9,
                           MANAGEMENT_KU_DISCRETE_10, MANAGEMENT_KU_DISCRETE_11
                       });
            }
        }

        /// <summary>
        ///     Вернет коллекцию со списком значений для режима Данные КУ
        /// </summary>
        public ObservableCollection<string> DataKuCollection
        {
            get
            {
                return this._dataKuCollection ??
                       (this._dataKuCollection = new ObservableCollection<string>()
                       {
                           NO,
                           DATA_KU_LIGHTING, DATA_KU_BACKLIGHT, DATA_KU_ILLUMINATION, DATA_KU_ENERGY_SAVING,
                           DATA_KU_HEATING, DATA_KU_ECONOMY_LIGHTING, DATA_KU_ECONOMY_BACKLIGHT, 
                           DATA_KU_ECONOMY_ILLUMINATION
                       });
            }
        }

        /// <summary>
        ///     Вернет коллекцию со списком значений для режима Выходы КУ
        /// </summary>
        public ObservableCollection<string> OutputsKuCollection
        {
            get
            {
                return this._outputsKuCollection ??
                       (this._outputsKuCollection = new ObservableCollection<string>()
                       {
                           NO, OUTPUTS_KU_RELAY_1, OUTPUTS_KU_RELAY_2, OUTPUTS_KU_RELAY_3
                       });
            }
        }

        /// <summary>
        ///     Включение автоматики (в секундах)
        /// </summary>
        public int TimeToStart
        {
            get { return this._timeToStart; }
            set
            {
                if (this._timeToStart.Equals(value)) return;
                this._timeToStart = value;
                OnPropertyChanged("TimeToStart");
            }
        }

        #endregion [Properties]

        #region [IRunoConfigurationModeViewModel]

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
        ///      Представляет команду отправки конфигурационных данных на устройство
        /// </summary>
        public ICommand SendConfiguration
        {
            get
            {
                return this._sendConfiguration ?? (this._sendConfiguration = new DelegateCommand(OnSendConfiguration));
            }
        }

        /// <summary>
        ///     Представляет команду считывания конфигурационных данных с устройства
        /// </summary>
        public ICommand GetConfiguration
        {
            get
            {
                return this._getConfiguration ?? (this._getConfiguration =
                    new DelegateCommand(InitializingOnNavigateTo));
            }
        }

        /// <summary>
        ///     Загружает конфигурацию из файла
        /// </summary>
        public ICommand GetConfigurationFromFileCommand
        {
            get
            {
                return this._getConfigurationFromFileCommand ?? (this._getConfigurationFromFileCommand =
                    new DelegateCommand(OnGetConfigurationFromFileCommand));
            }
        }

        /// <summary>
        ///     Сохраняет конфигурацию в файл
        /// </summary>
        public ICommand SaveConfigurationInFileCommand
        {
            get
            {
                return this._saveConfigurationInFileCommand ??
                       (this._saveConfigurationInFileCommand = new DelegateCommand(
                           OnSaveConfigurationInFileCommand));
            }
        }

        /// <summary>
        ///     Команда навигации назад на схему устройства
        /// </summary>
        public ICommand BackToSchemeCommand
        {
            get { return this._backToSchemeCommand ?? (this._backToSchemeCommand = new DelegateCommand(OnBackToScheme)); }
        }

        #endregion [IRunoConfigurationModeViewModel]

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
        ///     При переходе с вьюхи
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL, _currentDeviceViewModel); 
            this._deviceName = null;
            this._currentDeviceViewModel = null;
            this._sendConfiguration = null;
            this.FaultManagement.PropertyChanged -= FaultOnPropertyChanged;
            this.FaultPower.PropertyChanged -= FaultOnPropertyChanged;
            this.FaultSecurity.PropertyChanged -= FaultOnPropertyChanged;
            this.DataKuSelected.CollectionChanged -= ManagementKuSelectedOnCollectionChanged;
            this.OutputsKuSelected.CollectionChanged -= OutputsKuSelectedOnCollectionChanged;
            RunoVersionDescription = String.Empty;
            foreach (var faultCanal in this.FaultCanals)
            {
                faultCanal.PropertyChanged -= FaultOnPropertyChanged;
            }
        }

        /// <summary>
        ///     При переходе на вьюху
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
         Refresh();  
            if (navigationContext.Parameters[ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL] != null)
            {
                
                    this._currentDeviceViewModel = navigationContext.Parameters[ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL] as
                        IRuntimeDeviceViewModel;
                Refresh();
                this.DeviceName = this._currentDeviceViewModel.DeviceName;
                    this.FaultManagement.PropertyChanged += FaultOnPropertyChanged;
                    this.FaultPower.PropertyChanged += FaultOnPropertyChanged;
                    this.FaultSecurity.PropertyChanged += FaultOnPropertyChanged;
                    foreach (var faultCanal in this.FaultCanals)
                    {
                        faultCanal.PropertyChanged += FaultOnPropertyChanged;
                    }

                    this._logger = LogManager.GetLogger(this.DeviceName + " " + "Конфигурация устройства");
                    this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) + " Переход в режим конфигурирования устройства");
               
                this.InitializingOnNavigateTo();
                    this.ManagementKuSelected.CollectionChanged += ManagementKuSelectedOnCollectionChanged;
                    this.OutputsKuSelected.CollectionChanged += OutputsKuSelectedOnCollectionChanged;
                
            }
        }

        #endregion [INavigationAware]

        #region [Help Members]

        private const string DECLARATION_VERSION = "1.0";
        private const string DECLARATION_ENCODING = "utf-8";
        private void OnSaveConfigurationInFileCommand()
        {
            var fileDialog = new System.Windows.Forms.SaveFileDialog()
            {
                Filter = "Configuration files (*.lc)|*.lc",
                Title = "Сохраните файл"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var xDoc = new XDocument(new XDeclaration(DECLARATION_VERSION, DECLARATION_ENCODING, ""));
                var root = new XElement("ChannelManagment");
                root.Add(new XElement("AutomationTime", this.TimeToStart));
                for (int i = 0; i < this.DataKuSelected.Count; i++)
                {
                    root.Add(new XElement("Channels", new object[]
                    {
                        new XElement("IsTurnOn", false), new XElement("GraphicValue", this.DataKuCollection.IndexOf(this.DataKuSelected[i])),
                        new XElement("DiscretValue", this.ManagementKuCollection.IndexOf(this.ManagementKuSelected[i])), 
                        new XElement("ReleValue", this.OutputsKuCollection.IndexOf(this.OutputsKuSelected[i]))
                    }));
                }

                var checkBoxCount = 11;

                for (int i = 0; i < this.FaultCanals.Count; i++)
                {
                    var channelMasksElement = new XElement("ChannelMasks");
                    for (int j = 0; j < checkBoxCount; j++)
                    {
                        channelMasksElement.Add(new XElement("Value", this.FaultCanals[i][j]));
                    }
                    root.Add(channelMasksElement);
                }

                var securityMaskElement = new XElement("SecurityMask");
                for (int i = 0; i < checkBoxCount; i++)
                {
                    securityMaskElement.Add(new XElement("Value", this.FaultSecurity[i]));
                }
                root.Add(securityMaskElement);

                var managmentMaskElement = new XElement("ManagmentMask");
                for (int i = 0; i < checkBoxCount; i++)
                {
                    managmentMaskElement.Add(new XElement("Value", this.FaultManagement[i]));
                }
                root.Add(managmentMaskElement);

                var powerMaskElement = new XElement("PowerMask");
                for (int i = 0; i < checkBoxCount; i++)
                {
                    powerMaskElement.Add(new XElement("Value", this.FaultPower[i]));
                }
                root.Add(powerMaskElement);

                var errorMaskElement = new XElement("ErrorMask");
                for (int i = 0; i < checkBoxCount; i++)
                {
                    errorMaskElement.Add(new XElement("Value", this.FaultMask.CheckBoxResult[i]));
                }
                root.Add(errorMaskElement);

                xDoc.Add(root);
                xDoc.Save(fileDialog.FileName);
            }
            catch (Exception exception)
            {
                this.InteractWithError(exception);
            }
        }

        private void OnGetConfigurationFromFileCommand()
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "Configuration files (*.lc)|*.lc",
                Title = "Выберите файл"
            };
            if (fileDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                for (int i = 0; i < 8; i++)
                {
                    this.DataKuSelected[i] = this.DataKuCollection[0];
                    this.OutputsKuSelected[i] = this.OutputsKuCollection[0];
                    this.ManagementKuSelected[i] = this.ManagementKuCollection[0];
                    for (int j = 0; j < 11; j++)
                    {
                        this.FaultCanals[i][j] = false;
                    }
                }
                for (int j = 0; j < 11; j++)
                {
                    this.FaultSecurity[j] = false;
                    this.FaultPower[j] = false;
                    this.FaultManagement[j] = false;
                }
                var doc = XDocument.Load(fileDialog.FileName);

                this.IsInitializeNow = true;

                int channelsCounter = 0, channelMasksCounter = 0;
                foreach (XElement xElement in doc.Root.Elements())
                {
                    if (xElement.Name.ToString().Equals("Device"))
                    {
                        if (xElement.Value.ToString(CultureInfo.InvariantCulture) != "RUNO")
                        {
                            this.InteractWithError(
                                new Exception("Файл от устройства: " +
                                              xElement.Value.ToString(CultureInfo.InvariantCulture) +
                                              ". Требуется файл, соответствующий устройству: RUNO"));
                            this.IsInitializeNow = false;
                            return;
                        }
                    }


                    if (xElement.Name.ToString().Equals("AutomationTime"))
                    {
                        this.TimeToStart = Int32.Parse(xElement.Value.ToString(CultureInfo.InvariantCulture));
                    }
                    if (xElement.Name.ToString().Equals("Channels"))
                    {
                        foreach (XElement element in xElement.Elements())
                        {
                            if (element.Name.ToString().Equals("GraphicValue"))
                            {
                                this.DataKuSelected[channelsCounter] =
                                    this.DataKuCollection[
                                        Int32.Parse(element.Value.ToString(CultureInfo.InvariantCulture))];
                            }
                            if (element.Name.ToString().Equals("ReleValue"))
                            {
                                this.OutputsKuSelected[channelsCounter] =
                                    this.OutputsKuCollection[
                                        Int32.Parse(element.Value.ToString(CultureInfo.InvariantCulture))];
                            }
                            if (element.Name.ToString().Equals("DiscretValue"))
                            {
                                this.ManagementKuSelected[channelsCounter] =
                                    this.ManagementKuCollection[
                                        Int32.Parse(element.Value.ToString(CultureInfo.InvariantCulture))];
                            }
                        }
                        channelsCounter++;
                    }
                    if (xElement.Name.ToString().Equals("ChannelMasks"))
                    {
                        int i = 0;
                        foreach (var element in xElement.Elements())
                        {
                            this.FaultCanals[channelMasksCounter][i] =
                                Boolean.Parse(element.Value.ToString(CultureInfo.InvariantCulture));
                            i++;
                            if (i > 10) break;
                        }

                        channelMasksCounter++;
                    }
                    if (xElement.Name.ToString().Equals("SecurityMask"))
                    {
                        int i = 0;
                        foreach (var element in xElement.Elements())
                        {
                            this.FaultSecurity[i] = Boolean.Parse(element.Value.ToString(CultureInfo.InvariantCulture));
                            i++;
                            if (i > 10) break;
                        }
                    }
                    if (xElement.Name.ToString().Equals("ManagmentMask"))
                    {
                        int i = 0;
                        foreach (var element in xElement.Elements())
                        {
                            this.FaultManagement[i] = Boolean.Parse(element.Value.ToString(CultureInfo.InvariantCulture));
                            i++;
                            if (i > 10) break;
                        }
                    }
                    if (xElement.Name.ToString().Equals("PowerMask"))
                    {
                        int i = 0;
                        foreach (var element in xElement.Elements())
                        {
                            this.FaultPower[i] = Boolean.Parse(element.Value.ToString(CultureInfo.InvariantCulture));
                            i++;
                            if (i > 10) break;
                        }
                    }
                }
            }
            catch
            {
                this.InteractWithError(new Exception("Файл не может быть прочитан"));
            }
            finally
            {
                this.IsInitializeNow = false;
            }

        }

        private void OutputsKuSelectedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var newOutputKuSelected = OutputsKuSelected.Where(output => output != NO);
            var allOutputCount = newOutputKuSelected.Count();
            var matchCount = newOutputKuSelected.Distinct().Count();
            //Если значения не равны, значит появилось совпадение
            if (allOutputCount != matchCount)
            {
                //после выхода из обработчика, попадаем в другой(в codebehind) который отменит выбранный дискрет
                return;
            }
            else
            {
                for (int i = 0; i < TempOutputKuSelected.Count; i++)
                {
                    this.TempOutputKuSelected[i] = OutputsKuSelected[i];
                }
            }
        }

        private void ManagementKuSelectedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var newManagementKuCollection = ManagementKuSelected.Where(o => o != NO);
            var allDiskretCount = newManagementKuCollection.Count();
            var matchCount = newManagementKuCollection.Distinct().Count();
            //Если значения не равны, значит появилось совпадение
            if (allDiskretCount != matchCount)
            {
                //после выхода из обработчика, попадаем в другой который отменит выбранный дискрет
                return;
            }
            else
            {
                this.GetBooleanArrForDiskrets();
                if (this.TryInitializeFaultMask() == false)
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < TempManagmentCollection.Count; i++)
                    {
                        this.TempManagmentCollection[i] = ManagementKuSelected[i];
                    }
                }
            }
        }

        private void GetBooleanArrForDiskrets()
        {
             for (int i = 0; i < _diskretArr.Length; i++)
            {
                this._diskretArr[i] = false;
            }
            int diskretNumber;
            for (int i = 0; i < ManagementKuSelected.Count; i++)
            {
                if (Int32.TryParse(Regex.Match(ManagementKuSelected[i], @"\d+").Value, out diskretNumber))
                {
                    this._diskretArr[diskretNumber - 1] = true;
                }
            }
        }

        private async void InitializingOnNavigateTo()
        {
            var busyToken = this.InteractWithBusy();
            try
            {
                byte[] initializingData = null;
                byte[] versionData = null;

                initializingData =
                    await (this._currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection
                        .GetDataByAddressAsync(ADDRESS, LENGTH_WORD,
                            RequestStrings.GET_CONFIGURATION);
                if (initializingData == null)
                {
                    throw new Exception("Не удалось считать данные с устройства");
                }


                versionData = await (this._currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection
                    .GetDataByAddressAsync(RUNO_VERSION_ADDRESS, RUNO_VERSION_LENGHT,
                   RequestStrings.READ_VERSION);

                if (versionData == null)
                {
                    throw new Exception("Не удалось считать версию устройства");
                }



                this._isReadAndInitializeNow = true;
                this.InitializeFromReadingData(initializingData);
                for (int i = 0; i < versionData.Length; i += 2)
                {
                    if (versionData.Length < i + 2) break;
                    var t = versionData[i];
                    versionData[i] = versionData[i + 1];
                    versionData[i + 1] = t;
                }
                var versionNumberStringBuilder = new StringBuilder();
                versionNumberStringBuilder.Append(" ");
                for (int i = versionData.Length - 1; i > 15; i--)
                {
                    versionNumberStringBuilder.Append(versionData[i]).Append(".");
                }
                this.RunoVersionDescription =
                    "Runo " + //System.Text.Encoding.ASCII.GetString(versionData.Take(16).Where(el => el!=0).ToArray()) + 
                    versionNumberStringBuilder.ToString();
                this._isReadAndInitializeNow = false;
                if (this.TryInitializeFaultMask() == false)
                {
                    this.ResetFaultBoxs();
                    throw new Exception(
                        @"Устройство сконфигурировано не верно. Пересечение масок неисправностей. 
                                                    Маски неисправностей будут сброшены.");
                }
            }
            catch (Exception error)
            {
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private async void OnSendConfiguration()
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

                var resultPackage = this.CreateDataPackage();

                await (this._currentDeviceViewModel.Model as IRuntimeDevice).TcpDeviceConnection
                    .PostDataByAddressAsync(ADDRESS,  resultPackage,
                        RequestStrings.SEND_CONFIGURATION);
                this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) +
                                   " Изменение конфигурации устройства");
            }
            catch (Exception error)
            {
                this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) +
                                   " Не удалось измененить конфигурацию устройства. Ошибка: " + error.Message);
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            try
            {
                //await Task.Run(() => { _currentDeviceViewModel.InitializeStarterToChanel(); });
                (_currentDeviceViewModel.Model as IRuntimeDevice).InitializeAsync();
            }
            catch (Exception error)
            {
                this._logger.Trace(DateTime.Now.ToString(new CultureInfo("de-DE")) +
                                   " После обновления конфигурации инициализация устройства не удалась. Ошибка: " +
                                   error.Message);
                busyToken.Dispose();
                this.InteractWithError(error);
            }
            busyToken.Dispose();
        }

        private void OnBackToScheme()
        {
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

   
       
        private void InitializeFromReadingData(byte[] data)
        {
            Guard.AgainstNullReference(data, "data");
            if (data.Length != LENGTH_WORD * 2)
            {
                throw new ArgumentException("data argument must have lenght equals " + LENGTH_WORD);
            }

            for (int i = 0; i < 8; i++)
            {
                this.InitializeCanal(i, data);
            }

            var securityAddress = 12 * 8;
            var faultSecurityValue = (ushort)(data[securityAddress] * 256 + data[securityAddress + 1]);
            for (int i = 0; i < this._maskRelayFaultCanal.Length; i++)
            {
                var mask = this._maskRelayFaultCanal[i];
                if ((faultSecurityValue & mask) > 0)
                {
                    this.FaultSecurity[i] = true;
                }
            }

            var powerAddress = 12 * 8 + 16;
            var faultPowerValue = (ushort)(data[powerAddress] * 256 + data[powerAddress + 1]);
            for (int i = 0; i < this._maskRelayFaultCanal.Length; i++)
            {
                var mask = this._maskRelayFaultCanal[i];
                if ((faultPowerValue & mask) > 0)
                {
                    this.FaultPower[i] = true;
                }
            }

            var managementAddress = 12 * 8 + 8;
            var faultManagementValue = (ushort)(data[managementAddress] * 256 + data[managementAddress + 1]);
            for (int i = 0; i < this._maskRelayFaultCanal.Length; i++)
            {
                var mask = this._maskRelayFaultCanal[i];
                if ((faultManagementValue & mask) > 0)
                {
                    this.FaultManagement[i] = true;
                }
            }

            var timeToStartAddress = 12 * 8 + 8 * 3;
            this.TimeToStart = data[timeToStartAddress] * 256 + data[timeToStartAddress + 1];
        }

        //  Инициализирует канал. Index - номер канала (нумерация с нуля)
        private void InitializeCanal(int index, byte[] data)
        {
            var address = index * 12;
            var dataValue = (ushort)(data[address] * 256 + data[address + 1]);
            if (this._dataNameValueDictionary.ContainsValue(dataValue))
            {
                this.DataKuSelected[index] =
                    this._dataNameValueDictionary.First(pair => pair.Value.Equals(dataValue)).Key;
            }

            var managementValue = (ushort)data[address + 2];
            if (this._managementNameValueDictionary.ContainsValue(managementValue))
            {
                this.ManagementKuSelected[index] =
                    this._managementNameValueDictionary.First(pair => pair.Value.Equals(managementValue)).Key;
            }

            var outputsValue = (ushort)data[address + 3];
            if (this._outputsNameValueDictionary.ContainsValue(outputsValue))
            {
                this.OutputsKuSelected[index] =
                    this._outputsNameValueDictionary.First(pait => pait.Value.Equals(outputsValue)).Key;
            }

            var faultValue = (ushort)(data[address + 4] * 256 + data[address + 5]);
            for (int i = 0; i < this._maskRelayFaultCanal.Length; i++)
            {
                var mask = this._maskRelayFaultCanal[i];
                if ((faultValue & mask) > 0)
                {
                    this.FaultCanals[index][i] = true;
                }
                else
                {
                    this.FaultCanals[index][i] = false;
                }
            }
        }

        /// <summary>
        ///     Создает пакет данных из выбранных позиций на вьюке
        /// </summary>
        /// <returns>пакет данных для конфигурирования руно</returns>
        private byte[] CreateDataPackage()
        {
            var result = new byte[LENGTH_WORD * 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = 0;
            }

            for (int i = 0; i < 8; i++)
            {
                this.InitializeDataFromCanal(i, result);
            }


            var securityAddress = 12 * 8;
            var securityData = this.FaultSecurity.GetCheckBoxBytes();
            if (securityData.Length != 16)
            {
                throw new Exception("FaultDataArrayLength not equals 16");
            }
            securityData = securityData.Reverse().ToArray();
            ushort value = this.BoolArrayToInt(securityData);
            result[securityAddress] = (byte)(value / 256);
            result[securityAddress + 1] = (byte)(value % 256);


            var powerAddress = 12 * 8 + 16;
            var powerData = this.FaultPower.GetCheckBoxBytes();
            if (powerData.Length != 16)
            {
                throw new Exception("FaultDataArrayLength not equals 16");
            }
            powerData = powerData.Reverse().ToArray();
            value = this.BoolArrayToInt(powerData);
            result[powerAddress] = (byte)(value / 256);
            result[powerAddress + 1] = (byte)(value % 256);

            var managementAddress = 12 * 8 + 8;
            var managementData = this.FaultManagement.GetCheckBoxBytes();
            if (managementData.Length != 16)
            {
                throw new Exception("FaultDataArrayLength not equals 16");
            }
            managementData = managementData.Reverse().ToArray();
            value = this.BoolArrayToInt(managementData);
            result[managementAddress] = (byte)(value / 256);
            result[managementAddress + 1] = (byte)(value % 256);

            var timeToStartAddress = 12 * 8 + 8 * 3;
            result[timeToStartAddress] = (byte)(this.TimeToStart / 256);
            result[timeToStartAddress + 1] = (byte)(this.TimeToStart % 256);

            return result;
        }

        //Заполняет данные данными с канала
        private void InitializeDataFromCanal(int index, byte[] data)
        {
            var address = index * 12;
            data[address] = (byte)(this._dataNameValueDictionary[this.DataKuSelected[index]] / 256);
            data[address + 1] = (byte)(this._dataNameValueDictionary[this.DataKuSelected[index]] % 256);
            data[address + 2] = (byte)(this._managementNameValueDictionary[this.ManagementKuSelected[index]]);
            data[address + 3] = (byte)(this._outputsNameValueDictionary[this.OutputsKuSelected[index]]);

            var faultData = this.FaultCanals[index].GetCheckBoxBytes();
            if (faultData.Length != 16)
            {
                throw new Exception("FaultDataArrayLength not equals 16");
            }
            faultData = faultData.Reverse().ToArray();
            ushort value = this.BoolArrayToInt(faultData);
            data[address + 4] = (byte)(value / 256);
            data[address + 5] = (byte)(value % 256);
        }

        private ushort BoolArrayToInt(bool[] bits)
        {
            if (bits.Length > 16) throw new ArgumentException("Can only fit 32 bits in a uint");

            int r = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i])
                {
                    r |= 1 << (bits.Length - 1 - i);
                }
            }
            return (ushort)r;
        }
        private bool CheckRepair()
        {
            return (_currentDeviceViewModel.Model as IRuntimeDevice).StartersOnDevice.Any((starter =>
            {
                return starter.IsInRepairState != null && starter.IsInRepairState.Value;
            }));
        }
        #endregion [Help Members]

        #region [Help Fault Validation]

        private void FaultOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "CheckBoxResult" && (sender is IConfigCheckBoxControlViewModel))
            {
                var viewModel = sender as IConfigCheckBoxControlViewModel;
                if (this.TryInitializeFaultMask() == false)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        if (this.FaultMask[i] == true && viewModel[i] == true)
                        {
                            int count = 0;
                            foreach (var canal in this.FaultCanals)
                            {
                                if (canal[i])
                                {
                                    count++;
                                }
                            }
                            if (this.FaultManagement[i])
                                count++;
                            if (this.FaultPower[i])
                                count++;
                            if (this.FaultSecurity[i])
                                count++;
                            if (ManagementKuSelected.Contains("Дискрет №"+(i+1)))
                            {
                                count++;
                            }
                            if (count > 1)
                                viewModel[i] = false;
                        }
                    }
                }
            }
        }

        private void ResetFaultBoxs()
        {
            for (int i = 0; i < 11; i++)
            {
                this.FaultManagement[i] = false;
                this.FaultPower[i] = false;
                this.FaultSecurity[i] = false;
                this.FaultMask[i] = false;
                foreach (var faultCanal in this.FaultCanals)
                {
                    faultCanal[i] = false;
                }
            }
        }

        /// <summary>
        ///     Инициализирует маску ошибок.
        /// </summary>
        /// <returns>если false - то устройсво сконфигурировано неверно. Если true - всё хорошо.</returns>
        private bool TryInitializeFaultMask()
        {
            bool result = true;
            var tempMask = new bool[11];
            for (int i = 0; i < 11; i++)
            {
                if (this._diskretArr[i] == true)
                {
                    if (tempMask[i] == true)
                    {
                        _diskretArr[i] = false;
                        return false;
                    }
                    else
                    {
                        tempMask[i] = true;
                    }
                }


                if (this.FaultManagement[i] == true)
                {
                    if (tempMask[i] == true)
                    {
                        return false;
                    }
                    else
                    {
                        tempMask[i] = true;
                    }
                }
                if (this.FaultPower[i] == true)
                {
                    if (tempMask[i] == true)
                    {
                        return false;
                    }
                    else
                    {
                        tempMask[i] = true;
                    }
                }
                if (this.FaultSecurity[i] == true)
                {
                    if (tempMask[i] == true)
                    {
                        return false;
                    }
                    else
                    {
                        tempMask[i] = true;
                    }
                }
                foreach (var faultCanal in this.FaultCanals)
                {
                    if (faultCanal[i] == true)
                    {
                        if (tempMask[i] == true)
                        {
                            return false;
                        }
                        else
                        {
                            tempMask[i] = true;
                        }
                    }
                }
            }

            for (int i = 0; i < 11; i++)
            {
                this.FaultMask[i] = tempMask[i];
            }
            OnPropertyChanged("FaultMask");
            return result;
        }

        #endregion [Help Fault Validation]
    }
}
