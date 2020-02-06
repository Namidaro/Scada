using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;

namespace ULA.Devices.Presentation.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="StarterStatetoBrushConverter"/>. Convert <see cref="StarterStates"/> to <see cref="Brushes"/>. ON to Red. 
    ///     OFF to Green. CONFIG to Grey.
    ///     см. <see cref="EnumStatesToBrushConverter"/>. здесь аналогично, только для <see cref="StarterStates"/>. 
    ///     (Cостояний устройства в режиме реального времени)
    /// </summary>
    public class StarterStatetoBrushConverter : IValueConverter
    {
        /// <summary>
        ///     Convert <see cref="StarterStates"/> to <see cref="Brushes"/>. ON to Red. 
        ///     OFF to Green. CONFIG to Grey.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
                if (value.Equals(true))
                {
                    return Brushes.Red; 
                }
                else 
                {
                    return Brushes.LimeGreen;
                }
                //else if (value.ToString() == StarterStates.NO_CONNECT.ToString())
                //{
                //    return Brushes.Silver;
                //}
                //else
              // {
                   throw new ArgumentException("Source type must be CommandTypesEnum");
                //}
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
        public object ConvertBack(object value, System.Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
            /*if (value is Brushes)
            {
                var brushesValue = value as Brushes;
                if (brushesValue.Equals(Brushes.Red))
                {
                    return StarterStates.ON;
                } else if (brushesValue.Equals(Brushes.LimeGreen))
                {
                    return StarterStates.OFF;
                }
                else if(brushesValue.Equals(Brushes.Gray))
                {
                    return StarterStates.CONFIG;
                }
                else
                {
                    throw new ArgumentException("Convert " + brushesValue.ToString() + " to CommandTypesEnum value is imposible");
                }

            }
            else
            {
                throw new ArgumentException("Source type must be Brushes");
            }*/
        }
    }
}
