using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     
    /// </summary>
    public class ObjAsIntToLevelSignalString : IValueConverter
    {
        /// <summary>
        ///     Converts a value.Convert <see cref="object"/> to <see cref="Visibility"/>. Null to Collapsed. 
        ///     Not null to Visible
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int intValue = -1;
            if (value == null)
            {
                return "???";
            }
            else if (Int32.TryParse(value.ToString(), out intValue))
            {
                if (intValue > 0.5 && intValue < 31.5)
                {
                    return intValue.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    return "--";
                }
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        ///     NotImplementedException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            //??????????????????/
            return value;
        }
    }
}
