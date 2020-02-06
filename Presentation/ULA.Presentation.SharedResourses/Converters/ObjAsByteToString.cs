using System;
using System.Windows;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="ObjAsIntToString"/>. Convert <see cref="object"/> to <see cref="string"/>. 
    /// Type <see cref="object"/> is <see cref="byte"/>. Null to ???
    /// </summary>
    public class ObjAsIntToString : IValueConverter
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
            if (value == null)
            {
                return "???";
            }
            else if(value is double)
            {
                var doubleValue = (double)value;
                return doubleValue.ToString("N1");
            }
            else if (value is int)
            {
               return ((int) value).ToString("D2");
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
