using System.Collections.ObjectModel;
using System.Windows.Input;
using ULA.AddinsHost.ViewModel.Device;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Represents a interface of Scheme viewModel in Runtie mode
    /// </summary>
    public interface ISchemeModeRuntimeViewModel
    {


        /// <summary>
        ///     Показывает существет ли связь между первым(левым на схеме) пускателем и предохранителями(0-7)
        /// </summary>
        ObservableCollection<bool> Starter1ToResistorsLink { get; }

        /// <summary>
        ///     Показывает существет ли связь между вторым(правым на схеме) пускателем и предохранителями(0-7)
        /// </summary>
        ObservableCollection<bool> Starter2ToResistorsLink { get; }

         IRuntimeDeviceViewModel CurrentDeviceViewModel { get; }



        /// <summary>
        ///     Command represents action of changing mode to ListWidgetModeMode
        /// </summary>
        ICommand BackToListWidgetCommand { get; }

        /// <summary>
        ///     Команды навигации на вьюху конфигуратора руно
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Runo")]
        ICommand NavigateToConfigurationCommand { get; }

        /// <summary>
        ///     Команда навигации на вьюху графика освещения
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Sheduler")]
        ICommand NavigateToLightingShedulerCommand { get; }

     

        /// <summary>
        ///     открывает окно с данными аналогов
        /// </summary>
        ICommand ShowAnalogDataCommand { get; }

        bool IsPicon2 { get; set; }
        ICommand OpenPicon2ModuleInformationCommand { get; }
        
    }
}
