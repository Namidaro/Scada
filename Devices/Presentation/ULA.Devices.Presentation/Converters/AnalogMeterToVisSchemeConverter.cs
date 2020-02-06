using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Enums;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    /// конвертер для видимости кнопки показа счетчиков на схеме
    /// </summary>
   public class AnalogMeterToVisSchemeConverter : IValueConverter
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
            if (value == null) return Visibility.Collapsed;
            if ((value).ToString() ==DeviceStringKeys.DeviceAnalogMetersTagKeys.ENERGOMERA_METER_TYPE)
            {
                return Visibility.Visible;
            }
            if ((value).ToString() == DeviceStringKeys.DeviceAnalogMetersTagKeys.MSA_METER_TYPE)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
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