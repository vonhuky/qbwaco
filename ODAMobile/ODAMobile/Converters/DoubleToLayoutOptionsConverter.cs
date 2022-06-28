﻿using System;
using System.Globalization;

namespace Xamarin.Forms.Converters
{
    public class DoubleToLayoutOptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = double.TryParse(value.ToString(), out double val);
            if (result && val > 0)
            {
                return LayoutOptions.CenterAndExpand;
            }
            return LayoutOptions.FillAndExpand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}