using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;
using ULA.Presentation.Infrastructure.ViewModels;

namespace ULA.Devices.Presentation.Converters
{
    public class EnumStatesToCommandTypeBrushConverter : IValueConverter
    {
        /// <summary>
        ///     Represents a instance of <see cref="EnumStatesToCommandTypeBrushConverter"/>. Convert <see cref="CommandTypesEnum"/> to <see cref="Brushes"/>. 
        ///     Конвертирует тип <see cref="CommandTypesEnum"/> в <see cref="Brushes"/>. состояние ON(включено) -> красный. OFF -> зелёный. CONFIG ->
        ///     серый
        /// </summary>

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
                return Brushes.Red;
            }
            else if (value.ToString() == CommandTypesEnum.OFF.ToString())
            {
                return Brushes.Green;
            }
            else if (value.ToString() == CommandTypesEnum.AUTO.ToString())
            {
                return Brushes.Indigo;
            }
            else if (value.ToString() == CommandTypesEnum.MANUAL.ToString())
            {
                return Brushes.Indigo;
            }
            else if (value.ToString() == CommandTypesEnum.Repair.ToString())
            {
                return Brushes.Orange;
            }
            else if (value.ToString() == CommandTypesEnum.NoRepair.ToString())
            {
                return Brushes.Orange;
            }
            else
            {
                throw new ArgumentException("Source type must be CommandTypesEnum");
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
            if (value is SolidColorBrush)
            {
                var brushesValue = value as SolidColorBrush;
                if (brushesValue.Equals(Brushes.Red))
                {
                    return CommandTypesEnum.ON;
                }
                else if (brushesValue.Equals(Brushes.Green))
                {
                    return CommandTypesEnum.OFF;
                }
                else if (brushesValue.Equals(Brushes.Gray))
                {
                    return CommandTypesEnum.CONFIG;
                }
                else if (brushesValue.Equals(Brushes.Indigo))
                {
                    return CommandTypesEnum.MANUAL;
                }
                else if (brushesValue.Equals(Brushes.Orange))
                {
                    return CommandTypesEnum.NoRepair;
                }
                else if (brushesValue.Equals(Brushes.Orange))
                {
                    return CommandTypesEnum.Repair;
                }
                else if (brushesValue.Equals(Brushes.Indigo))
                {
                    return CommandTypesEnum.AUTO;
                }
                else
                {
                    throw new ArgumentException("Convert " + brushesValue.ToString() + " to CommandTypesEnum value is imposible");
                }

            }
            else
            {
                throw new ArgumentException("Source type must be SolidColorBrush");
            }
        }
    }


}

