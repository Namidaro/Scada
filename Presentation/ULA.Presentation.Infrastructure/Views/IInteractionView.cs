namespace ULA.Presentation.Infrastructure.Views
{
    /// <summary>
    ///     Exposes an interaction view functionality
    /// </summary>
    public interface IInteractionView
    {
        /// <summary>
        ///     Gets or sets an instance of <see cref="object" /> that represents current view's data context
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="string" /> that represents current interaction view's title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///  Gets or sets an instance of <see cref="string" /> that represents current interaction view's okButtonTitle
        /// </summary>
        string OkButtonTitle { get; set; }
    }
}