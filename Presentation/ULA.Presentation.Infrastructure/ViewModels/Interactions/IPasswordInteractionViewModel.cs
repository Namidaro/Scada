namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Интерфейс вью-модели окна ввода пароля
    /// </summary>
    public interface IPasswordInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Результат завершения вызова вьюшки
        /// </summary>
        MessageBoxResult Result { get; set; }

        /// <summary>
        ///     Свойство представляющее пароль введенный пользователем
        /// </summary>
        string Password { get; set; }

        /// <summary>
        ///     Gets or sets the title of current interaction request
        ///     Заголовок
        /// </summary>
        string Title { get; set; }
    }
}
