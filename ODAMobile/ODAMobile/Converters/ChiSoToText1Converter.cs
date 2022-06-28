using System;
using System.Globalization;

namespace Xamarin.Forms.Converters
{
    public class ChiSoToText1Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return "Ghi chỉ số";
            else
                return "Sửa chỉ số";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
