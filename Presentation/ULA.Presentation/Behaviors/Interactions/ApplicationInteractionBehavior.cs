using System;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using ULA.Presentation.SharedResourses.Controls;

namespace ULA.Presentation.Behaviors.Interactions
{
    /// <summary>
    ///     Represents application specific interaction behavior
    ///     Представляет специфичные для приложения поведения
    /// </summary>
    public class ApplicationInteractionBehavior : Behavior<Grid>
    {
        #region [Fields]

        private InteractionContext _context;
        private SubscriptionToken _modalDialogInteractionEventToken;

        #endregion [Fields]

        #region [Override members]

        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        ///     Вызывается после того как поведение привяжется к AssociatedObject
        /// </summary>
        /// <remarks>
        ///     Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            IServiceLocator serviceLocator = ServiceLocator.Current;
            if (serviceLocator == null) return;

            var eventAggregator = serviceLocator.GetInstance<IEventAggregator>();
            if (eventAggregator == null) return;

            var modalInteractionEvent = eventAggregator.GetEvent<InteractionRequestEvent>();
            this._modalDialogInteractionEventToken = modalInteractionEvent.Subscribe(OnModalDialogInteraction,
                ThreadOption.UIThread, true);
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        ///     Вызывается когда поведение отсоеденеятся AssociatedObject, но до полного разрыва
        /// </summary>
        /// <remarks>
        ///     Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            if (this._modalDialogInteractionEventToken != null)
                this._modalDialogInteractionEventToken.Dispose();
            this._modalDialogInteractionEventToken = null;
            if (this._context != null)
                DestroyDialog();
            base.OnDetaching();
        }

        #endregion [Override members]

        #region [Help members]

        /// <summary>
        ///     Вызывается при выводе интерактивных окон
        /// </summary>
        /// <param name="args"></param>
        private void OnModalDialogInteraction(InteractionRequestEventArgs args)
        {
            if (args.IsCanceled)
            {
                if (this._context == null) return;
                DestroyDialog();
                return;
            }

            /*TODO: deal with multiple dialogs request.*/
            if (this._context != null) return;
            this._context = InteractionContext.FromArguemnts(args);

            EventHandler closeHandler = null;
            closeHandler = (sender, ar) =>
            {
                this._context.ViewModel.OnInteractionComplete -= closeHandler;
                this._context.Callback();
            };
            this._context.BeforeInitialization();

            this._context.ViewModel.OnInteractionComplete += closeHandler;
            this._context.CurrentViewContent = new InteractionViewContent {Content = this._context.View};
            this._context.CurrentViewContent.SetValue(Grid.RowSpanProperty,
                AssociatedObject.RowDefinitions.Count == 0 ? 1 : AssociatedObject.RowDefinitions.Count);
            this._context.CurrentViewContent.SetValue(Grid.ColumnSpanProperty,
                AssociatedObject.ColumnDefinitions.Count == 0 ? 1 : AssociatedObject.ColumnDefinitions.Count);
            AssociatedObject.Children.Add(this._context.CurrentViewContent);

            this._context.AfterInitialization();
        }

        /// <summary>
        ///     Метож уничтожения диалога
        /// </summary>
        private void DestroyDialog()
        {
            this._context.BeforeUninitialization();
            AssociatedObject.Children.Remove(this._context.CurrentViewContent);
            this._context.AfterUninitialization();
            if(this._context != null)
                this._context.Dispose();
            this._context = null;
        }

        #endregion [Help members]
    }
}