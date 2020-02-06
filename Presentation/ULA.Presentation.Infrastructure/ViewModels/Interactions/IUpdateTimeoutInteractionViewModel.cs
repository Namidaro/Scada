namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Описывает функциональность вью-модели окна установки таймаута
    /// </summary>
    public interface IUpdateTimeoutInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets  the result of current interaction request
        /// </summary>
        MessageBoxResult Result { get; set; }

        /// <summary>
        ///     Описывает задержку в сукундах, которую будет выбирать пользователь
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        ///     Минимальное значение для обновления
        /// </summary>
        int MinValue { get; set; }

        /// <summary>
        ///     Максимальное значение для обновления
        /// </summary>
        int MaxValue { get; set; }
    }
}
