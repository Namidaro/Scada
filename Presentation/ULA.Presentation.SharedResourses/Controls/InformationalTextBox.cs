using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    ///     Represents a text box that contains information about the value to enter
    ///     Представляет text box который содержит информацию об вводимым значением
    /// </summary>
    public class InformationalTextBox : TextBox
    {
        #region [Ctor's]

        /// <summary>
        ///     Initializes <see cref="InformationalTextBox" /> type
        /// </summary>
        static InformationalTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (InformationalTextBox),
                new FrameworkPropertyMetadata(typeof (InformationalTextBox)));
        }

        #endregion [Ctor's]

        #region [Dependency Properties]

        /// <summary>
        ///     Gets InformationContentProperty metadata
        ///     Возвращает свойство с информации о данных textBox-а
        /// </summary>
        public static readonly DependencyProperty InformationContentProperty =
            DependencyProperty.Register("InformationContent", typeof (object), typeof (InformationalTextBox),
                new PropertyMetadata(null));

        /// <summary>
        ///     Gets InformationHeaderProperty metadata
        ///     Возвращает свойство с информации о заголовке textBox-а
        /// </summary>
        public static readonly DependencyProperty InformationHeaderProperty =
            DependencyProperty.Register("InformationHeader", typeof (string), typeof (InformationalTextBox),
                new PropertyMetadata(string.Empty));

        /// <summary>
        ///     Gets or sets the information content for the text box
        /// </summary>
        public object InformationContent
        {
            get { return GetValue(InformationContentProperty); }
            set { SetValue(InformationContentProperty, value); }
        }


        /// <summary>
        ///     Gets or sets the information header for the text box
        /// </summary>
        public string InformationHeader
        {
            get { return (string) GetValue(InformationHeaderProperty); }
            set { SetValue(InformationHeaderProperty, value); }
        }

        #endregion [Dependency Properties]
    }
}