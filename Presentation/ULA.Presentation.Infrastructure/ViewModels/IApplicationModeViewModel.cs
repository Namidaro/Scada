using System;
using System.Threading.Tasks;

namespace ULA.Presentation.Infrastructure.ViewModels
{
    /// <summary>
    ///     Exposes an application mode basic view model functionality
    /// </summary>
    public interface IApplicationModeViewModel : IDisposable
    {
        /// <summary>
        ///     Initializes current application mode asynchronously
        /// </summary>
        /// <returns>
        ///     An instance of System.Threading.Tasks.Task that represents current asynchronous operation
        /// </returns>
        Task InitializeAsync();

        /// <summary>
        ///     Un-initializes current application mode asynchronously
        /// </summary>
        /// <returns>
        ///     An instance of System.Threading.Tasks.Task that represents current asynchronous operation
        /// </returns>
        Task UninitializeAsync();

        /// <summary>
        ///     Invokes when initialization process is over
        /// </summary>
        void OnInitialized();
    }
}