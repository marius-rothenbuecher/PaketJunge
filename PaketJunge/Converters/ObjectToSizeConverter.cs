using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Data;

namespace PaketJunge.View.Converters
{
    public class ObjectToSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Marshal.SizeOf(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
