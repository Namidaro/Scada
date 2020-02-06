using ULA.Presentation.Infrastructure.ViewModels.Interactions;

namespace ULA.Presentation.Infrastructure.Services.Interactions
{
    /// <summary>
    ///     Exposes an interaction providers registry functionality
    /// </summary>
    public interface IInteractionProvidersRegistry
    {
        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined busy
        ///     interaction provider
        /// </summary>
        IInteractionProvider<IBusyInteractionViewModel> BusyInteractionProvider { get; }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined error
        ///     messagebox interaction provider
        /// </summary>
        IInteractionProvider<IErrorMessageBoxInteractionViewModel> ErrorMessageBoxInteractionProvider { get; }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     information messagebox interaction provider
        /// </summary>
        IInteractionProvider<IInformationMessageBoxInteractionViewModel> InformationMessageBoxInteractionProvider { get;
        }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     question messagebox interaction provider
        /// </summary>
        IInteractionProvider<IQuestionMessageBoxInteractionViewModel> QuestionMessageBoxInteractionProvider { get; }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionProvider{TViewModel}" /> that represents the system defined
        ///     warning messagebox interaction provider
        /// </summary>
        IInteractionProvider<IWarningMessageBoxInteractionViewModel> WarningMessageBoxInteractionProvider { get; }
    }
}