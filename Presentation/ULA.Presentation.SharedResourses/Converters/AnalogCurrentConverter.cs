using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class AnalogCurrentConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="isMSAparameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object isMSAparameter, CultureInfo culture)
        {
            var valueAsdouble = value as double?;
            if (valueAsdouble == null) return "???";

            if (valueAsdouble.HasValue)
            {
                return (valueAsdouble.Value).ToString("N1");
            }

            return "???";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
