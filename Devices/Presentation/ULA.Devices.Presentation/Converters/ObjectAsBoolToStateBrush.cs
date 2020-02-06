using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectAsBoolToStateBrush : IValueConverter
    {
        /// <summary>
        ///     Convert <see cref="CommandTypesEnum"/> to <see cref="Brushes"/>. ON to Red. 
        ///     OFF to Green. CONFIG to Grey.
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
                return Brushes.Silver;
            }
            else if (valueAsBool.Value == false)
            {
                return Brushes.LimeGreen;
            }
            else if (valueAsBool.Value == true)
            {
                return Brushes.Red;
            }
            throw new Exception("ObjectAsBoolToStateColor. WTF");
        }

        /// <summary>
        ///     Convert <see cref="Brushes"/> to <see cref="CommandTypesEnum"/>. Red to ON. 
        ///     Green to OFF. Grey to CONFIG.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Brush)
            {
                var brushesValue = value as Brush;
                if (brushesValue.Equals(Brushes.Red))
                {
                    return true;
                }
                else if (brushesValue.Equals(Brushes.LimeGreen))
                {
                    return false;
                }
                else if (brushesValue.Equals(Brushes.Gray))
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
