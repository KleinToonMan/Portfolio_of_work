using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WIL_DesktopApp.Converters
{
    // Converter class to convert boolean values to Visibility enum
    internal class BooleanToVisibilityConverter : IValueConverter
    {
        // Convert method: Boolean to Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue && boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        // ConvertBack method: Not implemented
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
