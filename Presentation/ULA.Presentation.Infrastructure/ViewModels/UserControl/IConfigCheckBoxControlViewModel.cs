using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ULA.Presentation.Infrastructure.ViewModels.UserControl
{
    /// <summary>
    ///     Представляет вью-модель для контрола с чекбоксами(Будет использоваться в конфигурации
    ///     устройства)
    /// </summary>
    public interface IConfigCheckBoxControlViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Вернет булевый масив как результаты чекбоксов
        /// </summary>
        /// <returns></returns>
        bool[] GetCheckBoxBytes();

        /// <summary>
        ///     Дает коллекцию с битовыми представлениями чекбоксов
        /// </summary>
        ObservableCollection<bool> CheckBoxResult { get; }

        /// <summary>
        ///     Индексатор для коллекция представляющей результаты чекбоксов
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        bool this[int index] { get; set; }

        /// <summary>
        /// показывает, выдерен ли хоть один чекбокс
        /// </summary>
        bool IsAtLeastOneSelected { get; }
    }
}
