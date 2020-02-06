using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     true и null to false, false to true
    /// </summary>
    public class ObjectAsBoolToIsEnabledRevertConverter : IValueConverter
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
                return false;
            }
            else if (valueAsBool.Value == true)
            {
                return false;
            }
            else
            {
                return true;
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
            if (value is bool)
            {
                if (value.Equals(true))
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
