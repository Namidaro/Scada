using System;
using System.Windows.Input;

namespace ULA.Presentation.Infrastructure.ViewModels.Interactions
{
    /// <summary>
    ///     Represents a syncronization time interaction view model functionally
    /// </summary>
    public interface ISynchronizationTimeInteractionViewModel : IInteractionViewModel
    {
        /// <summary>
        ///     Gets the date time of current interaction request
        /// </summary>
        DateTime DateTime { get; }

        /// <summary>
        ///     Gets the Hour of current interaction request
        /// </summary>
        int Hour { get; }

        /// <summary>
        ///     Gets  the Minute of current interaction request
        /// </summary>
        int Minute { get; }

        /// <summary>
        ///     Gets  the Second of current interaction request
        /// </summary>
        int Second { get; }

        /// <summary>
        ///     Gets the system time command of current interaction request
        /// </summary>
        ICommand SystemTimeCommand { get; }

        /// <summary>
        ///     Gets the increase second command of current interaction request
        /// </summary>
        ICommand IncreaseSecondCommand { get; }

        /// <summary>
        ///     Gets the increase minute command of current interaction request
        /// </summary>
        ICommand IncreaseMinuteCommand { get; }

        /// <summary>
        ///     Gets the increase hour command of current interaction request
        /// </summary>
        ICommand IncreaseHourCommand { get; }

        /// <summary>
        ///     Gets the reduce second command of current interaction request
        /// </summary>
        ICommand ReduceSecondCommand { get; }

        /// <summary>
        ///     Gets the reduce minute command of current interaction request
        /// </summary>
        ICommand ReduceMinuteCommand { get; }

        /// <summary>
        ///     Gets the reduce hour command of current interaction request
        /// </summary>
        ICommand ReduceHourCommand { get; }

        /// <summary>
        ///     Gets  the result of current interaction request
        /// </summary>
        MessageBoxResult Result { get; }
    }
}
