using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ULA.Presentation.SharedResourses.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class InfoIcon:TextBox

    {
        #region [Ctor's]

        /// <summary>
        ///     Initializes <see cref="InfoIcon" /> type
        /// </summary>
        static InfoIcon()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InfoIcon),
                new FrameworkPropertyMetadata(typeof(InfoIcon)));
        }

        #endregion [Ctor's]

        #region [Dependency Properties]

        /// <summary>
        ///     Gets InformationContentProperty metadata
        ///     Возвращает свойство с информации о данных textBox-а
        /// </summary>
        public static readonly DependencyProperty InformationContentProperty =
            DependencyProperty.Register("InformationContent", typeof(object), typeof(InfoIcon),
                new PropertyMetadata(null));

        /// <summary>
        ///     Gets InformationHeaderProperty metadata
        ///     Возвращает свойство с информации о заголовке textBox-а
        /// </summary>
        public static readonly DependencyProperty InformationHeaderProperty =
            DependencyProperty.Register("InformationHeader", typeof(string), typeof(InfoIcon),
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
            get { return (string)GetValue(InformationHeaderProperty); }
            set { SetValue(InformationHeaderProperty, value); }
        }

        #endregion [Dependency Properties]
    }
}
