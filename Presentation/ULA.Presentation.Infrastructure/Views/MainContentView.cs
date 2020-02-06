using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.Infrastructure.Views
{
    /// <summary>
    ///     Represents the main content view control
    ///     Представляет общие данные вьюшки
    /// </summary>
    public class MainContentView : UserControl
    {
        #region [Dependency properties]

        /// <summary>
        ///     Gets a TitleProperty metadata
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (MainContentView),
                new FrameworkPropertyMetadata(string.Empty, OnTitleChanged));

        /// <summary>
        ///     Gets or sets the title of current main content view
        /// </summary>
        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Application.Current.MainWindow.Title = (string) e.NewValue;
        }

        #endregion [Dependency properties]
    }
}