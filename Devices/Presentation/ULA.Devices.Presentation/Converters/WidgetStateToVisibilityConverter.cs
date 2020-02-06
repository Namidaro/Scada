using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using ULA.AddinsHost.Business.Device.Enums;

namespace ULA.Devices.Presentation.Converters
{
   public class WidgetStateToVisibilityConverter:IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WidgetState)
            {
                WidgetState ws =(WidgetState)value;
                if (ws == WidgetState.NoConnection) return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
