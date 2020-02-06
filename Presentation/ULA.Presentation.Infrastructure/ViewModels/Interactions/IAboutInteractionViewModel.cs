namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a about interaction view model functionally
    /// </summary>
    public interface IAboutInteractionViewModel : IInteractionViewModel
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