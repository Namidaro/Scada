using System.Net.Configuration;
using System.Windows.Input;

namespace ULA.AddinsHost.ViewModel.Device
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDeviceCommandQueueViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="runtimeDeviceViewModel"></param>
        void SetContext(IRuntimeDeviceViewModel runtimeDeviceViewModel);

        /// <summary>
        ///     Команада синхронизации времени на устройстве
        /// </summary>
        ICommand SetDateTimeCommand { get; }
        /// <summary>
        ///     Перевод в авторежим всех пускателей
        /// </summary>
        ICommand AutoModeAllStartersCommand { get; }

        /// <summary>
        /// Перевод в ручной режим все пускатели
        /// </summary>
        ICommand ManualModeAllStartersCommand { get; }
        /// <summary>
        ///     Включение режима ремонта на всех пускателей
        /// </summary>
        ICommand StartRepairAllStarter { get; }

        /// <summary>
        ///     Отключение режима ремонта на всех пускателях
        /// </summary>
        ICommand StopRepairAllStarter { get; }
        /// <summary>
        ///     Включить ночное
        /// </summary>
        ICommand RunNightlightCommand { get; }
        /// <summary>
        ///     Отключить ночное
        /// </summary>
        ICommand StopNightlightCommand { get; }
        /// <summary>
        ///     Включить вечернее
        /// </summary>
         ICommand RunEveningCommand { get; }
        /// <summary>
        ///     Отключить вечернее
        /// </summary>
         ICommand StopEveningCommand { get; }

        /// <summary>
        ///     Включить полное
        /// </summary>
         ICommand RunFullCommand { get; }
        /// <summary>
        ///     Отключить полное
        /// </summary>
         ICommand StopFullCommand { get; }

        /// <summary>
        ///     Включить подсветку
        /// </summary>
         ICommand RunBacklightCommand { get; }

        /// <summary>
        ///     Отключить подсветку
        /// </summary>
         ICommand StopBacklightCommand { get; }

        /// <summary>
        ///     Включить иллюминацию
        /// </summary>
         ICommand RunIlluminationCommand { get; }
        /// <summary>
        ///     Отключить иллюминацию
        /// </summary>
        ICommand StopIlluminationCommand { get; }


        /// <summary>
        ///     Включить энергосбережение
        /// </summary>
        ICommand RunEnergySavingCommand { get; }
        /// <summary>
        ///     Отключить энергосбережение
        /// </summary>
        ICommand StopEnergySavingCommand { get; }
    }


}