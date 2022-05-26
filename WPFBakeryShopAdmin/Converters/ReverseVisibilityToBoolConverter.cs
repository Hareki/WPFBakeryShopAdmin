using System;
using System.Windows;
using System.Windows.Data;

namespace WPFBakeryShopAdmin.Converters
{
    public class ReverseVisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            if (visibility == Visibility.Hidden) return true;
            else return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool loadingVisible = System.Convert.ToBoolean(value);
            if (loadingVisible) return Visibility.Hidden;
            else return Visibility.Visible;
        }
    }
}
