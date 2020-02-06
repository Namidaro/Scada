using System;
using System.Windows;
using System.Windows.Data;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="ObjAsBoolToAutoModeVisibilityConverter"/>. Convert <see cref="bool"/> to <see cref="Visibility"/>. 
    ///     true to Collapsed.   false to Visibility.
    ///     конвертирует <see cref="bool"/> в <see cref="Visibility"/> инвертным способом. true to Collapsed.   false to Visibility.
    /// </summary>
    public class ObjAsBoolToAutoModeVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///     Convert <see cref="bool"/> to <see cref="Visibility"/>. 
        ///     true to Collapsed.   false to Visibility.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var valueAsBool = value as bool?;
            if (!valueAsBool.HasValue)
            {
                return Visibility.Collapsed;
            }
            else if (valueAsBool.Value == false)
            {
                return Visibility.Visible;
            }
            else if (valueAsBool.Value == true)
            {
                return Visibility.Collapsed;
            }
            throw new Exception("ObjAsBoolToAutoModeVisibilityConverter. WTF");
        }

        /// <summary>
        ///     Convert <see cref="Visibility"/> to <see cref="bool"/>. 
        ///     Collapsed to true.   Visibility to false.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var visibilityValue = value as Visibility?;
            if (!visibilityValue.HasValue)
            {
                throw new ArgumentException("Source type must be Visibility");
            }
            if (visibilityValue.Value.Equals(Visibility.Visible))
            {
                return false;
            }
            else if (visibilityValue.Value.Equals(Visibility.Collapsed) ||
                     visibilityValue.Value.Equals(Visibility.Hidden))
            {
                return true;
            }
            else
            {
                throw new ArgumentException("Convert " + visibilityValue.ToString() +
                                            " to Visibility value is imposible");
            }

        }
    }
}

