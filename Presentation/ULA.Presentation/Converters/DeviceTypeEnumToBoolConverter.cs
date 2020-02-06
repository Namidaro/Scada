using System;
using System.Globalization;
using System.Windows.Data;

namespace ULA.Presentation.Converters
{
    /// <summary>
    /// </summary>
    public class DeviceTypeEnumToBoolConverter : IValueConverter
    {  /// <summary>
       ///     Определяет выделение радиокнопок по энаму
       /// </summary>
       /// <param name="value"></param>
       /// <param name="targetType"></param>
       /// <param name="parameter"></param>
       /// <param name="culture"></param>
       /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value).ToString() == parameter.ToString();
        }
        /// <summary>
        ///     Определяет выделение радио-кнопок по энаму
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter.ToString();
        }
    }
}