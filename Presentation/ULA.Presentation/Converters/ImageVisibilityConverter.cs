using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ULA.AddinsHost.Presentation;

namespace ULA.Presentation.Converters
{
    /// <summary>
    /// 
    /// </summary>
   public class ImageVisibilityConverter: IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            return Visibility.Collapsed;
            if (value is IBackgroundPicture)
            {
                if ((value as IBackgroundPicture).BackgroundImageSource != null)
                {
                   return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
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
