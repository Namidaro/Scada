using System;
using System.Globalization;
using System.Windows.Data;
using ULA.Business.Infrastructure.DeviceStringKeys;
using ULA.Business.Infrastructure.Enums;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    ///  конвертер для текста кнопки показа счетчиков на схеме
    /// </summary>
    public class AnalogMeterToTextSchemeConverter : IValueConverter
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
            if (value == null) return "";
            if ((value).ToString() == DeviceStringKeys.DeviceAnalogMetersTagKeys.NO)
            {
                return "";
            }
            //if ((value).ToString().Contains(AnalogMetersEnum.МСА961.ToString()))
            //{
            //    return "Показания МСА";
            //}
            return "Показания счетчика";

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