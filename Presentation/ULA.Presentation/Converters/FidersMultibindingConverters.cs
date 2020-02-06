using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;
using ULA.Presentation.ViewModels.Interactions.Entities;
using ULA.Presentation.ViewModels.UserControl;

namespace ULA.Presentation.Converters
{
    /// <summary>
    /// конвертер для вывода описания фидеров
 /// </summary>
    public class FidersMultibindingConverters : IMultiValueConverter
    {
        /// <summary>
        /// Задумка такая: привязыться к резисторам и описаниям фидеров и конвертация в модель отображения
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
    
            return null;
        }

        /// <summary>
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
