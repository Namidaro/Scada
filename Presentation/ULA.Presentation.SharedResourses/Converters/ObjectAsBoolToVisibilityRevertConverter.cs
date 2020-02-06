using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     true и null to Collapsed, false to Visibility
    /// </summary>
    public class ObjectAsBoolToVisibilityRevertConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a value.Convert <see cref="object"/> to <see cref="Brush"/>.  
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? valueAsBool = value as bool?;
            if (!valueAsBool.HasValue)
            {
                return Visibility.Collapsed;
            }
            else if (valueAsBool.Value == true)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        /// <summary>
        ///     NotImplementedException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Visibility)
            {
                var valueAsVisibility = value as Visibility?;
                if (valueAsVisibility.Equals(Visibility.Visible))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
