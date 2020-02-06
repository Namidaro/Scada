using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.ServiceLocation;
using Prism.Commands;
using Prism.Regions;
using ULA.AddinsHost.Business.Device;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.ViewModel.Device;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using ULA.AddinsHost.ViewModel.Factories;
using ULA.Business.Infrastructure.DataServices;
using ULA.Business.Infrastructure.DeviceDomain;
using ULA.Common;
using ULA.Devices.Presentation.Interfaces;
using ULA.Presentation.Infrastructure;

namespace ULA.Devices.Presentation.Runtime
{
    /// <summary>
    ///     Represents basic runtime logical deviceViewModel functionality
    ///     Реализация базового функционала устройства реального времени 
    /// </summary>
    public class RuntimeDeviceViewModel : DeviceViewModelBase, IRuntimeDeviceViewModel, INavigationAware
    {
        private readonly IStarterViewModelFactory _starterViewModelFactory;

        #region [Fields]

        private WidgetState _widgetState;
        private IDefectStateViewModel _defectStateViewModel;
        private readonly IDeviceCommandService _deviceCommandService;
        private readonly IOutgoingLinesViewModelFactory _outgoingLinesViewModelFactory;
        private readonly ICustomItemsViewModelFactory _customItemsViewModelFactory;
        private string _deviceSignature;
        private IOutgoingLinesViewModel _outgoingLinesViewModel;
        private ICustomItemsViewModel _customItemsViewModel;
        private bool _isTimeMode;

        #endregion [Fields]


        /// <summary>
        /// 
        /// </summary>
        public RuntimeDeviceViewModel(IStarterViewModelFactory starterViewModelFactory,
            IDefectStateViewModel defectStateViewModel, IDeviceCommandService deviceCommandService,
            IAnalogDataViewModel analogDataViewModel, IOutgoingLinesViewModelFactory outgoingLinesViewModelFactory,
            ICustomItemsViewModelFactory customItemsViewModelFactory, ICustomItemsViewModel customItemsViewModel,
            IQueryIndicatorViewModel queryIndicatorViewModel)
        {
            _starterViewModelFactory = starterViewModelFactory;
            StarterViewModels = new ObservableCollection<IStarterViewModel>();
            _defectStateViewModel = defectStateViewModel;
            _deviceCommandService = deviceCommandService;
            _outgoingLinesViewModelFactory = outgoingLinesViewModelFactory;
            _customItemsViewModelFactory = customItemsViewModelFactory;
            AnalogDataViewModel = analogDataViewModel;
            NavigateToSchemeCommand = new DelegateCommand(OnNavigateToSchemeExecute);
            StateWidget = WidgetState.NoConnection;
            _customItemsViewModel = customItemsViewModel;
            QueryIndicatorViewModel = queryIndicatorViewModel;
        }

        private void OnNavigateToSchemeExecute()
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


        #region [PresentationProperty]


        /// <summary>
        ///     Get or set value represent state of widget(deviceViewModel)
        ///     Свойство отображающее состояние устройства
        /// </summary>
        public WidgetState StateWidget
        {
            get { return this._widgetState; }
            set
            {

                if (this._widgetState.Equals(value)) return;
                this._widgetState = value;
                RaisePropertyChanged();
            }
        }

        public IAnalogDataViewModel AnalogDataViewModel { get; }

        public string DeviceSignature
        {
            get { return _deviceSignature; }
            set
            {
                if (_deviceSignature != null && this._deviceSignature.Equals(value)) return;
                this._deviceSignature = value;
                RaisePropertyChanged();
            }
        }

        public ICustomItemsViewModel CustomItemsViewModel
        {
            get { return _customItemsViewModel; }
        }

        public IQueryIndicatorViewModel QueryIndicatorViewModel { get; }

        public ICommand IsTimeModeChanged { get; set; }

        public bool IsTimeMode
        {
            get { return _isTimeMode; }
            set
            {
                _isTimeMode = value;
                (Model as IRuntimeDevice).SetUpdatingMode(value);
                IsTimeModeChanged?.Execute(null);
                RaisePropertyChanged();
            }
        }

        #endregion [PresentationProperty]


        public ObservableCollection<IStarterViewModel> StarterViewModels { get; }

        public IDefectStateViewModel DefectStateViewModel
        {
            get { return _defectStateViewModel; }
        }

