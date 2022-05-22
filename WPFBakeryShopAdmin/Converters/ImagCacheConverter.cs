using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace WPFBakeryShopAdmin.Converters
{
    public class ImagCacheConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string url = value as string;
            if (url == null) return null;
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.CacheOption = BitmapCacheOption.OnLoad;
            source.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            source.UriSource = new Uri(url, UriKind.RelativeOrAbsolute);
            
            //source.UriCachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore); 
            if (parameter != null)
            {
                int pixel = int.Parse(parameter.ToString());
                source.DecodePixelHeight = source.DecodePixelWidth = pixel;
            }
            source.EndInit();
            return source;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
