using ULA.Presentation.Behaviors.Interactions;
using ULA.Presentation.Infrastructure.Services.Interactions;
using YP.Toolkit.System.Tools.Patterns;

namespace ULA.Presentation.Services.Interactions
{
    /// <summary>
    ///     Represents an interacion request token
    ///     Представляет экземпляр интерактивного запроса
    /// </summary>
    public class InteractionToken : Disposable, IInteractionToken
    {
        #region [Fields]

        private InteractionRequestEvent _interactionRequestEvent;

        #endregion [Fields]

        #region [Ctor's]

        /// <summary>
        ///     Creates an instance of <see cref="InteractionToken" />
        /// </summary>
        /// <param name="interactionRequestEvent">
        ///     An instance of <see cref="InteractionRequestEvent" /> that represents modal dialog
        ///     interaction event
        /// </param>
        public InteractionToken(InteractionRequestEvent interactionRequestEvent)
        {
            this._interactionRequestEvent = interactionRequestEvent;
        }

        #endregion [Ctor's]

        #region [Override members]

        /// <summary>
        ///     Does actual explicit disposal of available managed resources
        /// </summary>
        protected override void OnDisposing()
        {
            if (IsDisposed) return;
            this._interactionRequestEvent.Publish(new InteractionRequestEventArgs {IsCanceled = true});
            this._interactionRequestEvent = null;
            base.OnDisposing();
        }

        #endregion [Override members]
    }
}