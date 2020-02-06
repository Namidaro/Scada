using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Represents a convertering strategy that gets a string with semicolon and transfers it into bulletin list
    /// </summary>
    public class SemicolonStringToBulletinListConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a value.
        /// </summary>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = System.Convert.ToString(value, culture);
            var strBuilder = new StringBuilder();
            foreach (var item in  str.Split(';'))
            {
                strBuilder.AppendLine(string.Format("\u2022 {0};", item));
            }
            return strBuilder.ToString();
        }

        /// <summary>
        ///     Converts a value.
        ///     <exception cref="NotSupportedException"></exception>
        /// </summary>
        /// <returns>
        ///     A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}