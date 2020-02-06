using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ULA.Common.AsyncServices
{
    /// <summary>
    ///     Represents a helper class that aggregates async initializations
    ///     Представляет класс асинхронной инициализации
    /// </summary>
    public static class AsyncInitialization
    {
        #region [Public members]

        /// <summary>
        ///     Ensures that all instance of <see cref="IAsyncInitializationService" /> are initializing
        ///     
        /// </summary>
        /// <param name="instances">
        ///     A collection of <see cref="object" /> that exposes <see cref="IAsyncInitializationService" />
        /// </param>
        /// functionality
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public static Task EnsureInitializedAsync(IEnumerable<object> instances)
        {
            return Task.Factory.ContinueWhenAll(
                instances.OfType<IAsyncInitializationService>().Select(s => s.Initialization).ToArray(),
                tasks => tasks, TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        ///     Ensures that all instance of <see cref="IAsyncInitializationService" /> are initializing
        /// </summary>
        /// <param name="instances">
        ///     A collection of <see cref="object" /> that exposes <see cref="IAsyncInitializationService" />
        /// </param>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public static Task EnsureInitializedAsync(params object[] instances)
        {
            return AsyncInitialization.EnsureInitializedAsync(instances.AsEnumerable());
        }

        #endregion [Public members]
    }
}