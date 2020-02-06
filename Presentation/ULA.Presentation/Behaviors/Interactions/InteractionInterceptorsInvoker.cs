using System;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Presentation.Behaviors.Interactions
{
    /// <summary>
    ///     Represents default interaction invokers registry functionality
    ///     Представляет функционал регистрации интерактивных вызовов
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class InteractionInterceptorsInvoker<TViewModel> : Disposable, IInteractionInterceptorsInvoker
        where TViewModel : IInteractionViewModel
    {
        #region [Fields]

        private InteractionInterceptor<TViewModel> _interceptors;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="InteractionInterceptorsInvoker{TViewModel}" />
        /// </summary>
        /// <param name="interceptors">An instance of <see cref="InteractionInterceptor{TViewModel}" /> to use</param>
        public InteractionInterceptorsInvoker(InteractionInterceptor<TViewModel> interceptors)
        {
            this._interceptors = interceptors;
        }

        #endregion [Ctor's]

        #region [IInteractionInterceptorsInvoker members]

        /// <summary>
        ///     Invokes an action that represents before initialization request phase
        ///     Вызывает действие представляющее фазу запроса перед инициализации
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        public void InvokeBeforeInitialization(object viewModel)
        {
            if (this._interceptors == null) return;
            InvokeInternal(this._interceptors.OnBeforeInitialization, viewModel);
        }

        /// <summary>
        ///     Invokes an action that represents after initialization request phase
        ///     Вызывает действие представляющее фазу запроса после инициализации
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        public void InvokeAfterInitialization(object viewModel)
        {
            if (this._interceptors == null) return;
            InvokeInternal(this._interceptors.OnAfterInitialization, viewModel);
        }

        /// <summary>
        ///     Invokes an action that represents before un-initialization request phase
        ///     Вызывает действие представляющее фазу запроса перед деинициализации
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        public void InvokeBeforeUnInitialization(object viewModel)
        {
            if (this._interceptors == null) return;
            InvokeInternal(this._interceptors.OnBeforeUnInitialization, viewModel);
        }

        /// <summary>
        ///     Invokes an action that represents after un-initialization request phase
        ///     Вызывает действие представляющее фазу запроса после деинициализации
        /// </summary>
        /// <param name="viewModel">An instance of current request's view model</param>
        public void InvokeAfterUnInitialization(object viewModel)
        {
            if (this._interceptors == null) return;
            InvokeInternal(this._interceptors.OnAfterUnInitialization, viewModel);
        }

        #endregion [IInteractionInterceptorsInvoker members]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        ///     Уничтожение ресурсов
        /// </summary>
        protected override void OnDisposing()
        {
            this._interceptors = null;
            base.OnDisposing();
        }

        #endregion [Override members]

        #region [Help members]
            
        /// <summary>
        ///     Внутренний вызов
        /// </summary>
        /// <param name="invocationAction"></param>
        /// <param name="viewModel"></param>
        private void InvokeInternal(Action<TViewModel> invocationAction, object viewModel)
        {
            var typedViewModel = (TViewModel) viewModel;
            if (invocationAction == null) return;
            invocationAction(typedViewModel);
        }

        #endregion [Help members]
    }
}