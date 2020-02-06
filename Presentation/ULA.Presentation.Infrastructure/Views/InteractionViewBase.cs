using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.Infrastructure.Views
{
    /// <summary>
    ///     Represents base entity for the all interaction request views
    /// </summary>
    public class InteractionViewBase : UserControl, IInteractionView
    {
        #region [Dependency properties]

        /// <summary>
        ///     Gets a TitleProperty metadata
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (InteractionViewBase),
                new PropertyMetadata(string.Empty));

        /// <summary>
        ///  Gets a OkButtonTitleProperty metadata
        /// </summary>
        public static readonly DependencyProperty OkButtonTitleProperty =
            DependencyProperty.Register("OkButtonTitle", typeof(string), typeof(InteractionViewBase),
                new PropertyMetadata(string.Empty));

        #endregion [Dependency properties]

        #region [IInteractionView members]

        /// <summary>
        ///     Gets or sets the title of current interaction request view
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        /// <summary>
        ///     Gets or sets the okButtonTitle of current interaction request view
        /// </summary>
        public string OkButtonTitle
        {
            get { return (string)GetValue(OkButtonTitleProperty); }
            set { SetValue(OkButtonTitleProperty, value); }
        }

        #endregion [IInteractionView members]
    }
}