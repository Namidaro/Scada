using System;
using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    ///     Represents modal view content control.
    ///     This control wrapes custom dialog template
    /// 
    ///     Представляеи модальную вьюху.
    ///     Этот контрол буде позже в шаблоне диалогового окна
    /// </summary>
    public class InteractionViewContent : ContentControl, IDisposable
    {
        #region [Ctor's]

        /// <summary>
        ///     Initializes the <see cref="InteractionViewContent" />
        /// </summary>
        static InteractionViewContent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (InteractionViewContent),
                new FrameworkPropertyMetadata(typeof (InteractionViewContent)));
        }

        #endregion [Ctor's]

        #region [Dependency properties]

        /// <summary>
        ///     Gets the TitleProperty metadata
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (InteractionViewContent),
                new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Gets or sets the title of current custom template view
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion [Dependency properties]

        #region [IDisposable members]

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (Content == null) return;
            var viewToDestroy = Content as FrameworkElement;
            if (viewToDestroy == null) return;
            var disposableViewModel = viewToDestroy.DataContext as IDisposable;
            if (disposableViewModel != null) disposableViewModel.Dispose();
            viewToDestroy.DataContext = null;
        }

        #endregion [IDisposable members]
    }
}