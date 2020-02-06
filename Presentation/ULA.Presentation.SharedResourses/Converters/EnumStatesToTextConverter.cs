using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ULA.Interceptors;

namespace ULA.Presentation.SharedResourses.Converters
{
    /// <summary>
    /// преобразоввание в текст вида вкл/выкл
    /// </summary>
   public class BoolToTextConverter : IValueConverter
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
            var valueAsBool = value as bool?;
            if (valueAsBool.HasValue == false)
            {
                return " ??? ";
            }
            else if (valueAsBool.Value == true)
            {
                return "Вкл. ";
            }
            else //if (valueAsBool.Value == false)
            {
                return "Выкл.";
            }
        }

        /// <summary>
        ///     Convert <see cref="Brushes"/> to <see cref="CommandTypesEnum"/>. Red to ON. 
        ///     Green to OFF. Grey to CONFIG.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Brushes)
            {
                var brushesValue = value as Brushes;
                if (brushesValue.Equals(Brushes.Red))
                {
                    return CommandTypesEnum.ON;
                }
                else if (brushesValue.Equals(Brushes.LimeGreen))
                {
                    return CommandTypesEnum.OFF;
                }
                else if (brushesValue.Equals(Brushes.Gray))
                {
                    return CommandTypesEnum.CONFIG;
                }
                else
                {
                    throw new ArgumentException("Convert " + brushesValue.ToString() + " to CommandTypesEnum value is imposible");
                }

            }
            else
            {
                throw new ArgumentException("Source type must be Brushes");
            }
        }
    }
}
