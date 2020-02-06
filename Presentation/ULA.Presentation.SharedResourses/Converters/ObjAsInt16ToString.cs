using System;
using System.Windows;
using System.Windows.Data;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="ObjAsIntToString"/>. Convert <see cref="object"/> to <see cref="string"/>. 
    /// Type <see cref="object"/> is <see cref="byte"/>. Null to ???
    /// </summary>
    public class ObjAsInt16ToString : IValueConverter
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
            var valueAsInt16 = value as int?;
            if (value == null)
            {
                return "???";
            }
            else if (valueAsInt16.HasValue)
            {
                return valueAsInt16.Value.ToString("D2");
            }
            else
            {
                throw new ArgumentException("Value is: " + value.ToString() + " with type: " + value.GetType().ToString() + " but must be byte");
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
