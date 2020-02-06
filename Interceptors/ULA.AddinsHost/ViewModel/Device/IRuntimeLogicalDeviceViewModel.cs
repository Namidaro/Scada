using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ULA.AddinsHost.Business.Device.Enums;
using ULA.AddinsHost.ViewModel.Device.CustomItems;
using ULA.AddinsHost.ViewModel.Device.OutgoingLines;
using ULA.AddinsHost.ViewModel.Factories;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    ///     Exposes runtime logical device functionality
    ///     Описывает функционал устройства в режиме реального времени
    /// </summary>
    public interface IRuntimeDeviceViewModel : ILogicalDeviceViewModelBase,IDisposable
    {
        /// <summary>
        /// пускатели
        /// </summary>
        ObservableCollection<IStarterViewModel> StarterViewModels { get; }
        /// <summary>
        /// 
        /// </summary>
        IDefectStateViewModel DefectStateViewModel { get; }

        /// <summary>
        /// 
        /// </summary>
        IOutgoingLinesViewModel OutgoingLinesViewModel { get; }

        /// <summary>
        /// 
        /// </summary>
        /// 
        ICommand NavigateToSchemeCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        WidgetState StateWidget { get; }
        /// <summary>
        /// 
        /// </summary>
        IAnalogDataViewModel AnalogDataViewModel { get; }

        /// <summary>
        /// 
        /// </summary>
        string DeviceSignature { get; }

        ICustomItemsViewModel CustomItemsViewModel { get; }

        IQueryIndicatorViewModel QueryIndicatorViewModel { get; }

        ICommand IsTimeModeChanged { get; set; }
        bool IsTimeMode { get; set; }
        
    }
}