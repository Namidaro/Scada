using System;
using System.Windows;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="NullVisibilityConverter"/>. Convert <see cref="object"/> to <see cref="Visibility"/>. Null to Collapsed. 
    ///     Not null to Visible
    /// </summary>
    public class NullVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a value.Convert <see cref="object"/> to <see cref="Visibility"/>. Null to Collapsed. 
        ///     Not null to Visible
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>D:\ULA3\trunk\ULA\Presentation\ULA.Presentation.SharedResourses\Converters\NullVisibilityConverter.cs
        /// <param name="culture"></param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        ///     Converts a value. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            Visibility? v = value as Visibility?;
            return ((v.HasValue) || (v.Value == Visibility.Collapsed)) ? null : "";
        }
    }
}
