using System;
using System.Windows.Data;
using ULA.AddinsHost.Business.Device;
using ULA.Presentation.Infrastructure;

namespace ULA.Presentation.Converters
{
    /// <summary>
    ///     Возвращает номер столбца в сетке листа устройств данного устройства по его значению "Number" из DataContainer-а
    /// </summary>
    public class GetColumnFromDeviceNumber : IValueConverter
    {
        /// <summary>
        ///     Определяет Номер столбца в сетке
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            var devicenumber = Int32.Parse(value.ToString());
            return (int)(devicenumber % ApplicationGlobalNames.WidgetListColumnCount);
        }

        /// <summary>
        ///   Это нам не понадобится. 
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
