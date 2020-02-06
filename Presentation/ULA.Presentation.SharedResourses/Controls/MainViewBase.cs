using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    ///     Represents the main view basic functionality
    ///     Представляет базовый функционал основной вьюхи
    /// </summary>
    public class MainViewBase : ModernWindow
    {
        #region [Dependency Properties]

        /// <summary>
        ///     Gets the ApplicationTitleProperty metadata
        ///     Заголовок приложения
        /// </summary>
        public static readonly DependencyProperty ApplicationTitleProperty =
            DependencyProperty.Register("ApplicationTitle", typeof (string), typeof (MainViewBase),
                new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Gets or sets the application title
        ///     Заголовок приложения
        /// </summary>
        public string ApplicationTitle
        {
            get { return (string) GetValue(ApplicationTitleProperty); }
            set { SetValue(ApplicationTitleProperty, value); }
        }

        #endregion [Dependency Properties]
    }
}