using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="ObjectAsBoolToStateColor"/>. Convert <see cref="bool"/> to <see cref="Brushes"/>. true to Red. 
    ///     OFF to Green.
    ///     Конвертирует <see cref="bool"/> в <see cref="Brushes"/>. true в красный 
    ///     OFF в зеленый
    /// </summary>
    public class ObjectAsBoolToStateColor : IValueConverter
    {
        /// <summary>
        ///     Convert <see cref="Brushes"/> to <see cref="Brushes"/>. ON to Red. 
        ///     OFF to Green. CONFIG to Grey.
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var valueAsBool = value as bool?;
            if (!valueAsBool.HasValue)
            {
                return Colors.Silver;
            }
            else if (valueAsBool.Value == false)
            {
                return Colors.LimeGreen;
            }
            else if (valueAsBool.Value == true)
            {
                return Colors.Red;
            }
            throw new Exception("ObjectAsBoolToStateColor. WTF");
        }

        /// <summary>
        ///     Convert <see cref="Brushes"/> to <see cref="EnumStates"/>. Red to ON. 
        ///     Green to OFF. Grey to CONFIG.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color)
            {
                var brushesValue = value as Color?;
                if (brushesValue.Equals(Colors.Red))
                {
                    return true;
                }
                else if (brushesValue.Equals(Colors.LimeGreen))
                {
                    return false;
                }
                else if (brushesValue.Equals(Colors.Gray))
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException("Convert " + brushesValue.ToString() + " to CommandTypesEnum value is imposible");
                }

            }
            else
            {
                throw new ArgumentException("Source type must be SolidColorBrush");
            }
        }
    }
}
