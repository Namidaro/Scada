using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    /// 
    /// </summary>
   public class ObjAsBoolToStateLimeBrush : IValueConverter
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
                return Brushes.Red;
            }
            else //if (valueAsBool.Value == false)
            {
                return Brushes.LimeGreen;
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
