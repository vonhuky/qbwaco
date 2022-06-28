using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xamarin.Forms.Converters
{
    public class ChiSoToColor1Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return "#3F51B5";
            else
                return "Orange";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
