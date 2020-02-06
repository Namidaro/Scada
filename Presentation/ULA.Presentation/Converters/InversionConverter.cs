using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ULA.Presentation.Converters
{
    /// <summary>
    /// </summary>
    public class InversionConverter : IValueConverter
    {
        /// <summary>
        ///     Определяет выделение радиокнопок по энаму
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value as bool?).HasValue)
            {
                if (!(value as bool?).Value) return true;
            }
            return false;
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
            if ((value as bool?).HasValue)
            {
                if (!(value as bool?).Value) return true;
            }
            return false;
        }
    }
}