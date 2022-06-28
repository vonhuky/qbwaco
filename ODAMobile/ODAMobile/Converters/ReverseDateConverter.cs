using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xamarin.Forms.Converters
{
    public class ReverseDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "1/1/0001 12:00:00 AM")
            {
                return DateTime.Now;
            }
            else
            {
                return (DateTime)value;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
