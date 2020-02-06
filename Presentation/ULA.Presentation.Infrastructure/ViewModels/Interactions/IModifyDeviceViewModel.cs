using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ULA.Presentation.Infrastructure.DTOs;
using ULA.Presentation.Infrastructure.ViewModels.CustomItems;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes a view model functionality for the create new device interaction request
    /// </summary>
    public interface IModifyDeviceViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets a value that indicates whether the dialog was canceled by a user
        /// </summary>
        bool IsCanceled { get; }


        /// <summary>
        ///     Gets an instance of <see cref="DeviceModel" /> that represents a model for the device that should be created
        /// </summary>
        DeviceModel ResultedDeviceModel { get; }
        

        /// <summary>
        ///     Sets a device for editing
        /// </summary>
        /// <param name="deviceModel">Device for editing.</param>
        void SetEditingContext(DeviceModel deviceModel, IEnumerable<string> deviceNamesExisting);
        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<IFiderDefinitionsViewModel> FiderDefinitionsViewModels { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommand AddFiderCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteFiderCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsFiderOrganization { get; set; }



        /// <summary>
        /// 
        /// </summary>
        ICommand AddSignalCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteSignalCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsSignalsEnabled { get; set; }


        ObservableCollection<ISignalDefinitionsViewModel> SignalDefinitionsViewModels { get; }



        /// <summary>
        /// 
        /// </summary>
        ICommand AddIndicatorCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteIndicatorCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsIndicatorsEnabled { get; set; }


        ObservableCollection<IIndicatorDefinitionsViewModel> IndicatorDefinitionsViewModels { get; }





        /// <summary>
        /// 
        /// </summary>
        ICommand AddCascadeCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        ICommand DeleteCascadeCommand { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsCascadeEnabled { get; set; }
        
        ObservableCollection<ICascadeDefinitionsViewModel> CascadeDefinitionsViewModels { get; }

        /// <summary>
        ///     Gets or sets the name of currently modified deviceViewModel
        ///     Имя устройства
        /// </summary>
        string DeviceName { get; set; }

        /// <summary>
        ///     Gets or sets the name of currently modified deviceViewModel
        ///     Имя устройства
        /// </summary>
        string DeviceType { get; set; }

        /// <summary>
        ///     Gets or sets the description of currently modified deviceViewModel
        ///     Описание устройства
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Tcp-ip address of currently modified deviceViewModel
        ///     Ip адрес устройства
        /// </summary>
        string TcpIpAddress { get; set; }

        /// <summary>
        ///     Gets or sets the Tcp-ip port of currently modified deviceViewModel
        ///     Tcp  порт устройства
        /// </summary>
        string TcpIpPort { get; set; }

        /// <summary>
        ///    коэффициент для счетчиков A
        /// </summary>
        string TransformKoefA { get; set; }


        /// <summary>
        ///    коэффициент для счетчиков B
        /// </summary>
        string TransformKoefB { get; set; }


        /// <summary>
        ///    коэффициент для счетчиков C
        /// </summary>
        string TransformKoefC { get; set; }




        /// <summary>
        ///     Описание стартеров
        /// </summary>
        List<string> StarterDescriptions { get; set; }


   




    }

}