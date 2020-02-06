using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class EnumStatesToColorConverter : IValueConverter
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
            if (value.ToString() == CommandTypesEnum.ON.ToString())
            {
                return Colors.Red;
            }
            else if (value.ToString() == CommandTypesEnum.OFF.ToString())
            {
                return Colors.LimeGreen;
            }
            else if (value.ToString() == CommandTypesEnum.CONFIG.ToString())
            {
                return Colors.Gray;
            }
            else
            {
                throw new ArgumentException("Source type must be CommandTypesEnum");
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
