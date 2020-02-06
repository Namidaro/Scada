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
    public class AnalogCurrentConverterForMsa961 : IMultiValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="isMSAparameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] value, Type targetType, object isMSAparameter, CultureInfo culture)
        {
            var valueAsdouble = value[0] as double?;
            if (valueAsdouble == null) return "???";

            int? koefAsdouble = value[1] as int?;
            if (koefAsdouble == null) return "???";

            if (valueAsdouble == ushort.MaxValue) return "???";

            if ((valueAsdouble.HasValue) && (koefAsdouble.HasValue))
            {
                //   var к = valueAsdouble.Value/5;

                return ((double) (valueAsdouble.Value / 40 * (koefAsdouble / 5))).ToString("N1");
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
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}