        public IOutgoingLinesViewModel OutgoingLinesViewModel
        {
            get { return _outgoingLinesViewModel; }
        }

        public ICommand NavigateToSchemeCommand { get; }




        #region Overrides of DeviceViewModelBase

        protected override void SetModel(ILogicalDevice model)
        {
            base.SetModel(model);
            (_model as IRuntimeDevice).TcpDeviceConnection.ConnectionRestoredAction += OnConnectionRestored;
            (_model as IRuntimeDevice).TcpDeviceConnection.ConnectionLostAction += OnConnectionLost;
            (_model as IRuntimeDevice).DeviceInitialized += OnDeviceInitialized;
            (_model as IRuntimeDevice).DeviceValuesUpdated += OnDeviceValuesUpdated;
            DefectStateViewModel.Model = (_model as IRuntimeDevice).DefectState;
            AnalogDataViewModel.Model = (_model as IRuntimeDevice).AnalogData;
            (_model as IRuntimeDevice).TcpDeviceConnection.TransactionCompleteAction += () =>
            {
                QueryIndicatorViewModel?.BeginIndication();
            };
        }


        private async void OnDeviceValuesUpdated()

        {
            StarterViewModels.ForEach(model => model?.Update());
            DefectStateViewModel.Update();
            DeviceSignature = (_model as IRuntimeDevice).DeviceSignature;
            _outgoingLinesViewModel?.Update();
            _customItemsViewModel?.Update();
            if (StarterViewModels.Any((model => model.IsInRepairState == true)))
            {
                StateWidget = WidgetState.Repair;
            }
            //TODO: check widget going into repair mode
            else if (DefectStateViewModel.HasAnyDefect)
            {
                StateWidget = WidgetState.Crash;
            }
            else
            {
                StateWidget = WidgetState.Norm;
            }

            try
            {
                await _deviceCommandService.TryExecuteCommandOnDevice(this);
            }
            catch (Exception e)
            {
                //
            }

        }

        private void OnDeviceInitialized()
        {
            try
            {
                DispatchService.Invoke(() =>
                {
                    StarterViewModels.Clear();

                    for (int i = 1; i <= 3; i++)
                    {
                        if ((_model as IRuntimeDevice).StartersOnDevice.Any((starter => starter.StarterNumber == i)))
                        {
                            StarterViewModels.Add(_starterViewModelFactory.CreateStarterViewModel(
                                (_model as IRuntimeDevice).StartersOnDevice.First(
                                    (starter => starter.StarterNumber == i))));
                        }
                        else
                        {
                            StarterViewModels.Add(new StarterViewModel());
                        }
                    }
                    _outgoingLinesViewModel = _outgoingLinesViewModelFactory.CreateOutgoingLinesViewModel(Model);
                    RaisePropertyChanged(nameof(OutgoingLinesViewModel));
                    _customItemsViewModel =
                        _customItemsViewModelFactory.CreateCustomItemsViewModel((_model as IRuntimeDevice).DeviceCustomItems);
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private async void OnConnectionLost()
        {
            if (StateWidget == WidgetState.NoConnection) return;
            await Task.Run((() =>
            {
                StateWidget = WidgetState.NoConnection;
                StarterViewModels.ForEach((model => model?.SetLostConnection()));
                DefectStateViewModel.SetLostConnection();
                OutgoingLinesViewModel?.SetConnectionLost();
                CustomItemsViewModel?.SetLostConnection();
            }));

        }

        private void OnConnectionRestored()
        {
            StateWidget = WidgetState.Norm;
        }


        #region Overrides of DisposableBindableBase

        protected override void OnDisposing()
        {
            base.OnDisposing();
            (_model as IRuntimeDevice).TcpDeviceConnection.ConnectionRestoredAction = null;
            (_model as IRuntimeDevice).TcpDeviceConnection.ConnectionLostAction = null;
            (_model as IRuntimeDevice).DeviceValuesUpdated = null;
            (_model as IRuntimeDevice).TcpDeviceConnection.TransactionCompleteAction = null;
            (_model as IRuntimeDevice).Dispose();
            IsTimeModeChanged = null;
        }

        #endregion

        #endregion

        #region Implementation of INavigationAware

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            navigationContext.Parameters.Add(ApplicationGlobalNames.CURRENT_DEVICE_VIEW_MODEL, this);
        }

        #endregion
    }

}