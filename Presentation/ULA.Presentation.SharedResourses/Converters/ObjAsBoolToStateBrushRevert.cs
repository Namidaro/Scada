using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Конвертирует bool? в цветную кисть true - красный, fale - зеленый, null - серый
    /// </summary>
    public class ObjAsBoolToStateBrushRevert : IValueConverter
    {
        /// <summary>
        ///     Конвертирует bool? в цветную кисть true - красный, fale - зеленый, null - серый
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
            if (valueAsBool.HasValue == false)
            {
                return Brushes.Gray;
            }
            else if (valueAsBool.Value == true)
            {
                return Brushes.Green;
            }
            else //if (valueAsBool.Value == false)
            {
                return Brushes.Red;
            }
        }

        /// <summary>
        ///    
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
