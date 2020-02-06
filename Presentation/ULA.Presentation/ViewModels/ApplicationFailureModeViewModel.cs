using System;
using System.Threading.Tasks;
using ULA.Presentation.Infrastructure.ViewModels;
using YP.Toolkit.System.ParallelExtensions;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Presentation.ViewModels
{
    /// <summary>
    ///     Represents an application failure mode view model functionality
    ///     Представляет мью-модель для режима Ошибка."Всё плохо"
    /// </summary>
    public class ApplicationFailureModeViewModel : Disposable, IApplicationFailureModeViewModel
    {
        #region [Fields]

        private Exception _failure;

        #endregion [Fields]

        #region [IApplicationFailureModeViewModel members]

        /// <summary>
        ///     Initializes current application mode asynchronously
        ///     Асинхронная инициализация
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public Task InitializeAsync()
        {
            return Task<object>.Factory.FromResult(null);
        }

        /// <summary>
        ///     Un-initializes current application mode asynchronously
        ///     Асинхронная деинициализация
        /// </summary>
        /// <returns>An instance of System.Threading.Tasks.Task that represents current asynchronous operation</returns>
        public Task UninitializeAsync()
        {
            return Task<object>.Factory.FromResult(null);
        }

        /// <summary>
        ///     Invokes when initialization process is over
        ///     Вызывается когда процесс инициализации закончен
        /// </summary>
        public void OnInitialized()
        {
            
        }

        /// <summary>
        ///     Sets an instance of <see cref="Exception" /> that represents application mode changing failure
        ///     Устанавливает ошибку вызвавшую переход на режим ошибки
        /// </summary>
        /// <param name="failure">An instance of <see cref="Exception" /> that represents application mode changing failure</param>
        public void SetFailure(Exception failure)
        {
            this._failure = failure;
        }

        #endregion [IApplicationFailureModeViewModel members]
    }
}