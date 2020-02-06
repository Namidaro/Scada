using System;
using Prism.Events;
using ULA.Presentation.Behaviors.Interactions;
using ULA.Presentation.Infrastructure.Services;
using ULA.Presentation.Infrastructure.Services.Interactions;
using ULA.Presentation.Infrastructure.ViewModels.Interactions;
using ULA.Presentation.Services.Interactions;
using YP.Toolkit.System.Validation;

namespace ULA.Presentation.Services
{
    /// <summary>
    ///     Represents a default system interaction service functionality
    ///     Представляет функционал сервиса интерактивного вызова
    /// </summary>
    public class InteractionService : IInteractionService
    {
        #region [Private fields]

        private readonly IEventAggregator _eventAggregator;

        #endregion [Private fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="InteractionService" />
        /// </summary>
        /// <param name="eventAggregator">
        ///     An instance of <see cref="IEventAggregator" /> to use
        /// </param>
        public InteractionService(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
        }

        #endregion [Ctor's]

        #region [IInteractionService members]

        /// <summary>
        ///     Sends an interaction request to a user
        ///     Отправляет интерактивный запрос пользователю(Вызов модального окна)
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     <see cref="Type" /> that represents the view model of the interaction request
        /// </typeparam>
        /// <param name="provider">
        ///     An instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current
        ///     interaction request provider
        /// </param>
        /// <param name="interactionInterceptor">
        ///     An instance of <see cref="InteractionInterceptor{TViewModel}" /> to use
        /// </param>
        /// <returns>
        ///     An instance of <see cref="IInteractionToken" /> that represents interaction request token
        /// </returns>
        public IInteractionToken Interact<TViewModel>(IInteractionProvider<TViewModel> provider,
            InteractionInterceptor<TViewModel> interactionInterceptor)
            where TViewModel : IInteractionViewModel
        {
            Guard.AgainstNullReference(provider, "provider");

            var interactionEvent = this._eventAggregator.GetEvent<InteractionRequestEvent>();
            var result = new InteractionToken(interactionEvent);

            interactionEvent.Publish(new InteractionRequestEventArgs
            {
                IsCanceled = false,
                InteractionProvider = provider,
                InterceptorsInvoker = new InteractionInterceptorsInvoker<TViewModel>(interactionInterceptor),
                Callback = result.Dispose
            });

            return result;
        }


        /// <summary>
        ///     Sends an interaction request to a user
        ///  Отправляет интерактивный запрос пользователю(Вызов модального окна)
        /// </summary>
        /// <typeparam name="TViewModel">
        ///     <see cref="Type" /> that represents the view model of the interaction request
        ///     Тип вью-модели на которую осуществляется переход
        /// </typeparam>
        /// <param name="provider">
        ///     An instance of <see cref="IInteractionProvider{TViewModel}" /> that represents current
        ///     interaction request provider
        ///     Провайдер интерактивного запроса
        /// </param>
        /// <param name="onInitialized">
        ///     An instance of <see cref="Action{T}" /> that represents an initialization callback
        ///     Функция инициализации
        /// </param>
        /// <param name="onUninitialized">
        ///     An instance of <see cref="Action{T}" /> that represents an uninitialization callback
        /// Функция деинициализации
        /// </param>
        /// <returns>
        ///     An instance of <see cref="IInteractionToken" /> that represents interaction request token
        /// </returns>
        public IInteractionToken Interact<TViewModel>(IInteractionProvider<TViewModel> provider,
            Action<TViewModel> onInitialized = null,
            Action<TViewModel> onUninitialized = null)
            where TViewModel : IInteractionViewModel
        {
            return Interact(provider, new InteractionInterceptor<TViewModel>
            {
                OnAfterInitialization = onInitialized,
                OnAfterUnInitialization = onUninitialized
            });
        }

        #endregion [IInteractionService members]
    }
}