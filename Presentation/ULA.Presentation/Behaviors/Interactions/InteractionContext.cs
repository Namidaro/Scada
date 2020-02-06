using System;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Infrastructure.Views;
using ULA.Presentation.SharedResourses.Controls;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Presentation.Behaviors.Interactions
{
    /// <summary>
    ///     Represents current interaction request context
    ///     Представляет контекст интерактивного запроса
    /// </summary>
    public class InteractionContext : Disposable
    {
        #region [Properties]

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionViewModel" /> that represents current injecting interaction view model
        ///     Сущность представляющая интерактивную вью-модель
        /// </summary>
        public IInteractionViewModel ViewModel { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionView" /> that represents current injecting interaction view
        ///     Сущность представляющая интерактивную вьюшку
        /// </summary>
        public IInteractionView View { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="IInteractionInterceptorsInvoker" /> that represents interaction interceptors
        ///     registry
        /// хз
        /// </summary>
        public IInteractionInterceptorsInvoker Invoker { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="InteractionViewContent" /> that represents current interaction view wrapping content
        ///     Сущность представляющая интерактивную контент для вьюшки
        /// </summary>
        public InteractionViewContent CurrentViewContent { get; set; }

        /// <summary>
        ///     Gets an instance of <see cref="Action" /> that represents cancel request callback
        ///     Функция обратного вызова
        /// </summary>
        public Action Callback { get; set; }

        #endregion [Properties]

        #region [Public members]

        /// <summary>
        ///     Delegates before un-initialization request action
        ///     Действие происходящее перед деинициализацией
        /// </summary>
        public void BeforeUninitialization()
        {
            this.Invoker.InvokeBeforeUnInitialization(this.ViewModel);
        }

        /// <summary>
        ///     Delegates after un-initialization request action
        ///     Действие происходящее после деинициализацией
        /// </summary>
        public void AfterUninitialization()
        {
            this.Invoker.InvokeAfterUnInitialization(this.ViewModel);
        }

        /// <summary>
        ///     Delegates before initialization request action
        ///     Действие происходящее перед инициализацией
        /// </summary>
        public void BeforeInitialization()
        {
            this.Invoker.InvokeBeforeInitialization(this.ViewModel);
        }

        /// <summary>
        ///     Delegates after initialization request action
        ///     Действие происходящее после инициализацией
        /// </summary>
        public void AfterInitialization()
        {
            this.Invoker.InvokeAfterInitialization(this.ViewModel);
        }

        #endregion [Public members]

        #region [Static members]

        /// <summary>
        ///     Creates an instance of <see cref="InteractionContext" /> from <see cref="InteractionRequestEventArgs" />
        ///     Создет контекст <see cref="InteractionContext" /> для запроса <see cref="InteractionRequestEventArgs" />
        /// </summary>
        /// <param name="args">An instance of <see cref="InteractionRequestEventArgs" /> to convert from</param>
        /// <returns>Created instance of <see cref="InteractionContext" /></returns>
        public static InteractionContext FromArguemnts(InteractionRequestEventArgs args)
        {
            IInteractionView view = args.InteractionProvider.InteractionView;
            IInteractionViewModel viewModel = args.InteractionProvider.InteractionViewModel;
            view.DataContext = viewModel;

            return new InteractionContext
            {
                Invoker = args.InterceptorsInvoker,
                Callback = args.Callback,
                ViewModel = viewModel,
                View = view
            };
        }

        #endregion [Static members]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        ///     Уничтожение ресурсов
        /// </summary>
        protected override void OnDisposing()
        {
            this.CurrentViewContent.Dispose();
            this.CurrentViewContent = null;
            this.Invoker.Dispose();
            this.Invoker = null;
            base.OnDisposing();
        }

        #endregion [Override members]
    }
}