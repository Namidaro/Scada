namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes question message box interaction view model functionality
    /// </summary>
    public interface IQuestionMessageBoxInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets the title of current interaction request
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the message of current interaction request
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Gets a value that indicates result of interaction with user
        /// </summary>
        MessageBoxResult Result { get; }
    }
}