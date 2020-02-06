namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes warning message box interaction view model functionality
    /// </summary>
    public interface IWarningMessageBoxInteractionViewModel : IInteractionViewModel
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

        /// <summary>
        ///     Gets a type byttons for interaction with user
        /// </summary>
        MessageBoxButtonType ButtonType { get; set; }
    }
}