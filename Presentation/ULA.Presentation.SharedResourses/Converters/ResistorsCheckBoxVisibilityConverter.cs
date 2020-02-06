using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="ULA.Presentation.SharedResourses.Converters.EnumStatesToBrushConverter"/>. Convert <see cref="CommandTypesEnum"/> to <see cref="Brushes"/>. ON to Red. 
    ///     OFF to Green. CONFIG to Grey.
    ///     Крнвертер из <see cref="CommandTypesEnum"/> в <see cref="Brushes"/>
    /// </summary>
    public class ResistorsCheckBoxVisibilityConverter : IValueConverter
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int? x = value as int?;
            return x < 0 ? Visibility.Collapsed : Visibility.Visible;
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
