namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Exposes system busy interaction view model functionality
    /// </summary>
    public interface IBusyInteractionViewModel : IInteractionViewModel
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
        ///     Is button visible
        /// </summary>
        bool ButtonVisible { get; set; }
    }
}