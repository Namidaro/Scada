namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes information message box interaction view model functionality
    /// </summary>
    public interface IInformationMessageBoxInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets or sets the title of current interaction request
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets or sets the message of current interaction request
        /// </summary>
        string Message { get; set; }
    }
}