using System;
using System.Globalization;

namespace Xamarin.Forms.Converters
{
    public class ChiSoToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return "Chưa ghi chỉ số";
            else
                return "Đã ghi chỉ số";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
