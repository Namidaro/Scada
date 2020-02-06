using System;
using ULA.Presentation.Infrastructure.Services.Interactions;

namespace ULA.Presentation.Behaviors.Interactions
{
    /// <summary>
    ///     Represents an interaction request event argument structural object
    ///     Представляет объект аргумента события для интерактивного запроса
    /// </summary>
    public class InteractionRequestEventArgs
    {
        /// <summary>
        ///     Gets or sets an instance of <see cref="IInteractionProvider" /> that represents current interaction request
        ///     provider
        ///     Свойство представляющее интерактивный провайдер запросов
        /// </summary>
        public IInteractionProvider InteractionProvider { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="IInteractionInterceptorsInvoker" /> that represents current interaction
        ///     request interseptors invokators registry
        ///     Свойство представляющее регистратор интерактивных запрососв(не уверен =)
        /// </summary>
        public IInteractionInterceptorsInvoker InterceptorsInvoker { get; set; }

        /// <summary>
        ///     Gets or sets the value that indicates whether current interaction request is canceled or not
        ///     Свойство указывающее был ли запрос отменен
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        ///     Gets or sets an instance of <see cref="Action" /> that represents cancel interaction request callback
        ///     Функция обратного вызова
        /// </summary>
        public Action Callback { get; set; }
    }
}