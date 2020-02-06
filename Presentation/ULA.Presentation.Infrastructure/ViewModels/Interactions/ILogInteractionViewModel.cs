using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Presentation.Infrastructure.DTOs;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a log interaction view model functionally
    /// </summary>
    public interface ILogInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets the title of current interaction request
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the log collection interaction request
        /// </summary>
        ICollectionView LogCollection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ICommand RefreshCommand { get; }

        /// <summary>
        ///     Gets an instance of <see cref="string" /> that represents description filter
        /// </summary>
        string DescriptionFilter { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="DateTime" /> that represents from date and time filter
        /// </summary>
        DateTime DateFromFilter { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="DateTime" /> that represents to date and time filter
        /// </summary>
        DateTime DateToFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsLoadingInProcess { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDeviceViewModel"></param>
        void OpenDeviceLog(IRuntimeDeviceViewModel runtimeDeviceViewModel);


    }
}