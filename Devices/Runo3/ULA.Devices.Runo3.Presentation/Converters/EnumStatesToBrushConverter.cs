﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using ULA.Interceptors;

namespace ULA.Devices.Runo3.Presentation.Converters
{
    /// <summary>
    ///     Represents a instance of <see cref="EnumStatesToBrushConverter"/>. Convert <see cref="CommandTypesEnum"/> to <see cref="Brushes"/>. ON to Red. 
    ///     OFF to Green. CONFIG to Grey.
    ///     Конвертирует тип <see cref="CommandTypesEnum"/> в <see cref="Brushes"/>. состояние ON(включено) -> красный. OFF -> зелёный. CONFIG ->
    ///     серый
    /// </summary>
    public class EnumStatesToBrushConverter : IValueConverter
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
                return Brushes.Red;
            }
            else if (value.ToString() == CommandTypesEnum.OFF.ToString())
            {
                return Brushes.LimeGreen;
            }
            else if (value.ToString() == CommandTypesEnum.CONFIG.ToString())
            {
                return Brushes.Silver;
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
                } else if (brushesValue.Equals(Brushes.LimeGreen))
                {
                    return CommandTypesEnum.OFF;
                }
                else if(brushesValue.Equals(Brushes.Gray))
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
                throw new ArgumentException("Source type must be SolidColorBrush");
            }
        }
    }
}
