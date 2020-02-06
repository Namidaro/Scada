using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;
using ULA.Presentation.Infrastructure.ViewModels.UserControl;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
/// конвертер цвета для настраиваемого сигнала
/// </summary>
   public class CustomSignalForegroundMultibindingConverter:IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].ToString() == CommandTypesEnum.ON.ToString())
            {
                return (values[1].ToString() == bool.TrueString) ? Brushes.Red : Brushes.LimeGreen;
            }
            else if (values[0].ToString() == CommandTypesEnum.OFF.ToString())
            {
                return (values[1].ToString() == bool.TrueString) ? Brushes.LimeGreen : Brushes.Red;
            }
            else if (values[0].ToString() == CommandTypesEnum.CONFIG.ToString())
            {
                return Brushes.Gray;
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
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
